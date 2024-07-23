using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 전체적인 대화창을 관리

// UI매니저에서 public으로 미리 텍스트를 전부 가지고 있다가
// trigger 콜라이더에 플레이어가 닿으면 trigger 스크립트에서 UIManager의 함수를 호출하여 원하는 텍스트 출력

// 배경 fade in -> (텍스트 fade in -> 텍스트 fade out) 반복 -> 배경 fade out
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public DialogDB dialogdb;

    public Image txtBG;
    Text txt;

    WaitForSeconds textDelay = new WaitForSeconds(3f);
    public float fadeDuration = 1.5f;

    public GameObject ending;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        txt = txtBG.GetComponentInChildren<Text>();
    }

    public void PrintMessage(int id)
    {
        StartCoroutine(TextChange(id));
    }

    public void EndingImage()
    {
        ending.SetActive(true);
    }

    IEnumerator TextChange(int id)
    {
        // 배경 fade in
        yield return StartCoroutine(BackgroundFadeIn());
        // 해당 id의 모든 텍스트 출력
        for (int i = 0; i < dialogdb.Dialogs.Count; i++)
        {
            if (dialogdb.Dialogs[i].id == id)
            {
                // 엑셀에 적어놓은 텍스트 할당
                txt.text = dialogdb.Dialogs[i].dialog;
                // 텍스트 fade in
                yield return StartCoroutine(TextFadeIn());
                // 텍스트 읽는 대기 시간
                yield return textDelay;
                // 텍스트 fade out
                yield return StartCoroutine(TextFadeOut());
            }
        }
        // 마지막 엔딩 텍스트가 끝날 경우 엔딩 이미지 띄우기
        if (id == 3) EndingImage();
        // 배경 fade out
        yield return StartCoroutine(BackgroundFadeOut());
    }

    private IEnumerator TextFadeIn()
    {
        float elapsedTime = 0f;
        Color color = txt.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            txt.color = color;
            yield return null;
        }
    }

    private IEnumerator TextFadeOut()
    {
        float elapsedTime = 0f;
        Color color = txt.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
            txt.color = color;
            yield return null;
        }
    }

    private IEnumerator BackgroundFadeIn()
    {
        float elapsedTime = 0f;
        Color bgColor = txtBG.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            bgColor.a = alpha * (168f / 255f); // 배경 알파값을 0~168으로 설정 (168/255로 변환)
            txtBG.color = bgColor;
            yield return null;
        }
        bgColor.a = 168f / 255f; // 최종 알파값 설정
        txtBG.color = bgColor;
    }

    private IEnumerator BackgroundFadeOut()
    {
        float elapsedTime = 0f;
        Color bgColor = txtBG.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
            bgColor.a = alpha * (168f / 255f); // 배경 알파값을 168~0으로 설정 (168/255로 변환)
            txtBG.color = bgColor;
            yield return null;
        }
        bgColor.a = 0f; // 최종 알파값 설정
        txtBG.color = bgColor;
    }
}
