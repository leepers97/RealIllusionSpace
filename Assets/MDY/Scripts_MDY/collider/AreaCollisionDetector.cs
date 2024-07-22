using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCollisionDetector : MonoBehaviour
{
    public float requiredCollisionArea = 1.0f; // 임계값을 설정합니다.
    private float totalCollisionArea = 0.0f;
    private bool isRigidbodyAdded = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (isRigidbodyAdded) return;

        float collisionArea = CalculateCollisionArea(collision);

        totalCollisionArea += collisionArea;

        if (totalCollisionArea >= requiredCollisionArea)
        {
            AddRigidbody();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (isRigidbodyAdded) return;

        float collisionArea = CalculateCollisionArea(collision);

        totalCollisionArea += collisionArea;

        if (totalCollisionArea >= requiredCollisionArea)
        {
            AddRigidbody();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (isRigidbodyAdded) return;

        float collisionArea = CalculateCollisionArea(collision);

        totalCollisionArea -= collisionArea;

        if (totalCollisionArea < 0)
        {
            totalCollisionArea = 0;
        }
    }

    private float CalculateCollisionArea(Collision collision)
    {
        // 충돌 면적을 계산합니다.
        // 간단한 예로 충돌 지점의 개수를 사용하지만,
        // 실제 면적 계산을 원하면 더욱 정교한 계산을 해야 합니다.
        return collision.contacts.Length * 0.1f; // 충돌 지점의 개수를 기반으로 면적을 추정합니다.
    }

    private void AddRigidbody()
    {
        gameObject.AddComponent<Rigidbody>();
        isRigidbodyAdded = true;
    }
}