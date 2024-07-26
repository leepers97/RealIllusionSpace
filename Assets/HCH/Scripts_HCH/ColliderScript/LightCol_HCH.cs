using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCol_HCH : MonoBehaviour
{
    public Light directionalLight;

    private void OnTriggerEnter(Collider other)
    {
        directionalLight.enabled = true;
        RenderSettings.ambientIntensity = 1;
        RenderSettings.reflectionIntensity = 1;
    }
}
