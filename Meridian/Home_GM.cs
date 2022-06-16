using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Home_GM : MonoBehaviour
{
    [Header("天數")]
    public int DAY_int;//音效

    [Header("開始遊戲")]
    public GameObject start_game;

    [Header("新遊戲")]
    public GameObject new_game;

    [Header("載入遊戲")]
    public GameObject Load_game;   
    
    [Header("離開遊戲")]
    public GameObject Leave;



    [Header("確認初始化視窗")]
    public GameObject Initialize_window;

    AudioSource sour;//音效
    [Header("黑幕")]
    public GameObject Shady;   
    [Header("開場")]
    public GameObject opening;
    [Header("開場")]
    public GameObject opening_text;
    [Header("信封")]
    public GameObject envelope;
    public bool opening_bool;

    int End_int;//結局

    private void Awake()
    {
        End_int = PlayerPrefs.GetInt("結局");
        if (End_int != 0)
        {
            Planting(); //田地資料
            Chicken();//雞舍資料
            Grilled();//漁獲資料
            playerjson();//玩家資料

            //初始化
            Planting_store();
            Chicken_store();
            Grilled_store();
            player_store();
            PlayerPrefs.DeleteAll();

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        sour = GetComponent<AudioSource>();
        DAY_int = PlayerPrefs.GetInt("天數");

        if (DAY_int == 0)//新玩家
        {
            start_game.SetActive(true);
        }
        else
        {
            new_game.SetActive(true);
            Load_game.SetActive(true);
        }
    }

    private void Update()
    {
        if (opening_bool == true)
        {
            if (Input.anyKeyDown)
            {
                start_game.SetActive(false);
                Leave.SetActive(false);
                opening_text.SetActive(false);
                envelope.SetActive(true);
                opening_bool = false;
            }
        }
    }



    public void start_new_game()//開始遊戲
    {
        sour.Play();
        opening_bool =true;
        opening.SetActive(true);
    }



    public void loading_progress()//加載遊戲進度
    {
        sour.Play();
        SceneManager.LoadScene(2);
    }

    public void Game_initialization()//初始化遊戲
    {
        sour.Play();
        Initialize_window.SetActive(true);

        Planting(); //田地資料
        Chicken();//雞舍資料
        Grilled();//漁獲資料
        playerjson();//玩家資料
    }

    public void Initialize_yes()//確認初始化
    {
        sour.Play();
        Planting_store();
        Chicken_store();
        Grilled_store();
        player_store();
        PlayerPrefs.DeleteAll();
        Shady.SetActive(true);
        Invoke("LoadScene", 0.2F);
    }

    public void Initialize_no()//不初始化
    {
        sour.Play();
        Initialize_window.SetActive(false);
    }

    void LoadScene()
    {
        SceneManager.LoadScene(0);
    }

    public void Leave_game()//離開遊戲
    {
        sour.Play();
        Shady.SetActive(true);
        Invoke("Quit_GAME", 0.2F);
    }

    void Quit_GAME()
    {
        Application.Quit();
    }

    [Header("田地資料")]
    [SerializeField] Field_Information[] field_Information;//田地資料
    [Header("種子數量")]
    [SerializeField] Humber_seed number_Seed;
    [Header("作物數量")]
    [SerializeField] Humber_rops number_Rops;
    void Planting()//田地資料
    {
        for (int i = 0; i < field_Information.Length; i++)
        {
            //做一個讀取器
            //StreamReader類用於從流中讀取字符串。它繼承了TextReader類。它提供Read()和ReadLine()方法從流中讀取數據。
            StreamReader field = new StreamReader(System.IO.Path.Combine("Assets/Meridiandata", "Field_data" + i.ToString() + ".json"));

            string fieldJson = field.ReadToEnd();

            field.Close();

            //將讀取的string改成player物件型態
            field_Information[i] = JsonUtility.FromJson<Field_Information>(fieldJson);
            //print("Name:" + p1.name);




            field_Information[i].Field_status = 0;
            field_Information[i].seed_Number = 0;
            field_Information[i].seed_status = 0;
        }




        //歸零種子資料
        //StreamReader類用於從流中讀取字符串。它繼承了TextReader類。它提供Read()和ReadLine()方法從流中讀取數據。
        StreamReader Seed = new StreamReader(System.IO.Path.Combine("Assets/Meridiandata", "Harvest_Seed" + ".json"));

        string SeedJson = Seed.ReadToEnd();

        Seed.Close();
        //將讀取的string改成player物件型態
        number_Seed = JsonUtility.FromJson<Humber_seed>(SeedJson);


        number_Seed.White_radish_seed = 0;
        number_Seed.carrot_seed = 0;
        number_Seed.Blue_radish_seed = 0;
        number_Seed.corn_seed = 0;
        number_Seed.Diamond_corn_seed = 0;
        number_Seed.moon_seed = 0;
        number_Seed.sun_seed = 0;



        //歸零作物資料

        //StreamReader類用於從流中讀取字符串。它繼承了TextReader類。它提供Read()和ReadLine()方法從流中讀取數據。
        StreamReader Rops = new StreamReader(System.IO.Path.Combine("Assets/Meridiandata", "Harvest_crops" + ".json"));

        string RopsJson = Rops.ReadToEnd();

        Rops.Close();

        //將讀取的string改成player物件型態
        number_Rops = JsonUtility.FromJson<Humber_rops>(RopsJson);


        number_Rops.White_radish_int = 0;
        number_Rops.carrot_int = 0;
        number_Rops.Blue_radish_int = 0;
        number_Rops.corn_int = 0;
        number_Rops.Diamond_corn_int = 0;
        number_Rops.moon_int = 0;
        number_Rops.sun_int = 0;
    }

    [Header("雞舍資料")]
    [SerializeField] Egg egg;
    void Chicken()//雞舍資料
    {
        //StreamReader類用於從流中讀取字符串。它繼承了TextReader類。它提供Read()和ReadLine()方法從流中讀取數據。
        StreamReader ChickenReader = new StreamReader(System.IO.Path.Combine("Assets/Meridiandata", "Harvest_egg" + ".json"));

        string ChickenJson = ChickenReader.ReadToEnd();

        ChickenReader.Close();

        //將讀取的string改成player物件型態
        egg = JsonUtility.FromJson<Egg>(ChickenJson);



        egg.Regular_egg = 0;
        egg.Silver_egg = 0;
        egg.Golden_egg = 0;
        egg.EGG_int = 0;
    }


    [Header("漁獲資料")]
    [SerializeField] Grilled grilled;//漁獲資料
    void Grilled()//漁獲資料
    {
        //StreamReader類用於從流中讀取字符串。它繼承了TextReader類。它提供Read()和ReadLine()方法從流中讀取數據。
        StreamReader fileReader = new StreamReader(System.IO.Path.Combine("Assets/Meridiandata", "Harvest_grilled" + ".json"));

        string stringJson = fileReader.ReadToEnd();

        fileReader.Close();

        //將讀取的string改成player物件型態
        grilled = JsonUtility.FromJson<Grilled>(stringJson);



        grilled.Baked_successfully = 0;
        grilled.Baking_failed = 0;
        grilled.Number_fish = 0;
    }


    [Header("玩家資料")]
    [SerializeField] Player_profile player_Profile;
    void playerjson()
    {
        //StreamReader類用於從流中讀取字符串。它繼承了TextReader類。它提供Read()和ReadLine()方法從流中讀取數據。
        StreamReader fileReader = new StreamReader(System.IO.Path.Combine("Assets/Meridiandata", "Player_profile" + ".json"));

        string stringJson = fileReader.ReadToEnd();

        fileReader.Close();
        //將讀取的string改成player物件型態
        player_Profile = JsonUtility.FromJson<Player_profile>(stringJson);


        player_Profile.money = 300;
        player_Profile.hp = 100;
        player_Profile.cure_potionn = 0;
        player_Profile.invigorating_blood_potion = 0;
        player_Profile.Jasper_beads = 0;
        player_Profile.morning_beads = 0;
        player_Profile.flame_beads = 0;
        player_Profile.unknown_ore = 0;
        player_Profile.weird_ore = 0;
        player_Profile.Magic_ore = 0;
        player_Profile.Arrow_level = 0;
        player_Profile.Huge_Arrow = 0;//巨大箭矢
        player_Profile.LightFeather_Arrows = 0;//輕羽箭矢
        player_Profile.Sweep_Arrows = 0;//掃除箭矢
        player_Profile.BreakingDemon_Arrows = 0;//破魔箭矢
    }




    //刷新檔案資料

    void Planting_store()//儲存田地資料
    {
        for (int i = 0; i < field_Information.Length; i++)
        {
            //將此player裡面的屬性轉成string(json格式)

            string fieldString = JsonUtility.ToJson(field_Information[i]);

            //將字串saveString存到硬碟中

            StreamWriter field = new StreamWriter(System.IO.Path.Combine("Assets/Meridiandata", "Field_data" + i.ToString() + ".json"));

            field.Write(fieldString);

            field.Close();
        }


        //將此player裡面的屬性轉成string(json格式)

        string SeedString = JsonUtility.ToJson(number_Seed);

        //將字串saveString存到硬碟中

        StreamWriter Seed = new StreamWriter(System.IO.Path.Combine("Assets/Meridiandata", "Harvest_Seed" + ".json"));

        Seed.Write(SeedString);

        Seed.Close();



        //將此player裡面的屬性轉成string(json格式)

        string cropsString = JsonUtility.ToJson(number_Rops);

        //將字串saveString存到硬碟中

        StreamWriter crops = new StreamWriter(System.IO.Path.Combine("Assets/Meridiandata", "Harvest_crops" + ".json"));

        crops.Write(cropsString);

        crops.Close();
    }


    void Chicken_store()//雞舍資料
    {
        //將此player裡面的屬性轉成string(json格式)

        string ChickenString = JsonUtility.ToJson(egg);

        //將字串saveString存到硬碟中

        StreamWriter ChickenWriter = new StreamWriter(System.IO.Path.Combine("Assets/Meridiandata", "Harvest_egg" + ".json"));

        ChickenWriter.Write(ChickenString);

        ChickenWriter.Close();
    }

    void Grilled_store()//漁獲資料
    {
        //將此player裡面的屬性轉成string(json格式)

        string GrilledString = JsonUtility.ToJson(grilled);

        //將字串saveString存到硬碟中

        StreamWriter GrilledWriter = new StreamWriter(System.IO.Path.Combine("Assets/Meridiandata", "Harvest_grilled" + ".json"));

        GrilledWriter.Write(GrilledString);

        GrilledWriter.Close();
    }

    void player_store()//玩家資料
    {
        //將此player裡面的屬性轉成string(json格式)

        string saveString = JsonUtility.ToJson(player_Profile);

        //將字串saveString存到硬碟中

        StreamWriter file = new StreamWriter(System.IO.Path.Combine("Assets/Meridiandata", "Player_profile" + ".json"));

        file.Write(saveString);

        file.Close();
    }
}
