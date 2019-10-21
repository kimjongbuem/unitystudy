using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SameSceneTransferMap : MonoBehaviour
{
    public Transform target;
    public BoxCollider2D targetBound;
    private CameraManager theCamera;
    private MovingObject thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        theCamera = FindObjectOfType<CameraManager>();
        thePlayer = FindObjectOfType<MovingObject>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            theCamera.setBound(targetBound);
            thePlayer.transform.position = target.position;
            theCamera.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, theCamera.transform.position.z);
        }
    }
}
