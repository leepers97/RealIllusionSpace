using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager_HCH : MonoBehaviour
{
    public GameObject player;
    // �׽�Ʈ�� �ڷ���Ʈ
    public Transform[] TelePos;

    // ���� �׽�Ʈ ������
    public GameObject lightSign;
    public bool testLighting = false;
    public Light directionalLight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �׽�Ʈ�� �ڷ���Ʈ
        TestTeleport();

        // LightPlace�� ��쿡�� ����
        if (testLighting)
        {
            directionalLight.enabled = false;
            RenderSettings.ambientIntensity = 0;
            RenderSettings.reflectionIntensity = 0;
            //lightSign.SetActive(true);
        }
        else
        {
            directionalLight.enabled = true;
            RenderSettings.ambientIntensity = 1;
            RenderSettings.reflectionIntensity = 1;
            //lightSign.SetActive(false);
        }
    }

    void TestTeleport()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            player.transform.position = TelePos[0].position;
            testLighting = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            player.transform.position = TelePos[1].position;
            testLighting = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            player.transform.position = TelePos[2].position;
            testLighting = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            player.transform.position = TelePos[3].position;
            testLighting = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            player.transform.position = TelePos[4].position;
            testLighting = false;
        }
    }
}
