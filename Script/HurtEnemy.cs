using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{ 

    public GameObject prefab_Floating_Text;
    public GameObject parent;
    public GameObject effect;
    public string atk_sound;
    private PlayerStat playerStat;
    // Start is called before the first frame update
    void Start()
    {
        playerStat = FindObjectOfType<PlayerStat>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "enemy")
        {
            int dmg = collision.gameObject.GetComponent<EnemyStat>().Hit(playerStat.atk);
            AudioManager.instance.Play(atk_sound);
            Vector3 vector1 = collision.transform.position; vector1.y += 20;
            var clone1 = Instantiate(effect, vector1, Quaternion.Euler(Vector3.zero));


            Vector3 vector = collision.transform.position;
            vector.x += 70; vector.y += 70;
            var clone = Instantiate(prefab_Floating_Text, vector, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingText>().text.text = dmg.ToString();
            clone.GetComponent<FloatingText>().text.fontSize = 25;
            clone.GetComponent<FloatingText>().text.color = Color.yellow;
            clone.transform.SetParent(parent.transform);
        }
    }
}
