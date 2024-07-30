using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 기본적으로 플레이어와 충돌할 수 없다
// targetable 오브젝트와 충돌 중이라면 플레이어와도 충돌 가능하다

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
        // targetable 오브젝트와 충돌 시
        if(collision.gameObject.layer == 9)
        {
            // 다른 오브젝트도 충돌 가능하게 하고
            //isTouchable = true;
            Physics.IgnoreCollision(playerCol, col, false);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // targetable 오브젝트와 충돌 해제 시
        if (collision.gameObject.layer == 9)
        {
            //isTouchable = false;
            Physics.IgnoreCollision(playerCol, col, true);
        }
    }
}
