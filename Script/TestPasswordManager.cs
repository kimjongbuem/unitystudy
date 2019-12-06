using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPasswordManager : MonoBehaviour
{
    private PasswordManager thePM; bool check;
    void Start()
    {
        thePM = FindObjectOfType<PasswordManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !check)
        {
            check = true;
            thePM.ShowPassword(123123);
        }
    }
}
