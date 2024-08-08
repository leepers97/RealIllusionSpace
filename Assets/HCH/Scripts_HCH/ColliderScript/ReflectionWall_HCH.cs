using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어만 지나갈 수 있는 콜라이더
// 오브젝트를 잡은 상태에서는 통과하지 못하게 한다

public class ReflectionWall_HCH : MonoBehaviour
{
    Collider col;
    Collider[] playerCols;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();
        playerCols = GameManager.instance.player.GetComponentsInChildren<Collider>();
        foreach (Collider playerCol in playerCols)
        {
            Physics.IgnoreCollision(playerCol, col, true);
        }
    }

    private void Update()
    {
        if (GameManager.instance.player.GetComponent<Grab3_HCH>().grabbedObject != null)
        {
            foreach (Collider playerCol in playerCols)
            {
                Physics.IgnoreCollision(playerCol, col, false);
            }
        }
        else
        {
            foreach (Collider playerCol in playerCols)
            {
                Physics.IgnoreCollision(playerCol, col, true);
            }
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        isColEnabled = false;
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        isColEnabled = true;
    //    }
    //}
}
