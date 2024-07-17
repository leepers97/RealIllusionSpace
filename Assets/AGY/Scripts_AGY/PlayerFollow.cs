using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform
    public float distance = 5.0f; // �÷��̾�� ����� ������Ʈ ���� �Ÿ�

    void Update()
    {
        // �÷��̾��� �ü��� ���� ������Ʈ�� ���� �Ÿ���ŭ ����߸���
        Vector3 targetPosition = player.position + player.forward * distance;
        transform.position = targetPosition;
    }
}
