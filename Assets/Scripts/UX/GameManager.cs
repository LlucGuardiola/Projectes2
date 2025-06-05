using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void GameStart()
    {
        MainScene();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void MainScene ()
    {
        SceneManager.LoadScene("Blockout");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Invoke("MainScene", 1f);
        CameraFade.StartFade(false, 1f);
    }
}
