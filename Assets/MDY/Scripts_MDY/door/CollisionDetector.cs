using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public ObjectBFader objectBFader;
    private int collisionCount = 0;

    private void OnCollisionEnter(Collision collision)
    {
        collisionCount++;
        if (collisionCount == 1)
        {
            objectBFader.StartFadingOut();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        collisionCount--;
        if (collisionCount == 0)
        {
            objectBFader.StartFadingIn();
        }
    }
}