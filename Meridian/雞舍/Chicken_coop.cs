using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Chicken_coop : MonoBehaviour
{
    //取雞蛋事件
    //雞舍資料

    [SerializeField] Egg egg;

    [Header("雞舍")]
    public GameObject ChickenCoop_OBJ;
    [Header("雞舍TEXT")]
    public GameObject[] ChickenCoop_TEXT;

    [Header("雞舍ui音效")]
    public AudioSource Chicken_sour;//音效
    [Header("背景音樂")]
    public GameObject background_sour;//音效

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && night_bool == false)//觸碰雞舍開啟起雞舍視窗
        {
            background_sour.GetComponent<music_system>().game_Music();
            if (egg.EGG_int == 0)//玩家今日還未取蛋
            {
                ChickenCoop_OBJ.SetActive(true);
            }
            else //玩家今日已取蛋
            {
                ChickenCoop_OBJ.SetActive(true);
                ChickenCoop_TEXT[0].SetActive(true);
                ChickenCoop_TEXT[1].GetComponent<Text>().text = "沒有雞蛋可取喔~";
            }
            Touch_chicken.GetComponent<Touch_chicken>().mode();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && night_bool == false)//離開雞舍關閉雞舍視窗
        {
            ChickenCoop_OBJ.SetActive(false);
            ChickenCoop_TEXT[0].SetActive(false);
            //ChickenCoop_TEXT.GetComponent<Text>().text = "沒有雞蛋可取喔~";
            Touch_chicken.GetComponent<Touch_chicken>().mode_f();
            background_sour.GetComponent<music_system>().FarmDay_Music();
        }
    }



    public void LeaveChicken_coop()//離開雞舍
    {
        Touch_chicken.GetComponent<Touch_chicken>().mode_f();
        ChickenCoop_OBJ.SetActive(false);
        ChickenCoop_TEXT[0].SetActive(false);
        background_sour.GetComponent<music_system>().FarmDay_Music();
        Chicken_sour.Play();
    }

    [Header("摸雞")]
    public GameObject Touch_chicken;
    public void Cross_day()//跨日歸零
    {
        Touch_chicken.GetComponent<Touch_chicken>().Cross_day();
        egg.EGG_int = 0;
    }
    bool night_bool;
    public void night()
    {
        grilled_store(); ;
        if (night_bool == false)
        {
            night_bool = true;
        }
        else { night_bool = false; }
    }
    public void egg_EGG()
    {
        egg.EGG_int = 1;
    }



    public void aaa(int Retrieval_rate )//點擊雞蛋時
    {
        if (Retrieval_rate < 35)
        {
            Regular_int++;
            Regular_text.text = Regular_int.ToString();
        }
        if (Retrieval_rate > 34 && Retrieval_rate < 77)
        {
            Silver_int++;
            Silver_text.text = Silver_int.ToString();
        }
        if (Retrieval_rate > 76 && Retrieval_rate <= 100)
        {
            Golden_int++;
            Golden_text.text = Golden_int.ToString();
        }
    }

        [Header("一般雞蛋text")]
    public Text Regular_text;
    [Header("銀色雞蛋text")]
    public Text Silver_text;
    [Header("金色雞蛋text")]
    public Text Golden_text;

    //收成數量
    public void harvest(int crop_int, int harvest_int)
    {
        switch (crop_int)
        {
            case 7:
                Regular_int = Regular_int + harvest_int;
                break;
            case 8:
                Silver_int = Silver_int + harvest_int;
                break;
            case 9:
                Golden_int = Golden_int + harvest_int;
                break;
        }
        Regular_text.text = Regular_int.ToString();
        Silver_text.text = Silver_int.ToString();
        Golden_text.text =Golden_int.ToString();
        grilled_store();
    }

    public void grilled_store()//儲存雞舍
    {
        egg.Regular_egg = Regular_int;
        egg.Silver_egg = Silver_int;
        egg.Golden_egg = Golden_int;
        //將此player裡面的屬性轉成string(json格式)

        string saveString = JsonUtility.ToJson(egg);

        //將字串saveString存到硬碟中

        StreamWriter file = new StreamWriter(System.IO.Path.Combine("Assets/Meridiandata", "Harvest_egg" + ".json"));

        file.Write(saveString);

        file.Close();

    }

    static public int Regular_int;
    static public int Silver_int;
    static public int Golden_int;
    public void grilled_Read()//讀取雞舍資料
    {
        //StreamReader類用於從流中讀取字符串。它繼承了TextReader類。它提供Read()和ReadLine()方法從流中讀取數據。
        StreamReader fileReader = new StreamReader(System.IO.Path.Combine("Assets/Meridiandata", "Harvest_egg" + ".json"));

        string stringJson = fileReader.ReadToEnd();

        fileReader.Close();

        //將讀取的string改成player物件型態
        egg = JsonUtility.FromJson<Egg>(stringJson);
        Golden_int = egg.Golden_egg;
        Silver_int = egg.Silver_egg;
        Regular_int = egg.Regular_egg;
        Regular_text.text =Regular_int.ToString();
        Silver_text.text = Silver_int.ToString();
        Golden_text.text = Golden_int.ToString();
    }
}

[System.Serializable]
public struct Egg
{
    [Header("一般雞蛋")]
    public int Regular_egg;
    [Header("銀色雞蛋")]
    public int Silver_egg;
    [Header("金色雞蛋")]
    public int Golden_egg;
    [Header("今日已取過雞蛋")]
    public int EGG_int;
}

