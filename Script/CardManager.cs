using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardManager : MonoBehaviour
{
    public Sprite[] spritePaes;
    private bool isMix = false;
    private const int PAE_COUNT = 48;
    private ReversePaeManager _reversePaeManager;
    private Dictionary<int, Sprite> _dictionaryPae; // 단순하게 정수값 기준 이미지 딕셔너리
    private Dictionary<Dictionary<int, Sprite>, Pae> _paeInfoDictionary; // 패 정보 딕셔너리
    private bool[] selectedPae; private int count = 0;

    void Start()
    {
        _dictionaryPae = new Dictionary<int, Sprite>();
        _reversePaeManager = FindObjectOfType<ReversePaeManager>();
        selectedPae = new bool[PAE_COUNT + 1];
        for(int i = 1; i<= PAE_COUNT; i++)
        {
            _dictionaryPae.Add(i, spritePaes[i - 1]);
            selectedPae[i] = false;
        }
       
    }
    public void Mix()
    {
        while (count < PAE_COUNT) // test 3
        {
            int r = Random.Range(1, PAE_COUNT);
            if (!selectedPae[r])
            {
                _reversePaeManager.Integer_PaeImage_Value_Stack.Push(r);
                _reversePaeManager.GameObject_PaeImage_Stack.Push(_dictionaryPae[r]);
                selectedPae[r] = true;
                count++;
            }
            else
            {
                for(int i = 1; i <= PAE_COUNT; i++)
                {
                    if (!selectedPae[i])
                    {
                        _reversePaeManager.Integer_PaeImage_Value_Stack.Push(i);
                        _reversePaeManager.GameObject_PaeImage_Stack.Push(_dictionaryPae[i]);
                        selectedPae[i] = true;
                        count++;
                        break;
                    }
                }
            }
            Debug.Log(count);
        }
        count = 0;
    }
    public void Distribute_Pae()
    {
        _reversePaeManager.Distribute_Pae();
    }
    // Update is called once per frame
    void Update()
    {
        const int LEFT = 0;
        if (Input.GetMouseButtonDown(LEFT) && GameManager.GM.isGameStart)
        {
            if (!isMix)
            {
                Mix(); isMix = true;
            }
            Distribute_Pae();
        }
    }
}
