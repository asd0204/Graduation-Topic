using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Grilled_fish : MonoBehaviour
{
    //處理烤魚相關事件
    //漁獲資料


    [Header("烤魚視窗")]
    public GameObject Fire_roast;

    [Header("音效")]
    public AudioSource[] sour;//音效
    [Header("背景音樂")]
    public GameObject background_sour;//音效


    [Header("烤魚玩法")]
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

    public void Fire_Clip()//烤魚音效
    {
        sour[2].Play();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player"&& night_bool == false)//觸碰河開啟起烤魚視窗
        {
            sour[1].Play();
            Fire_roast.SetActive(true);
            fish_text.text = Number_fish_int.ToString();
            Grilled_text[0].text =Baked_successfully_int.ToString();
            Grilled_text[1].text =Baking_failed_int.ToString();
            background_sour.GetComponent<music_system>().game_Music();
            Debug.Log(Number_fish_int);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && night_bool == false)//關閉烤魚視窗
        {
            Fire_roast.SetActive(false);
            sour[1].Stop();
            background_sour.GetComponent<music_system>().FarmDay_Music();
        }
    }

    bool night_bool;
    public void night()
    {
        if (night_bool == false)
        {
            night_bool = true;
            grilled_store();//儲存漁獲
        }
        else { night_bool = false; }
    }


    public void ExitFire_roast()//關閉烤魚視窗
    {
        Fire_roast.SetActive(false);
        sour[0].Play();
        sour[1].Stop();
        background_sour.GetComponent<music_system>().FarmDay_Music();
    }

    [Header("魚")]
    public GameObject fish_obj;
    [Header("生成父物件")]
    public Image Grilledfish_canv;
    public void Instantiate_fish()//生成新鮮魚肉
    {
        if (Number_fish_int > 0)
        {
            Instantiate(fish_obj, Grilledfish_canv.transform, false);
            Fresh_fish(-1);
            sour[0].Play();
        }
    }

    [Header("火堆")]
    public GameObject Fire_obj;
    public void fire()
    {
        Instantiate(Fire_obj, Grilledfish_canv.transform, false);
    }

    [Header("釣到的魚的數量")]
    public Text fish_text;
    [SerializeField] Grilled grilled;//漁獲資料
    public void Fresh_fish(int fish_int)//紀錄玩家所有魚肉
    {
        Number_fish_int += fish_int;
        fish_text.text = Number_fish_int.ToString();
        grilled_store();
    }

    [Header("烤魚的數量")]
    public Text[] Grilled_text;
    public void successfully_text()//烤成功
    {
        Baked_successfully_int++;
        Grilled_text[0].text =Baked_successfully_int.ToString();
        grilled_store();
    }
    public void failed_text()//烤失敗
    {
        Baking_failed_int++;
        Grilled_text[1].text = Baking_failed_int.ToString();
        grilled_store();
    }

    public void grilled_store()//儲存漁獲
    {
        grilled.Baked_successfully = Baked_successfully_int ;
        grilled.Baking_failed= Baking_failed_int ;
        grilled.Number_fish= Number_fish_int ;
        //將此player裡面的屬性轉成string(json格式)

        string saveString = JsonUtility.ToJson(grilled);

        //將字串saveString存到硬碟中

        StreamWriter file = new StreamWriter(System.IO.Path.Combine("Assets/Meridiandata", "Harvest_grilled" + ".json"));

        file.Write(saveString);

        file.Close();

    }

    public void grilled_Read()//讀取漁獲資料
    {
        //StreamReader類用於從流中讀取字符串。它繼承了TextReader類。它提供Read()和ReadLine()方法從流中讀取數據。
        StreamReader fileReader = new StreamReader(System.IO.Path.Combine("Assets/Meridiandata", "Harvest_grilled" + ".json"));

        string stringJson = fileReader.ReadToEnd();

        fileReader.Close();

        //將讀取的string改成player物件型態
        grilled = JsonUtility.FromJson<Grilled>(stringJson);

        Baked_successfully_int = grilled.Baked_successfully;
        Baking_failed_int = grilled.Baking_failed;
        Number_fish_int = grilled.Number_fish;
        fish_text.text = Number_fish_int.ToString();
        Grilled_text[0].text =Baked_successfully_int.ToString();
        Grilled_text[1].text = Baking_failed_int.ToString();
    }

    static public int Baked_successfully_int;
    static public int Baking_failed_int;
    static public int Number_fish_int;

    //收成數量
    public void harvest(int crop_int, int harvest_int)
    {
        switch (crop_int)
        {
            case 10:
                Number_fish_int = Number_fish_int + harvest_int;
                break;
            case 11:
                Baked_successfully_int = Baked_successfully_int + harvest_int;
                break;
            case 12:
                Baking_failed_int = Baking_failed_int + harvest_int;
                break;
        }
        fish_text.text =Number_fish_int.ToString();
        Grilled_text[0].text = Baked_successfully_int.ToString();
        Grilled_text[1].text =Baking_failed_int.ToString();
    }
}


[System.Serializable]
public struct Grilled
{
    [Header("烤成功的數量")]
    public int Baked_successfully;
    [Header("烤失敗的數量")]
    public int Baking_failed;
    [Header("釣到的魚的數量")]
    public int Number_fish;
}
