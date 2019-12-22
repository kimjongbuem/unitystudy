using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyStat : MonoBehaviour
{
    public GameObject hp_background;
    public Image hp_bar;
    public int hp;
    public int exp;
    public int curHp;
    public int mp;
    public int curMp;
    public int def;
    public int atk;
    void Start()
    {
        curHp = hp;
        hp_bar.fillAmount = 1.0f;
    }
    public int Hit(int _dmg)
    {
        int dmg = _dmg;
        if (def >= _dmg) dmg = 1;
        else dmg = _dmg - def;

        curHp -= dmg;
        hp_bar.fillAmount = (float)curHp / hp;
        if (curHp <= 0)
        {
            Destroy(this.gameObject);
            PlayerStat.instance.current_exp += exp;
        }
        hp_background.SetActive(true);
        StopAllCoroutines(); StartCoroutine(RemoveHpBar());
        return dmg;
    }
    IEnumerator RemoveHpBar()
    {
        yield return new WaitForSeconds(3.5f);
        hp_background.SetActive(false);
    }
}
