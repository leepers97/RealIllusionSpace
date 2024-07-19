using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioData[] soundResources;
    Dictionary<string, AudioClip> soundDB = new Dictionary<string, AudioClip>();

    public int poolSize;
    public GameObject soundNodePrefab;
    Queue<AudioNode> soundPool = new Queue<AudioNode>();

    // Start is called before the first frame update
    void Awake()
    {
        // 싱글턴 선언
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        // 미리 담아둔 사운드 파일 딕셔너리에 넣기
        foreach (var soundResource in soundResources)
        {
            soundDB.Add(soundResource.key, soundResource.clip);
        }

        // poolSize만큼 노드 생성
        for (int i = 0; i < poolSize; i++)
        {
            MakeNode();
        }
    }

    void MakeNode()
    {
        // 노드 프리팹 생성 후 soundPool에 노드 추가
        AudioNode audioNode = Instantiate(soundNodePrefab, transform).GetComponent<AudioNode>();
        soundPool.Enqueue(audioNode);
    }

    // 특정 위치에서 사운드 재생
    public void PlaySound(string key, Vector3 pos)
    {
        // db에 매개변수로 받은 key가 없다면 알림
        if (!soundDB.ContainsKey(key))
        {
            Debug.LogError("There is no key in sound DB " + key);
            return;
        }

        // 노드 가져오고 재생
        AudioNode node = GetNode();
        node.transform.position = pos;
        node.Play(soundDB[key]);
    }

    // 특정 부모 오브젝트의 자식으로 사운드 재생
    public void PlaySound(string key, Transform parent)
    {
        // db에 매개변수로 받은 key가 없다면 알림
        if (!soundDB.ContainsKey(key))
        {
            Debug.LogError("There is no key in sound DB " + key);
            return;
        }

        // 노드 가져오고 재생
        AudioNode node = GetNode();
        node.transform.SetParent(parent);
        node.transform.localPosition = Vector3.zero;
        node.Play(soundDB[key]);

    }

    // 노드 가져오기
    AudioNode GetNode()
    {
        // soundPool에 노드가 없으면 노드 생성
        if (soundPool.Count < 1)
        {
            MakeNode();
        }

        // soundPool에서 노드 가져오기
        AudioNode node = soundPool.Dequeue();
        return node;
    }

    // 노드 되돌려놓기
    public void SetNode(AudioNode node)
    {
        // 사용 끝난 노드 soundPool에 되돌려놓기
        node.transform.SetParent(transform);
        soundPool.Enqueue(node);
    }

    [Serializable]
    public class AudioData
    {
        public string key;
        public AudioClip clip;
    }
}