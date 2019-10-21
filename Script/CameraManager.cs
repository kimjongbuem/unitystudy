using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    static public CameraManager instance;
    public GameObject target; // camera가 따라갈 대상
    public float moveSpeed;// 카메라의 속도
    private Vector3 targetPosition; // 대상의 현재 위치값.

    public BoxCollider2D bound;
    private Vector3 maxBound, minBound;

    // 박스컬라이더 영역의 최소, 최대 xyz 값을 지님.

     private float halfWIdth, halfHeight;
    // 카메라의 반너비, 반높이 값을 지닐 변수.

    private Camera theCamera;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
    }

    void Start()
    {
        theCamera = GetComponent<Camera>();
        minBound = bound.bounds.min; maxBound = bound.bounds.max;
        halfHeight = theCamera.orthographicSize;
        halfWIdth = halfHeight * Screen.width / Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        if(target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z); // z는 해당 우선 순위값을 위해.
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime); //
            float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWIdth, maxBound.x - halfWIdth);
            /* (pos, e1 , e2) => pos는 e1, e2사이에 있으면 그대로 반환하나 e1보다 작으면 e1, e2 보다 크면 e2rk kqsghks */
            float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);
            this.transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }
    public void setBound(BoxCollider2D newBound)
    {
        bound = newBound;
        minBound = bound.bounds.min; maxBound = bound.bounds.max;
    }
}
