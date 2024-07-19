using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// triggerEnter하면
// ui매니저에게 id를 넘겨준다

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
