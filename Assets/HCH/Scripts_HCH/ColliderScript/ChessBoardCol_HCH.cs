using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �⺻������ �÷��̾�� �浹�� �� ����
// targetable ������Ʈ�� �浹 ���̶�� �÷��̾�͵� �浹 �����ϴ�

public class ChessBoardCol_HCH : MonoBehaviour
{
    [SerializeField]
    bool _IsTouchable = false;

    Collider col;
    Collider[] playerCols;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();
        playerCols = GameManager.instance.player.GetComponentsInChildren<Collider>();
        foreach(Collider playerCol in playerCols)
        {
            Physics.IgnoreCollision(playerCol, col, true);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // targetable ������Ʈ�� �浹 ��
        if (collision.gameObject.layer == 9)
        {
            // �ٸ� ������Ʈ�� �浹 �����ϰ� �ϰ�
            foreach (Collider playerCol in playerCols)
            {
                Physics.IgnoreCollision(playerCol, col, false);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // targetable ������Ʈ�� �浹 ���� ��
        if (collision.gameObject.layer == 9)
        {
            foreach (Collider playerCol in playerCols)
            {
                Physics.IgnoreCollision(playerCol, col, true);
            }

        }
    }
}
