using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public string characterName;
    public BoxCollider2D boxColider;
    public int walkCount;
    private bool notCor = false;
    public LayerMask layerMask;// 통과불가
    public float speed;
    protected Vector3 vector;
    public Animator animator;
    public Queue<string> queue;
   protected int currentWalkCount = 0;

    public void Move(string direction, int frequency = 5)
    {
        queue.Enqueue(direction);
        if (!notCor)
        {
            notCor = true;
            StartCoroutine(MoveCorutine(frequency));
            
        }
    }
    public void Turn(string _dir)
    {
        vector.Set(0, 0, vector.z);
        switch (_dir)
        {
            case "UP": vector.y = 1f; break;
            case "DOWN": vector.y = -1f; break;
            case "LEFT": vector.x = -1f; break;
            case "RIGHT": vector.x = 1f; break;
        }
        animator.SetFloat("DirX", vector.x); animator.SetFloat("DirY", vector.y);
    }
    IEnumerator MoveCorutine(int frequency)
    {
        while(queue.Count != 0)
        {
            switch (frequency)
            {
                case 1: yield return new WaitForSeconds(4f); break;
                case 2: yield return new WaitForSeconds(3f); break;
                case 3: yield return new WaitForSeconds(2f); break;
                case 4: yield return new WaitForSeconds(1f); break;
                case 5: break;
            }
            string _dir = queue.Dequeue();
            vector.Set(0, 0, vector.z);
            switch (_dir)
            {
                case "UP": vector.y = 1f; break;
                case "DOWN": vector.y = -1f; break;
                case "LEFT": vector.x = -1f; break;
                case "RIGHT": vector.x = 1f; break;
            }

            animator.SetFloat("DirX", vector.x); animator.SetFloat("DirY", vector.y);
            while (true)
            {
                bool collisions = CheckCollision();
                if (collisions)
                {
                    animator.SetBool("Walking", false);
                    yield return new WaitForSeconds(1f);
                }
                else break;
            }

            animator.SetBool("Walking", true);
            boxColider.offset = new Vector2(0.7f * vector.x * speed * walkCount, 0.7f * vector.y * speed * walkCount);

            while (currentWalkCount < walkCount)
            {
                transform.Translate(vector.x * speed, vector.y * speed, 0);
                currentWalkCount++;
                if (currentWalkCount == 12) boxColider.offset = Vector2.zero;
                yield return new WaitForSeconds(0.01f);
            }
            currentWalkCount = 0;
            if (frequency != 5) animator.SetBool("Walking", false);
        }
        notCor = false;
        animator.SetBool("Walking", false);
    }
    protected bool CheckCollision()
    {
        RaycastHit2D hit;
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount);
        boxColider.enabled = false;
        hit = Physics2D.Linecast(start, end, layerMask);
        boxColider.enabled = true;
        if (hit.transform != null) return true;
        return false;
    }
}
