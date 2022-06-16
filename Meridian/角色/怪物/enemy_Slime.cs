using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class enemy_Slime : MonoBehaviour
{
    //處理史萊姆相關

    [Header("傷害值")]
    public float damage;
    [Header("玩家")]
    public GameObject player;
    [Header("速度")]
    public float speed;//速度


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("玩家");
        sour = GetComponent<AudioSource>();//抓取走路音效撥放器
        rest = 10;//判斷休息
        Stop = true;
        dir = new Vector3(Random.Range(x_float[0], x_float[1]), Random.Range(y_float[0], y_float[1]), 0);//初始方向   
        InvokeRepeating("rest_time", 1, 1);//狀態系統
    }

    private void Update()
    {
        if (Stop && Slime_t == 0)
        {
            mobile();
        }
    }

    public Vector3 dir;//this.gameobject的方向
    [Header("行走範圍")]
    public float[] x_float;
    public float[] y_float;
    void mobile()//移動
    {
        this.transform.localPosition += dir.normalized * speed*Time.deltaTime;
    }

    [Header("攻擊音效")]
    public AudioSource Attack_sound;//攻擊音效  
    private void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.gameObject.tag == "Player" && Slime_t == 1)//攻擊到玩家
         {
             player.gameObject.GetComponent<player>().Injuried(damage);//給予傷害
            this.gameObject.GetComponent<Transform>().position -= new Vector3(10, 10, 0) * Time.deltaTime;
            Attack_sound.Play();//播放攻擊音效
         }
        if (collision.gameObject.tag == "牆")//遊走時碰到牆壁
        {
            this.gameObject.GetComponent<Transform>().position -= new Vector3(10, 10, 0) * Time.deltaTime;
            dir = new Vector3(Random.Range(x_float[0], x_float[1]), Random.Range(y_float[0], y_float[1]));//初始方向 
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && Slime_t == 1)//攻擊到玩家
         {
             player.gameObject.GetComponent<player>().Injuried(damage);//給予傷害
             Attack_sound.Play();//播放攻擊音效
         }

    }

    int Slime_t;//攻擊狀態關關0=遊走1=攻擊
    int rest;//休息狀態判斷時間
    int activity;//決定是否活動
    public bool Stop;//休息開關
    AudioSource sour;//走路音效
    public void rest_time()//狀態
    {
        Slime_t = Slime.Slime_t;
        rest--;//每個狀態的時間
        if (Slime_t == 1)//如果是攻擊狀態不休息
        {
            Stop = true;
            rest = Random.Range(0, 10);
            sour.Play();//播放走路音效
        }
        if (rest <= 0 && Slime_t == 0)
        {
            activity = Random.Range(0, 10);//判斷是否要休息
            if (activity <= 4)//休息
            {
                Stop = false;//休息
                rest = Random.Range(0, 10);
                sour.Stop();//停止走路音效
            }
            else if (activity > 4)
            {
                rest = Random.Range(0, 10);//活動時間
                dir = new Vector3(Random.Range(x_float[0], x_float[1]), Random.Range(y_float[0], y_float[1]));//初始方向 
                Stop = true;//不休息
                sour.Play();//播放走路音效
            }

        }
    }



}
