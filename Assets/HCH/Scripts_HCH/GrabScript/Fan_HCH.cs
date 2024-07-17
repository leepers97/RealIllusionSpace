using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 바람에 닿으면 물체를 날려버리고 싶다

public class Fan_HCH : MonoBehaviour
{
    public Vector3 windDirection = new Vector3(1, 0, 0); // 바람의 방향
    public float windStrength = 10f; // 바람의 세기

    private void OnTriggerStay(Collider other)
    {
        // 바람에 닿은 오브젝트가 Rigidbody를 가지고 있는지 확인
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // 바람의 힘을 적용
            rb.AddForce(windDirection.normalized * windStrength);
        }
    }
}
