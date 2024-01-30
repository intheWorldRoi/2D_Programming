using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // 싱글톤패턴
    AudioSource audioSource;
    AudioClip nowClip; // 현재 재생중인 오디오클립
    // Start is called before the first frame update

    private void Awake()
    {
        
        if(instance == null)
        {
            instance = this; 
            DontDestroyOnLoad(instance);  // 싱글톤패턴..
        }
    }
    
    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject g = new GameObject(sfxName + "Sound"); //효과음을 재생하는 오브젝트 생성
        audioSource = g.AddComponent<AudioSource>(); // 오브젝트에 오디오소스 컴포넌트 붙임
        audioSource.clip = clip; //오브젝트 클립 지정
        nowClip = clip; // 현재 재생 클립 지정
        audioSource.Play(); // 재생

        Destroy(g, clip.length); // 클립 길이만큼 재생하고 끝남
    }

    public void Stop()
    {
        if(nowClip != null)
        {
            audioSource.Stop(); // 재생 멈춤
        }
        
    }

    public bool Playing()
    {
        return audioSource.isPlaying; // 재생중인지 반환
    }
}
