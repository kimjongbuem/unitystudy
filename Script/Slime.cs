using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MovingObject
{
    
    public float attackDelay; // 플레이어가 피할수 있도록 공격 유예 ....
    public float inter_MoveWaitTime; // 대기시간
    private float current_interMWT;

    public GameObject health_background;
    public string attck_sound;


    private Vector2 playerPos;
    private string dir;
    private int random_Value;
    // Start is called before the first frame update
    void Start()
    {
        queue = new Queue<string>();
        current_interMWT = inter_MoveWaitTime;
    }
    private bool IsNearPlayer()
    {
        playerPos = PlayManager.instance.transform.position;
        if (Mathf.Abs(Mathf.Abs(playerPos.x) - Mathf.Abs(this.transform.position.x)) <= speed * walkCount * 1.01f &&
            Mathf.Abs(Mathf.Abs(playerPos.y) - Mathf.Abs(this.transform.position.y)) <= speed * walkCount * 0.45f
            ) return true;
        else if (Mathf.Abs(Mathf.Abs(playerPos.x) - Mathf.Abs(this.transform.position.x)) <= speed * walkCount * 0.25f &&
            Mathf.Abs(Mathf.Abs(playerPos.y) - Mathf.Abs(this.transform.position.y)) <= speed * walkCount * 1.01f
            ) return true;
            return false;
    }
    // Update is called once per frame
    void Update()
    {
        current_interMWT -= Time.deltaTime;
        if(current_interMWT <= 0)
        {
            current_interMWT = inter_MoveWaitTime;
            if (IsNearPlayer())
            {
                Flip();
                return;
            }
            RandomDirection();
            
            if (CheckCollision())
            {
                queue.Clear();
                return;
            }
            base.Move(dir);
        }
    }
    private void Flip()
    {
        Vector3 local_scale = transform.localScale;
        if (playerPos.x > this.transform.position.x)
        {
            local_scale.x = -1f;
        }
        else
        {
            local_scale.x = 1f;
        }
        health_background.transform.localScale = local_scale;

        transform.localScale = local_scale;
        animator.SetTrigger("Attack");
        StartCoroutine(AttackCorutine());
    }
    IEnumerator AttackCorutine()
    {
        yield return new WaitForSeconds(attackDelay);
        if (IsNearPlayer())
        {
            AudioManager.instance.Play(attck_sound);
            PlayerStat.instance.BeDamagedPlayer(GetComponent<EnemyStat>().atk);
        }
    }
    private void RandomDirection()
    {
        vector.Set(0, 0, vector.z);
        random_Value = Random.Range(0, 4);
        switch (random_Value)
        {
            case 0:
                vector.y = 1.0f;
                dir = "UP";
                break;
            case 1:
                vector.y = -1.0f;
                dir = "DOWN";
                break;
            case 2:
                vector.x = -1.0f;
                dir = "LEFT";
                break;
            case 3:
                vector.x = 1.0f;
                dir = "RIGHT";
                break;
        }
    }
}
