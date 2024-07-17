using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   public Transform playerBody; // �÷��̾��� ����
    public Transform playerHead; // �÷��̾��� �Ӹ�

    public float mouseSensitivity = 100f;
    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None; // Ŀ�� �׻� Ȱ��ȭ
        Cursor.visible = true; // Ŀ�� �׻� ���̰� ����
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // �÷��̾��� ������ �¿�� ȸ��
        playerBody.Rotate(Vector3.up * mouseX);

        // �÷��̾��� �Ӹ��� ���Ϸ� ȸ��
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // ���� ȸ�� ���� ����

        playerHead.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
