using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ �� ������ ������, 7:59�� ������
// �ð��� �������� ������ ������, 7:59�� ������
// ���� 7:59�� 8:00�� ��

// �� ��Ƽ������ ��� ���� ������ �ٲٰ�
// �ð� ��Ƽ������ ��� ���������� �ٲ۴�

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

    // Start is called before the first frame update
    void Start()
    {
        sevenRends = seven.GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //foreach(GameObject wall in walls)
            //{
            //    Renderer wallRend = wall.GetComponent<Renderer>();
            //    wallRend.material = blackMat;
            //}
            cam.clearFlags = CameraClearFlags.SolidColor;
            cam.backgroundColor = Color.black;
            cam.cullingMask = 1 << LayerMask.NameToLayer(targetLayerName);
            foreach (Renderer rend in sevenRends)
            {
                rend.material = redMat;
            }
        }
    }
}
