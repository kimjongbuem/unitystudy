using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {

    IEnumerator walkingCorutine()
    {
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runspeed;
                ispreesedShiftKey = true;
            }
            else
            {
                applyRunSpeed = 0;
                ispreesedShiftKey = false;
            }
            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);
            while (currentWalkCount < walkCount)
            {
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (applyRunSpeed + speed), 0, 0);
                }
                if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
                }
                if (ispreesedShiftKey) currentWalkCount++;
                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);
            }
            canMove = true;
            currentWalkCount = 0;
        }
    }
    bool ispreesedShiftKey = false;
    bool canMove = true;

    public float speed;
    private Vector3 vector;
    // Use this for initialization
    public float runspeed;
    private float applyRunSpeed;
    public int walkCount;
    private int currentWalkCount = 0;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(canMove)
		if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0){
                canMove = false;
                StartCoroutine (walkingCorutine());
        }
    }
}
