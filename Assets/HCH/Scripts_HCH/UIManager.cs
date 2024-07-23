using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ��ü���� ��ȭâ�� ����

// UI�Ŵ������� public���� �̸� �ؽ�Ʈ�� ���� ������ �ִٰ�
// trigger �ݶ��̴��� �÷��̾ ������ trigger ��ũ��Ʈ���� UIManager�� �Լ��� ȣ���Ͽ� ���ϴ� �ؽ�Ʈ ���

// ��� fade in -> (�ؽ�Ʈ fade in -> �ؽ�Ʈ fade out) �ݺ� -> ��� fade out
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
        // ��� fade in
        yield return StartCoroutine(BackgroundFadeIn());
        // �ش� id�� ��� �ؽ�Ʈ ���
        for (int i = 0; i < dialogdb.Dialogs.Count; i++)
        {
            if (dialogdb.Dialogs[i].id == id)
            {
                // ������ ������� �ؽ�Ʈ �Ҵ�
                txt.text = dialogdb.Dialogs[i].dialog;
                // �ؽ�Ʈ fade in
                yield return StartCoroutine(TextFadeIn());
                // �ؽ�Ʈ �д� ��� �ð�
                yield return textDelay;
                // �ؽ�Ʈ fade out
                yield return StartCoroutine(TextFadeOut());
            }
        }
        // ������ ���� �ؽ�Ʈ�� ���� ��� ���� �̹��� ����
        if (id == 3) EndingImage();
        // ��� fade out
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
            bgColor.a = alpha * (168f / 255f); // ��� ���İ��� 0~168���� ���� (168/255�� ��ȯ)
            txtBG.color = bgColor;
            yield return null;
        }
        bgColor.a = 168f / 255f; // ���� ���İ� ����
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
            bgColor.a = alpha * (168f / 255f); // ��� ���İ��� 168~0���� ���� (168/255�� ��ȯ)
            txtBG.color = bgColor;
            yield return null;
        }
        bgColor.a = 0f; // ���� ���İ� ����
        txtBG.color = bgColor;
    }
}
