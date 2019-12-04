using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChoiceManager : MonoBehaviour
{
    static public ChoiceManager instance;
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
    private AudioManager theAudio;
    private string question;
    private List<string> answerList;

    public GameObject go; // 평소에 비활성화 시킬... (setActive)


    public Text questionText;
    public Text[] answerTexts;
    public GameObject[] answerPanel;
    public GameObject questionPanel;

    public Animator anim;

    public string keySound;
    public string enterSound;

    public bool choiceing; // 대기 ()=>! 
    private bool keyInput; // 키처리활성화
    private int count; // 배열의 크기
    private int result = 0; // 선택한 선택창

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);
    
    // Start is called before the first frame update
    void Start()
    {
        go.SetActive(false);
        theAudio = FindObjectOfType<AudioManager>();
        keyInput = false;
        answerList = new List<string>();
        questionText.text = "";
        for(int i = 0; i < answerTexts.Length; i++)
        {
            answerTexts[i].text = "";
            answerPanel[i].SetActive(false);
        }
    }

    public int GetResult()
    {
        return result;
    }

    //public void ShowChoice(Choice _choice)
    //{
    //    go.SetActive(true);
    //    keyInput = true;
    //    choiceing = true;
    //    question = _choice.question;
    //    questionPanel.SetActive(true);
    //    for (int i = 0; i < _choice.answers.Length; i++)
    //    {
    //        answerList.Add(_choice.answers[i]);
    //        answerPanel[i].SetActive(true);
    //        count = i;
    //    }
    //    anim.SetBool("Appear", true);
    //    StartCoroutine(ChoiceCorutine());
    //}
    //IEnumerator ChoiceCorutine()
    //{
    //    yield return new WaitForSeconds(0.2f);
    //    StartCoroutine(TypingQuestion());
    //    for(int i = 0; i <= count; i++)
    //    {
    //        StartCoroutine(TypingAnwer(i));
    //    }
    //}
    //IEnumerator TypingQuestion()
    //{
    //    for(int i = 0; i < question.Length; i++)
    //    {
    //        questionText.text += question[i];
    //        yield return waitTime;
    //    }
    //}
    //IEnumerator TypingAnwer(int choice)
    //{
    //    yield return new WaitForSeconds(0.4f + choice * 0.1f);
    //    for (int i = 0; i < answerList[choice].Length; i++)
    //    {
    //        answerTexts[choice].text += answerList[choice][i];
    //        yield return waitTime;
    //    }
    //}
    //// Update is called once per frame
    //void Update()
    //{
    //    if (keyInput)
    //    {
    //        if (Input.GetKeyDown(KeyCode.UpArrow))
    //        {
    //            theAudio.Play(keySound);
    //            if (result > 0) result--;
    //            else result = count;
    //            Selection();
    //        }
    //        else if (Input.GetKeyDown(KeyCode.DownArrow))
    //        {
    //            theAudio.Play(keySound);
    //            if (result < count) result++;
    //            else result = 0;
    //            Selection();
    //        }
    //        else if (Input.GetKeyDown(KeyCode.Z))
    //        {
    //            theAudio.Play(enterSound);
    //            keyInput = false;
    //            ExitChoice();
    //        }
    //    }
    //}
    //public void Selection() // 선택
    //{
    //    Color color = answerPanel[0].GetComponent<Image>().color;
    //    color.a = 0.75f;
    //    for(int i = 0; i <= count; i++)
    //    {
    //        answerPanel[i].GetComponent<Image>().color = color;
    //    }
    //    color.a = 1.0f;
    //    answerPanel[result].GetComponent<Image>().color = color;
    //}
    //public void ExitChoice()
    //{
    //    for(int i = 0; i <= count; i++)
    //    {
    //        answerTexts[i].text = "";
    //        answerPanel[i].SetActive(false);
    //    }
    //    questionPanel.SetActive(false);
    //    questionText.text = "";
    //    choiceing = false;
    //    answerList.Clear();
    //    anim.SetBool("Appear", false);
        
    //    go.SetActive(false);
    //}
    public void ShowChoice(Choice _choice)
    {
        go.SetActive(true);
        keyInput = true;
        question = _choice.question;
        questionPanel.SetActive(true);
        for(int i = 0; i < _choice.answers.Length; i++)
        {
            answerList.Add(_choice.answers[i]);
            answerPanel[i].SetActive(true);
            count = i;
        }
        choiceing = true;
        anim.SetBool("Appear", true);
        StartCoroutine(ChoiceCorutine());
    }
    IEnumerator ChoiceCorutine()
    {
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(QuestionCorutine());
        for(int i = 0; i <= count; i++)
        {
            StartCoroutine(AnswerCorutine(i));
        }
    }
    IEnumerator QuestionCorutine()
    {
        for(int i = 0; i < question.Length; i++)
        {
            questionText.text += question[i];
            yield return waitTime;
        }
    }
    IEnumerator AnswerCorutine(int choice)
    {
        yield return new WaitForSeconds(0.3f + choice * 0.2f);
        for (int i = 0; i < answerList[choice].Length; i++)
        {
            answerTexts[choice].text += answerList[choice][i];
            yield return waitTime;
        }
    }
    void Update()
    {
        if (keyInput)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                theAudio.Play(keySound);
                if (result > 0) result--;
                else result = count;
                Selection();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                theAudio.Play(keySound);
                if (result < count) result++;
                else result = 0;
                Selection();
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                theAudio.Play(enterSound);
                keyInput = false;
                ExitChoice();
            }
        }
    }
    public void Selection()
    {
        Color color = answerPanel[0].GetComponent<Image>().color;
        color.a = 0.4f;
        for(int i = 0; i<= count; i++)
        {
            answerPanel[i].GetComponent<Image>().color = color;
        }
        color.a = 0.7f;
        answerPanel[result].GetComponent<Image>().color = color;
    }

    public void ExitChoice()
    {
        Debug.Log(result);
        result = 0;
        questionPanel.SetActive(false);
        questionText.text = "";
        for(int i = 0; i <= count; i++)
        {
            answerTexts[i].text = "";
            answerPanel[i].SetActive(false);
        }
        go.SetActive(false);
        answerList.Clear();
        choiceing = false;
        anim.SetBool("Appear",false);
        
    }
}
