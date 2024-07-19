using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// triggerEnter�ϸ�
// ui�Ŵ������� id�� �Ѱ��ش�

public class UiTrigger_HCH : MonoBehaviour
{
    public int id;
    [SerializeField]
    bool isPrinted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isPrinted == false)
        {
            UIManager.instance.PrintMessage(id);
            isPrinted = true;
        }
    }
}
