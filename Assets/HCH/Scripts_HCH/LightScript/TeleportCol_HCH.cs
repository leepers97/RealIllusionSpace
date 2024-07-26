using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 닿으면 미리 지정해둔 리스폰 포인트로 텔레포트

public class TeleportCol_HCH : MonoBehaviour
{
    public Transform targetSpawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.position = targetSpawnPoint.position;
        }
    }
}
