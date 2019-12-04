using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public ParticleSystem rain;
    public string rainSound;
    private AudioManager theAudio;
    static public WeatherManager instance;
    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else Destroy(this.gameObject);

    }// Start is called before the first frame update
    #endregion
    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
    }
    public void Rain()
    {
        theAudio.Play(rainSound);
        rain.Play();
    }
    public void RainStop()
    {
        theAudio.Stop(rainSound);
        rain.Stop();
    }
    public void RainDrop()
    {
        StartCoroutine(RainDropCorutine());
    }
    IEnumerator RainDropCorutine()
    {
        for (int i = 1; i <= 10; i++) {
            rain.Emit(i * 10);
            yield return new WaitForSeconds(0.4f);
         }
        Rain();
    }
}
