using UnityEngine;

public class CollisionDetector2 : MonoBehaviour
{
    public Renderer objectCRenderer; // 물체 C의 Renderer 
    public Material collisionMaterial; // 충돌이 감지될 때 사용할 메터리얼
    public Material defaultMaterial; // 충돌이 감지되지 않을 때 사용할 메터리얼
    private int collisionCount = 0;

    private void Start()
    {
        // 초기 메터리얼 설정
        objectCRenderer.material = defaultMaterial;
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisionCount++;
        if (collisionCount == 1)
        {
            objectCRenderer.material = collisionMaterial; // 충돌 시 메터리얼 
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        collisionCount--;
        if (collisionCount == 0)
        {
            objectCRenderer.material = defaultMaterial; // 충돌 종료 시 메터리얼 
        }
    }
}