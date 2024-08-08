using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundStarter_HCH : MonoBehaviour
{
    public GameObject soundObj;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = soundObj.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.enabled = true;
        }
    }
}
