using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName; // 이동할 맵의 이름
    private MovingObject thePlayer;
    // Start is called before the first frame update
    void Start()
    {
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
        }
    }
}
