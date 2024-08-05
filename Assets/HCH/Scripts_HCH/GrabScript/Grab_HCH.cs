using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 1. ���ٹ� ��Ŀ���� ���� �ʿ�(��� �̵� �� �����Ÿ�)
// 2. Ŭ�� �� �̵� �߿� �ٸ� ������Ʈ�� �浹 ����

public class Grab_HCH : MonoBehaviour
{
    [Header("Components")]
    public Transform target;

    [Header("Parameters")]
    public LayerMask targetMask;
    public LayerMask ignoreTargetMask;
    // ���� ��ü ������ �ּҰŸ���
    public float offsetFactor;
    
    // ī�޶�� ������Ʈ���� ���� �Ÿ�
    float originalDistance;
    // ũ�� ���� �� ������Ʈ�� ���� ũ��
    Vector3 originalScale;
    // �� �����ӿ� ���� ������Ʈ�� ũ��
    Vector3 targetScale;

    // ��Ŭ���ϰ� ��ü ȸ�� �� �ӵ�
    public float rotateSpeed = 5;
    // ���� ȸ����
    Vector3 currentRotation;
    // ��ü ȸ�� �� ī�޶� ȸ�� ���� ���� player��ũ��Ʈ
    Player_HCH player;

    void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        player = GetComponentInParent<Player_HCH>();
    }

    void Update()
    {
        HandleInput();
        RotateTarget();
        ResizeTarget();
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ���� ���õ� Ÿ���� ���ٸ�
            if (target == null)
            {
                RaycastHit hit;
                // targetMask�� ���õ� layer�� ������Ʈ�� ray�� ����
                if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, targetMask))
                {
                    // ray �� �¾Ҵ��� Ȯ�ο�
                    Debug.DrawRay(transform.position, transform.forward * 20, Color.red);
                    Debug.Log("Raycast hit: " + hit.transform.name);

                    // ray�� ���� ������Ʈ�� Ÿ���� ��
                    target = hit.transform;

                    // Ÿ�� ������Ʈ�� ���ְ� �ϰ� �ٸ� ������Ʈ���� �浹�� ���´�
                    target.GetComponent<Rigidbody>().isKinematic = true;
                    target.GetComponent<Collider>().isTrigger = true;

                    // ĳ����(ī�޶�)�� Ÿ�� ������Ʈ ���� �Ÿ��� ���
                    originalDistance = Vector3.Distance(transform.position, target.position);

                    // Ÿ�� ������Ʈ�� ������ ���� ����
                    originalScale = target.localScale;

                    // �ϴ� �ٲ� Ÿ�� ������Ʈ ������ ���� ���� Ÿ�� ������Ʈ ������ �� ����
                    targetScale = target.localScale;
                    target.gameObject.transform.SetParent(transform);

                    target.gameObject.layer = LayerMask.NameToLayer("Targeting");
                    //int targetingLayer = LayerMask.NameToLayer("Targeting");
                    //SetChildLayerRecursion(target.gameObject, targetingLayer);
                }
            }
            // �ٽ� ��Ŭ���� �Ѵٸ�(���� ���õ� Ÿ���� �ִٸ�)
            else
            {
                // ���� �и��Ǵ� ������Ʈ��� �и���Ų��
                if (target.CompareTag("DivideCube"))
                {
                    target.GetComponent<DividedCube_HCH>().DivideCube();
                }

                // ������Ʈ�� �������¸� �ٽ� �ǵ����� �ٽ� �ٸ� ������Ʈ�� �浹�� �����ϰ� �Ѵ�(��Ż�� ����)
                target.GetComponent<Rigidbody>().isKinematic = false;
                if(!target.gameObject.CompareTag("Portal")) target.GetComponent<Collider>().isTrigger = false;

                // Ÿ���� ����
                target.gameObject.layer = LayerMask.NameToLayer("Targetable");
                target.transform.parent = null;
                //int targetingLayer = LayerMask.NameToLayer("Targetable");
                //SetChildLayerRecursion(target.gameObject, targetingLayer);
                target = null;


            }
        }
    }

    void RotateTarget()
    {
        // ���� ���õ� Ÿ���� ���ٸ� ����
        // if (target == null) return;
        // ��Ŭ���ϸ� ī�޶� �̵� ����
        if (Input.GetMouseButtonDown(1)) player.isCamMove = false;
        if (Input.GetMouseButton(1))
        {
            // Ÿ���� ���� ���� �ޱ�
            currentRotation = target.rotation.eulerAngles;
            // ���콺 x ������ ���� �޾�
            currentRotation.y += -Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
            // ��ü�� ȸ��
            target.rotation = Quaternion.Euler(currentRotation);
        }
        // ��Ŭ�� �����ϸ� �ٽ� ī�޶� �̵�
        if (Input.GetMouseButtonUp(1)) player.isCamMove = true;
    }

    public float smoothSpeed = 10f;
    void ResizeTarget()
    {
        // ���� ���õ� Ÿ���� ���ٸ�
        if (target == null)
        {
            // �ƹ��ϵ� �Ͼ�� ����
            return;
        }

        RaycastHit hit;
        // Ÿ���� �� �� ���� ������Ʈ�� ray�� ����
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, ignoreTargetMask))
        {
            Debug.DrawRay(transform.position, transform.forward * 20, Color.red);
            // Ÿ���� ��ġ�� ray�� ���� �� ����(Ÿ���� �� �� ����)���� offsetFactor����ŭ�� �Ÿ��� �ΰ� ����
            Vector3 offset = new Vector3(
            transform.forward.x * offsetFactor * targetScale.x,
            transform.forward.y * offsetFactor * targetScale.y,
            transform.forward.z * offsetFactor * targetScale.z
            );
            //target.position = hit.point - offset;
            //target.position = hit.point - transform.forward * offsetFactor * targetScale.x;

            Vector3 targetPos = hit.point - offset;
            //Vector3 targetPos = hit.point - transform.forward * offsetFactor * targetScale.x;
            target.position = Vector3.Lerp(target.position, targetPos, Time.deltaTime * smoothSpeed);

            // ���� ĳ����(ī�޶�)�� Ÿ�� ������Ʈ ���� �Ÿ��� ����ϰ�
            float currentDistance = Vector3.Distance(transform.position, target.position);

            // ���� Ÿ�ٰ� ĳ���� ���� �Ÿ�, ���� Ÿ�ٰ� ĳ���� ���� �Ÿ��� ������ ����ϰ� 
            float distanceRatio = currentDistance / originalDistance;

            // Ÿ�� ������Ʈ�� x, y, z ������ ���� distanceRatio������ �Ҵ�
            targetScale.x = originalScale.x * distanceRatio;
            targetScale.y = originalScale.y * distanceRatio;
            targetScale.z = originalScale.z * distanceRatio;

            // Ÿ�� ������Ʈ�� ������ ���� ���� ������ ���� ����
            target.localScale = targetScale;
        }
    }

    void SetChildLayerRecursion(GameObject go, int layer)
    {
        // �θ� ������Ʈ ���̾� ����
        go.layer = layer;

        // ��� �ڽ� ������Ʈ ���̾� ����
        foreach(Transform child in go.transform)
        {
            SetChildLayerRecursion(child.gameObject, layer);
        }
    }
}
