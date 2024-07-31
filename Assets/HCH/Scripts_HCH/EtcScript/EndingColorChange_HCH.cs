using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ �� ������ ������, 7:59�� ������
// �ð��� �������� ������ ������, 7:59�� ������
// ���� 7:59�� 8:00�� ��

// �� ��Ƽ������ ��� ���� ������ �ٲٰ�
// �ð� ��Ƽ������ ��� ���������� �ٲ۴�

// 7:59�� 5�� �����̰� 8:00�� �ȴ�

public class EndingColorChange_HCH : MonoBehaviour
{
    public Material blackMat;
    public Material redMat;

    public GameObject[] walls;
    public GameObject seven;
    public GameObject eight;

    Renderer[] sevenRends;

    public Camera cam;
    string targetLayerName = "Ending";

    WaitForSeconds flickerDelay = new WaitForSeconds(1f);
    WaitForSeconds delay = new WaitForSeconds(4.25f);

    // Start is called before the first frame update
    void Start()
    {
        sevenRends = seven.GetComponentsInChildren<Renderer>();
    }

    IEnumerator EndingFlicker(int flickerCount)
    {
        yield return delay;
        for(int i = 0; i < flickerCount; i++)
        {
            yield return flickerDelay;
            seven.SetActive(false);
            yield return flickerDelay;
            seven.SetActive(true);
        }
        yield return delay;
        eight.SetActive(true);
        seven.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cam.clearFlags = CameraClearFlags.SolidColor;
            cam.backgroundColor = Color.black;
            cam.cullingMask = 1 << LayerMask.NameToLayer(targetLayerName);
            foreach (Renderer rend in sevenRends)
            {
                rend.material = redMat;
            }
            StartCoroutine(EndingFlicker(5));
        }
    }
}
