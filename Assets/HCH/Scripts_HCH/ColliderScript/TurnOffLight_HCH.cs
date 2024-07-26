using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 성능을 위해 지나간 맵의 라이트를 끈다
public class TurnOffLight_HCH : MonoBehaviour
{
    public Light[] lights;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach(Light light in lights)
            {
                light.enabled = false;
            }
        }
    }
}
