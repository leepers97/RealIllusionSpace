using UnityEngine;

public class CollisionAreaRigidbodyAdder : MonoBehaviour
{
    public float requiredCollisionArea = 1.0f; // 필요한 총 충돌 면적
    private float totalCollisionArea = 0f; // 누적된 총 충돌 면적
    private bool isRigidbodyAdded = false; // Rigidbody가 추가되었는지 여부

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
        float area = 0f;

        foreach (ContactPoint contact in collision.contacts)
        {
            // 충돌 지점에서 면적을 계산합니다. 간단히 원형 면적으로 가정합니다.
            float radius = 0.1f; // 임의의 반지름 값 (조절 가능)
            area += Mathf.PI * Mathf.Pow(radius, 2);
        }

        return area;
    }

    private void AddRigidbody()
    {
        gameObject.AddComponent<Rigidbody>();
        isRigidbodyAdded = true;
    }
}