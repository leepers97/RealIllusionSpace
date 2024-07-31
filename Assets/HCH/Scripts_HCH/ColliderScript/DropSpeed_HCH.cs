using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ �÷��̾��� �������� �ӵ��� �����

public class DropSpeed_HCH : MonoBehaviour
{
    public float dragValue = 3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody>().drag = dragValue;
        }
    }
}
