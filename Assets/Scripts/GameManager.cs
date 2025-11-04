using System.ComponentModel.Design;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform tiles;
    [SerializeField] private LayerMask tileLayerMask;
    [SerializeField] private TextMeshProUGUI sunText;
    [SerializeField] private LayerMask sunMask;
    [SerializeField] private Sprite currentShooterSprite;

    public int suns;
    public GameObject currentShooter;
    public static GameManager Instance;

    public void BuyShooter(GameObject shooterPrefab, Sprite sprite)
    {
        currentShooter = shooterPrefab;
        currentShooterSprite = sprite;
    }
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void GameOver()
    {
        Debug.Log("Game Over - you lost your teeth!");
        Time.timeScale = 0f; 
    }

    private void Update()
    {
        sunText.text = suns.ToString();
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, tileLayerMask);

        foreach (Transform tile in tiles)
            tile.GetComponent<SpriteRenderer>().enabled = false;

        if (hit.collider && currentShooter)
        {
            hit.collider.GetComponent<SpriteRenderer>().sprite = currentShooterSprite;
            hit.collider.GetComponent<SpriteRenderer>().enabled = true;

            if(Input.GetMouseButtonDown(0) && !hit.collider.GetComponent<Tile>().hasShooter)
            {
                Instantiate(currentShooter, hit.collider.transform.position, Quaternion.identity);
                hit.collider.GetComponent<Tile>().hasShooter = true;
                currentShooter = null;
                currentShooterSprite = null;
            }

        }
        RaycastHit2D sunHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, sunMask);
        if (sunHit.collider)
        {
            if (Input.GetMouseButtonDown(0))
            {
                suns += 25;
                Destroy(sunHit.collider.gameObject);
            }
        }
    }
}
