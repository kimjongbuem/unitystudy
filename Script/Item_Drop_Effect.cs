using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Drop_Effect : MonoBehaviour
{
    Animator _anim;
    InventoryManager _IM;
    public int  item_id; //
    public string key_sound;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _IM = FindObjectOfType<InventoryManager>();
    }

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                AudioManager.instance.Play(key_sound);
                _IM.AddInventoryList(item_id);
                new WaitForSeconds(0.01f);
                Destroy(this.gameObject);
            }
        }
    }
}
