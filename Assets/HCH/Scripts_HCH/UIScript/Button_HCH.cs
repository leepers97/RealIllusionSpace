using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

// 커서를 텍스트에 가져다 대면 색깔이 점점 하늘색으로 변하게 한다.
// 커서를 떼면 다시 원 색깔로 변하게 한다.
// r값 : 165

public class Button_HCH : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Text txt;
    public Color originColor;
    public Color pointerColor;

    AudioSource buttonAudioSource;
    [Header("버튼 클릭 소리")]
    public AudioClip buttonClickSound;

    void Start()
    {
        txt = GetComponentInChildren<Text>();
        txt.color = originColor;

        buttonAudioSource = GetComponent<AudioSource>();
    }

    // 커서 가져다 댔을 때 하늘색으로 변경
    public void OnPointerEnter(PointerEventData eventData)
    {
        txt.color = pointerColor;
    }

    // 커서 떼면 원색으로 변경
    public void OnPointerExit(PointerEventData eventData)
    {
        txt.color = originColor;
    }

    // 게임 시작 버튼
    // 현재 테스트용 씬 적용중
    public void OnClickGameStart()
    {
        buttonAudioSource.PlayOneShot(buttonClickSound);
        SceneManager.LoadScene("Map_HCH");
    }

    // 게임 종료 버튼
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
