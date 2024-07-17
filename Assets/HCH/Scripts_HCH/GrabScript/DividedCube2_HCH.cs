using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DividedCube2_HCH : MonoBehaviour
{
    // �ڽ� ������Ʈ�� rigidbody�� �޾Ƽ�
    // grab�� Ÿ������ �ڽ� ������Ʈ�� ���õȴٸ�
    // isKinematic�� ��� ����

    [SerializeField]
    Rigidbody[] rb;
    public bool isDivide = false;

    private void Start()
    {
        rb = gameObject.GetComponentsInChildren<Rigidbody>();
    }

    private void Update()
    {
        foreach (Rigidbody rigid in rb)
        {
            if (rigid.gameObject == GameManager.instance.grab.grabbedObject)
            {
                isDivide = true;
                break;
            }
        }
        if (isDivide)
        {
            foreach (Rigidbody rigid in rb)
            {
                if (rigid.gameObject == GameManager.instance.grab.grabbedObject) return;
                rigid.isKinematic = false;
            }
        }
    }

    public void Divide()
    {
        isDivide = true;
        if (isDivide)
        {
            foreach (Rigidbody rigid in rb)
            {
                rigid.isKinematic = false;
            }
        }
    }
}
