using UnityEngine;

public class IgnorePlayerCollision : MonoBehaviour
{
    private Collider boxCollider;

    void Start()
    {
        // 이 오브젝트의 Collider를 가져옵니다.
        boxCollider = GetComponent<Collider>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트가 "Player" 태그를 가지고 있는지 확인합니다.
        if (collision.gameObject.CompareTag("Player"))
        {
            // "Player" 태그를 가진 오브젝트와의 충돌을 무시합니다.
            Physics.IgnoreCollision(collision.collider, boxCollider);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트가 "Player" 태그를 가지고 있는지 확인합니다.
        if (other.CompareTag("Player"))
        {
            // "Player" 태그를 가진 오브젝트와의 충돌을 무시합니다.
            Physics.IgnoreCollision(other, boxCollider);
        }
    }
}