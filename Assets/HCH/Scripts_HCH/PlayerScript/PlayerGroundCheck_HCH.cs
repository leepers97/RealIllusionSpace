using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 충돌 감지해서 player스크립트에 전달

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
