using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ٶ��� ������ ��ü�� ���������� �ʹ�

public class Fan_HCH : MonoBehaviour
{
    public Vector3 windDirection = new Vector3(1, 0, 0); // �ٶ��� ����
    public float windStrength = 10f; // �ٶ��� ����

    private void OnTriggerStay(Collider other)
    {
        // �ٶ��� ���� ������Ʈ�� Rigidbody�� ������ �ִ��� Ȯ��
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // �ٶ��� ���� ����
            rb.AddForce(windDirection.normalized * windStrength);
        }
    }
}
