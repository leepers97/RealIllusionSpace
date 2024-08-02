using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 오브젝트의 원래 위치를 기억하고 함수 호출 시 오브젝트의 위치를 원래대로 되돌린다

public class ObjectReset_HCH : MonoBehaviour
{
    public GameObject[] obj;
    public Transform[] originalPos;

    public ChessBoardCol_HCH[] chessBoard;

    // Start is called before the first frame update
    void Start()
    {
        //for(int i = 0; i < obj.Length; i++)
        //{
        //    originalPos[i] = obj[i].transform;
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < obj.Length; i++)
            {
                obj[i].transform.localPosition = originalPos[i].localPosition;
            }
            //for (int i = 0; i < chessBoard.Length; i++)
            //{
            //    chessBoard[i].ResetCol();
            //}
        }        
    }
}
