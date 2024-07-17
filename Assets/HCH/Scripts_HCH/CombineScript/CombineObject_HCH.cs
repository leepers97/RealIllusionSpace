using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� ������ player�� ������ ��
// Ư�� �������� üũ�ϰ�
// ���� �ð� ���� Ư�� ������ �����ϸ�
// ������Ʈ�� �����ϰ�
// �ʵ� ������Ʈ�� ����

// �������� +- 1.5����?

public class CombineObject_HCH : MonoBehaviour
{
    public GameObject combinedObjectPrefab;
    public GameObject dividedObject;

    public Transform combinedAngle;
    public Transform combinedSpawnPos;

    public float checkRotationRange = 1.5f;
    public float checkTime = 2f;
    float currentTime;

    bool isSpawn = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (isSpawn) return;
        // player�� �浹 ��
        if (other.gameObject.CompareTag("Player"))
        {
            // Angle�� ������ ���� ������Ʈ�� rotation x, y���� checkRotationRange�̳����
            // player�� rotation���� target�� rotation���� ����
            Vector3 playerRotation = other.transform.rotation.eulerAngles;
            Vector3 targetRotation = combinedAngle.rotation.eulerAngles;

            // player�� rotation.x���� target�� rotation.y���� ���̸� ���ϰ�
            float rotX = Mathf.Abs(playerRotation.x - targetRotation.x);
            float rotY = Mathf.Abs(playerRotation.y - targetRotation.y);

            // �� ���̰� checkRotationRange�̳����
            if (rotX <= checkRotationRange && rotY <= checkRotationRange)
            {
                Debug.Log($"�÷��̾�� ��� ������Ʈ�� ȸ�� ���� {checkRotationRange} ���� �̳��Դϴ�.");
                // �ð��� �����ϰ� checkTime�� �Ǹ�
                currentTime += Time.deltaTime;
                if(currentTime > checkTime)
                {
                    // ������ ���� ������Ʈ�� �����ϰ� ������ �и��� ���� ������Ʈ�� ��Ȱ��ȭ
                    GameObject combinedObject = Instantiate(combinedObjectPrefab);
                    combinedObject.transform.position = combinedSpawnPos.position;
                    dividedObject.SetActive(false);
                    isSpawn = true;
                }
            }
            else
            {
                Debug.Log($"�÷��̾�� ��� ������Ʈ�� ȸ�� ���� {checkRotationRange} ���� �ʰ��Դϴ�.");
                currentTime = 0f;
            }
        }
    }
}
