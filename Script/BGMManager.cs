using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    static public BGMManager instance;
    public AudioClip[] clip;
    private AudioSource _source;
    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    private void Play()
    {
        _source.Play();
    }
    private void Stop()
    {
        _source.Stop();
    }
    private void FadeInOutMusic(bool isIn)
    {
        StopAllCoroutines();
        if (isIn) StartCoroutine(FadeOutMusicCorutine());
        else StartCoroutine(FadeInMusicCorutine());
    }
    IEnumerator FadeOutMusicCorutine()
    {
        for (float i = 1.0f; i >= 0; i -= 0.001f)
        {
            _source.volume = i;
            yield return waitTime;
        }
    }
    IEnumerator FadeInMusicCorutine()
    {
        for (float i = 0.0f; i <= 1.0f; i += 0.001f)
        {
            _source.volume = i;
            yield return waitTime;
        }
    }
}
