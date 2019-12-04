using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event1 : MonoBehaviour
{
    public Dialog dialog_1, dialog_2;
    OrderManager _OM;
    DialogManager _DM;
    PlayManager _PM;
    FadeManager _FM;
    bool ischecked = false;
    void Start()
    {
        _OM = FindObjectOfType<OrderManager>();
        _PM = FindObjectOfType<PlayManager>();
        _DM = FindObjectOfType<DialogManager>();
        _FM = FindObjectOfType<FadeManager>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player" && !ischecked && _PM.animator.GetFloat("DirY") == 1.0f && Input.GetKey(KeyCode.Z))
        {
            ischecked = true;
            StartCoroutine(EventCorutine());
        }   
    }
    private IEnumerator EventCorutine()
    {
        _OM.PreLoadCharacter();
        _DM.ShowDialog(dialog_1);
        _OM.PlayerDialogDontMove(false);
        yield return new WaitUntil(() => !_DM.talking);

        _OM.Move("Player", "RIGHT");
        _OM.Move("Player", "RIGHT");
        _OM.Move("Player", "UP");
         yield return new WaitUntil(() => _PM.queue.Count == 0);

        _FM.Flash();
        _DM.ShowDialog(dialog_2);
        yield return new WaitUntil(() => !_DM.talking);
        _OM.PlayerDialogDontMove(true);
    }
}
