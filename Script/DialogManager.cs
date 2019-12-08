using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text text;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer dialogWindowsRenderer;
    public static DialogManager dialogManager;

    private OrderManager theOrder;
    private List<string> listSetence;
    private List<Sprite> listSprite;
    private List<Sprite> listDialogWindows;
    private int dialogCount;
    public Animator animatorSpirte;
    public Animator animatorDialogWindow;
    private AudioManager _audioManager;
    public string typeSound, enterSound;
    public bool talking = false;
    private bool keyActivate = false;
    bool onlyText = false;

    #region Singleton
    private void Awake()
    {
        if (dialogManager == null)
        {
            DontDestroyOnLoad(this.gameObject);
            dialogManager = this;
        }
        else Destroy(this.gameObject);
    }
    #endregion
    void Start()
    {
       text.text = "";
        listSetence = new List<string>();
        listSprite = new List<Sprite>();
        listDialogWindows = new List<Sprite>();
        _audioManager = FindObjectOfType<AudioManager>();
        theOrder = FindObjectOfType<OrderManager>();
        dialogCount = 0;
    }
    public void ShowText(string[] texts)
    {
        theOrder.PlayerDialogDontMove(false);
        onlyText = true;
        talking = true;
        for (int i = 0; i < texts.Length; i++)
        {
            listSetence.Add(texts[i]);
        }
        StartCoroutine(StartOnlyTextCorutine());

    }
    IEnumerator StartOnlyTextCorutine()
    {
        keyActivate = true;
        for (int i = 0; i < listSetence[dialogCount].Length; i++)
        {
            text.text += listSetence[dialogCount][i]; if (i % 6 == 1) _audioManager.Play(typeSound);
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Update is called once per frame
    public void ShowDialog(Dialog dialog)
    {
        theOrder.PlayerDialogDontMove(false);
        onlyText = false;
        talking = true;
        for(int i = 0; i < dialog.sentence.Length; i++)
        {
            listSetence.Add(dialog.sentence[i]);
            listDialogWindows.Add(dialog.dialogWindows[i]);
            listSprite.Add(dialog.objects[i]);
        }
        animatorDialogWindow.SetBool("Appear", true);
        animatorSpirte.SetBool("Appear", true);
        StartCoroutine(StartDialogCorutine());

    }
    IEnumerator StartDialogCorutine()
    {
        if (dialogCount > 0)
        {
            if (listDialogWindows[dialogCount] != listDialogWindows[dialogCount - 1])
            {
                animatorSpirte.SetBool("Change", true);
                animatorDialogWindow.SetBool("Appear", false);
                yield return new WaitForSeconds(0.2f);
                dialogWindowsRenderer.GetComponent<SpriteRenderer>().sprite = listDialogWindows[dialogCount];
                spriteRenderer.GetComponent<SpriteRenderer>().sprite = listSprite[dialogCount];
                animatorDialogWindow.SetBool("Appear", true);
                animatorSpirte.SetBool("Change", false);
            }
            else
            {
                if(listSprite[dialogCount] != listSprite[dialogCount - 1])
                {
                    animatorSpirte.SetBool("Change", true);
                    yield return new WaitForSeconds(0.1f);
                    spriteRenderer.GetComponent<SpriteRenderer>().sprite = listSprite[dialogCount];
                    animatorSpirte.SetBool("Change", false);
                }
                else yield return new WaitForSeconds(0.05f);
                
            }
        }else
        {
            dialogWindowsRenderer.GetComponent<SpriteRenderer>().sprite = listDialogWindows[dialogCount];
            spriteRenderer.GetComponent<SpriteRenderer>().sprite = listSprite[dialogCount];
        }
        keyActivate = true;
        for (int i =0; i < listSetence[dialogCount].Length; i++)
        {
            text.text += listSetence[dialogCount][i]; if (i % 6 == 1) _audioManager.Play(typeSound);
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void ExitDialog()
    {
        dialogCount = 0;
        text.text = "";
        listSetence.Clear();
        listDialogWindows.Clear();
        listSprite.Clear();
        animatorDialogWindow.SetBool("Appear", false);
        talking = false;
        animatorSpirte.SetBool("Appear", false);
        theOrder.PlayerDialogDontMove(true);
    }
    void Update()
    {
        if(talking && keyActivate)
        if (Input.GetKeyDown(KeyCode.Z))
        {
                keyActivate = false;
                _audioManager.Play(enterSound);
                text.text = "";
            dialogCount++;
            if (dialogCount == listSetence.Count)
            {
                StopAllCoroutines();
                ExitDialog();
            }
            else
            {
                StopAllCoroutines();
                    if (onlyText) StartCoroutine(StartOnlyTextCorutine());
                    else StartCoroutine(StartDialogCorutine());
            }
        }
    }
}
