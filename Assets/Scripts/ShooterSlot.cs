using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShooterSlot : MonoBehaviour
{
    [SerializeField] private Sprite Sprite;
    [SerializeField] private GameObject ShooterPrefab;
    [SerializeField] private Image Image;
    [SerializeField] private TextMeshProUGUI PriceText;
    [SerializeField] private int Price;
    private GameManager gms;

    private void Start()
    {
        gms =GameObject.Find("GameManager").GetComponent<GameManager>();
        GetComponent<Button>().onClick.AddListener(BuyShooter);
    }
    private void BuyShooter()
    {
        if (gms.suns >= Price && !gms.currentShooter)
        {
            gms.suns -= Price;
            gms.BuyShooter(ShooterPrefab, Sprite);
        }
    }
    private void OnValidate()
    {
        if (Sprite)
        {
            Image.enabled = true;
            Image.sprite = Sprite;
            PriceText.text = Price.ToString();
        }
        else
        {
            Image.enabled = false;
        }   

    }
}   
