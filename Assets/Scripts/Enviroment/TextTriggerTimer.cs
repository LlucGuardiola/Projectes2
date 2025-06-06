using UnityEngine;
using System.Collections;

public class TextTriggerTimer : MonoBehaviour
{
    public GameObject worldText;
    private bool yaActivado = false;
    public PlayerAttack playerAttack;

    private void Start()
    {
        worldText.SetActive(false);
        playerAttack = GameObject.Find("Player").GetComponent<PlayerAttack>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !yaActivado && playerAttack.CanAttack)
        {
            yaActivado = true;
            worldText.SetActive(true);
            StartCoroutine(DesactivarTexto());
        }
    }

    IEnumerator DesactivarTexto()
    {
        yield return new WaitForSeconds(5f);
        worldText.SetActive(false);
    }
}
