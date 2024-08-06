using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// dissolve value�� 1�̵Ǹ� ����ī�޶��� ������� ����
// ���� �ݶ��̴��� ������ ����ī�޶��� ���� 0���� ���� ���̱�

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
