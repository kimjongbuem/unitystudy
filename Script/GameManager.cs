using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GAME_STATE{
    TITLE = 100, PREV_WINNER_TURN, RREV_LOSER_TURN, MIXING,SUFFLEING,FLIP, FUK
}


public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    public bool isGameStart = false;
    #region Singleton
    private void Awake()
    {
        if (GM == null)
        {
            GM = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
    }
    #endregion
    const int TITLE = 0, IN_GAME = 1;
    public GameObject[] maps; // map[0] title, map[1] game ..

    private CameraManager theCamera;
    public static bool isSetted = false;
    // Start is called before the first frame update
    void Start()
    {
        theCamera = FindObjectOfType<CameraManager>();
        theCamera.SetCamera(maps[0]);
        isSetted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (TitleManager.isStart)
        {
            if (!isSetted)
            {
                theCamera.SetCamera(maps[IN_GAME]);
                theCamera.SetSize(IN_GAME);
                isSetted = true;
                isGameStart = true;
                Debug.Log("인게임");
            }
        }
        else
        {
            if(!isSetted) theCamera.SetCamera(maps[TITLE]);
        }
    }
}
