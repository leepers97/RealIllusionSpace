using UnityEngine;

public class CollisionDetector4 : MonoBehaviour
{
    private Vector3 originalPosition;
    public float moveDistance = 0.05f; // 이동 거리
    private int collisionCount = 0;
    private bool isObjectDown = false;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisionCount++;
        if (collisionCount > 0 && !isObjectDown)
        {
            MoveDown();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        collisionCount--;
        if (collisionCount == 0 && isObjectDown)
        {
            MoveUp();
        }
    }

    private void MoveDown()
    {
        transform.position = new Vector3(originalPosition.x, originalPosition.y - moveDistance, originalPosition.z);
        isObjectDown = true;
    }

    private void MoveUp()
    {
        transform.position = originalPosition;
        isObjectDown = false;
    }
}