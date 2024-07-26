using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffGameObjects_HCH : MonoBehaviour
{
    public GameObject[] gameObjects_Off;
    public GameObject[] gameObjects_On;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (GameObject gameObject in gameObjects_On)
            {
                gameObject.SetActive(true);
            }
            foreach (GameObject gameObject in gameObjects_Off)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
