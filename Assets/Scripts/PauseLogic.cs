using UnityEngine;

public class PauseLogic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject PausePanel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void Continue()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
