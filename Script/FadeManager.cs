using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    static public FadeManager instance;
    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else Destroy(this.gameObject);
    }
    #endregion
    public SpriteRenderer white, black;
    private Color color; private WaitForSeconds waitTime = new WaitForSeconds(0.01f);
    public void FadeOut(float speed=0.02f)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutCorutine(speed));
    }
    IEnumerator FadeOutCorutine(float speed = 0.02f)
    {
        color = black.color;
        while (color.a < 1.0f)
        {
            color.a += speed;
            black.color = color;
            yield return waitTime;
        }
    }
    public void FadeIn(float speed = 0.02f)
    {
        StopAllCoroutines();
        StartCoroutine(FadeInCorutine(speed));
    }
    IEnumerator FadeInCorutine(float speed = 0.02f)
    {
        color = black.color;
        while (color.a > 0f)
        {
            color.a -= speed;
            black.color = color;
            yield return waitTime;
        }
    }

    public void FlashOut(float speed = 0.02f)
    {
        StopAllCoroutines();
        StartCoroutine(FlashOutCorutine(speed));
    }
    IEnumerator FlashOutCorutine(float speed = 0.02f)
    {
        color = white.color;
        while (color.a < 1.0f)
        {
            color.a += speed;
            white.color = color;
            yield return waitTime;
        }
    }
    public void FlashIn(float speed = 0.02f)
    {
        StopAllCoroutines();
        StartCoroutine(FlashInCorutine(speed));
    }
    IEnumerator FlashInCorutine(float speed = 0.02f)
    {
        color = white.color;
        while (color.a > 0f)
        {
            color.a -= speed;
            white.color = color;
            yield return waitTime;
        }
    }
    public void Flash(float speed = 0.1f)
    {
        StopAllCoroutines();
        StartCoroutine(FlashCorutine(speed));
    }
    IEnumerator FlashCorutine(float speed)
    {
        color = white.color;
        while (color.a < 1.0f)
        {
            color.a += speed;
            white.color = color;
            yield return waitTime;
        }
        while (color.a > 0f)
        {
            color.a -= speed;
            white.color = color;
            yield return waitTime;
        }
    }
}
