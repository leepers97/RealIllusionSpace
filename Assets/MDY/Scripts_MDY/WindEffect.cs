using UnityEngine;

public class WindEffect : MonoBehaviour
{
    public Vector3 windDirection = new Vector3(1, 0, 0); // 바람의 방향 (기본적으로 오른쪽)
    public float windStrength = 10f; // 바람의 강도
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트 가져오기
    }

    private void FixedUpdate()
    {
        ApplyWind();
    }

    private void ApplyWind()
    {
        if (rb != null)
        {
            // 바람의 힘 적용
            rb.AddForce(windDirection.normalized * windStrength);
        }
    }
}