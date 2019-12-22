using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MovingObject
{
    private float applyRunSpeed;
    private bool canMove = true;
    private bool applyRunFlag = false;
    static public MovingObject instance;
    public float runSpeed;

    private bool attacking = false;
    public float attackdelay;
    private float currentAttackdelay;
    public string walkSound_1;
    public string walkSound_2;
    public string walkSound_3;
    public string walkSound_4;
    public string currentMapName; // 
    //
    //public AudioClip walksound1;
    //public AudioClip walksound2;
    private AudioSource audioSource;
    private AudioManager _audioManager;
    public bool dialogDontMove = true; 
    void Start()
    {
        if (instance == null)
        {
            queue = new Queue<string>();
            instance = this;
            DontDestroyOnLoad(this.gameObject); // 이 오브젝트를 다른씬으로 갈때마다 파괴 ㄴㄴ
            boxColider = GetComponent<BoxCollider2D>();
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
            _audioManager = FindObjectOfType<AudioManager>();
            characterName = "Player";
        }
        else Destroy(this.gameObject);
    }

    void Update()
    {
        if (canMove && dialogDontMove && !attacking)
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(Mycorutine());
            }

        if(dialogDontMove && !attacking)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentAttackdelay = attackdelay;
                attacking = true;
                animator.SetBool("Attacking", true);
            }
        }

        if (attacking)
        {
            currentAttackdelay -= Time.deltaTime;
            if(currentAttackdelay <= 0)
            {
                animator.SetBool("Attacking", false);
                attacking = false;
            }
        }
    }

    private IEnumerator Mycorutine()
    {
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0 && !attacking)
        {
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

            bool colisionFlag = base.CheckCollision();
            if (colisionFlag) break;

            animator.SetBool("Walking", true);
            boxColider.offset = new Vector2(0.7f *vector.x * speed * walkCount, 0.7f * vector.y * speed * walkCount);
            int temp = Random.Range(1, 4);
            switch (temp)
            {
                case 1:
                    _audioManager.Play(walkSound_1);
                    break;
                case 2:
                    _audioManager.Play(walkSound_2);
                    break;
                case 3:
                    _audioManager.Play(walkSound_3);
                    break;
                case 4:
                    _audioManager.Play(walkSound_4);
                    break;
            }
            while (currentWalkCount < walkCount)
            {
                if (vector.x != 0) transform.Translate((applyRunSpeed + speed) * vector.x, 0, 0);
                else if (vector.y != 0) transform.Translate(0, (applyRunSpeed + speed) * vector.y, 0);

                if (applyRunFlag) currentWalkCount++;
                currentWalkCount++;
                if (currentWalkCount == 12) boxColider.offset = Vector2.zero;
                yield return new WaitForSeconds(0.01f);

            }
   
            //audioSource.Play();
          
            currentWalkCount = 0;
        }
        animator.SetBool("Walking", false);
        canMove = true;
    }
}
