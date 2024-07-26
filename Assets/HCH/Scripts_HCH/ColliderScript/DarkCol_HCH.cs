using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkCol_HCH : MonoBehaviour
{
    public Light directionalLight;

    private void OnTriggerEnter(Collider other)
    {
        directionalLight.enabled = false;
        RenderSettings.ambientIntensity = 0;
        RenderSettings.reflectionIntensity = 0;
    }
}
