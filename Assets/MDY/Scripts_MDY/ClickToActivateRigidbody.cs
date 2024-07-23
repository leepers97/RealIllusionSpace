using UnityEngine;

public class ClickToActivateRigidbody : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.isKinematic = true; // 초기 상태에서는 Rigidbody를 비활성화
    }

    private void OnMouseDown()
    {
        if (rb.isKinematic)
        {
            rb.isKinematic = false; // 물체가 클릭되었을 때 Rigidbody 활성화
            this.enabled = false; // 스크립트 비활성화
        }
    }
}