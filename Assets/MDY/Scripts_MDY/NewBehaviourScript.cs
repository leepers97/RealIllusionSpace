using UnityEngine;

public class BlockSpecificObject : MonoBehaviour
{
    public string blockTag = "NoPass"; // 막고자 하는 오브젝트의 태그

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(blockTag))
        {
            // 특정 오브젝트가 통과하지 못하도록 막기 위해 오브젝트의 이동을 멈춤
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                // 추가적으로 오브젝트를 뒤로 밀어내는 효과를 줌
                Vector3 pushBackDirection = -other.transform.forward; // 밀어내는 방향
                rb.AddForce(pushBackDirection * 100f, ForceMode.Impulse); // 힘의 크기와 타입 조절
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(blockTag))
        {
            // 트리거 안에 있을 때 오브젝트의 이동을 계속해서 막음
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                // 추가적으로 오브젝트를 계속해서 뒤로 밀어내는 효과를 줌
                Vector3 pushBackDirection = -other.transform.forward; // 밀어내는 방향
                rb.AddForce(pushBackDirection * 100f, ForceMode.Force); // 힘의 크기와 타입 조절
            }
        }
    }
}