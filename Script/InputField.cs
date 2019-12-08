using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputField : MonoBehaviour
{
    PlayManager thePlayer;
    public Text names;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayManager>();
        names.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            thePlayer.characterName = names.text;
            Destroy(this.gameObject);
        }
    }
}
