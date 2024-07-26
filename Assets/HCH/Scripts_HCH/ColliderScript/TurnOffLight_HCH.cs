using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ���� ������ ���� ����Ʈ�� ����
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
