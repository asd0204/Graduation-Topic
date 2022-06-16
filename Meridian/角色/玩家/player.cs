using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class player : MonoBehaviour
{
    Rigidbody2D r2d;//玩家體質
    Animator aor;//玩家動畫

    [Header("走路音效")]
    public AudioSource sour;//音效

    [Header("其他音效")]
    public AudioSource other_sound;
    [Header("受傷音效")]
    public AudioClip Hurt_Clip;
    [Header("拉弓音效")]
    public AudioClip draw_bow_Clip;
    [Header("放箭音效")]
    public AudioClip shoot_arrows_Clip;
    [Header("箭擊中音效")]
    public AudioClip hit_Clip;
    [Header("撿東西音效")]
    public AudioClip pick_things_Clip;
    [Header("使用藥水音效")]
    public AudioClip potion_Clip;


    [Header("玩家資料")]
    [SerializeField] Player_profile player_Profile;

   int index;//場景編號

    [Header("黑幕")]
    public GameObject Shady;

    [Header("中毒")]
    public GameObject poisoning_animation;
    [Header("受傷")]
    public GameObject injury_animation;

    // Start is called before the first frame update

    private void Awake()
    {
        //獲取編號
        index = PlayerPrefs.GetInt("出口");
        r2d = GetComponent<Rigidbody2D>();//抓取自身體質
        sour = GetComponent<AudioSource>();
        aor = GetComponent<Animator>();//抓取動畫撥放器
        speedMax = speed;
        hpMax = 100;
        //money = 300;

    }

    void Start()
    {
        if (index == 1||index==-1)//回到莊園
        {
            money_Read();//錢錢讀黨
            if (hp_float < 1)
            {
                hp_float = 1;
                this.GetComponent<map_system>().treat();
            }
            else { this.GetComponent<map_system>().Forest(); }
            index = 0;
            PlayerPrefs.SetInt("出口", index);
            index = SceneManager.GetActiveScene().buildIndex;
        }
        else
        {
            index = SceneManager.GetActiveScene().buildIndex;
            if (index >= 3)//還在迷宮裡
            {
                maze_information_Read();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (track==0)
        {
            mobile();//走路
        }
        life();//玩家生命
    }

    public int track;//是否暫停
    public void track_int(int int_int)//是否暫停
    {
        track = int_int;
    }

    [Header("移動速度"), Range(0, 50)]
    public float speed;//現有速度
    float speedMax;//速度最大值
    float x;
    float y;
    void mobile()//走路
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        r2d.velocity = (new Vector2(speed * x, speed * y));
        aor.SetFloat("上下行走", y);
        aor.SetFloat("左右行走", x);

    }
    public void SPEED_FLOAT(float speed_FLAT)//控制玩家速度
    {
        speed = speed_FLAT;
    }
    public void Walk_music()//走路音效
    {
        if (x != 0 || y != 0)
        {
            sour.Play();
        }
        else { sour.Stop(); }
    }

    [Header("血量"), Range(0, 100)]
    static public float hp_float;//現有血量
    float hpMax;//血量上限
    [Header("現有血量")]
    public Image blood;//血條標示
    [Header("血條效果")]
    public Image Blood_effect;

    public void Injuried(float harm)//玩家受傷
    {
        hp_float -= harm;
        other_sound.clip = Hurt_Clip;
        other_sound.Play();
        injury_animation.GetComponent<Player_injured>().injury();
    }


    [Header("GM系統")]
    public GameObject GAME_GM;
    void life()//玩家生命
    {
        blood.fillAmount = hp_float / hpMax;
        if (Blood_effect.fillAmount > blood.fillAmount)//效果>血條
        {
            Blood_effect.fillAmount -= 0.01f;
        }
        else { Blood_effect.fillAmount = blood.fillAmount; }

        if (hp_float<1)
        {
            Serious_injury_storage();//重傷儲存
            Shady.SetActive(true);
            CancelInvoke("poisoning");
            poisoning_animation.SetActive(false);
            if (index > 2)
            {
                treat();
            }

            if (index == 2)
            {
                DAY_int = GM.DAY_int;
                if (DAY_int < 15)
                {
                    this.GetComponent<map_system>().treat();
                    hp_float = 1;
                    money_store();
                }
                else
                {
                    //壞結局
                    GAME_GM.GetComponent<GM>().BAD_ending();
                }

            }

        }
    }

    public void poisoning_true()//開啟中毒狀態
    {
        poisoning_animation.SetActive(true);
        InvokeRepeating("poisoning", 1, 1);//狀態系統
    }


    [Header("中毒音效")]
    public AudioSource poisoning_sour;//音效
    public void poisoning()//中毒
    {
        poisoning_sour.Play();
        hp_float -= hpMax * 0.01f;
    }

    // int treat_int;

    public int DAY_int;
    public void treat()//殘血時回教堂
    {
        index = -1;
        PlayerPrefs.SetInt("出口", index);
        SceneManager.LoadScene(2);
    }


    public void draw_bow()//拉弓音效
    {
        other_sound.clip = draw_bow_Clip;
        other_sound.Play();
    }

    public void shoot_arrows()//放箭音效
    {
        other_sound.clip = shoot_arrows_Clip;
        other_sound.Play();
    }

    public void arrows()//弓箭擊中音效
    {
        other_sound.clip = shoot_arrows_Clip;
        other_sound.Play();
    }

    public void pick_things()//撿東西音效
    {
        other_sound.clip = pick_things_Clip;
        other_sound.Play();
    }

    public void potion(int harm)//使用藥水音效
    {
        if (harm==10)
        {
            if (cure>0)
            {
                harvest(0, -1);
                hp_float += harm;
                blood.fillAmount = hp_float / hpMax;
                other_sound.clip = potion_Clip;
                other_sound.Play();
            }

        }
        else
        {
            if (invigorating_blood>0)
            {
                harvest(1, -1);
                hp_float += harm;
                blood.fillAmount = hp_float / hpMax;
                other_sound.clip = potion_Clip;
                other_sound.Play();
                poisoning_animation.SetActive(false);
                CancelInvoke("poisoning");
            }

        }
        if (hp_float > hpMax)
        {
            hp_float = hpMax;
        }

    }


    public void maze_information_store()//迷宮資料儲存
    {
        Fully_stored();//完全儲存

        maze_store();//迷宮資料儲存
    }

    void maze_store()//迷宮資料儲存
    {
        //將此player裡面的屬性轉成string(json格式)

        string saveString = JsonUtility.ToJson(player_Profile);

        //將字串saveString存到硬碟中

        StreamWriter file = new StreamWriter(System.IO.Path.Combine("Assets/Meridiandata", "maze_information" + ".json"));

        file.Write(saveString);

        file.Close();
        Debug.Log("儲存");
    }

    public void maze_information_Read()//迷宮資料讀取
    {
        //StreamReader類用於從流中讀取字符串。它繼承了TextReader類。它提供Read()和ReadLine()方法從流中讀取數據。
        StreamReader fileReader = new StreamReader(System.IO.Path.Combine("Assets/Meridiandata", "maze_information" + ".json"));

        string stringJson = fileReader.ReadToEnd();

        fileReader.Close();

        //將讀取的string改成player物件型態
        player_Profile = JsonUtility.FromJson<Player_profile>(stringJson);

        Full_read();//完全讀取

    }

    public void money_store()//儲存資產
    {
        Fully_stored();//完全儲存
        monstore();//儲存資產
    }
    void monstore()//儲存資產
    {
        //將此player裡面的屬性轉成string(json格式)

        string saveString = JsonUtility.ToJson(player_Profile);

        //將字串saveString存到硬碟中

        StreamWriter file = new StreamWriter(System.IO.Path.Combine("Assets/Meridiandata", "Player_profile" + ".json"));

        file.Write(saveString);

        file.Close();

        maze_store();//同步更新迷宮暫存
    }



    public void money_Read()//讀取資產   meridiandata存檔資料夾
    {
        //StreamReader類用於從流中讀取字符串。它繼承了TextReader類。它提供Read()和ReadLine()方法從流中讀取數據。
        StreamReader fileReader = new StreamReader(System.IO.Path.Combine("Assets/Meridiandata", "Player_profile" + ".json"));

        string stringJson = fileReader.ReadToEnd();

        fileReader.Close();

        //將讀取的string改成player物件型態
        player_Profile = JsonUtility.FromJson<Player_profile>(stringJson);

        Full_read();//完全讀取
    }

    void Fully_stored()//完全儲存
    {
        player_Profile.hp = hp_float;
        player_Profile.money = money;

        player_Profile.cure_potionn = cure;//治療藥水
        player_Profile.invigorating_blood_potion = invigorating_blood;//活血藥水

        player_Profile.Jasper_beads = Jasper_beads_int;//碧玉珠
        player_Profile.morning_beads = morning_beads_int;//晨光珠
        player_Profile.flame_beads = flame_beads_int;//赤焰珠

        player_Profile.unknown_ore = unknown_ore_int;//不知名的礦石
        player_Profile.weird_ore = weird_ore_int;//詭異的礦石
        player_Profile.Magic_ore = Magic_ore_int;//魔力礦石

        player_Profile.Arrow_level = Bow_Arrow_level;//弓箭等級
        player_Profile.Huge_Arrow = Huge_int;//巨大箭矢
        player_Profile.LightFeather_Arrows = LightFeather_int;//輕羽箭矢
        player_Profile.Sweep_Arrows = Sweep_int;//掃除箭矢
        player_Profile.BreakingDemon_Arrows = BreakingDemon_int;//破魔箭矢
    }

    void Serious_injury_storage()//重傷儲存
    {
        player_Profile.hp = hp_float;
        player_Profile.money = money;

        player_Profile.cure_potionn = cure;//治療藥水
        player_Profile.invigorating_blood_potion = invigorating_blood;//活血藥水

        player_Profile.Huge_Arrow = Huge_int;//巨大箭矢
        player_Profile.LightFeather_Arrows = LightFeather_int;//輕羽箭矢
        player_Profile.Sweep_Arrows = Sweep_int;//掃除箭矢
        player_Profile.BreakingDemon_Arrows = BreakingDemon_int;//破魔箭矢
        monstore();//更新所有資料
    }

    void Full_read()//完全讀取
    {
        hp_float = player_Profile.hp;
        money = player_Profile.money;

        cure = player_Profile.cure_potionn;//治療藥水
        invigorating_blood = player_Profile.invigorating_blood_potion;//活血藥水

        Jasper_beads_int = player_Profile.Jasper_beads;//碧玉珠
        morning_beads_int = player_Profile.morning_beads; //晨光珠
        flame_beads_int = player_Profile.flame_beads;//赤焰珠

        unknown_ore_int = player_Profile.unknown_ore;//不知名的礦石
        weird_ore_int = player_Profile.weird_ore;//詭異的礦石
        Magic_ore_int = player_Profile.Magic_ore;//魔力礦石

        Bow_Arrow_level = player_Profile.Arrow_level;//弓箭等級
        Huge_int = player_Profile.Huge_Arrow;//巨大箭矢
        LightFeather_int = player_Profile.LightFeather_Arrows;//輕羽箭矢
        Sweep_int = player_Profile.Sweep_Arrows;//掃除箭矢
        BreakingDemon_int = player_Profile.BreakingDemon_Arrows;//破魔箭矢

        Display_number_props();//顯示道具數量
    }




    void Display_number_props()//顯示道具數量
    {
        cure_text.text = cure.ToString();//治療藥水
        invigorating_blood_text.text = invigorating_blood.ToString();//活血藥水
        Huge_text.text = Huge_int.ToString();//巨大箭矢
        LightFeather_text.text = LightFeather_int.ToString();//輕羽箭矢
        Sweep_text.text = Sweep_int.ToString();//掃除箭矢
        BreakingDemon_text.text = BreakingDemon_int.ToString();//破魔箭矢
    }

    [Header("武器庫")]
    public GameObject Armory;

    bool Item_slot_bool;
    public void Item_slot()//道具欄未顯示
    {
        if (Item_slot_bool)
        {
            Item_slot_bool = false;
            Armory.SetActive(false);
        }
        else { Armory.SetActive(true); Item_slot_bool = true; }
    }


    static public int money;

    public void money_system(int money_int)//金錢系統
    {
        money = money + money_int;
        money_store();
    }

    [Header("治療藥水")]
    public int cure;
    [Header("治療藥水文字")]
    public Text cure_text;
    [Header("活血藥水")]
    public int invigorating_blood;
    [Header("活血藥水文字")]
    public Text invigorating_blood_text;
    public void harvest(int crop_int, int harvest_int)
    {
        switch (crop_int)
        {
            case 0://治療藥水
                cure = cure + harvest_int;
                cure_text.text = cure.ToString();
                break;
            case 1://活血藥水
                invigorating_blood = invigorating_blood + harvest_int;
                invigorating_blood_text.text = invigorating_blood.ToString();
                break;
        }
        if (index==2)
        {
            money_store();
        }
        else
        {
            maze_information_store();
        }
    }


    [Header("巨大箭矢")]
    public int Huge_int;
    [Header("巨大箭矢文字")]
    public Text Huge_text;

    [Header("輕羽箭矢")]
    public int LightFeather_int;
    [Header("輕羽箭矢文字")]
    public Text LightFeather_text;

    [Header("掃除箭矢")]
    public int Sweep_int;
    [Header("掃除箭矢文字")]
    public Text Sweep_text;

    [Header("破魔箭矢")]
    public int BreakingDemon_int;
    [Header("破魔箭矢文字")]
    public Text BreakingDemon_text;

    public void weapon_table(int weapon_number, int Number_weapons)
    {
        if (Number_weapons==-1)//使用減少
        {
            switch (weapon_number)
            {
                case 1://巨大箭矢
                    if (Huge_int>0)
                    {
                        Huge_int = Huge_int + Number_weapons;
                        Huge_text.text = Huge_int.ToString();
                    }
                    else { weapon_number = 0;this.GetComponent<character_direction>().Arrow_type(weapon_number); }
                    break;
                case 2://輕羽箭矢
                    if (LightFeather_int > 0)
                    {
                        LightFeather_int = LightFeather_int + Number_weapons;
                        LightFeather_text.text = LightFeather_int.ToString();
                    }
                    else { weapon_number = 0; this.GetComponent<character_direction>().Arrow_type(weapon_number); }
                    break;
                case 3://掃除箭矢
                    if (Sweep_int > 0)
                    {
                        Sweep_int = Sweep_int + Number_weapons;
                        Sweep_text.text = Sweep_int.ToString();
                    }
                    else { weapon_number = 0; this.GetComponent<character_direction>().Arrow_type(weapon_number); }
                    break;
                case 4://破魔箭矢
                    if (BreakingDemon_int > 0)
                    {
                        BreakingDemon_int = BreakingDemon_int + Number_weapons;
                        BreakingDemon_text.text = BreakingDemon_int.ToString();
                    }
                    else { weapon_number = 0; this.GetComponent<character_direction>().Arrow_type(weapon_number); }
                    break;
            }
        }
        
        
        if (Number_weapons >= 1)//購買增加
        {
            switch (weapon_number)
            {
                case 1://巨大箭矢
                    Huge_int = Huge_int + Number_weapons;
                    Huge_text.text = Huge_int.ToString();
                    break;
                case 2://輕羽箭矢
                    LightFeather_int = LightFeather_int + Number_weapons;
                    LightFeather_text.text = LightFeather_int.ToString();
                    break;
                case 3://掃除箭矢
                    Sweep_int = Sweep_int + Number_weapons;
                    Sweep_text.text = Sweep_int.ToString();
                    break;
                case 4://破魔箭矢
                    BreakingDemon_int = BreakingDemon_int + Number_weapons;
                    BreakingDemon_text.text = BreakingDemon_int.ToString();
                    break;
            }
        }
        if (index == 2)
        {
            money_store();
        }
        else
        {
            maze_information_store();
        }
    }


    static public int Bow_Arrow_level;//弓箭等級
    public void Weapon_Upgrade()//武器升級系統
    {
        Bow_Arrow_level++;
        money_store();
    }


    static public int Jasper_beads_int;//碧玉珠

    static public int morning_beads_int;//晨光珠

    static public int flame_beads_int;//赤焰珠

    static public int unknown_ore_int;//不知名的礦石

    static public int weird_ore_int;//詭異的礦石

    static public int Magic_ore_int;//魔力礦石
    public void number_beads(int crop_int, int harvest_int)
    {
        switch (crop_int)
        {
            case 0://碧玉珠
                Jasper_beads_int = Jasper_beads_int + harvest_int;
                break;
            case 1://晨光珠
                morning_beads_int = morning_beads_int + harvest_int;
                break;
            case 2://赤焰珠
                flame_beads_int = flame_beads_int + harvest_int;
                break;
            case 3://不知名的礦石
                unknown_ore_int = unknown_ore_int + harvest_int;
                break;
            case 4://詭異的礦石
                weird_ore_int = weird_ore_int + harvest_int;
                break;
            case 5://魔力礦石
                Magic_ore_int = Magic_ore_int + harvest_int;
                break;
        }
        if (index == 2)
        {
            money_store();
        }
        else
        {
            maze_information_store();
        }
    }

}

[System.Serializable]
public struct Player_profile //玩家資料
{

    [Header("玩家錢錢")]
    [SerializeField] public int money;

    [Header("玩家生命")]
    [SerializeField] public float hp;



    [Header("治療藥水")]
    [SerializeField] public int cure_potionn;

    [Header("活血藥水")]
    [SerializeField] public int invigorating_blood_potion;



    [Header("碧玉珠")]
    [SerializeField] public int Jasper_beads;

    [Header("晨光珠")]
    [SerializeField] public int morning_beads;

    [Header("赤焰珠")]
    [SerializeField] public int flame_beads;



    [Header("不知名的礦石")]
    [SerializeField] public int unknown_ore;

    [Header("詭異的礦石")]
    [SerializeField] public int weird_ore;

    [Header("魔力礦石")]
    [SerializeField] public int Magic_ore;


    [Header("箭矢等級")]
    [SerializeField] public int Arrow_level;//弓箭等級;

    [Header("巨大箭矢")]
    [SerializeField] public int Huge_Arrow;

    [Header("輕羽箭矢")]
    [SerializeField] public int LightFeather_Arrows;

    [Header("掃除箭矢")]
    [SerializeField] public int Sweep_Arrows;

    [Header("破魔箭矢")]
    [SerializeField] public int BreakingDemon_Arrows;
}