using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RmManager : MonoBehaviour
{
    public GameObject virtualCam;
    public Transform checkpointPosition;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
        /*    foreach (var cam in GameObject.FindGameObjectsWithTag("VirtualCamera"))
            {
                if (cam != virtualCam)
                    cam.SetActive(false);
            }  */

            virtualCam.SetActive(true);
            CheckpointManager.Instance.SetCheckpoint(checkpointPosition.position, virtualCam);
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
