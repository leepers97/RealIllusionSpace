using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 이 스크립트가 달린 오브젝트와 target오브젝트의 scale값을 같게 한다
// 연결되어있는 두 포탈의 각도를 같게 한다
// 포탈의 x축은 고정시킨다
public class SameScale_HCH : MonoBehaviour
{
    public GameObject target;
    public GameObject anotherPortal;

    // Update is called once per frame
    void Update()
    {
        // 포탈 크기에 따라 반대쪽 맵 자체가 커지도록 조절
        target.transform.localScale = gameObject.transform.localScale;

        // 두 포탈의 회전값 동기화
        anotherPortal.transform.rotation = gameObject.transform.rotation;

        // 현재 회전 값 가져오기
        Vector3 currentRotation = transform.rotation.eulerAngles;
        // x, z 축 값을 0으로 고정
        currentRotation.x = 0;
        currentRotation.z = 0;
        // 오브젝트의 회전 값 업데이트
        transform.rotation = Quaternion.Euler(currentRotation);
    }
}
