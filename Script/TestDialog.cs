using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialog : MonoBehaviour
{
    [SerializeField] public Dialog dialog;
    private DialogManager theDM;
    void Start()
    {
        theDM = FindObjectOfType<DialogManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            theDM.ShowDialog(dialog);
        }
    }
}
