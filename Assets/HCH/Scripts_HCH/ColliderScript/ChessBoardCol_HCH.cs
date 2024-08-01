using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �⺻������ �÷��̾�� �浹�� �� ����
// targetable ������Ʈ�� �浹 ���̶�� �÷��̾�͵� �浹 �����ϴ�

public class ChessBoardCol_HCH : MonoBehaviour
{
    [SerializeField]
    bool _IsTouchable = false;
    public int targetableObjCount = 0;

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
            _IsTouchable = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // targetable ������Ʈ�� �浹 ��
        if (collision.gameObject.layer == 9)
        {
            targetableObjCount++;
            // �ٸ� ������Ʈ�� �浹 �����ϰ� �ϰ�
            foreach (Collider playerCol in playerCols)
            {
                Physics.IgnoreCollision(playerCol, col, false);
                _IsTouchable = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // targetable ������Ʈ�� �浹 ���� ��
        if (collision.gameObject.layer == 9)
        {
            targetableObjCount--;
            // ������Ʈ�� �̹� �ִٸ� �浹 ����
            if (targetableObjCount <= 0)
            {
                foreach (Collider playerCol in playerCols)
                {
                    Physics.IgnoreCollision(playerCol, col, true);
                    _IsTouchable = false;
                }
            }
        }
    }

    public void ResetCol()
    {
        foreach (Collider playerCol in playerCols)
        {
            Physics.IgnoreCollision(playerCol, col, true);
            _IsTouchable = false;
        }
    }
}
