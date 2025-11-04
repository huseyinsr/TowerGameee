using System.Collections.Generic;
using UnityEngine;

public class ToothpasteShooter : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 1f;

    [Header("Sound Settings")]
    [SerializeField] private AudioClip fireSound; 
    private AudioSource audioSource;

    private float timer = 0f;
    private List<Enemy> enemiesInRange = new List<Enemy>();

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (enemiesInRange.Count > 0)
        {
            timer += Time.deltaTime;
            if (timer >= 1f / fireRate)
            {
                Shoot();
                timer = 0f;
            }
        }
        else
        {
            timer = 0f;
        }
    }

    private void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            if (fireSound != null)
            {
                audioSource.PlayOneShot(fireSound);
            }
        }
    }

    public void OnDetectionEnter(Enemy enemy)
    {
        if (!enemiesInRange.Contains(enemy))
            enemiesInRange.Add(enemy);
    }

    public void OnDetectionExit(Enemy enemy)
    {
        if (enemiesInRange.Contains(enemy))
            enemiesInRange.Remove(enemy);
    }
}
