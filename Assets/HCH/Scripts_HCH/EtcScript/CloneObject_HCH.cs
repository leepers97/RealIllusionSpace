using UnityEngine;

public class CloneObject_HCH : MonoBehaviour
{
    public Camera playerCamera; // �÷��̾� ī�޶�
    public float scaleDecrement = 0.9f; // ũ�� ���� ���� (�� ������ 90%�� ����)

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� Ŭ�� ��
        {
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject == gameObject && hit.collider.tag == "Clone")
                {
                    hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    CloneObject(hit.collider.gameObject);
                }
            }
        }
    }

    void CloneObject(GameObject original)
    {
        Renderer renderer = original.GetComponent<Renderer>();
        float objectSize = renderer.bounds.size.z; // ���� ��ü�� ���̸� �������� ���� ��ġ ���

        // �÷��̾�� ���� ��ü ������ ���� ����
        Vector3 directionToPlayer = (playerCamera.transform.position - original.transform.position).normalized;

        // �÷��̾�� �� ����� �ʿ� ����, ������ ���� �� �÷� ��ġ�� �ʵ��� ����
        Vector3 clonePosition = original.transform.position + directionToPlayer * (objectSize * 0.75f);

        // ������ ��ü�� �÷��̾ �ٶ󺸵��� ȸ��
        Quaternion cloneRotation = Quaternion.LookRotation(directionToPlayer);

        // ��ü ���� �� ��ġ�� ȸ�� ����
        GameObject clone = Instantiate(original, clonePosition, cloneRotation);
        // �� ������ ������ ���� ����
        clone.transform.localScale = original.transform.localScale * scaleDecrement;
    }
}