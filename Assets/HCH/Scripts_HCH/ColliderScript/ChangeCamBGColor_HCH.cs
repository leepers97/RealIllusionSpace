using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamBGColor_HCH : MonoBehaviour
{
    public Camera cam;
    public Color backgroundColor = Color.black;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cam.clearFlags = CameraClearFlags.SolidColor;
            cam.backgroundColor = backgroundColor;
        }     
    }
}
