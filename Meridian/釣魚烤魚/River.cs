using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class River : MonoBehaviour
{
    //處理河水釣魚相關事件

    [Header("烤魚系統")]
    public GameObject Grilled_fish;

    [Header("河流音效")]
    public AudioSource sour;//音效
    [Header("背景音樂")]
    public GameObject background_sour;//音效

    // Update is called once per frame
    void Update()
    {
        if (Take_up.value == 1 || Take_up.value == 0)
        {
            Fish_bait();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && night_bool == false)//觸碰河開啟起釣魚按鈕
        {
            fishing_game.SetActive(true);
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && night_bool == false)//離開關閉釣魚按鈕
        {
            Leave_fishing();
            fishing_game.SetActive(false);
            sour.Stop();
        }
    }
    bool night_bool;
    public void night()
    {
        store();
        if (night_bool==false)
        {
            night_bool = true;
        }
        else { night_bool = false; }
    }

    public void store()
    {
        Grilled_fish.GetComponent<Grilled_fish>().night();
    }

    public void grilled_Read()//讀取漁獲資料
    {
        Grilled_fish.GetComponent<Grilled_fish>().grilled_Read();
    }

    [Header("釣魚玩法")]
    public GameObject How_play;
    bool How_play_bool;
    public void Gameplay()
    {
        if (How_play_bool==false)
        {
            How_play.SetActive(true);
            How_play_bool = true;
        }
        else
        {
            How_play.SetActive(false);
            How_play_bool = false;
        }

    }

    [Header("釣魚視窗鈕")]
    public GameObject fishing_game;
    [Header("釣魚視窗")]
    public GameObject fishing;
    [Header("釣魚收線鈕")]
    public GameObject fishing_button;
    [Header("收線條")]
    public Slider Take_up;
    int fishing_int;//魚上鉤時間
    [Header("阻擋釣魚文字")]
    public GameObject fishing_text;
    [Header("釣竿動畫")]
    public Animator fishing_rod;
    int fishing_fishing;
    public void Fishing_window()//顯示釣魚畫面
    {
        sour.Play();
        background_sour.GetComponent<music_system>().game_Music();
        Number_fish = 0;
        fish_text.text = Number_fish.ToString();
        fishing.SetActive(true);
        fishing_rod.SetInteger("拋竿", Fishingto_day);
        if (Fishingto_day<10)//今日釣魚樹如果低於10才能釣魚
        {
            fishing_int = Random.Range(5, 10);
            InvokeRepeating("fishing_time", 1, 1);
        }
        else
        {
            fishing_text.SetActive(true);
        }
        fishing_fishing = 1;
    }

    [Header("釣魚音效")]
    public AudioSource fish_sour;//音效
    [Header("上鉤音效")]
    public AudioClip[] fish_Clip;//音效
    void fishing_time()//魚上鉤時間
    {
        fishing_rod.SetInteger("拋竿", Fishingto_day);
        if (Fishingto_day < 10)//今日釣魚樹如果低於10才能釣魚
        {
            if (fishing_int > 0)
            {
                fishing_int--;
            }
            else//魚上鉤顯示按鈕
            {
                fishing_rod.SetBool("鉤到魚", true);
                fish_sour.clip = fish_Clip[0];
                fish_sour.Play();
                Take_up.value = 0.5f;
                fishing_button.SetActive(true);
                InvokeRepeating("Reel", 1, 0.5f);
                CancelInvoke("fishing_time");
            }
        }
        else { CancelInvoke("fishing_time"); fishing_text.SetActive(true); }
    }
    public void Take()//玩家拉竿
    {
        Take_up.value -= 0.05f;
        fish_sour.Play();
    }

    void Reel()//魚的力道
    {
        Take_up.value += 0.05f;
        //Take_up.value += Time.deltaTime;
    }

    [Header("釣到的魚的數量")]
    public Text fish_text;
    int Number_fish;//當前釣到的魚的數量
    public int Fishingto_day;//用於紀錄玩家今日掉多少魚


    void Fish_bait()//釣魚結果
    {
        if (Take_up.value == 0)
        {
            fishing_rod.SetTrigger("釣魚成功");
            fish_sour.clip = fish_Clip[1];
            fish_sour.Play();
            Fishingto_day++;
            Number_fish++;
            fish_text.text = Number_fish.ToString();
            Grilled_fish.GetComponent< Grilled_fish>().Fresh_fish(1);//增加玩家新鮮魚數量
        }
        if (Take_up.value == 1)
        {
            fishing_rod.SetTrigger("斷線");
        }
        Take_up.value = 0.5f;
        CancelInvoke("Reel");
        fishing_button.SetActive(false);//關閉釣魚鈕
        fishing_int = Random.Range(5, 30);
        InvokeRepeating("fishing_time", 1, 1);
        fishing_rod.SetBool("鉤到魚", false);
    }

    public void Leave_fishing()//結束釣魚
    {
        CancelInvoke("Reel");
        CancelInvoke("fishing_time");
        fishing_button.SetActive(false);//關閉釣魚鈕
        fishing.SetActive(false);
        if (fishing_fishing==1)
        {
            background_sour.GetComponent<music_system>().FarmDay_Music();
            fishing_fishing = 0;
        }
    }

    public void Cross_day()//跨日歸零
    {
        if (Fishingto_day> 0)
        {
            Fishingto_day = 0;
            fishing_text.SetActive(false);
        }
    }
}
