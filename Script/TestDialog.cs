using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialog : MonoBehaviour
{
    public string[] texts;
    [SerializeField] public Dialog dialog;
    private DialogManager theDM; bool check;
    void Start()
    {
        theDM = FindObjectOfType<DialogManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player" && !check)
        {
            check = true;
            //theDM.ShowDialog(dialog);
            theDM.ShowText(texts);
        }
    }
}
