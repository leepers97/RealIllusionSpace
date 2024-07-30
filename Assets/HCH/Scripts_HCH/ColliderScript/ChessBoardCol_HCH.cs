using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �⺻������ �÷��̾�� �浹�� �� ����
// targetable ������Ʈ�� �浹 ���̶�� �÷��̾�͵� �浹 �����ϴ�

public class ChessBoardCol_HCH : MonoBehaviour
{
    [SerializeField]
    bool isTouchable = false;

    BoxCollider col;
    CapsuleCollider playerCol;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider>();
        playerCol = GameManager.instance.player.gameObject.GetComponent<CapsuleCollider>();
        Physics.IgnoreCollision(playerCol, col, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
        // targetable ������Ʈ�� �浹 ��
        if(collision.gameObject.layer == 9)
        {
            // �ٸ� ������Ʈ�� �浹 �����ϰ� �ϰ�
            //isTouchable = true;
            Physics.IgnoreCollision(playerCol, col, false);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // targetable ������Ʈ�� �浹 ���� ��
        if (collision.gameObject.layer == 9)
        {
            //isTouchable = false;
            Physics.IgnoreCollision(playerCol, col, true);
        }
    }
}
