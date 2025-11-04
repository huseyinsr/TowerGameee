using UnityEngine;

public class SunSpawner : MonoBehaviour
{
    [SerializeField] private GameObject sunObject;

    private void Start()
    {
        SpawnSun();
    }

    void SpawnSun()
    {
        Instantiate(sunObject);
        Invoke("SpawnSun", Random.Range(5f, 10f));
    }
}
