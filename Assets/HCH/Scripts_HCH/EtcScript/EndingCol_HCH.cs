using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �ݶ��̴� ������ �߷� ���ϰ� �Ѵ�

public class EndingCol_HCH : MonoBehaviour
{
    public float gravity = -30;
    public Camera cam;
    private void OnTriggerEnter(Collider other)
    {
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = Color.red;
        if (other.gameObject.CompareTag("Player"))
        {
            // ���� �� �ʹ� ������ �������� ���� ����
            GameManager.instance.player.isJump = false;

            ConstantForce cf = other.gameObject.GetComponent<ConstantForce>();
            cf.force = new Vector3(0, gravity, 0);
        }
    }
}
