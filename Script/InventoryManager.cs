using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryManager : MonoBehaviour
{
    #region Singleton
    public static InventoryManager instance;
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
    private OrderManager theOrder;
    private AudioManager theAudio;
    private DatabaseManager theDatabase;
    private OkOrCancel OOC;
    public string key_sound;
    public string beep_sound;
    public string cancel_sound;
    public string open_sound;
    public string enter_sound;
    public GameObject go_OOC;
    private InventroySlot[] slots;

    private List<Item> inventoryItemList;
    private List<Item> inventoryCategoryList;

    public Text descrition;
    public string[] tabDescription; // 탭 부연설명

    public Transform gridSlot; //slot 부모객체

    public GameObject go; // 활성화 관련
    public GameObject[] categoryPanels;

    private int selectedItem; // 선택된 아이템;
    private int selectedCategory;

    private bool inventoryActive;
    private bool categoryActive;
    private bool itemActive;
    private bool stopKeyInput;
    private bool preventExec;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);
    //public GameObject[] selection_Window;

    // Start is called before the first frame update
    public void AddInventoryList(int item_id, int count = 1)
    {
       for(int i = 0; i < theDatabase.itemList.Count; i++)
        {
            if(item_id == theDatabase.itemList[i].itemID)
            {
                for (int j = 0; i < inventoryItemList.Count; j++)
                {
                    if(inventoryItemList[j].itemID == item_id)
                    {
                        if(inventoryItemList[j].itemType == Item.ItemType.USE||
                            inventoryItemList[j].itemType == Item.ItemType.ETC ||
                            inventoryItemList[j].itemType == Item.ItemType.QUEST)
                        {
                            inventoryItemList[j].itemCount += count;
                            return;
                        }
                        else if(inventoryItemList[j].itemType == Item.ItemType.EQUIPIMENT)
                        {
                            for(int c = 0; c < count; c++) inventoryItemList.Add(theDatabase.itemList[i]);
                            return;
                        }

                        
                    }
                }
                if (theDatabase.itemList[i].itemType == Item.ItemType.USE ||
                            theDatabase.itemList[i].itemType == Item.ItemType.ETC ||
                            theDatabase.itemList[i].itemType == Item.ItemType.QUEST)
                {
                    inventoryItemList.Add(theDatabase.itemList[i]);
                    inventoryItemList[inventoryItemList.Count - 1].itemCount += count;
                    return;
                }
                else if (theDatabase.itemList[i].itemType == Item.ItemType.EQUIPIMENT)
                {
                    for (int c = 0; c < count; c++) inventoryItemList.Add(theDatabase.itemList[i]);
                    return;
                }
                return;
            }
        }
        Debug.LogError("Database don't have this item id ");
    }

    void Start()
    {
        OOC = FindObjectOfType<OkOrCancel>();
        theDatabase = FindObjectOfType<DatabaseManager>();
        theOrder = FindObjectOfType<OrderManager>();
        theAudio = FindObjectOfType<AudioManager>();
        inventoryItemList = new List<Item>();
        inventoryCategoryList = new List<Item>();
        slots = gridSlot.GetComponentsInChildren<InventroySlot>();
        // test code //
        inventoryItemList.Add(new Item(10001, "빨간 포션", "체력을 100만큼 회복!", Item.ItemType.USE));
        inventoryItemList.Add(new Item(10002, "파랑 포션", "마나를 100만큼 회복!", Item.ItemType.USE));
        //inventoryItemList.Add(new Item(10003, "농축된 빨간 포션", "체력을 500만큼 회복!", Item.ItemType.USE));
        //inventoryItemList.Add(new Item(10004, "농축된 파랑 포션", "마나를 300만큼 회복!", Item.ItemType.USE));
        //inventoryItemList.Add(new Item(11001, "랜덤 상자", "랜덤으로 A급 장비아이템이 나온다!", Item.ItemType.USE));
        //inventoryItemList.Add(new Item(20001, "도란 검", "기본 단검", Item.ItemType.EQUIPIMENT));
        //inventoryItemList.Add(new Item(21001, "사파이어 반지", "마나와 민첩을 증가 시주는 반지!", Item.ItemType.EQUIPIMENT));
        //inventoryItemList.Add(new Item(30001, "고대 유물 조각1", "고대 유물의 조각1 이다!", Item.ItemType.QUEST));
        //inventoryItemList.Add(new Item(30002, "고대 유물 조각2", "고대 유물의 조각2 이다!", Item.ItemType.QUEST));
        //inventoryItemList.Add(new Item(30003, "고대 유물", "말 그대로 오래된 고대 유물!", Item.ItemType.QUEST));
    }
    public void ShowCategory()
    {
        RemoveSlot();
        SelectedCategory();
    }
    public void SelectedCategory()
    {
        StopAllCoroutines();
        Color color = categoryPanels[0].GetComponent<Image>().color;
        color.a = 0.0f;
        for(int i = 0; i < categoryPanels.Length; i++)
        {
            categoryPanels[i].GetComponent<Image>().color = color;
        }
        descrition.text = tabDescription[selectedCategory];
        StartCoroutine(SelectedEffectCorutine());
    }
    IEnumerator SelectedEffectCorutine()
    {
        Color color = categoryPanels[0].GetComponent<Image>().color;
        while (categoryActive)
        {
            while (color.a < 0.5f)
            {
                color.a += 0.03f;
                categoryPanels[selectedCategory].GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a > 0.0f)
            {
                color.a -= 0.03f;
                categoryPanels[selectedCategory].GetComponent<Image>().color = color;
                yield return waitTime;
            }
        }
        
        yield return new WaitForSeconds(0.3f);
    }
    public void RemoveSlot()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveItem();
            slots[i].gameObject.SetActive(false);
        }
    }

    public void ShowItem()
    {
        inventoryCategoryList.Clear();
        RemoveSlot();
        selectedItem = 0;
        switch(selectedCategory)
        {
            case 0:
                for(int i = 0; i < inventoryItemList.Count; i++)
                {
                    if(inventoryItemList[i].itemType == Item.ItemType.USE)
                    {
                        inventoryCategoryList.Add(inventoryItemList[i]);
                    }
                }
                break;
            case 1:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (inventoryItemList[i].itemType == Item.ItemType.EQUIPIMENT)
                    {
                        inventoryCategoryList.Add(inventoryItemList[i]);
                    }
                }
                break;
            case 2:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (inventoryItemList[i].itemType == Item.ItemType.QUEST)
                    {
                        inventoryCategoryList.Add(inventoryItemList[i]);
                    }
                }
                break;
            case 3:
                for (int i = 0; i < inventoryItemList.Count; i++)
                {
                    if (inventoryItemList[i].itemType == Item.ItemType.ETC)
                    {
                        inventoryCategoryList.Add(inventoryItemList[i]);
                    }
                }
                break;
        }
        for(int i = 0; i < inventoryCategoryList.Count; i++)
        {
            slots[i].gameObject.SetActive(true);
            slots[i].AddItem(inventoryCategoryList[i]);
        }
        SelectedItem();
    }
    public void SelectedItem()
    {
        StopAllCoroutines();
        if (inventoryCategoryList.Count > 0)
        {
            Color color = slots[0].GetComponent<Image>().color;
            color.a = 0.0f;
            for(int i = 0; i < inventoryCategoryList.Count; i++)
            {
                slots[i].GetComponent<Image>().color = color;
            }
            descrition.text = inventoryCategoryList[selectedItem].itemDescrition;
        }
        else descrition.text = "해당 타입의 아이템을 가지고 있지 않습니다.";
        StartCoroutine(SelectedItemCorutine());
    }
    IEnumerator SelectedItemCorutine()
    {
        Color color = slots[selectedItem].GetComponent<Image>().color;
        while (itemActive)
        {
            while (color.a < 0.5f)
            {
                color.a += 0.03f;
                slots[selectedItem].GetComponent<Image>().color = color;
                yield return waitTime;
            }
            while (color.a > 0.0f)
            {
                color.a -= 0.03f;
                slots[selectedItem].GetComponent<Image>().color = color;
                yield return waitTime;
            }
        }
        yield return new WaitForSeconds(0.3f);
    }
    // Update is called once per frame
    void Update()
    {
        if (!stopKeyInput)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                inventoryActive = !inventoryActive;

                if (inventoryActive)
                {
                    theAudio.Play(open_sound);
                    theOrder.PlayerDialogDontMove(false);
                    go.SetActive(true);
                    selectedCategory = 0;
                    categoryActive = true;
                    itemActive = false;
                    ShowCategory();
                }
                else
                {
                    theAudio.Play(cancel_sound);
                    go.SetActive(false);
                    categoryActive = false;
                    itemActive = false;
                    theOrder.PlayerDialogDontMove(true);
                }
            }
            if (inventoryActive)
            {
                if (categoryActive)
                {
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        if (selectedCategory < categoryPanels.Length - 1) selectedCategory++;
                        else selectedCategory = 0;
                        theAudio.Play(key_sound);
                        SelectedCategory();
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        if (selectedCategory > 0) selectedCategory--;
                        else selectedCategory = categoryPanels.Length - 1 ;
                        theAudio.Play(key_sound);
                        SelectedCategory();
                    }
                    else if (Input.GetKeyDown(KeyCode.Z))
                    {
                        theAudio.Play(enter_sound);
                        Color color = categoryPanels[0].GetComponent<Image>().color;
                        color.a = 1.0f;
                        categoryPanels[selectedCategory].GetComponent<Image>().color = color;
                        categoryActive = false;
                        itemActive = true;
                        preventExec = true;
                        ShowItem();
                    }
                    else if (Input.GetKeyDown(KeyCode.X))
                    {
                        theAudio.Play(cancel_sound);
                        go.SetActive(false);
                        categoryActive = false;
                        itemActive = false;
                        theOrder.PlayerDialogDontMove(true);
                    }
                }
                else if (itemActive)
                {
                    if(inventoryCategoryList.Count > 0)
                    {
                        if (Input.GetKeyDown(KeyCode.UpArrow))
                        {
                            if (selectedItem  > 1) selectedItem -= 2;
                            else selectedItem  = inventoryCategoryList.Count - 1 - selectedItem;
                            theAudio.Play(key_sound);
                            SelectedItem();
                        }
                        else if (Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            if (selectedItem + 2 < inventoryCategoryList.Count) selectedItem += 2;
                            else selectedItem = selectedItem % 2;
                            theAudio.Play(key_sound);
                            SelectedItem();
                        }
                        else if (Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            if (selectedItem  < inventoryCategoryList.Count - 1) selectedItem ++;
                            else selectedItem = 0;
                            theAudio.Play(key_sound);
                            SelectedItem();
                        }
                        else if (Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            if (selectedItem > 0) selectedItem--;
                            else selectedItem = inventoryCategoryList.Count - 1;
                            theAudio.Play(key_sound);
                            SelectedItem();;
                        }
                        else if (Input.GetKeyDown(KeyCode.X))
                        {
                            theAudio.Play(cancel_sound);
                            itemActive = false;
                            categoryActive = true;
                            preventExec = true;
                            ShowCategory();
                        }
                        else if (Input.GetKeyDown(KeyCode.Z)&& !preventExec)
                        {
                            preventExec = true;
                            if (selectedCategory == 0) // 소모품
                            {
                                theAudio.Play(enter_sound);
                                stopKeyInput = true; // 
                                StartCoroutine(OOCCorutine());
                            }else if(selectedCategory == 1)
                            {
                                // 장비
                            }
                            else
                            {
                                theAudio.Play(beep_sound);
                            }
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.X))
                    {
                        theAudio.Play(cancel_sound);
                        itemActive = false;
                        categoryActive = true;
                        preventExec = true;
                        ShowCategory();
                    }
                }
            }
            if (Input.GetKeyUp(KeyCode.Z))
            {
                preventExec = false;
            }
        }
    }
    IEnumerator OOCCorutine()
    {
        go_OOC.SetActive(true);
        OOC.ShowTwoChoice("사용", "취소");
        yield return new WaitUntil(() => !OOC.activate);
        if (OOC.GetResult())
        {
            for(int i = 0; i < inventoryItemList.Count; i++)
            {
                if(inventoryItemList[i].itemID == inventoryCategoryList[selectedItem].itemID)
                {
                    theDatabase.UseItem(inventoryItemList[i].itemID);
                    if (inventoryItemList[i].itemCount > 1) --inventoryItemList[i].itemCount;
                    else inventoryItemList.RemoveAt(i);
                    
                    ShowItem();
                    break;
                }
            }
        }
        stopKeyInput = false;
        go_OOC.SetActive(false);
    }

}
