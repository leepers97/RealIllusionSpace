using UnityEngine;

public class CloneOnClick : MonoBehaviour
{
    public Camera playerCamera; // 플레이어 카메라
    private float clickCooldown = 0.2f; // 클릭 쿨다운 시간
    private float lastClickTime = 0f; // 마지막 클릭 시간

    void Update()
    {
        if (Time.time - lastClickTime >= clickCooldown) // 쿨다운 시간이 지난 경우
        {
            if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 클릭 시
            {
                Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider.gameObject == gameObject && hit.collider.CompareTag("Clone"))
                    {
                        CloneObject(hit.collider.gameObject);
                        lastClickTime = Time.time; // 마지막 클릭 시간 갱신
                    }
                }
            }
        }
    }

    void CloneObject(GameObject original)
    {
        Renderer renderer = original.GetComponent<Renderer>();
        float objectSize = renderer.bounds.size.z; // 원본 물체의 깊이를 기준으로 복제 위치 계산

        // 플레이어와 원본 물체 사이의 방향 벡터
        Vector3 directionToPlayer = (playerCamera.transform.position - original.transform.position).normalized;

        // 플레이어에게 더 가까운 쪽에 복제, 간격을 조금 더 늘려 겹치지 않도록 설정
        Vector3 clonePosition = original.transform.position + directionToPlayer * (objectSize * 0.75f); // 기존 0.5f에서 0.75f로 변경

        // 복제된 물체가 플레이어를 바라보도록 회전
        Quaternion cloneRotation = Quaternion.LookRotation(directionToPlayer);

        // 물체 복제 및 위치와 회전 설정
        GameObject clone = Instantiate(original, clonePosition, cloneRotation);
        clone.transform.localScale = original.transform.localScale;

        // 복제된 오브젝트에서 CloneOnClick 스크립트를 비활성화하고 0.2초 후에 다시 활성화
        var cloneScript = clone.GetComponent<CloneOnClick>();
        if (cloneScript != null)
        {
            cloneScript.enabled = false;
            cloneScript.Invoke("EnableScript", 0.3f);
        }
    }

    void EnableScript()
    {
        this.enabled = true;
    }
}