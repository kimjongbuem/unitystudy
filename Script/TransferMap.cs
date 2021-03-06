﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    /*
     맵이동관련 
     
     */
    public Transform target;


    /*씬이동*/
    public string transferMapName; // 이동할 맵의 이름

    bool flag = true;
    private CameraManager theCamera;
    private MovingObject thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        //if (!flag)
        //    theCamera = FindObjectOfType<CameraManager>();
        thePlayer = FindObjectOfType<MovingObject>();
        /*
         * GetComponet(단일) vs FindObject (복수) // 해당 스크립트가 붙어있는 컴포넌트만 꺼내올수 있음. 
         */
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 박스컬라이더에 닿는 순간 활성화 //
        if(collision.gameObject.name == "Player")
        {
            thePlayer.currentMapName = transferMapName;
            SceneManager.LoadScene(transferMapName);
            //if (flag) SceneManager.LoadScene(transferMapName);
            //else
            //{
            //    thePlayer.transform.position = target.position;
            //    theCamera.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, theCamera.transform.position.z);
            //}
        }
    }
}
