using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1.2�� ������ �������� �Ҹ�

public class PlayerFalling : MonoBehaviour
{
    float airTime = 0;
    public float jumpTime = 0.8f;

    public GameObject groundCheckObj;
    GroundCheck groundCheck;

    AudioSource audioSource;
    [Header("�������� �Ҹ�")]
    public AudioClip fallingSound;

    // Start is called before the first frame update
    void Start()
    {
        groundCheck = groundCheckObj.GetComponent<GroundCheck>();
        audioSource = GameManager.instance.player.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        print(airTime);
        if (!groundCheck.isGrounded)
        {
            airTime += Time.deltaTime;
            if(airTime >= jumpTime)
            {
                if(!audioSource.isPlaying) audioSource.PlayOneShot(fallingSound);
            }
        }
        else
        {
            airTime = 0;
            audioSource.Stop();
        }
    }
}
