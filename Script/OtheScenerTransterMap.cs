using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OtheScenerTransterMap : MonoBehaviour
{
    /*
    맵이동관련 

    */
    /*씬이동*/
    public string transferMapName; // 이동할 맵의 이름
    private PlayManager thePlayer;
    private CameraManager theCamera;
    private FadeManager theFade;
    private OrderManager theOrder;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayManager>();
        theFade = FindObjectOfType<FadeManager>();
        theOrder = FindObjectOfType<OrderManager>();
        /*
         * GetComponet(단일) vs FindObject (복수) // 해당 스크립트가 붙어있는 컴포넌트만 꺼내올수 있음. 
         */
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 박스컬라이더에 닿는 순간 활성화 //
        if (collision.gameObject.name == "Player")
        {
            StartCoroutine(TransferCorutine());
        }
    }
    IEnumerator TransferCorutine()
    {
        thePlayer.currentMapName = transferMapName;
        theOrder.PlayerDialogDontMove(false);
        theFade.FadeOut(); yield return new WaitForSeconds(1f);
        
        SceneManager.LoadScene(transferMapName);
        theFade.FadeIn();
        //yield return new WaitForSeconds(0.4f);
        theOrder.PlayerDialogDontMove(true);
    }
}
