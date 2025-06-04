using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayableDirector timeline;
    public float changeSceneTime;

    public void GameStart()
    {
        SceneManager.LoadScene("Blockout");
    }

    public void Exit()
    {
        Application.Quit();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject lifeBar = GameObject.Find("LifeBar");
            if (lifeBar != null)
            {
                lifeBar.SetActive(false);
            }

            GameObject dashBar = GameObject.Find("DashBar");
            if (dashBar != null)
            {
                dashBar.SetActive(false);
            }

            timeline.Play();
        }
    }

    private void Update()
    {
        if (timeline.state != PlayState.Playing) return;

        changeSceneTime -= Time.deltaTime;
        if (changeSceneTime <= 0) { SceneManager.LoadScene("EndGameScene"); }

    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
