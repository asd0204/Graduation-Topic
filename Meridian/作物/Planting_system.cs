using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Planting_system : MonoBehaviour
{
    //處理種田系統
    //田地資料、種子資料、作物資料

    [Header("土地")]
    public GameObject[] land;
    [Header("白蘿蔔")]
    public GameObject White_radish;
    [Header("紅蘿蔔")]
    public GameObject carrot;
    [Header("藍蘿蔔")]
    public GameObject Blue_radish;
    [Header("玉米")]
    public GameObject corn;
    [Header("鑽石玉米")]
    public GameObject Diamond_corn;
    [Header("月亮")]
    public GameObject moon;
    [Header("太陽")]
    public GameObject sun;

    [Header("種子視窗")]
    public GameObject Planting_game;

    [Header("田地資料")]
    [SerializeField] Field_Information[] field_Information;//田地資料
    [Header("種子數量")]
    [SerializeField] Humber_seed number_Seed;
    [Header("作物數量")]
    [SerializeField] Humber_rops number_Rops;

    [Header("音效")]
    public AudioSource sour;//音效

    [Header("撥種音效")]
    public AudioClip Seed_Clip;//音效

    [Header("收成音效")]
    public AudioClip harvest_Clip;//音效

    [Header("UI音效")]
    public AudioClip ui_Clip;//音效

    private void Awake()
    {
        //land = GameObject.FindGameObjectsWithTag("田地");        //field_Information = new Field_Information();
        sour = GetComponent<AudioSource>();
    }

    int land_int;//告知土地編號
    public void Planting_options(int Planting_land)
    {
        land_int = Planting_land;//告知土地編號
        Planting_game.SetActive(true);//開啟種子選單
        field_Information[land_int].Field_status = 0;//記錄這塊還未種植
        sour.clip = ui_Clip;
        sour.Play();
        Humber_seed_text();
    }

    public void Planting_optionsF()
    {
        Planting_game.SetActive(false);//關閉種子選單
        field_Information[land_int].Field_status = 0;//記錄這塊還未種植
        sour.clip = ui_Clip;
        sour.Play();
    }

    [Header("確認種植窗口")]
    public GameObject Confirmation_window;
    public void seed_N(int Seed)//選擇種子並跳出確認窗口
    {
        sour.clip = ui_Clip;
        sour.Play();
        seed_int = Seed;
        switch (Seed)
        {
            case 0:if (number_Seed.White_radish_seed > 0) { Confirmation_window.SetActive(true); }break;//白蘿蔔
            case 1:if (number_Seed.carrot_seed > 0) { Confirmation_window.SetActive(true); }break;//紅蘿蔔
            case 2:if (number_Seed.Blue_radish_seed > 0) { Confirmation_window.SetActive(true); }break;//藍蘿蔔
            case 3:if (number_Seed.corn_seed > 0) { Confirmation_window.SetActive(true); }break;//玉米
            case 4:if (number_Seed.Diamond_corn_seed > 0) { Confirmation_window.SetActive(true); }break;//鑽石玉米
            case 5:if (number_Seed.moon_seed > 0){ Confirmation_window.SetActive(true); }break;//月亮夕顏
            case 6:if (number_Seed.sun_seed > 0) { Confirmation_window.SetActive(true); }break;//太陽朝露
        }
    }

    public void seed_F()//關閉確認窗口
    {
        Confirmation_window.SetActive(false);
        sour.clip = ui_Clip;
        sour.Play();
    }

    int seed_int;//紀錄玩家所選種子
    public void Seed_menu()//仲夏種子
    {
        switch (seed_int)
        {
            case 0://白蘿蔔
                if (number_Seed.White_radish_seed > 0)
                {
                    White_radish.GetComponent<seed>().land_Number(land_int);//告訴作物它是種在哪塊地的
                    White_radish.GetComponent<seed>().seed_status(0);//告訴作物它現在的生長階段
                    White_radish.GetComponent<seed>().crop_int(seed_int);//告訴作物它現在是甚麼作物
                    Instantiate(White_radish, land[land_int].transform.position, land[land_int].transform.rotation);
                    Planting_information_input();
                    number_Seed.White_radish_seed--;
                }
                break;
            case 1://紅蘿蔔
                if (number_Seed.carrot_seed>0)
                {
                    carrot.GetComponent<seed>().land_Number(land_int);//告訴作物它是種在哪塊地的
                    carrot.GetComponent<seed>().seed_status(0);//告訴作物它現在的生長階段
                    carrot.GetComponent<seed>().crop_int(seed_int);//告訴作物它現在是甚麼作物
                    Instantiate(carrot, land[land_int].transform.position, land[land_int].transform.rotation);
                    Planting_information_input();
                    number_Seed.carrot_seed--;
                }

                break;
            case 2://藍蘿蔔
                if (number_Seed.Blue_radish_seed>0)
                {
                    Blue_radish.GetComponent<seed>().crop_int(seed_int);//告訴作物它現在是甚麼作物
                    Blue_radish.GetComponent<seed>().land_Number(land_int);//告訴作物它是種在哪塊地的
                    Blue_radish.GetComponent<seed>().seed_status(0);//告訴作物它現在的生長階段
                    Instantiate(Blue_radish, land[land_int].transform.position, land[land_int].transform.rotation);
                    Planting_information_input();
                    number_Seed.Blue_radish_seed--;
                }

                break;
            case 3://玉米
                if (number_Seed.corn_seed>0)
                {
                    corn.GetComponent<seed>().crop_int(seed_int);//告訴作物它現在是甚麼作物
                    corn.GetComponent<seed>().land_Number(land_int);//告訴作物它是種在哪塊地的
                    corn.GetComponent<seed>().seed_status(0);//告訴作物它現在的生長階段
                    Instantiate(corn, land[land_int].transform.position, land[land_int].transform.rotation);
                    Planting_information_input();
                    number_Seed.corn_seed--;
                }

                break;
            case 4://鑽石玉米
                if (number_Seed.Diamond_corn_seed>0)
                {
                    Diamond_corn.GetComponent<seed>().crop_int(seed_int);//告訴作物它現在是甚麼作物
                    Diamond_corn.GetComponent<seed>().land_Number(land_int);//告訴作物它是種在哪塊地的
                    Diamond_corn.GetComponent<seed>().seed_status(0);//告訴作物它現在的生長階段
                    Instantiate(Diamond_corn, land[land_int].transform.position, land[land_int].transform.rotation);
                    Planting_information_input();
                    number_Seed.Diamond_corn_seed--;
                }

                break;
            case 5://月亮夕顏
                if (number_Seed.moon_seed>0)
                {
                    moon.GetComponent<seed>().crop_int(seed_int);//告訴作物它現在是甚麼作物
                    moon.GetComponent<seed>().land_Number(land_int);//告訴作物它是種在哪塊地的
                    moon.GetComponent<seed>().seed_status(0);//告訴作物它現在的生長階段
                    Instantiate(moon, land[land_int].transform.position, land[land_int].transform.rotation);
                    Planting_information_input();
                    number_Seed.moon_seed--;
                }

                break;
            case 6://太陽朝露
                if (number_Seed.sun_seed>0)
                {
                    sun.GetComponent<seed>().crop_int(seed_int);//告訴作物它現在是甚麼作物
                    sun.GetComponent<seed>().land_Number(land_int);//告訴作物它是種在哪塊地的
                    sun.GetComponent<seed>().seed_status(0);//告訴作物它現在的生長階段
                    Instantiate(sun, land[land_int].transform.position, land[land_int].transform.rotation);
                    Planting_information_input();
                    number_Seed.sun_seed--;
                }

                break;
        }

    }

    void Planting_information_input()//種植訊息輸入
    {
        sour.clip = Seed_Clip;
        sour.Play();
        land[land_int].GetComponent<Farming>().Seed_menu();//使該土地不可種植
        field_Information[land_int].Field_status = 1;//記錄這塊已有種植
        field_Information[land_int].seed_Number= seed_int;//紀錄土地種植作物為何
        field_Information[land_int].seed_status = 0;//紀錄該種子稱長階段
        Planting_game.SetActive(false);//關閉種子選單
        Confirmation_window.SetActive(false);//關閉確認窗口
        store();
    }


    void Humber_seed_text()
    {
        seed_text[0].text = number_Seed.White_radish_seed.ToString();
        seed_text[1].text = number_Seed.carrot_seed.ToString();
        seed_text[2].text = number_Seed.Blue_radish_seed.ToString();
        seed_text[3].text = number_Seed.corn_seed.ToString();
        seed_text[4].text = number_Seed.Diamond_corn_seed.ToString();
        seed_text[5].text = number_Seed.moon_seed.ToString();
        seed_text[6].text = number_Seed.sun_seed.ToString();
    }


    [Header("田地阻攔")]
    public GameObject[] Block;
    public void Block_off_fields()//夜晚封鎖田地
    {
        for (int i = 0; i < land.Length; i++)
        {
            land[i].GetComponent<Farming>().field_off();
        }
        Block[0].GetComponent<BoxCollider2D>().isTrigger = false;
        Block[1].GetComponent<BoxCollider2D>().isTrigger = false;
    }

    public void Cross_day()//遊戲中紀錄該種子稱長階段//跨日生長
    {
        for(int i=0;i< field_Information.Length; i++)
        {
            if (field_Information[i].Field_status == 1)//當田地有種植時
            {
                if (field_Information[i].seed_status< 20)//作物成長少於20在加避免unity爆掉
                {
                    field_Information[i].seed_status++;
                }
            }
            else { field_Information[i].seed_status = 0; }
        }
        for (int i = 0; i < land.Length; i++)
        {
            land[i].GetComponent<Farming>().field_open();
        }
        store();
        Block[0].GetComponent<BoxCollider2D>().isTrigger = true;
        Block[1].GetComponent<BoxCollider2D>().isTrigger = true;
    }


    public void Renew_fields(int land_No)//當玩家收割完之後更新田地資料
    {
        if (field_Information[land_No].Field_status==1)
        {
            field_Information[land_No].Field_status = 0;
            field_Information[land_No].seed_status = 0;
            field_Information[land_No].seed_Number = 0;
            sour.clip = harvest_Clip;
            sour.Play();
            land[land_No].GetComponent<Farming>().Seed_reply();//收割後使該土地可繼續種植
            store();
        }
    }

     public void store()//儲存土地是否種植
     {
        for (int i = 0; i < field_Information.Length; i++)
        {
        //將此player裡面的屬性轉成string(json格式)

        string saveString = JsonUtility.ToJson(field_Information[i]);

        //將字串saveString存到硬碟中

        StreamWriter file = new StreamWriter(System.IO.Path.Combine("Assets/Meridiandata", "Field_data" + i.ToString() + ".json"));

        file.Write(saveString);

        file.Close();
        }
        seed_store();//儲存現有種子
        crops_store();//儲存收成作物

    }

     public void Read()//讀取土地是否種植
     {
        for (int i = 0; i < field_Information.Length; i++)
        {
            //做一個讀取器
            //StreamReader類用於從流中讀取字符串。它繼承了TextReader類。它提供Read()和ReadLine()方法從流中讀取數據。
            StreamReader fileReader = new StreamReader(System.IO.Path.Combine("Assets/Meridiandata", "Field_data" + i.ToString() + ".json"));

            string stringJson = fileReader.ReadToEnd();

            fileReader.Close();

            //將讀取的string改成player物件型態
            field_Information[i] = JsonUtility.FromJson<Field_Information>(stringJson);
            //print("Name:" + p1.name);
        }
        Read_crop();// 讀取得田地如果有種植查看種植物是甚麼及生長階段
        seed_Read();//讀取種子數量
        Rops_Read();//讀取以收成作物
    }


    void Read_crop()//讀取得田地如果有種植查看種植物是甚麼及生長階段
    {
        for (int i = 0; i < field_Information.Length; i++)
        {
            if (field_Information[i].Field_status == 1)//當田地有種植時
            {
                Input_crop(field_Information[i].seed_Number, field_Information[i].seed_status, i);//將作物放置對應田地
                land[i].GetComponent<Farming>().Seed_menu();//使該土地不可種植
            }
        }
    }

    //將作物放置對應田地
    void Input_crop(int Input,int growth,int Field)//Input=作物編號  growth=生長階段  Field=田地
    {
        switch (Input)
        {
            case 0://白蘿蔔
                White_radish.GetComponent<seed>().crop_int(Input);//告訴作物它現在是甚麼作物
                White_radish.GetComponent<seed>().land_Number(Field);//告訴作物它是種在哪塊地的
                White_radish.GetComponent<seed>().seed_status(growth);//告訴作物它現在的生長階段
                Instantiate(White_radish, land[Field].transform.position, land[Field].transform.rotation);
                break;
            case 1://紅蘿蔔
                carrot.GetComponent<seed>().crop_int(Input);//告訴作物它現在是甚麼作物
                carrot.GetComponent<seed>().land_Number(Field);//告訴作物它是種在哪塊地的
                carrot.GetComponent<seed>().seed_status(growth);//告訴作物它現在的生長階段
                Instantiate(carrot, land[Field].transform.position, land[Field].transform.rotation);
                break;
            case 2://藍蘿蔔
                Blue_radish.GetComponent<seed>().crop_int(Input);//告訴作物它現在是甚麼作物
                Blue_radish.GetComponent<seed>().land_Number(Field);//告訴作物它是種在哪塊地的
                Blue_radish.GetComponent<seed>().seed_status(growth);//告訴作物它現在的生長階段
                Instantiate(Blue_radish, land[Field].transform.position, land[Field].transform.rotation);
                break;
            case 3://玉米
                corn.GetComponent<seed>().crop_int(Input);//告訴作物它現在是甚麼作物
                corn.GetComponent<seed>().land_Number(Field);//告訴作物它是種在哪塊地的
                corn.GetComponent<seed>().seed_status(growth);//告訴作物它現在的生長階段
                Instantiate(corn, land[Field].transform.position, land[Field].transform.rotation);
                break;
            case 4://鑽石玉米
                Diamond_corn.GetComponent<seed>().crop_int(Input);//告訴作物它現在是甚麼作物
                Diamond_corn.GetComponent<seed>().land_Number(Field);//告訴作物它是種在哪塊地的
                Diamond_corn.GetComponent<seed>().seed_status(growth);//告訴作物它現在的生長階段
                Instantiate(Diamond_corn, land[Field].transform.position, land[Field].transform.rotation);
                break;
            case 5://月亮
                moon.GetComponent<seed>().crop_int(Input);//告訴作物它現在是甚麼作物
                moon.GetComponent<seed>().land_Number(Field);//告訴作物它是種在哪塊地的
                moon.GetComponent<seed>().seed_status(growth);//告訴作物它現在的生長階段
                Instantiate(moon, land[Field].transform.position, land[Field].transform.rotation);
                break;
            case 6://太陽
                sun.GetComponent<seed>().crop_int(Input);//告訴作物它現在是甚麼作物
                sun.GetComponent<seed>().land_Number(Field);//告訴作物它是種在哪塊地的
                sun.GetComponent<seed>().seed_status(growth);//告訴作物它現在的生長階段
                Instantiate(sun, land[Field].transform.position, land[Field].transform.rotation);
                break;
        }
    }

    public void seed_store()//儲存種子數量
    {
        //將此player裡面的屬性轉成string(json格式)

        string saveString = JsonUtility.ToJson(number_Seed);

        //將字串saveString存到硬碟中

        StreamWriter file = new StreamWriter(System.IO.Path.Combine("Assets/Meridiandata", "Harvest_Seed" + ".json"));

        file.Write(saveString);

        file.Close();

    }

    public void seed_Read()//讀取種子數量
    {
        //StreamReader類用於從流中讀取字符串。它繼承了TextReader類。它提供Read()和ReadLine()方法從流中讀取數據。
        StreamReader fileReader = new StreamReader(System.IO.Path.Combine("Assets/Meridiandata", "Harvest_Seed"  + ".json"));

        string stringJson = fileReader.ReadToEnd();

        fileReader.Close();

        //將讀取的string改成player物件型態
        number_Seed = JsonUtility.FromJson< Humber_seed > (stringJson);
    }

    //存讀黨

    static public int White_radish_int;
    static public int carrot_int;
    static public int Blue_radish_int;
    static public int corn_int;
    static public int Diamond_corn_int;
    static public int moon_int;
    static public int sun_int;

    void update_crops()//轉換
    {
        White_radish_int= number_Rops.White_radish_int;
        carrot_int= number_Rops.carrot_int;
        Blue_radish_int= number_Rops.Blue_radish_int;
        corn_int= number_Rops.corn_int;
        Diamond_corn_int= number_Rops.Diamond_corn_int;
        moon_int= number_Rops.moon_int;
        sun_int= number_Rops.sun_int;
    }

    //收成數量
    public void harvest(int crop_int, int harvest_int)
    {
        switch (crop_int)
        {
            case 0:
                White_radish_int = White_radish_int + harvest_int;
                break;
            case 1:
                carrot_int = carrot_int + harvest_int;
                break;
            case 2:
                Blue_radish_int = Blue_radish_int + harvest_int;
                break;
            case 3:
                corn_int = corn_int + harvest_int;
                break;
            case 4:
                Diamond_corn_int = Diamond_corn_int + harvest_int;
                break;
            case 5:
                moon_int = moon_int + harvest_int;
                break;
            case 6:
                sun_int = sun_int + harvest_int;
                break;
        }
        store();
    }


    [Header("種子數量")]
    public Text[] seed_text;
    //種子數量
    public void harvest_seed(int crop_int, int harvest_int)
    {
        switch (crop_int)
        {
            case 0:
                number_Seed.White_radish_seed += harvest_int;
                break;
            case 1:
                number_Seed.carrot_seed += harvest_int;
                break;
            case 2:
                number_Seed.Blue_radish_seed += harvest_int;
                break;
            case 3:
                number_Seed.corn_seed += harvest_int;
                break;
            case 4:
                number_Seed.Diamond_corn_seed += harvest_int;
                break;
            case 5:
                number_Seed.moon_seed += harvest_int;
                break;
            case 6:
                number_Seed.sun_seed += harvest_int;
                break;
        }
        store();
    }

    public void crops_store()//儲存收成作物
    {
        number_Rops.White_radish_int=White_radish_int;
        number_Rops.carrot_int=carrot_int;
        number_Rops.Blue_radish_int=Blue_radish_int;
        number_Rops.corn_int=corn_int;
        number_Rops.Diamond_corn_int=Diamond_corn_int;
        number_Rops.moon_int=moon_int;
        number_Rops.sun_int=sun_int;

        //將此player裡面的屬性轉成string(json格式)

        string saveString = JsonUtility.ToJson(number_Rops);

        //將字串saveString存到硬碟中

        StreamWriter file = new StreamWriter(System.IO.Path.Combine("Assets/Meridiandata", "Harvest_crops" + ".json"));

        file.Write(saveString);

        file.Close();



    }

    public void Rops_Read()//讀取作物數量
    {
        //StreamReader類用於從流中讀取字符串。它繼承了TextReader類。它提供Read()和ReadLine()方法從流中讀取數據。
        StreamReader fileReader = new StreamReader(System.IO.Path.Combine("Assets/Meridiandata", "Harvest_crops" + ".json"));

        string stringJson = fileReader.ReadToEnd();

        fileReader.Close();

        //將讀取的string改成player物件型態
        number_Rops = JsonUtility.FromJson<Humber_rops>(stringJson);
        update_crops();//更新資料
    }

}

[System.Serializable]
public struct Field_Information //田地資料16
{
    [Header("紀錄是否有種植作物16")]
    [SerializeField] public int Field_status;
    [Header("紀錄種植作物編號16")]
    [SerializeField] public int seed_Number;
    [Header("紀錄種植作物成長階段16")]
    [SerializeField] public int seed_status;
}

[System.Serializable]
public struct Humber_seed //種子數量
{
    [Header("白蘿蔔種子")]
    [SerializeField] public int White_radish_seed;
    [Header("紅蘿蔔種子")]
    [SerializeField] public int carrot_seed;
    [Header("藍蘿蔔種子")]
    [SerializeField] public int Blue_radish_seed;
    [Header("玉米種子")]
    [SerializeField] public int corn_seed;
    [Header("鑽米種子")]
    [SerializeField] public int Diamond_corn_seed;
    [Header("月亮種子")]
    [SerializeField] public int moon_seed;
    [Header("太陽種子")]
    [SerializeField] public int sun_seed;
}

[System.Serializable]
public struct Humber_rops //作物數量
{

    [Header("白蘿蔔")]
    [SerializeField] public int White_radish_int;
    [Header("紅蘿蔔")]
    [SerializeField] public int carrot_int;
    [Header("藍蘿蔔")]
    [SerializeField] public int Blue_radish_int;
    [Header("玉米")]
    [SerializeField] public int corn_int;
    [Header("鑽米")]
    [SerializeField] public int Diamond_corn_int;
    [Header("月亮")]
    [SerializeField] public int moon_int;
    [Header("太陽")]
    [SerializeField] public int sun_int;
}
