using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ºûÀÌ ÇÑ¹ø + Âª°Ô ¼¼¹ø ±ôºýÀÌ°Ô ÇÑ´Ù(¹Ýº¹)

public class LightFlicker_HCH : MonoBehaviour
{
    Light light;

    WaitForSeconds longDelay = new(0.4f);
    WaitForSeconds shortDelay = new(0.1f);
    int flickerCnt = 3;

    AudioSource audioSource;
    [Header("Àü±¸ ±ôºýÀÌ´Â ¼Ò¸®")]
    public AudioClip bulbSound;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        StartCoroutine(LightFlick());
        audioSource = GetComponent<AudioSource>();
    }

    IEnumerator LightFlick()
    {
        while (true)
        {
            yield return shortDelay;
            light.enabled = true;
            if(!audioSource.isPlaying) audioSource.PlayOneShot(bulbSound);
            yield return longDelay;
            for (int i = 0; i < flickerCnt; i++)
            {
                yield return shortDelay;
                light.enabled = false;
                yield return shortDelay;
                light.enabled = true;
            }
        }
    }
}
