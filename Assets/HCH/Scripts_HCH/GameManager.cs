using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Player_HCH player;
    //public Grab_HCH grab;
    public Grab3_HCH grab;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
                buttonAudioSource.PlayOneShot(buttonClickSound);
                Application.Quit();
#endif
        }
    }
}
