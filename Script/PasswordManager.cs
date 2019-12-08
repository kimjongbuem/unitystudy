using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PasswordManager : MonoBehaviour
{
    private OrderManager theOrder;
    private AudioManager theAudio;

    public string key_sound, enter_sound, cancel_sound, correct_sound; // 사운드목록

    private int arrayIndex = 0; // 배열크기
    private int selectedTextBox; // 선택된 자릿수/// 
    private int result, correctNumber; // 플레이어가 도출한 정답 그리고 진짜 정답

    public GameObject passwordObject;
    public Text[] passwordText;
    public GameObject[] password_panels;

    public Animator anim;
    public bool activated; // return new waitUntil.. 패스워드 다눌러질때까지 대기..

    private bool keyInput; // 항상 키처리 활성화 비활성화
    private bool correctFlag; // 정답인가? 아닌가?
    static public PasswordManager instance;
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
    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        theOrder = FindObjectOfType<OrderManager>();
        keyInput = false;
        correctFlag = false;
    }
    public void ShowPassword(int _correctNumber)
    {
        theOrder.PlayerDialogDontMove(false);
        passwordObject.SetActive(true);
        correctNumber = _correctNumber;
        activated = true;
        correctFlag = false;
        keyInput = true;
        selectedTextBox = 0;
        
        string temp = correctNumber.ToString();
        for(int i =0; i < temp.Length; i++)
        {
            password_panels[i].SetActive(true);
            passwordText[i].text = "0";
            arrayIndex = i;
        }
         SetColor();
        passwordObject.transform.position = new Vector3(passwordObject.transform.position.x + arrayIndex * 60, 
            passwordObject.transform.position.y, passwordObject.transform.position.z);
        result = 0;
        anim.SetBool("Appear", true);
    }

    public bool GetResult()
    {
        return correctFlag;
    }

    public void SetNumber(string _arrow)
    {
        int temp = int.Parse(passwordText[selectedTextBox].text);

        if (_arrow == "DOWN")
        {
            if (temp == 0) temp = 9;
            else temp--;
        }
        else if(_arrow == "UP")
        {
            if (temp == 9) temp = 0;
            else temp++;
        }
        passwordText[selectedTextBox].text = temp.ToString();
    }

    void Update()
    {
        if (keyInput)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow)) // 9 8 7 6 ..
            {
                theAudio.Play(key_sound);
                SetNumber("DOWN");
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow)) // 9 8 7 6 ..
            {
                theAudio.Play(key_sound);
                SetNumber("UP");
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                theAudio.Play(key_sound);
                if (selectedTextBox == arrayIndex) selectedTextBox = 0;
                else selectedTextBox++;
                SetColor();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                theAudio.Play(key_sound);
                if (selectedTextBox == 0) selectedTextBox = arrayIndex;
                else selectedTextBox--;
                SetColor();
            }
            else if (Input.GetKeyDown(KeyCode.Z)) // 결정키
            {
                keyInput = false;
                StartCoroutine(OXCorutine());
            }
            else if (Input.GetKeyDown(KeyCode.X)) //취소키
            {
                theAudio.Play(key_sound);
                keyInput = false;
                StartCoroutine(ExitCorutine());
            }
        }
    }
    public void SetColor()
    {
        Color color = passwordText[0].color;
        color.a = 0.3f;
        for(int i = 0; i <= arrayIndex; i++)
        {
            passwordText[i].color = color;
        }
        color.a = 1.0f;
        passwordText[selectedTextBox].color = color;
    }
 
    IEnumerator OXCorutine()
    {
        Color color = passwordText[0].color;
        color.a = 1.0f;
        string tempStringNumber="";
        for (int i = arrayIndex; i >= 0; i--)
        {
            passwordText[i].color = color;
            tempStringNumber += passwordText[i].text;
        }
        yield return new WaitForSeconds(1f); 

        result = int.Parse(tempStringNumber);

        if (result == correctNumber)
        {
            theAudio.Play(correct_sound);
            yield return new WaitForSeconds(0.9f);
            theAudio.Stop(correct_sound);
            correctFlag = true;
        }
        else theAudio.Play(cancel_sound);

        StartCoroutine(ExitCorutine());
    }
    IEnumerator ExitCorutine()
    {
        result = 0;
        anim.SetBool("Appear", false);
       
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i <= arrayIndex; i++)
        {
            password_panels[i].SetActive(false);
            passwordText[i].text = "";
        }
        
        passwordObject.transform.position = new Vector3(passwordObject.transform.position.x - arrayIndex * 60,
            passwordObject.transform.position.y, passwordObject.transform.position.z);
        passwordObject.SetActive(false);
        activated = false;
        keyInput = false;
        theOrder.PlayerDialogDontMove(true);
    }
}
