using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioNode : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;

    // ���� ���
    public void Play(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
        StartCoroutine(WaitSound());
    }

    IEnumerator WaitSound()
    {
        // ����޼ҵ�, ���ٽ� ���
        // ����� ����ð����� ���
        yield return new WaitWhile(() => audioSource.isPlaying);

        // ����� ������ ��� �ǵ�������
        SoundManager.instance.SetNode(this);
    }
}
