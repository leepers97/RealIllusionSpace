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
        // �̱��� ����
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        // �̸� ��Ƶ� ���� ���� ��ųʸ��� �ֱ�
        foreach (var soundResource in soundResources)
        {
            soundDB.Add(soundResource.key, soundResource.clip);
        }

        // poolSize��ŭ ��� ����
        for (int i = 0; i < poolSize; i++)
        {
            MakeNode();
        }
    }

    void MakeNode()
    {
        // ��� ������ ���� �� soundPool�� ��� �߰�
        AudioNode audioNode = Instantiate(soundNodePrefab, transform).GetComponent<AudioNode>();
        soundPool.Enqueue(audioNode);
    }

    // Ư�� ��ġ���� ���� ���
    public void PlaySound(string key, Vector3 pos)
    {
        // db�� �Ű������� ���� key�� ���ٸ� �˸�
        if (!soundDB.ContainsKey(key))
        {
            Debug.LogError("There is no key in sound DB " + key);
            return;
        }

        // ��� �������� ���
        AudioNode node = GetNode();
        node.transform.position = pos;
        node.Play(soundDB[key]);
    }

    // Ư�� �θ� ������Ʈ�� �ڽ����� ���� ���
    public void PlaySound(string key, Transform parent)
    {
        // db�� �Ű������� ���� key�� ���ٸ� �˸�
        if (!soundDB.ContainsKey(key))
        {
            Debug.LogError("There is no key in sound DB " + key);
            return;
        }

        // ��� �������� ���
        AudioNode node = GetNode();
        node.transform.SetParent(parent);
        node.transform.localPosition = Vector3.zero;
        node.Play(soundDB[key]);

    }

    // ��� ��������
    AudioNode GetNode()
    {
        // soundPool�� ��尡 ������ ��� ����
        if (soundPool.Count < 1)
        {
            MakeNode();
        }

        // soundPool���� ��� ��������
        AudioNode node = soundPool.Dequeue();
        return node;
    }

    // ��� �ǵ�������
    public void SetNode(AudioNode node)
    {
        // ��� ���� ��� soundPool�� �ǵ�������
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