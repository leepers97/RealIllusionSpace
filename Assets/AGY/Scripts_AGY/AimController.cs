using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    public GameObject objectToDuplicate; // ������ ������Ʈ
    public LayerMask duplicateLayer; // ���� ������ ���̾�

    private Transform aimTransform; // ������ Transform
    private Renderer lastHoveredRenderer; // ���������� ȣ���� Renderer

    void Start()
    {
        aimTransform = GetComponent<Transform>();
    }

    void Update()
    {
        // ���� ��ġ ����
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, duplicateLayer))
        {
            // ���콺�� ������Ʈ ���� ���� �� ���� ����
            Renderer renderer = hit.collider.GetComponent<Renderer>();
            if (renderer != null)
            {
                // ������ ȣ���� Renderer�� ���� �ʱ�ȭ
                if (lastHoveredRenderer != null && lastHoveredRenderer != renderer)
                {
                    lastHoveredRenderer.material.color = Color.white;
                }

                // ���� ȣ���� Renderer�� ���� ����
                renderer.material.color = Color.yellow;
                lastHoveredRenderer = renderer;
            }

            // ���콺 Ŭ�� �� ������Ʈ ����
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 spawnPosition = hit.point;
                Quaternion spawnRotation = Quaternion.identity;

                // ��ü ����
                GameObject clonedObject = Instantiate(objectToDuplicate, spawnPosition, spawnRotation);
            }
        }
        else
        {
            // ���콺�� ������Ʈ ���� ���� �� ������ ȣ���� Renderer�� ���� �ʱ�ȭ
            if (lastHoveredRenderer != null)
            {
                lastHoveredRenderer.material.color = Color.white;
                lastHoveredRenderer = null;
            }
        }

        // ���� ��ġ ������Ʈ
        aimTransform.position = hit.point;
        aimTransform.rotation = Quaternion.LookRotation(ray.direction);
    }
}
