using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class NPCMove { // npc 움직이는 커스텀클래스
    [Tooltip("NPCMove를 체크하면 npc가 움직임")]
    public bool npcMove;
    public string[] direction; // npc가 움직일 방향들 설정
    [Range(1,5)]
    [Tooltip("1 천천히 2 약간 빠른 천천히 3 보통 4 빠름 5 연속적인.")]// 스크롤바 지정
    public int frequency; // 속도정보
}

public class NPCManager : MovingObject
{
    [SerializeField]
    public NPCMove npcMove;
    // Start is called before the first frame update
    void Start()
    {
        queue = new Queue<string>();
        StartCoroutine(MoveCorutine()); //EX
    }

    // Update is called once per frame
    public void SetMove(bool ismove)
    {

    }
    IEnumerator MoveCorutine()
    {
        if(npcMove.direction.Length != 0)
        {
            for(int i = 0; i < npcMove.direction.Length; i++)
            {
                yield return new WaitUntil(() => queue.Count < 2);
                base.Move(npcMove.direction[i],npcMove.frequency);
                if (i == npcMove.direction.Length - 1) i = -1;
            }
        }
    }
    
}
