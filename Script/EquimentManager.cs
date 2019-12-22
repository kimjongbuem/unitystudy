using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EquimentManager : MonoBehaviour
{

    private OrderManager theOrder;
    private AudioManager theAudio;
    private OkOrCancel OOC;
    private PlayerStat playerStat;
    private InventoryManager theInven;
    
    private int addATK = 0, addDEF = 0, addHRP = 0, addMRP = 0; 

    private bool keyInput = true;
    private bool activated = false;

    public Text[] texts;
    public Image[] slots;

    int selectedNumber = 0;

    public Item[] equimentList;

    public string key_sound;
    public string enter_sound;
    public string take_off_Sound;

    public GameObject weaponTest;
    public GameObject equi;
    public GameObject go_OOC;
    public GameObject go_Selected_UI;

    private const int WEAPON = 0, SHILED = 1, ARMOR = 2,NEC = 5,
        RIGHT_RING = 3, LEFT_RING = 4, HELMET = 6, LEFT_GLOVE = 7, RIGHT_GLOVE = 8,
        BELT = 9, LEFT_BOOTS = 10, RIGHT_BOOTS = 11;
    private const int ATK = 0, DEF = 1, HPR = 6, MPR = 7;
    // Start is called before the first frame update
    void Start()
    {
        theInven = FindObjectOfType<InventoryManager>();
        theOrder = FindObjectOfType<OrderManager>();
        theAudio = FindObjectOfType<AudioManager>();
        playerStat = FindObjectOfType<PlayerStat>();
        OOC = FindObjectOfType<OkOrCancel>();
    }
    public void Selected_UIBox()
    {
        go_Selected_UI.transform.position = slots[selectedNumber].transform.position;
    }

    public void ClearEquip()
    {
        Color color = slots[0].color;
        color.a = 0;
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].sprite = null;
            slots[i].color = color;
        }
    }
    public void ShowEquip()
    {
        Color color = slots[0].color;
        color.a = 1f;
        for(int i = 0; i < slots.Length; i++)
        {
            if(equimentList[i].itemID != 0)
            {
                slots[i].sprite = equimentList[i].icon;
                slots[i].color = color;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (keyInput)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                activated = !activated;
                if (activated)
                {
                    theAudio.Play(key_sound);
                    theOrder.PlayerDialogDontMove(false);
                    selectedNumber = 0;
                    equi.SetActive(true);
                    ClearEquip();
                    ShowEquip();
                    ShowStat();
                }
                else
                {
                    theAudio.Play(key_sound);
                    equi.SetActive(false);
                    theOrder.PlayerDialogDontMove(true);
                    ClearEquip();
                }
            }

            if (activated)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    theAudio.Play(key_sound);
                    if (selectedNumber > 0) selectedNumber--;
                    else selectedNumber = slots.Length - 1;
                    Selected_UIBox();
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    theAudio.Play(key_sound);
                    if (selectedNumber < slots.Length - 1) selectedNumber++;
                    else selectedNumber = 0; Selected_UIBox();
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    theAudio.Play(key_sound);
                    if (selectedNumber > 0) selectedNumber--;
                    else selectedNumber = slots.Length - 1; Selected_UIBox();
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    theAudio.Play(key_sound);
                    if (selectedNumber < slots.Length - 1) selectedNumber++;
                    else selectedNumber = 0; Selected_UIBox();
                }
                else if (Input.GetKeyDown(KeyCode.Z))
                {
                    if(equimentList[selectedNumber].itemID != 0)
                    {
                        theAudio.Play(enter_sound);
                        keyInput = false;
                        StartCoroutine(OOCCorutine("해제", "취소"));
                    }
                }
            }
        }
        
    }
    public void ShowStat()
    {
        if (addATK == 0) texts[ATK].text = playerStat.atk.ToString();
        else texts[ATK].text = playerStat.atk.ToString() + "(+" + addATK.ToString() + ")";

        if (addDEF == 0) texts[DEF].text = playerStat.def.ToString();
        else texts[DEF].text = playerStat.def.ToString() + "(+" + addDEF.ToString() + ")";

        if (addHRP == 0) texts[HPR].text = playerStat.hrp.ToString();
        else texts[HPR].text = playerStat.hrp.ToString() + "(+" + addHRP.ToString() + ")";

        if (addMRP == 0) texts[MPR].text = playerStat.mrp.ToString();
        else texts[MPR].text = playerStat.mrp.ToString() + "(+" + addMRP.ToString() + ")";
    }

    public void EquipItem(Item _item)
    {
        string temp = _item.itemID.ToString().Substring(0, 3);
        int num = int.Parse(temp);
        switch (num)
        {
            case 200:
                EquipItemCheck(WEAPON, _item);
                weaponTest.SetActive(true);
                weaponTest.GetComponent<SpriteRenderer>().sprite = _item.icon;
                break;
            case 201:
                EquipItemCheck(SHILED, _item);
                break;
            case 202:
                EquipItemCheck(ARMOR, _item);
                break;
            case 203:
                EquipItemCheck(RIGHT_RING, _item);
                break;
        }

    }
    public void EquipItemCheck(int itemNumber, Item _item)
    {
        if(equimentList[itemNumber].itemID == 0)
        {
            equimentList[itemNumber] = _item;
        }
        else
        {
            theInven.AddInventoryList(equimentList[itemNumber].itemID);
            equimentList[itemNumber] = _item;
        }
        Wear(_item);
        ShowStat();
    }

    IEnumerator OOCCorutine(string _up, string _down)
    {
        go_OOC.SetActive(true);
        OOC.ShowTwoChoice(_up, _down);
        yield return new WaitUntil(() => !OOC.activate);
        if (OOC.GetResult())
        {
            theInven.EquipToInventory(equimentList[selectedNumber]);
            theAudio.Play(take_off_Sound);
            ClearEquip();
            TakeOffItem(equimentList[selectedNumber]);
            if(selectedNumber == WEAPON) weaponTest.SetActive(false);
            equimentList[selectedNumber] = new Item(0, "", "", Item.ItemType.EQUIPIMENT, 0);
            ShowStat();
            ShowEquip();
        }
        keyInput = true;
        go_OOC.SetActive(false);
    }
    public void TakeOffItem(Item _Item)
    {
        playerStat.atk -= _Item.atk;
        playerStat.def -= _Item.def;
        playerStat.hrp -= _Item.rhp;
        playerStat.mrp -= _Item.rmp;
        addATK -= _Item.atk;
        addDEF -= _Item.def;
        addHRP -= _Item.rhp;
        addMRP -= _Item.rmp;
    }
    public void Wear(Item _Item)
    {
        playerStat.atk += _Item.atk;
        playerStat.def += _Item.def;
        playerStat.hrp += _Item.rhp;
        playerStat.mrp += _Item.rmp;
        addATK += _Item.atk;
        addDEF += _Item.def;
        addHRP += _Item.rhp;
        addMRP += _Item.rmp;
    }
}
