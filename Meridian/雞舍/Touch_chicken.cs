using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class Touch_chicken : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    //摸雞事件

    [SerializeField] Egg egg;

    [Header("雞舍TEXT")]
    public GameObject[] ChickenCoop_TEXT;

    [Header("摸雞")]
    public bool Successfully_touched;

    [Header("取蛋成功")]
    public float Egg_retrieval;

    [Header("雞舍音效")]
    public AudioSource Chicken_sour;//音效

    [Header("生氣音效")]
    public AudioClip anger_Clip;//音效

    [Header("睡覺音效")]
    public AudioClip sleep_Clip;//音效

    [Header("摸雞音效")]
    public AudioSource Touch_sour;//音效

    [Header("生蛋音效")]
    public AudioSource lay_eggs_sour;//音效

    //[Header("母雞放鬆值")]
    //public Text chicken_text;

    [Header("雞舍系統")]
    public GameObject ChickenCoop;

    Animator Aor;
    void Start()
    {
        InvokeRepeating("Hen_sober", 1, 0.5f);//母雞清醒
        Aor = GetComponent<Animator>();
    }

    void Update()
    {
        EggRetrieval_rate();
        Aor.SetFloat("雞睡眠", Egg_retrieval);
        //chicken_text.text = Egg_retrieval.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Successfully_touched == false && egg.EGG_int == 0)
        {
            Successfully_touched = true;
            Hen_sleep();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Successfully_touched == true)
        {
            Successfully_touched = false;
        }
    }


    [Header("摸雞玩法")]
    public GameObject How_play;
    bool How_play_bool;
    public void Gameplay()
    {
        if (How_play_bool == false)
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


    void Hen_sleep()//母雞睡覺
    {
        Egg_retrieval += 0.2f;
        Touch_sour.Play();
    }

    void Hen_sober()//母雞清醒
    {
        if (Egg_retrieval > 0)
        {
            Egg_retrieval -= 0.2f;
        }
    }


    [Header("雞蛋機率")]
    public int Retrieval_rate;

    [Header("金色雞蛋")]
    public GameObject Golden;

    [Header("銀色雞蛋")]
    public GameObject Silver;

    [Header("一般雞蛋")]
    public GameObject Regular;


    public int lay_egg;//以生過蛋
    public void EggRetrieval_rate()//成功判斷生蛋機率
    {
        if (Egg_retrieval >= 3 && lay_egg == 0)
        {
            Chicken_sour.clip = sleep_Clip;
            Chicken_sour.Play();
           Retrieval_rate = Random.Range(1, 100);
            lay_egg = 1;
            if (Retrieval_rate < 35)
            {
                lay_eggs_sour.Play();
                Regular.SetActive(true);
            }
            if (Retrieval_rate > 34 && Retrieval_rate < 77)
            {
                lay_eggs_sour.Play();
                Silver.SetActive(true);
            }
            if (Retrieval_rate > 76 && Retrieval_rate <= 100)
            {
                lay_eggs_sour.Play();
                Golden.SetActive(true);
            }
        }
    }

    void FailedEgg_Retrieval()//取蛋失敗
    {
        Aor.SetBool("雞生氣", true);
        ChickenCoop_TEXT[0].SetActive(true);
        ChickenCoop_TEXT[1].GetComponent<Text>().text = "母雞發現你意圖不軌取不到雞蛋了~";
        Chicken_sour.clip = anger_Clip;
        Chicken_sour.Play();
    }

    public void aaa()//點擊雞蛋時
    {
        Regular.SetActive(false);
        Silver.SetActive(false);
        Golden.SetActive(false);
        ChickenCoop.GetComponent<Chicken_coop>().egg_EGG();
        if (Egg_retrieval > 3)//判斷母雞是否睡著
        {
            Egg_retrieval = 0;
            ChickenCoop_TEXT[0].SetActive(true);
            ChickenCoop_TEXT[1].GetComponent<Text>().text = "喜得一顆新鮮雞蛋";
            if (Retrieval_rate < 35)
            {
                ChickenCoop.GetComponent<Chicken_coop>().aaa(Retrieval_rate);
            }
            if (Retrieval_rate > 34 && Retrieval_rate < 77)
            {
                ChickenCoop.GetComponent<Chicken_coop>().aaa(Retrieval_rate);
            }
            if (Retrieval_rate > 76 && Retrieval_rate <= 100)
            {
                 ChickenCoop.GetComponent<Chicken_coop>().aaa(Retrieval_rate);
            }
        }
        else
        {
            FailedEgg_Retrieval();
        }
    }

    public void Cross_day()//跨日歸零
    {
        lay_egg = 0;
        if (Egg_retrieval > 0)
        {
            Egg_retrieval = 0;
            Aor.SetBool("雞生氣", false);
        }
        Regular.SetActive(false);
        Silver.SetActive(false);
        Golden.SetActive(false);
    }

    [Header("滑鼠手")]
    public Texture2D cursorTexture; //要替換的光標圖片
    public CursorMode cursorMode = CursorMode.Auto;//渲染形式，auto爲平臺自適應顯示 
    public Vector2 hotSpot = Vector2.zero;// 響應區域 (vector2.zero) 

    public void mode()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    public void mode_f()
    {
        Cursor.SetCursor(null, hotSpot, cursorMode);
    }
}