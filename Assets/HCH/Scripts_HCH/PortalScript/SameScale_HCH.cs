using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// �� ��ũ��Ʈ�� �޸� ������Ʈ�� target������Ʈ�� scale���� ���� �Ѵ�
public class SameScale_HCH : MonoBehaviour
{
    public GameObject target;

    // Update is called once per frame
    void Update()
    {
        target.transform.localScale = gameObject.transform.localScale;
    }
}
