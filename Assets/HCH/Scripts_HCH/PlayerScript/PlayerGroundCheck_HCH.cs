using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �浹 �����ؼ� player��ũ��Ʈ�� ����

public class PlayerGroundCheck_HCH : MonoBehaviour
{
    Player_HCH player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Player_HCH>();
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    print("11");
    //    if (player != null)
    //    {
    //        player.FeetCollisionEnter(collision);
    //    }
    //}
}
