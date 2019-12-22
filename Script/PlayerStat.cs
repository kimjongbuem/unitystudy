using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStat : MonoBehaviour
{
    public static PlayerStat instance;

    public Slider hpslider;
    public Slider mpslider;

    public int lv;
    public int hrp, mrp;
    public int atk;
    public int def;
 
    public int hp;
    public int current_hp;

    public int mp;
    public int current_mp;

    public int[] exp;
    public int current_exp;

    public string dmg_sound;

    public float time;
    private float curTime;
    public GameObject parent;
    public GameObject prefabs_text;

    WaitForSeconds waitTime = new WaitForSeconds(0.1f);
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        current_hp = hp;
        current_mp = mp;
        curTime = time;
    }
    private void Update()
    {
        hpslider.maxValue = hp;
        mpslider.maxValue = mp;

        hpslider.value = current_hp;
        mpslider.value = current_mp;

        if (current_exp >= exp[lv])
        {
            lv++;
            current_exp -= exp[lv - 1];
            hp += lv * 50;
            mp += lv * 30;
            current_hp = hp;
            current_mp = mp;
            atk += 5;
            def += 2;
        }
        curTime -= Time.deltaTime;
        if(curTime <= 0)
        {
            if(hrp > 0)
            {
                if (current_hp + hrp <= hp) current_hp += hrp;
                else current_hp = hp;
            }
            if (mrp > 0)
            {
                if (current_mp + mrp <= mp) current_mp += mrp;
                else current_mp = mp;
            }
            curTime = time;
        }
    }

    public void BeDamagedPlayer(int enemy_atk)
    {
        int dmg;
        if (def >= enemy_atk) dmg = 1;
        else dmg = enemy_atk - def;

        current_hp -= dmg;
        Vector3 vector = this.transform.position;
        vector.y += 56; vector.x += 70;
        var clone = Instantiate(prefabs_text, vector, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<FloatingText>().text.text = dmg.ToString();
        clone.GetComponent<FloatingText>().text.fontSize = 25;
        clone.GetComponent<FloatingText>().text.color = Color.red;
        clone.transform.SetParent(parent.transform);
        StartCoroutine(DamageCorutine());

        if (current_hp < 0)
            Debug.Log("죽음");
        AudioManager.instance.Play(dmg_sound);
    }
    // Update is called once per frame
    IEnumerator DamageCorutine()
    {
        Color color = GetComponent<SpriteRenderer>().color;
        color.a = 0;
        GetComponent<SpriteRenderer>().color = color;
        yield return waitTime;
        color.a = 1;
        GetComponent<SpriteRenderer>().color = color;
        yield return waitTime;
        color.a = 0;
        GetComponent<SpriteRenderer>().color = color;
        yield return waitTime;
        color.a = 1;
        GetComponent<SpriteRenderer>().color = color;
        yield return waitTime;
        color.a = 0;
        GetComponent<SpriteRenderer>().color = color;
        yield return waitTime;
        color.a = 1;
        GetComponent<SpriteRenderer>().color = color;
    }
}
