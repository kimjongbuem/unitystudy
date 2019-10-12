using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    private BoxCollider2D boxColider;
    public LayerMask layerMask;// 통과불가

    public float speed;
    private Vector3 vector;
    public float runSpeed;
    // Start is called before the first frame update
    // Update is called once per frame
    private float applyRunSpeed;
    public int walkCount;
    private int currentWalkCount = 0;
    private bool canMove = true;
    private bool applyRunFlag = false;
    private Animator animator;

    public string currentMapName; // 

    void Start()
    {
        DontDestroyOnLoad(this.gameObject); // 이 오브젝트를 다른씬으로 갈때마다 파괴 ㄴㄴ
        boxColider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();    
    }

    void Update()
    {
        if (canMove)
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine (Mycorutine());
            }
    }
    
    private IEnumerator Mycorutine()
    {
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0) {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed;
                applyRunFlag = true;
            }
            else
            {
                applyRunFlag = false;
                applyRunSpeed = 0;
            }

            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);
            if (vector.x != 0) vector.y = 0;
            if (vector.y != 0) vector.x = 0;
            animator.SetFloat("DirX", vector.x); animator.SetFloat("DirY", vector.y);

            RaycastHit2D hit;
            Vector2 start = transform.position;
            Vector2 end = start+ new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount);
            boxColider.enabled= false;
            hit = Physics2D.Linecast(start, end, layerMask);
            boxColider.enabled = true;

            if (hit.transform != null) break;

            animator.SetBool("Walking", true);

            while (currentWalkCount < walkCount)
            {
                if (vector.x != 0) transform.Translate((applyRunSpeed + speed) * vector.x, 0, 0);
                else if (vector.y != 0) transform.Translate(0, (applyRunSpeed + speed) * vector.y, 0);

                if (applyRunFlag) currentWalkCount++;
                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);
            }
            currentWalkCount = 0;
        }
        animator.SetBool("Walking", false);
        canMove = true;
    }
}
