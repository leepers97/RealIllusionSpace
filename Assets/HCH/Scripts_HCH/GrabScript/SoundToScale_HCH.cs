using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ü�� ũ�⿡ ���� �ٸ� ���带 ����ϰ� �ʹ�
// �����ϰ� ����� 5�̻��̸� ū�Ҹ�
// �����ϰ� ����� 1 ~ 5�� �߰��Ҹ�
// �����ϰ� ����� 1���ϸ� �����Ҹ��� ����Ѵ�

public class SoundToScale_HCH : MonoBehaviour
{
    float upper = 4f;
    float lower = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) return;
        float scaleAvr = (transform.localScale.x + transform.localScale.y + transform.localScale.z) / 3;
        if(scaleAvr > upper)
        {
            SoundManager.instance.PlaySound("Col_Big", this.transform);
        }
        else if (scaleAvr < lower)
        {
            SoundManager.instance.PlaySound("Col_Small", this.transform);
        }
        else
        {
            SoundManager.instance.PlaySound("Col_Middle", this.transform);
        }
    }
}
