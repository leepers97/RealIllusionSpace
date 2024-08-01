using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ݶ��̴��� �������� ������ ���� ����Ʈ�� �ڷ���Ʈ�Ѵ�

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
