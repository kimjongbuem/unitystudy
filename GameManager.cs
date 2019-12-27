using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Bound[] bounds;
    private PlayManager thePlayer;
    private CameraManager theCamera;
    SaveNLoad saveNload;
    private FadeManager theFade;
    private MenuManager theMenu;
    private DialogManager dialogManager;
    // Start is called before the first frame update
    //public void Start()
    //{
    //    saveNload = FindObjectOfType<SaveNLoad>();
    //    saveNload.CallLoad();
    //}
    public void LoadStart()
    {
        StartCoroutine(LoadWaitCorutine());
    }
    IEnumerator LoadWaitCorutine()
    {
        yield return new WaitForSeconds(0.5f);
        thePlayer = FindObjectOfType<PlayManager>();
        bounds = FindObjectsOfType<Bound>();
        theCamera = FindObjectOfType<CameraManager>();
        theFade = FindObjectOfType<FadeManager>();
        theCamera.target = GameObject.Find("Player");
        for(int i = 0; i < bounds.Length; i++)
        {
            if(bounds[i].boundName == thePlayer.currentMapName)
            {
                bounds[i].SetBound();
                break;
            }
        }
        theFade.FadeIn();
    }
}
