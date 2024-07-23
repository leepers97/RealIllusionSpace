using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 콜라이더 닿으면 중력 강하게 한다

public class EndingCol_HCH : MonoBehaviour
{
    public float gravity = -30;
    public Camera cam;
    private void OnTriggerEnter(Collider other)
    {
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = Color.red;
        if (other.gameObject.CompareTag("Player"))
        {
            // 점프 시 너무 빠르게 떨어지는 현상 제어
            GameManager.instance.player.isJump = false;

            ConstantForce cf = other.gameObject.GetComponent<ConstantForce>();
            cf.force = new Vector3(0, gravity, 0);
        }
    }
}
