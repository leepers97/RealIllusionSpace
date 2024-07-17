using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 바닥에 닿으면 초기 위치로 텔레포트 된다

public class LightPlaceTeleport_HCH : MonoBehaviour
{
    public Transform spawnPoint;
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.position = spawnPoint.position;
        }
    }
}
