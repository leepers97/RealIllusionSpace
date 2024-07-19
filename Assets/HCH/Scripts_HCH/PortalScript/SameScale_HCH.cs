using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// �� ��ũ��Ʈ�� �޸� ������Ʈ�� target������Ʈ�� scale���� ���� �Ѵ�
// ����Ǿ��ִ� �� ��Ż�� ������ ���� �Ѵ�
// ��Ż�� x���� ������Ų��
public class SameScale_HCH : MonoBehaviour
{
    public GameObject target;
    public GameObject anotherPortal;

    // Update is called once per frame
    void Update()
    {
        // ��Ż ũ�⿡ ���� �ݴ��� �� ��ü�� Ŀ������ ����
        target.transform.localScale = gameObject.transform.localScale;

        // �� ��Ż�� ȸ���� ����ȭ
        anotherPortal.transform.rotation = gameObject.transform.rotation;

        // ���� ȸ�� �� ��������
        Vector3 currentRotation = transform.rotation.eulerAngles;
        // x, z �� ���� 0���� ����
        currentRotation.x = 0;
        currentRotation.z = 0;
        // ������Ʈ�� ȸ�� �� ������Ʈ
        transform.rotation = Quaternion.Euler(currentRotation);
    }
}
