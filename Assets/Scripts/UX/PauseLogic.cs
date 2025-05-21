using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseLogic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject PausePanel;
    public static bool IsPaused = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Return()
    {
        SceneManager.LoadScene("MainMenuScene");
        IsPaused = false;
        PausePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void OnPause()
    {
        if (IsPaused)
        {
            PausePanel.SetActive(false);
            Time.timeScale = 1.0f;
        }
        else
        {
            PausePanel.SetActive(true);
            Time.timeScale = 0.0f;
        }

        IsPaused = !IsPaused;
    }

    public void Resart()
    {
        SceneManager.LoadScene("Blockout");
        IsPaused = false;
        PausePanel.SetActive(false);
        Time.timeScale = 1.0f;

    }

    public void Resume()
    {
        IsPaused = false;
        PausePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
