using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    private ToothpasteShooter shooter;

    void Awake()
    {
        shooter = GetComponentInParent<ToothpasteShooter>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy e = other.GetComponent<Enemy>();
        if (e != null)
            shooter.OnDetectionEnter(e);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Enemy e = other.GetComponent<Enemy>();
        if (e != null)
            shooter.OnDetectionExit(e);
    }
}
