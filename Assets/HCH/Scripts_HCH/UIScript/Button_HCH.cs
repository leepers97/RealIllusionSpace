using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

// Ŀ���� �ؽ�Ʈ�� ������ ��� ������ ���� �ϴû����� ���ϰ� �Ѵ�.
// Ŀ���� ���� �ٽ� �� ����� ���ϰ� �Ѵ�.
// r�� : 165

public class Button_HCH : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Text txt;
    public Color originColor;
    public Color pointerColor;

    AudioSource buttonAudioSource;
    [Header("��ư Ŭ�� �Ҹ�")]
    public AudioClip buttonClickSound;

    void Start()
    {
        txt = GetComponentInChildren<Text>();
        txt.color = originColor;

        buttonAudioSource = GetComponent<AudioSource>();
    }

    // Ŀ�� ������ ���� �� �ϴû����� ����
    public void OnPointerEnter(PointerEventData eventData)
    {
        txt.color = pointerColor;
    }

    // Ŀ�� ���� �������� ����
    public void OnPointerExit(PointerEventData eventData)
    {
        txt.color = originColor;
    }

    // ���� ���� ��ư
    // ���� �׽�Ʈ�� �� ������
    public void OnClickGameStart()
    {
        buttonAudioSource.PlayOneShot(buttonClickSound);
        SceneManager.LoadScene("Map_HCH");
    }

    // ���� ���� ��ư
    public void OnClickGameQuit()
    {
        #if UNITY_EDITOR
                buttonAudioSource.PlayOneShot(buttonClickSound);
                UnityEditor.EditorApplication.isPlaying = false;
#else
                buttonAudioSource.PlayOneShot(buttonClickSound);
                Application.Quit();
#endif
    }
}
