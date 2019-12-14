using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound {
    public string name;
    public AudioClip clip;
    private AudioSource source;
    public float Volum;
    public bool loop;


    public void setSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.loop = loop;
        source.volume = Volum;
    }

   public void Play()
    {
        source.Play();
    }
    public void Stop()
    {
        source.Stop();
    }
    public void setLoop(bool b)
    {
        source.loop = b;
    }
    public void setVolume(float _volume)
    {
        source.volume = _volume;
    }
}


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
    }
    [SerializeField] public Sound[] sounds;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < sounds.Length; i++)
        {
            GameObject soundObj = new GameObject("사운드 파일 이름 :" + i 
                + " =" + sounds[i].name);
            sounds[i].setSource(soundObj.AddComponent<AudioSource>());
            soundObj.transform.SetParent(this.transform);
        }
    }
    public void Play(string _soundName)
    {
        for (int i = 0; i < sounds.Length; i++){
            if(_soundName == sounds[i].name)
            {
                sounds[i].Play();
                return;
            }
        }
    }
    public void Stop(string _soundName)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (_soundName == sounds[i].name)
            {
                sounds[i].Stop();
                return;
            }
        }
    }
    public void SetLoop(string _soundName, bool b)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (_soundName == sounds[i].name)
            {
                sounds[i].setLoop(b);
                return;
            }
        }
    }
    public void SetVolume(string _soundName, float _volume)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (_soundName == sounds[i].name)
            {
                sounds[i].Volum = _volume;
                sounds[i].setVolume(_volume);
                return;
            }
        }
    }
}
