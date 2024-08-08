using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 물체의 크기에 따라 다른 사운드를 출력하고 싶다
// 스케일값 평균이 5이상이면 큰소리
// 스케일값 평균이 1 ~ 5면 중간소리
// 스케일값 평균이 1이하면 작은소리를 출력한다

public class SoundToScale_HCH : MonoBehaviour
{
    float upper = 3f;
    float lower = 1.5f;

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
