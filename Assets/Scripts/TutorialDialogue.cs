using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialDialogue : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject dialoguePanel;      
    [SerializeField] private GameObject backgroundPanel;     
    [SerializeField] private TextMeshProUGUI dialogueText;   
    [SerializeField] private Image dialogueImage;            

    [Header("Sound")]
    [SerializeField] private AudioClip clickSound;           
    private AudioSource audioSource;

    [Header("Dialogue Settings")]
    [TextArea(2, 5)]
    [SerializeField] private string[] dialogueLines;       

    [SerializeField] private Sprite[] dialogueImages;        

    private int currentLine = 0;
    private bool tutorialActive = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        Time.timeScale = 0f;
        currentLine = 0;
        ShowDialogue();
    }

    void Update()
    {
        if (tutorialActive && Input.GetMouseButtonDown(0))
        {
            PlayClickSound();

            currentLine++;
            if (currentLine < dialogueLines.Length)
            {
                ShowDialogue();
            }
            else
            {
                EndTutorial();
            }
        }
    }

    void ShowDialogue()
    {
        if (dialoguePanel != null) dialoguePanel.SetActive(true);
        if (backgroundPanel != null) backgroundPanel.SetActive(true);

        dialogueText.text = dialogueLines[currentLine];

        if (dialogueImages != null && currentLine < dialogueImages.Length && dialogueImages[currentLine] != null)
        {
            dialogueImage.sprite = dialogueImages[currentLine];
            dialogueImage.enabled = true;
        }
        else
        {
            dialogueImage.enabled = false;
        }
    }

    void EndTutorial()
    {
        if (dialoguePanel != null) dialoguePanel.SetActive(false);
        if (backgroundPanel != null) backgroundPanel.SetActive(false);

        Time.timeScale = 1f; 
        tutorialActive = false;
    }

    void PlayClickSound()
    {
        if (clickSound != null)
            audioSource.PlayOneShot(clickSound);
    }
}
