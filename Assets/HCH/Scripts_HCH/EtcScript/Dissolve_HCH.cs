using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// dissolve 머티리얼의 dissolve값을 dissolveSpeed값에 따라 점점 1로 가게 한다

public class Dissolve_HCH : MonoBehaviour
{
    public Material dissolveMat;

    bool isDissolve = false;
    public float dissolveSpeed = 1f;
    float value = 0;

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
        }
        if (isDissolve)
        {
            if(value > 1)
            {
                value = 0;
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
