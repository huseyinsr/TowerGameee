using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 2f;

    [Header("Attack")]
    [SerializeField] private int contactDamage = 10;
    [SerializeField] private float damageInterval = 1f;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip hitSound;  
    private AudioSource audioSource;

    private Health myHealth;
    private Health currentTarget;
    private float damageTimer = 0f;
    private bool isAttacking = false;

    public System.Action OnEnemyDeath;

    private void Awake()
    {
        myHealth = GetComponent<Health>();
        myHealth.onDeath.AddListener(HandleDeath);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (!isAttacking)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else
        {
            damageTimer += Time.deltaTime;
            if (currentTarget != null && damageTimer >= damageInterval)
            {
                currentTarget.TakeDamage(contactDamage);
                damageTimer = 0f;
            }
            else if (currentTarget == null)
            {
                isAttacking = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Health h = other.GetComponent<Health>();
        if (h != null && (other.CompareTag("Shooter") || other.CompareTag("Teeth")))
        {
            currentTarget = h;
            isAttacking = true;
            damageTimer = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Health h = other.GetComponent<Health>();
        if (h == currentTarget)
        {
            currentTarget = null;
            isAttacking = false;
        }
    }

    private void HandleDeath()
    {
        OnEnemyDeath?.Invoke();
    }

    public void TakeDamage(int amount)
    {
        myHealth.TakeDamage(amount);

        if (hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }
}
