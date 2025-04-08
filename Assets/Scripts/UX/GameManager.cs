using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
 
    public void GameStart()
    {
        SceneManager.LoadScene("BlockoutScene");
    }

    public void OnQuit()
    {
        //Debug.Log("salir");
        //Application.Quit();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene("EndGameScene");
    }
}
