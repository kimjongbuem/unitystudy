using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TestMove {
    public string name;
    public string dir;

}

public class OtherManagerTesting : MonoBehaviour
{
    [SerializeField]
    public TestMove[] move;
    private OrderManager order;
    private BoxCollider2D BoxCollider;
    // Start is called before the first frame update
    void Start()
    {
        order = FindObjectOfType<OrderManager>();
        BoxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            order.PreLoadCharacter();
            for(int i = 0; i < move.Length; i++)
            {
                order.Move(move[i].name, move[i].dir);
            }
        }
    }
}
