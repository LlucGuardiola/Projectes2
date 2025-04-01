using UnityEngine;

public class PauseLogic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject PausePanel;
    private bool isPaused = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPause()
    {
        if (isPaused)
        {
            PausePanel.SetActive(false);
            Time.timeScale = 1.0f;
        }
        else
        {
            PausePanel.SetActive(true);
            Time.timeScale = 0.0f;
        }

        isPaused = !isPaused;
    }

}
