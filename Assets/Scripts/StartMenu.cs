using UnityEngine;
using UnityEngine.SceneManagement;
public class startmenu : MonoBehaviour
{
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject OptionsButton;
    private void onStartButtonClicked()
    {
        //Debug.Log("Start button clicked, loading game scene...");
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
    }
    private void onTuturiolButtonClicked()
    {
        SceneManager.LoadScene(1);
    }
    private void onExitButtonClicked()
    {
        //Debug.Log("Exit button clicked, quitting application...");
        Application.Quit();
    }
    private void onOptionsButtonClicked()
    {
        //Debug.Log("options button clicked, loading game scene...");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Options");
    }

    private void onLevel1ButtonClicked()
    {
        SceneManager.LoadScene(3);
        Time.timeScale = 1;
    }

    private void onLevel2ButtonClicked()
    {
        SceneManager.LoadScene(4);
        Time.timeScale = 1;
    }

    private void onLevel3ButtonClicked()
    {
        SceneManager.LoadScene(5);
        Time.timeScale = 1;
    }

    private void onLevel4ButtonClicked()
    {
        SceneManager.LoadScene(6);
        Time.timeScale = 1;
    }

    private void onOtherLevelsButtonClicled()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1;
    }
}
