using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffGameObjects_HCH : MonoBehaviour
{
    public GameObject[] gameObjects;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
