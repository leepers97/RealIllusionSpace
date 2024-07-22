using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 떨어질 때 배경색은 빨간색, 7:59은 검은색
// 시간을 지나가면 배경색은 검은색, 7:59은 빨간색
// 추후 7:59가 8:00이 됨

// 벽 머티리얼을 모두 검은 색으로 바꾸고
// 시간 머티리얼을 모두 빨간색으로 바꾼다

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
