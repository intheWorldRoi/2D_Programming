using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // �̱�������
    AudioSource audioSource;
    AudioClip nowClip; // ���� ������� �����Ŭ��
    // Start is called before the first frame update

    private void Awake()
    {
        
        if(instance == null)
        {
            instance = this; 
            DontDestroyOnLoad(instance);  // �̱�������..
        }
    }
    
    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject g = new GameObject(sfxName + "Sound"); //ȿ������ ����ϴ� ������Ʈ ����
        audioSource = g.AddComponent<AudioSource>(); // ������Ʈ�� ������ҽ� ������Ʈ ����
        audioSource.clip = clip; //������Ʈ Ŭ�� ����
        nowClip = clip; // ���� ��� Ŭ�� ����
        audioSource.Play(); // ���

        Destroy(g, clip.length); // Ŭ�� ���̸�ŭ ����ϰ� ����
    }

    public void Stop()
    {
        if(nowClip != null)
        {
            audioSource.Stop(); // ��� ����
        }
        
    }

    public bool Playing()
    {
        return audioSource.isPlaying; // ��������� ��ȯ
    }
}
