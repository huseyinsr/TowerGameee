using UnityEngine;

public class ToothpasteProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private  int damage = 25;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private bool destroyOnHit = true;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shooter")) return;

        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage); 
            if (destroyOnHit) Destroy(gameObject);
        }
    }
}
