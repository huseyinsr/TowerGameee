using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public class WaveEnemy
{
    public GameObject enemyPrefab;
    public int count = 1;
    public float specialSpawnTime = -1f;
}

public class WaveSpawner : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float levelDuration = 90f;
    [SerializeField] private float startDelay = 5f;

    [Header("Enemies")]
    [SerializeField] private WaveEnemy[] normalEnemies;
    [SerializeField] private WaveEnemy[] specialEnemies;

    private int totalEnemiesToSpawn = 0;
    private int enemiesAlive = 0;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI enemyCounterText;  // 👈 new UI reference

    void Start()
    {
        // Count total enemies
        foreach (WaveEnemy wave in normalEnemies)
            totalEnemiesToSpawn += wave.count;
        foreach (WaveEnemy wave in specialEnemies)
            totalEnemiesToSpawn += wave.count;

        UpdateEnemyCounterUI(); // Show at start
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(startDelay);

        foreach (WaveEnemy wave in normalEnemies)
        {
            if (wave.enemyPrefab == null || wave.count <= 0) continue;

            float interval = levelDuration / wave.count;

            for (int i = 0; i < wave.count; i++)
            {
                SpawnEnemy(wave.enemyPrefab);
                yield return new WaitForSeconds(interval);
            }
        }

        foreach (WaveEnemy wave in specialEnemies)
        {
            if (wave.enemyPrefab == null || wave.count <= 0 || wave.specialSpawnTime < 0f) continue;

            yield return new WaitForSeconds(wave.specialSpawnTime);

            for (int i = 0; i < wave.count; i++)
            {
                SpawnEnemy(wave.enemyPrefab);
            }
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        if (spawnPoints.Length == 0 || enemyPrefab == null) return;

        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy = Instantiate(enemyPrefab, sp.position, sp.rotation);

        enemiesAlive++;
        totalEnemiesToSpawn--;

        UpdateEnemyCounterUI();

        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
            e.OnEnemyDeath += HandleEnemyDeath;
        }
    }

    private void HandleEnemyDeath()
    {
        enemiesAlive--;
        UpdateEnemyCounterUI();

        if (totalEnemiesToSpawn <= 0 && enemiesAlive <= 0)
        {
            SceneManager.LoadScene(7);
        }
    }

    private void UpdateEnemyCounterUI()
    {
        if (enemyCounterText != null)
        {
            int totalRemaining = enemiesAlive + totalEnemiesToSpawn;
            enemyCounterText.text = "Enemy Counter: " + totalRemaining.ToString();
        }
    }
}
