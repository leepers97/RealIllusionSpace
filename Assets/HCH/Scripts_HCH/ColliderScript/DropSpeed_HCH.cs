using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 닿으면 플레이어의 떨어지는 속도를 늦춘다

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
