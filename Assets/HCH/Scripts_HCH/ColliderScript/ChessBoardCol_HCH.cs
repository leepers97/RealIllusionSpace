using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 기본적으로 플레이어와 충돌할 수 없다
// targetable 오브젝트와 충돌 중이라면 플레이어와도 충돌 가능하다

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
        // targetable 오브젝트와 충돌 시
        if (collision.gameObject.layer == 9)
        {
            targetableObjCount++;
            // 다른 오브젝트도 충돌 가능하게 하고
            foreach (Collider playerCol in playerCols)
            {
                Physics.IgnoreCollision(playerCol, col, false);
                _IsTouchable = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // targetable 오브젝트와 충돌 해제 시
        if (collision.gameObject.layer == 9)
        {
            targetableObjCount--;
            // 오브젝트가 이미 있다면 충돌 가능
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
