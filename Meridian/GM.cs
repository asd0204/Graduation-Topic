using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class GM : MonoBehaviour
{
    [Header("河水系統")]
    public GameObject River;
    [Header("雞舍系統")]
    public GameObject Chicken;
    [Header("種田管理器")]
    public GameObject Planting_system;
    [Header("現有種植作物")]
    public GameObject[] seed;
    [Header("睡覺跨日視窗")]
    public GameObject CrossDay_obj;
    [Header("黑幕")]
    public GameObject Shady;
    [Header("玩家")]
    public GameObject player;

    [Header("草原系統")]
    public GameObject grassland;    
    
    
    [Header("環境光源")]
    public GameObject ambient_light;

    [Header("音效")]
    public AudioSource sour;//音效


    [Header("天數")]
     static public int DAY_int;//音效

    [Header("小怪物")]
    public GameObject[] monster;
    [Header("巨大史萊姆")]
    public GameObject[] giant_slime;
    [Header("史萊姆王")]
    public GameObject[] king_slime;
    [Header("毒牙蝙蝠")]
    public GameObject[] fangs_bat;
    [Header("怪物數量")]
    public int[] monster_int;

    [Header("背景音樂")]
    public music_system Music_System;


    // Start is called before the first frame update
    void Start()
    {
        River = GameObject.Find("河水系統");
        Chicken = GameObject.Find("雞舍系統");
        Planting_system = GameObject.Find("田地系統");
        player = GameObject.Find("玩家");
        sour = GetComponent<AudioSource>();
        //讀黨     匯出時存檔資料夾要改成 meridiandata
        Planting_system.GetComponent<Planting_system>().Read();//種植作物讀黨
         River.GetComponent<River>().grilled_Read();//漁獲讀黨
         Chicken.GetComponent<Chicken_coop>().grilled_Read();//雞舍讀黨
        player.GetComponent<player>().money_Read();//錢錢讀取
        DAY_int = PlayerPrefs.GetInt("天數");
        Cross_bool=PlayerPrefs.GetInt("日夜");
        if (Cross_bool==1)
        {
            into_the_night();
        }
        if (DAY_int < 1)
        {
            knight.SetActive(true);
        }
    }




    static public int Cross_bool;//日夜

    
    public void Cross()
    {
        if (Cross_bool==0)//如果是白天(0)就進入夜晚
        {      
            Cross_bool = 1;
            PlayerPrefs.SetInt("日夜", Cross_bool);
            Music_System.Fighting_Music();
            into_the_night();
        }
        else 
        {
            Cross_bool = 0;
            PlayerPrefs.SetInt("日夜", Cross_bool);
            by_day();
        }
    }

    [Header("柵欄門")]
    public GameObject[] fence_gate;   
    [Header("第一天夜晚對話")]
    public GameObject first_night_conversation;    
    [Header("第15天對話")]
    public GameObject last_drama;
    void into_the_night()//進入夜晚
    {
        grassland.GetComponent<grassland_system>().into_the_night();
        sour.Play();
        fence_gate[0].GetComponent<BoxCollider2D>().isTrigger = true;//開啟柵欄門
        fence_gate[1].SetActive(false);
        CrossDay_obj.SetActive(false);
        Shady.SetActive(true);
        player.GetComponent<map_system>().night();//關閉地圖
        River.GetComponent<River>().night();//關閉釣魚
        Chicken.GetComponent<Chicken_coop>().night();//關閉偷蛋
        Planting_system.GetComponent<Planting_system>().Block_off_fields();
        Planting_system.GetComponent<Planting_system>().store();//儲存土地是否種植及限有種子儲存
        player.GetComponent<player>().money_store();//錢錢儲存
        ambient_light.GetComponent<Light2D>().color = new Color(0.5f,0.5f,0.5f);

        if (DAY_int==0)
        {
            first_night_conversation.SetActive(true);
        }
        else
        {
            knight.SetActive(false);
        }

        if (DAY_int == 15)//最後一天夜晚觸發
        {
            last_drama.SetActive(true);
        }
    }

    [Header("第二天早上對話")]
    public GameObject Conversation_next_morning;   
    void by_day()//跨日到白天
    {
        DAY_int = PlayerPrefs.GetInt("天數");
        DAY_int++;
        PlayerPrefs.SetInt("天數", DAY_int);
        Debug.Log(DAY_int);
        if (DAY_int > 15)
        {
            GOOOD_ending();
        }
        else
        {
            Music_System.FarmDay_Music();
            CrossDay_obj.SetActive(false);
            Shady.SetActive(true);
            Planting_system.GetComponent<Planting_system>().Cross_day();//作物更新
            River.GetComponent<River>().night();//開啟釣魚
            River.GetComponent<River>().Cross_day();//釣魚清0
            Chicken.GetComponent<Chicken_coop>().night();//開啟偷蛋
            Chicken.GetComponent<Chicken_coop>().Cross_day();//偷蛋清0
            player.GetComponent<map_system>().night();//關閉地圖
            seed = GameObject.FindGameObjectsWithTag("種子");
            fence_gate[0].GetComponent<BoxCollider2D>().isTrigger = false;//關閉柵欄門
            fence_gate[1].SetActive(true);
            player.GetComponent<player>().money_store();//錢錢儲存
            CrossDay_seed();//跨日生長
            ambient_light.GetComponent<Light2D>().color = new Color(1f, 1f, 1f);
            if (DAY_int == 0)
            {
                Conversation_next_morning.SetActive(true);
                knight.SetActive(true);
            }
            else if (DAY_int == 1)
            {
                Conversation_next_morning.SetActive(true);
                knight.SetActive(false);
            }
        }
        Days_int();
    }

    [Header("騎士")]
    public GameObject knight;
    public void rest()//重傷跨日到白天
    {
        Clean_up();
        Cross_bool = 0;
        PlayerPrefs.SetInt("日夜", Cross_bool);
        DAY_int = PlayerPrefs.GetInt("天數");
        DAY_int++;
        PlayerPrefs.SetInt("天數", DAY_int);
        Debug.Log(DAY_int);
        Music_System.FarmDay_Music();
        CrossDay_obj.SetActive(false);
        Shady.SetActive(true);
        Planting_system.GetComponent<Planting_system>().Cross_day();//作物更新

        River.GetComponent<River>().Cross_day();//釣魚清0

        Chicken.GetComponent<Chicken_coop>().Cross_day();//偷蛋清0

        seed = GameObject.FindGameObjectsWithTag("種子");
        fence_gate[0].GetComponent<BoxCollider2D>().isTrigger = false;//關閉柵欄門
        fence_gate[1].SetActive(true);
        player.GetComponent<player>().money_store();//錢錢儲存
        CrossDay_seed();//跨日生長
        player.GetComponent<character_direction>().enabled = false;
        ambient_light.GetComponent<Light2D>().color = new Color(1f, 1f, 1f);
        if (DAY_int == 0)
        {
            Conversation_next_morning.SetActive(true);
            knight.SetActive(true);
        }
        else if (DAY_int == 1)
        {       
            Conversation_next_morning.SetActive(true);
            knight.SetActive(false);
        }
        Days_int();
    }

    public void rst()//重傷跨日到白天
    {
        River.GetComponent<River>().night();//開啟釣魚
        Chicken.GetComponent<Chicken_coop>().night();//開啟偷蛋
        player.GetComponent<map_system>().night();//地圖
    }


    void Clean_up()//清怪
    {
        monster = GameObject.FindGameObjectsWithTag("怪");
        giant_slime = GameObject.FindGameObjectsWithTag("巨大史萊姆");
        king_slime = GameObject.FindGameObjectsWithTag("史萊姆王");
        fangs_bat = GameObject.FindGameObjectsWithTag("毒牙蝙蝠");
        if (monster.Length > 0)
        {
            for (int i = 0; i < monster.Length; i++)
            {
                Destroy(monster[i].gameObject);
            }
        }
        if (giant_slime.Length > 0)
        {
            for (int i = 0; i < giant_slime.Length; i++)
            {
                Destroy(giant_slime[i].gameObject);

            }
        }
        if (king_slime.Length > 0)
        {
            for (int i = 0; i < king_slime.Length; i++)
            {
                Destroy(king_slime[i].gameObject);
            }
        }

        if (fangs_bat.Length > 0)
        {
            for (int i = 0; i < fangs_bat.Length; i++)
            {
                Destroy(fangs_bat[i].gameObject);
            }
        }
        EnemySpawner.EnemyLiveCount=0;
    }

    public void enter_maze()//進入迷宮
    {
        Planting_system.GetComponent<Planting_system>().store();//儲存土地是否種植及限有種子儲存
        player.GetComponent<player>().money_store();//錢錢儲存
        River.GetComponent<River>().store();//漁獲存檔
        Chicken.GetComponent<Chicken_coop>().grilled_store();//雞舍存檔

    }

    public void Cross_dayNO()//不跨日
    {
        CrossDay_obj.SetActive(false);
    }

    public void CrossDay_seed()//遊戲中紀錄該種子稱長階段//跨日生長
    {
        for (int i = 0; i < seed.Length; i++)
        {
            seed[i].GetComponent<seed>().Cross_day();
        }
    }

    [Header("提醒字幕")]
    public GameObject Reminder_subtitles;
    [Header("提醒字幕")]
    public Text Reminder_Text;
    void Days_int()//提醒玩家這是第幾天
    {
        Reminder_subtitles.SetActive(true);
        Reminder_Text.text = "第 " + DAY_int.ToString() + " 天";
        Reminder_subtitles.GetComponent<Reminder>().Reminder_true();
    }



    void GOOOD_ending()//好結局
    {
        player.GetComponent<player>().track_int(1);
        Shady.SetActive(true);
        Invoke("Good_text", 0.2f);
    }

    public GameObject Good;
    void Good_text()
    {
        Good.SetActive(true);
    }


    public void BAD_ending()//壞結局
    {

        Clean_up();
        Shady.SetActive(true);
        Invoke("BAD_text", 0.2f);
    }

    public GameObject Bad;
    void BAD_text()
    {
        Bad.SetActive(true);
    }
}
