using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾ ������ �� �ִ� �ݶ��̴�
// ������Ʈ�� ���� ���¿����� ������� ���ϰ� �Ѵ�

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
