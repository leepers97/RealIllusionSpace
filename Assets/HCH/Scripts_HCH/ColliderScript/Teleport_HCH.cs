using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 콜라이더를 지나가면 지정한 스폰 포인트로 텔레포트한다

public class Teleport_HCH : MonoBehaviour
{
    public Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = spawnPoint.position;
        }
    }
}
