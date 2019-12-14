using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager instance;
    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else Destroy(this.gameObject);

    }// Start is called before the first frame update
    #endregion
    public string[] var_name;
    public float[] var;
    public string[] switchName;
    public bool[] switchFlags;
    public List<Item> itemList = new List<Item>();
    void Start()
    {
        // test code //
        itemList.Add(new Item(10001, "빨간 포션", "체력을 100만큼 회복!", Item.ItemType.USE));
        itemList.Add(new Item(10002, "파랑 포션", "마나를 100만큼 회복!", Item.ItemType.USE));
        itemList.Add(new Item(10003, "농축된 빨간 포션", "체력을 500만큼 회복!", Item.ItemType.USE));
        itemList.Add(new Item(10004, "농축된 파랑 포션", "마나를 300만큼 회복!", Item.ItemType.USE));
        itemList.Add(new Item(11001, "랜덤 상자", "랜덤으로 A급 장비아이템이 나온다!", Item.ItemType.USE));
        itemList.Add(new Item(20001, "도란 검", "기본 단검", Item.ItemType.EQUIPIMENT));
        itemList.Add(new Item(21001, "사파이어 반지", "마나와 민첩을 증가 시주는 반지!", Item.ItemType.EQUIPIMENT));
        itemList.Add(new Item(30001, "고대 유물 조각1", "고대 유물의 조각1 이다!", Item.ItemType.QUEST));
        itemList.Add(new Item(30002, "고대 유물 조각2", "고대 유물의 조각2 이다!", Item.ItemType.QUEST));
        itemList.Add(new Item(30003, "고대 유물", "말 그대로 오래된 고대 유물!", Item.ItemType.QUEST));
    }
    public void UseItem(int _itemid)
    {
        switch (_itemid)
        {
            case 10001: Debug.Log("hp 50회복!"); break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
