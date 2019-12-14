using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OkOrCancel : MonoBehaviour
{
    public GameObject up_Panel, down_Panel;
    public Text upText, downText;
    //public GameObject go;
    private AudioManager theAudio;
    public string key_sound, cancel_sound, enter_sound;
    private bool keyInput = false, result; 
        
       public bool activate; bool first = true;

    private WaitForSeconds waitTime = new WaitForSeconds(0.015f);
    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        result = true;
    }
    public bool GetResult()
    {
        return result;
    }
    public void ShowTwoChoice(string _up, string _down)
    {
        activate = true;
        result = true;
        upText.text = _up; downText.text = _down;
        up_Panel.SetActive(true);
        down_Panel.SetActive(false);
        StartCoroutine(ShowTwoChoiceCorutine());
    }
    IEnumerator ShowTwoChoiceCorutine()
    {
        yield return new WaitForSeconds(0.02f);
        keyInput = true;
        
    }
    // Update is called once per frame
    void Update()
    {
        if (keyInput)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                result = !result;
                StartCoroutine(ChoiceCorutine());
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                theAudio.Play(enter_sound);
                activate = false;
                keyInput = false;
                first = true;
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                theAudio.Play(cancel_sound);
                activate = false;
                keyInput = false;
                result = false; first = true;
            }
            else if(first)
            {
                first = false;
                StartCoroutine(ChoiceCorutine());
            }
        }
    }
    IEnumerator UseChoicing()
    {
        up_Panel.SetActive(true);
        down_Panel.SetActive(false);
        while (result)
        {
            Color color = up_Panel.GetComponent<Image>().color;
            color.a = 0.0f;
            while (color.a < 0.65f)
            {
                color.a += 0.03f;
                up_Panel.GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a >= 0.0f)
            {
                color.a -= 0.03f;
                up_Panel.GetComponent<Image>().color = color;
                yield return waitTime;
            }
        }
    }
    IEnumerator UnUseChoicing()
    {
        up_Panel.SetActive(false);
        down_Panel.SetActive(true);
        while (!result)
        {
            Color color = down_Panel.GetComponent<Image>().color;
            color.a = 0.0f;
            while (color.a < 0.65f)
            {
                color.a += 0.03f;
                down_Panel.GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a >= 0.0f)
            {
                color.a -= 0.03f;
                down_Panel.GetComponent<Image>().color = color;
                yield return waitTime;
            }
        }
    }
    IEnumerator ChoiceCorutine()
    {
        theAudio.Play(key_sound);

        

        yield return waitTime; StopAllCoroutines();
        if (result) StartCoroutine(UseChoicing());
        else StartCoroutine(UnUseChoicing());
    }
}


