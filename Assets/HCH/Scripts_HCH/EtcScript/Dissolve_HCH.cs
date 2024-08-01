using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// dissolve 머티리얼의 dissolve값을 dissolveSpeed값에 따라 점점 1로 가게 한다

// 1이 되면 맵을 업데이트 한다
// 안개를 켠다

public class Dissolve_HCH : MonoBehaviour
{
    public Material dissolveMat;

    bool isDissolve = false;
    public float dissolveSpeed = 1f;
    float value = 0;

    public Camera cam;

    public GameObject nextMap;

    public GameObject fogVolume;
    // Start is called before the first frame update
    void Start()
    {
        dissolveMat.SetFloat("_Dissolve", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isDissolve = true;
            cam.clearFlags = CameraClearFlags.SolidColor;
            cam.backgroundColor = Color.white;
        }
        if (isDissolve)
        {
            if(value > 1)
            {
                //value = 0;
                fogVolume.SetActive(true);
                nextMap.SetActive(true);
                isDissolve = false;
            }
            value += dissolveSpeed * Time.deltaTime;
            Dissolve();
        }
    }

    public void Dissolve()
    {
        dissolveMat.SetFloat("_Dissolve", value);
    }
}
