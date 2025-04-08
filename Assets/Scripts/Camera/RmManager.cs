using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RmManager : MonoBehaviour
{
    public GameObject virtualCam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {   
            virtualCam.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            virtualCam.SetActive(false);
        }
    }

}
