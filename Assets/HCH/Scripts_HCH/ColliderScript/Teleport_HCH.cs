using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ݶ��̴��� �������� ������ ���� ����Ʈ�� �ڷ���Ʈ�Ѵ�

public class Teleport_HCH : MonoBehaviour
{
    public Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        print("11");
        if (other.CompareTag("Player"))
        {
            print("22");
            other.transform.position = spawnPoint.position;
        }
    }
}
