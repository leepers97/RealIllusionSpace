using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// dissolve ��Ƽ������ dissolve���� dissolveSpeed���� ���� ���� 1�� ���� �Ѵ�

// 1�� �Ǹ� ���� ������Ʈ �Ѵ�
// �Ȱ��� �Ҵ�

// ������ ���۽� ������ ��� ����
// 0.8 ~ 1.0 ���� ���� ���̱�
// 0.9 �� ���� ��� ����
public class Dissolve_HCH : MonoBehaviour
{
    public Material dissolveMat;

    bool isDissolve = false;
    public float dissolveSpeed = 1f;
    float value = 0;

    public Camera cam;

    public GameObject nextMap;

    public GameObject fogVolume;

    AudioSource audioSource;
    public AudioClip dissorveBG;
    bool isPlayed = false;

    BGController_HCH bgController;
    // Start is called before the first frame update
    void Start()
    {
        dissolveMat.SetFloat("_Dissolve", 0);
        audioSource = GetComponent<AudioSource>();
        bgController = GetComponent<BGController_HCH>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDissolve)
        {
            if(value > 1)
            {
                //value = 0;
                fogVolume.SetActive(true);
                nextMap.SetActive(true);
                isDissolve = false;
                bgController.BGStart();
            }
            value += dissolveSpeed * Time.deltaTime;
            if (!isPlayed)
            {
                audioSource.PlayOneShot(dissorveBG);
                isPlayed = true;
            }
            //if(value > 0.8f)
            //{
            //    audioSource.volume -= dampSpeed * Time.deltaTime;
            //}
            Dissolve();
        }
    }

    public void Dissolve()
    {
        dissolveMat.SetFloat("_Dissolve", value);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isDissolve = true;
            cam.clearFlags = CameraClearFlags.SolidColor;
            cam.backgroundColor = Color.white;
        }
    }
}
