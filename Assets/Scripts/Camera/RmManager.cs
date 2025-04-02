using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RmManager : MonoBehaviour
{
    public GameObject virtualCam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        virtualCam.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        virtualCam.SetActive(false);
    }

}
