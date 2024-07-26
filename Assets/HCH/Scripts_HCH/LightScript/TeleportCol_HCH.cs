using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ �̸� �����ص� ������ ����Ʈ�� �ڷ���Ʈ

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
