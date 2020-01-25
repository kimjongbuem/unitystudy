using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReversePaeManager : MonoBehaviour
{
    WaitForSeconds waitTime = new WaitForSeconds(0.24f);
    public Stack<int> Integer_PaeImage_Value_Stack;
    public Stack<Sprite> GameObject_PaeImage_Stack;
    public GameObject[] reversePaes;
    public float removeTime;
    public float curTime;
    private int curIndex ;
    private PlayerMe playerMe; private Computer com;
    private void Start()
    {
        Integer_PaeImage_Value_Stack = new Stack<int>();
        GameObject_PaeImage_Stack = new Stack<Sprite>();
        playerMe = FindObjectOfType<PlayerMe>(); com = FindObjectOfType<Computer>();
        curIndex = reversePaes.Length - 1;
        Vector3 vector = reversePaes[curIndex].transform.position;
        for(int i = 1; i <= curIndex; i++)
        {
            Vector3 temp = vector;
            temp.Set(vector.x - 1.008f * i, vector.y + 1.008f * i, vector.z);
            reversePaes[curIndex - i].transform.position = temp;
        }
        curIndex = 0;
    }

    void Update()
    {
        
    }
    private IEnumerator WaitCorutine()
    {
        yield return waitTime;
    }

    public void Reset()
    {
        curIndex = 0;
        curTime = 0;
        SetAllActive(true);
    }
    public void SetAllActive(bool active)
    {
        for (int i = 0; i < reversePaes.Length; i++) reversePaes[i].SetActive(active);
    }

    

    public void Distribute_Pae() // 각 패들을 5장을 각각 유저에게 주며 2번반복.
    {
        for(int r = 0; r < 10; r+=5)
        for(int i = 0; i < 5; i++)
        {
            reversePaes[curIndex++].SetActive(false); 
            playerMe.havingPaes[r + i].GetComponent<SpriteRenderer>().sprite = GameObject_PaeImage_Stack.Pop();
            reversePaes[curIndex++].SetActive(false); 
            com.havingPaes[r + i].GetComponent<SpriteRenderer>().sprite = GameObject_PaeImage_Stack.Pop();
            com.hidePaes[r + i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("images/hidePae");
            WaitCorutine();
        }
    }
    
}
