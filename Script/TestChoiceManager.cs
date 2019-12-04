using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChoiceManager : MonoBehaviour
{
    // Start is called before the first frame update
    private OrderManager theOrder;
    private ChoiceManager theChoice;
    [SerializeField]
    public Choice choice;
    public bool flag;
    void Start()
    {
        theOrder = FindObjectOfType<OrderManager>();
        theChoice = FindObjectOfType<ChoiceManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!flag)
        {
            StartCoroutine(TestCorutine());
        }
    }
    IEnumerator TestCorutine()
    {
        flag = true;
        theOrder.PlayerDialogDontMove(false);
        theChoice.ShowChoice(choice);
        yield return new WaitUntil(() => !theChoice.choiceing);
        theOrder.PlayerDialogDontMove(true);

    }
}
