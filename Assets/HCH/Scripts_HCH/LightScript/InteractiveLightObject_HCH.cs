using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ���� range, intensity���� ��ü�� scale���� ����Ѵ�. 
// �� ������Ʈ�� Ư�� ���� ������Ʈ(ex: ��)�� ������ �����ٴ�� ������ �����Ѵ�.
// ���� range, intensity ���� ������� �̻��̾�� ������ �����Ѵ�.
public class InteractiveLightObject_HCH : MonoBehaviour
{
    Vector3 originalScale;

    Light light;
    public float lightRangePower = 3;
    public float lightIntensityPower = 3;
    public float lightLimit;

    public float lineSize = 3;
    public float scanRange;
    public Collider[] hitInfo;
    public LayerMask targetLayer;
    HashSet<Collider> previousDetectedObjects = new HashSet<Collider>();


    // Start is called before the first frame update
    void Start()
    {
        light = GetComponentInChildren<Light>();
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // ������ ���� ���
        Vector3 currentScale = transform.localScale;
        float scaleMultiplier = currentScale.x / originalScale.x;

        // ������ ������ ���� ���� ����, ���� ����
        light.range = scaleMultiplier * lightRangePower;
        light.intensity = scaleMultiplier * lightIntensityPower;

        // ��ó ������Ʈ Ž��(������ -> �ϴ� �� ����, ���� ���������ε� ���)
        //DetectTransparent();
    }

    // ��� ����
    void DetectTransparent()
    {
        scanRange = light.range;
        hitInfo = Physics.OverlapSphere(transform.position, scanRange, targetLayer);
        HashSet<Collider> currentDetectedObjects = new HashSet<Collider>(hitInfo);
        // ���Ӱ� ������ ������Ʈ ó��
        foreach (Collider hit in hitInfo)
        {
            if (!previousDetectedObjects.Contains(hit))
            {
                // ���� ����� ���߿��� scanRange < lightLimit �ȸ���
                if (scanRange < lightLimit) return;
                hit.GetComponent<MeshRenderer>().enabled = false;
            }
        }

        // �������� ������Ʈ ó��
        foreach (Collider hit in previousDetectedObjects)
        {
            if (!currentDetectedObjects.Contains(hit))
            {
                hit.GetComponent<MeshRenderer>().enabled = true;
            }
        }

        // ���� ������ ������Ʈ�� ó�� 
        previousDetectedObjects = currentDetectedObjects;

    }

    void OnDrawGizmos()
    {
        // Ž�� ���� ����� ǥ��
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, scanRange);
    }
}
