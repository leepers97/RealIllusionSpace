using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾ ��ȣ�ۿ��� ������ ������Ʈ�� �浹 �� ��ư�� ������
// ����Ǿ��ִ� ���� ������
// ��� ǥ�õ� �ٲ��

public class InteractiveButton_HCH : MonoBehaviour
{
    public GameObject door;

    public float buttonPressSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 9 || collision.gameObject.CompareTag("Player"))
        {
            door.transform.Translate(Vector3.left);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 9 || collision.gameObject.CompareTag("Player"))
        {

        }
    }
}
