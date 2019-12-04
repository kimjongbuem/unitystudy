using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    private PlayManager thePlayer;
    private List<MovingObject> movingObjects;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayManager>();
    }
    public void PreLoadCharacter()
    {
        movingObjects = ToList();
    }
    public void PlayerDialogDontMove(bool move)
    {
        thePlayer.dialogDontMove = move;
    }
    public List<MovingObject> ToList()
    {
        List<MovingObject> tempList = new List<MovingObject>();
        MovingObject[] temp = FindObjectsOfType<MovingObject>();
        for(int i = 0; i < temp.Length; i++)
        {
            tempList.Add(temp[i]);
        }
        return tempList;
    }
    public void Move(string _name, string _dir)
    {
        
        for (int i = 0; i < movingObjects.Count;  i++)
        {
            if(_name == movingObjects[i].characterName)
            {
                movingObjects[i].Move(_dir);
            }
        }
    }
    public void Turn(string _name, string _dir)
    {

        for (int i = 0; i < movingObjects.Count; i++)
        {
            if (_name == movingObjects[i].characterName)
            {
                movingObjects[i].Turn(_dir);
            }
        }
    }
    public void SetTransparent(string _name, bool active)
    {

        for (int i = 0; i < movingObjects.Count; i++)
        {
            if (_name == movingObjects[i].characterName)
            {
                movingObjects[i].gameObject.SetActive(active);
            }
        }
    }
}
