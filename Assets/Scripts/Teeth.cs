using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Health))]
public class Teeth : MonoBehaviour
{
    private Health health;

    void Awake()
    {
        health = GetComponent<Health>();
        if (health != null)
            health.onDeath.AddListener(OnTeethDestroyed);
    }

    private void OnTeethDestroyed()
    {
        Debug.Log("Teeth destroyed -> Game Over");
        SceneManager.LoadScene(8);
    }
}
