using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    //處理箭相關


    int Bow_Arrow_level;//弓箭等級
    [Header("速度")]
    public float speed;
    [Header("射擊座標")]
    public Vector3 worldPosition;
    [Header("射擊開關")]
    public bool shooting;
    [Header("箭生成位置")]
    public GameObject Arrow_spawn;
    [Header("攻擊力")]
    public float attack_power;
    [Header("玩家")]
    public GameObject play;
    // Use this for initialization
    void Start()
    {
        Bow_Arrow_level = player.Bow_Arrow_level;
        switch (Bow_Arrow_level)
        {
            case 1:
                attack_power = 10;
                speed = 0.2f;
                break;
            case 2:
                speed = 0.3f;
                attack_power = 15;
                break;
        }
        Arrow_spawn = GameObject.Find("武器生成點");
        play = GameObject.Find("玩家");
        play.GetComponent<player>().shoot_arrows();//放箭音效
        this.transform.position = Arrow_spawn.transform.position;
    }

    private void GetPos()//標定滑鼠位置
    {
        gameObject.transform.parent = null;
        worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;
        shooting = true;
    }

    //物體離開螢幕
    void OnBecameInvisible()
    {
        play.GetComponent<player>().arrows();//弓箭擊中音效
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (shooting == false)
        {
            GetPos();
        }
        //this.transform.position +=new Vector3(0, speed,0)*Time.deltaTime ;

        if (this.transform.position == worldPosition)
        {
            Destroy(this.gameObject);//到達滑鼠點刪除
        }
        else { gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, worldPosition, speed); }        //未到index位置，繼續移动

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "怪"|| collision.gameObject.tag == "巨大史萊姆"|| collision.gameObject.tag == "史萊姆王"|| collision.gameObject.tag == "毒牙蝙蝠")//攻擊到怪
        {
            collision.gameObject.GetComponent<monster_blood>().Injuried(attack_power);
            play.GetComponent<player>().arrows();//弓箭擊中音效
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "魔王")//攻擊到怪
        {
            collision.gameObject.GetComponent<Demon_King>().Injuried(attack_power);
            play.GetComponent<player>().arrows();//弓箭擊中音效
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "牆"|| collision.gameObject.tag == "河")//遊走時碰到牆壁
        {
            play.GetComponent<player>().arrows();//弓箭擊中音效
            Destroy(this.gameObject);
        }
    }
}
