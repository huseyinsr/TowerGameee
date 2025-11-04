using UnityEngine;
using UnityEngine.Rendering;

public class Sun : MonoBehaviour
{
    private float dropToYPos;
    private float speed = .40f;
    private void Start()
    {
        transform.position = new Vector3(Random.Range(-9f, 9f), 8f, 0f);
        dropToYPos = Random.Range(-4f, 3f);
        Destroy(gameObject, Random.Range(6f,12f));
    }

    private void Update()
    {
        if(transform.position.y >= dropToYPos)
            transform.position -= new Vector3(0, speed * Time.fixedDeltaTime, 0);

    }
}
    