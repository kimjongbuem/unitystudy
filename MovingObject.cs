using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {

    IEnumerator walkingCorutine()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            bool ispreesedShiftKey = false;
            canMove = false;

            while (currentWalkCount < walkCount) {
                if (Input.GetKey(KeyCode.LeftShift)) {
                    runspeed = 2.0f;
                    ispreesedShiftKey = true;
                }
                else runspeed = 0;
                vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (runspeed + speed), 0, 0);
                }
                if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + runspeed), 0);
                }
                if (ispreesedShiftKey) currentWalkCount++;
                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);
            }
            canMove = true;
            currentWalkCount = 0;
        }
    }

    bool canMove = true;
    public float speed;
    private Vector3 vector;
    // Use this for initialization
    private float runspeed;
    public int walkCount;
    private int currentWalkCount = 0;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(canMove)
		if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0){
            StartCoroutine (walkingCorutine());
        }
    }
}
