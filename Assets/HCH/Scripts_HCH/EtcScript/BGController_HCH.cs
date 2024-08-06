using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// dissolve value가 1이되면 메인카메라의 배경음악 실행
// 엔딩 콜라이더에 닿으면 메인카메라의 볼륨 0으로 점차 줄이기

public class BGController_HCH : MonoBehaviour
{
    public GameObject cam;

    AudioSource audioSource;
    public float dampSpeed = 0.1f;

    public bool isEnding;
    [SerializeField]
    bool dampStart = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = cam.GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (isEnding && dampStart) audioSource.volume -= dampSpeed * Time.deltaTime;
    }

    public void BGStart()
    {
        audioSource.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dampStart = true;

        }
    }
}
