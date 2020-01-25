using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public static bool isStart = false;

    public void GameExit()
    {
        Application.Quit();
    }

    public void GameStart()
    {
        isStart = true;
        GameManager.isSetted = false;
    }
}
