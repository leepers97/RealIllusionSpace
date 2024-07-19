using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioNode : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;

    // 사운드 재생
    public void Play(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
        StartCoroutine(WaitSound());
    }

    IEnumerator WaitSound()
    {
        // 무명메소드, 람다식 사용
        // 오디오 재생시간동안 대기
        yield return new WaitWhile(() => audioSource.isPlaying);

        // 재생이 끝나면 노드 되돌려놓기
        SoundManager.instance.SetNode(this);
    }
}
