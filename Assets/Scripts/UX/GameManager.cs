using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void GameStart()
    {
        Cinematic();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void Cinematic()
    {
        SceneManager.LoadScene("Cinematica");
    }
    public void MainScene ()
    {
        SceneManager.LoadScene("Blockout");
        CameraFade.StartFade(true, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Invoke("MainScene", 1f);
        CameraFade.StartFade(false, 1f);
    }
}
