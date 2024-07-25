using UnityEngine;

public class CollisionDetector3 : MonoBehaviour
{
    public Renderer objectDRenderer; // 물체 D의 Renderer 
    public Material collisionMaterial; // 충돌이 감지될 때 사용할 메터리얼
    public Material defaultMaterial; // 충돌이 감지되지 않을 때 사용할 메터리얼
    private int collisionCount = 0;

    private void Start()
    {
        // 초기 메터리얼 설정
        objectDRenderer.material = defaultMaterial;
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisionCount++;
        if (collisionCount == 1)
        {
            objectDRenderer.material = collisionMaterial; // 충돌 시 메터리얼 
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        collisionCount--;
        if (collisionCount == 0)
        {
            objectDRenderer.material = defaultMaterial; // 충돌 종료 시 메터리얼 
        }
    }
}