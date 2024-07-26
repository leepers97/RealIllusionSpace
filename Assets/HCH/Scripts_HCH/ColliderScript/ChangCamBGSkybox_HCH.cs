using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangCamBGSkybox_HCH : MonoBehaviour
{
    public Camera cam;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cam.clearFlags = CameraClearFlags.Skybox;
        }
    }
}
