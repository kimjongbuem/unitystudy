using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SameSceneTransferMap : MonoBehaviour
{
    public Animator anim_leftdoor;
    public Animator anim_rightdoor;
    [Tooltip("문이 있으면 true , 아님 false")]
    public bool door; // 문이 있냐없냐?
    public int doorCount;
    [Tooltip("UP, DOWN, LEFT, RIGHT")]
    public string direction;

    private Vector2 vector;
    public Transform target;
    OrderManager theOrder;
    public BoxCollider2D targetBound;
    private CameraManager theCamera;
    private MovingObject thePlayer;
    private FadeManager theFade;
    // Start is called before the first frame update
    void Start()
    {
        theCamera = FindObjectOfType<CameraManager>();
        thePlayer = FindObjectOfType<PlayManager>();
        theOrder = FindObjectOfType<OrderManager>();
        theFade = FindObjectOfType<FadeManager>();
    }
    IEnumerator TransferCorutine()
    {
        theOrder.PreLoadCharacter();
        theOrder.PlayerDialogDontMove(false);
        theFade.FadeOut();
        if (door)
        {
            anim_leftdoor.SetBool("Open", true);
            if (doorCount == 2) anim_rightdoor.SetBool("Open", true);
            yield return new WaitForSeconds(0.8f);
        }
        else yield return new WaitForSeconds(0.5f);
        if (door)
        {
            anim_leftdoor.SetBool("Open", false);
            if (doorCount == 2) anim_rightdoor.SetBool("Open", false);
        }
        yield return new WaitForSeconds(0.3f);
        
        theCamera.setBound(targetBound);
        thePlayer.transform.position = new Vector3(target.position.x, target.position.y,
        thePlayer.transform.position.z);
        theCamera.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, theCamera.transform.position.z);
        theFade.FadeIn();
        yield return new WaitForSeconds(0.5f);
        theOrder.PlayerDialogDontMove(true);
    }
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!door)
        if (collision.gameObject.name == "Player")
        {
            StartCoroutine(TransferCorutine());
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (door)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                vector.Set(thePlayer.animator.GetFloat("DirX"), thePlayer.animator.GetFloat("DirY"));
                switch (direction)
                {
                    case "UP":
                        if (vector.y == 1.0f)
                            StartCoroutine(TransferCorutine());
                        break;
                    case "DOWN":
                        if (vector.y == -1.0f)
                            StartCoroutine(TransferCorutine());
                        break;
                    case "LEFT":
                        if (vector.x == -1.0f)
                            StartCoroutine(TransferCorutine());
                        break;
                    case "RIGHT":
                        if (vector.x == 1.0f)
                            StartCoroutine(TransferCorutine());
                        break;
                    default:
                        StartCoroutine(TransferCorutine());
                        break;
                }

            }
        }
    }
}
