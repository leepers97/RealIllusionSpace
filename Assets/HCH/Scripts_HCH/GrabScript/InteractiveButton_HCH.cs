using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어나 상호작용이 가능한 오브젝트와 충돌 시 버튼이 눌리고
// 연결되어있는 문이 열린다
// 비상구 표시도 바뀐다

public class InteractiveButton_HCH : MonoBehaviour
{
    public GameObject door;

    public float buttonPressSpeed = 5f;

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
        if(collision.gameObject.layer == 9 || collision.gameObject.CompareTag("Player"))
        {
            door.transform.Translate(Vector3.left);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 9 || collision.gameObject.CompareTag("Player"))
        {

        }
    }
}
