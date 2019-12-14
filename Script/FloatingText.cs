using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FloatingText : MonoBehaviour
{
    public Text text;
    public float moveSpeed; public float destoryTime;
    private Vector3 vec;
    // Update is called once per frame
    void Update()
    {
        vec.Set(text.transform.position.x, text.transform.position.y + (moveSpeed * Time.deltaTime), text.transform.position.z);
        text.transform.position = vec;
        destoryTime -= Time.deltaTime;
        if (destoryTime <= 0) Destroy(this.gameObject);

    }
}
