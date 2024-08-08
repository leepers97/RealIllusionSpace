using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DividedCube_HCH : MonoBehaviour
{
    public GameObject cubePrefab;
    // ���� �Ŀ�
    public float divideForce;
    // ���� ��ġ
    public Vector3 offset = Vector3.zero;

    public void DivideCube()
    {
        // �Լ��� ȣ��Ǹ� ���� ���� ���� ť�긦 �����Ѵ�
        //GameObject dividedCube = Instantiate(cubePrefab, transform.position, Quaternion.identity);
        GameObject dividedCube = Instantiate(cubePrefab, transform.position, transform.rotation);
        // ������ ť���� ũ��� ���� ť���� ũ�⿡ ����Ѵ�
        dividedCube.transform.localScale = transform.localScale / 3;

        // ���� ť����� rigidbody�� ������ ���߽�Ų��
        Rigidbody[] rb = dividedCube.GetComponentsInChildren<Rigidbody>();
        for(int i = 0; i < rb.Length; i++)
        {
            rb[i].AddExplosionForce(divideForce, transform.position + offset, 10f);
        }
        // ���� ť��� ��Ȱ��ȭ�Ѵ�
        gameObject.SetActive(false);
    }
}
