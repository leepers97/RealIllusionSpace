using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ٴڿ� ������ �ʱ� ��ġ�� �ڷ���Ʈ �ȴ�

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
