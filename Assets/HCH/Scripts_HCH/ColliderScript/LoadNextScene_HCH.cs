using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextScene_HCH : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UIManager.instance.LoadNextScene();
        }
    }
}
