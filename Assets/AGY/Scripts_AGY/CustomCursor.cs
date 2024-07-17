using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CustomCursor : MonoBehaviour
{
    // Start is called before the first frame update
    public Image cursorImage; // UI �̹����� Ŀ�� ����
    public RectTransform canvasRectTransform; // Ŀ���� ��ġ�� ĵ����

    void Start()
    {
        Cursor.visible = false; // �ý��� Ŀ�� �����
    }

    void Update()
    {
        Vector2 cursorPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRectTransform,
            Input.mousePosition,
            null,
            out cursorPosition
        );
        cursorImage.rectTransform.anchoredPosition = cursorPosition;
    }
}
