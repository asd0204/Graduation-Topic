using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class seed : MonoBehaviour
{
    //作物生長系統

    [Header("種田管理器")]
    public GameObject Planting_system;

    public SpriteRenderer Crop_obj;
    [Header("作物成長圖")]
    public Sprite[] Crop_growth;

    public int seed_int;//成長階段
    [Header("成長階段最大值")]
    public int seed_MAX;//成長階段最大值

    [Header("顯示可收穫")]
    public GameObject harvestable_OBJ;

    // [Header("土地")]
    // public GameObject[] land;
    public int land_No;//土地編號用於紀錄該作物長在哪塊地上

    // Start is called before the first frame update
    void Start()
    {
        Crop_obj = GetComponent<SpriteRenderer>();
       // land = GameObject.FindGameObjectsWithTag("田地");
        Planting_system = GameObject.Find("田地系統");
    }

    private void Update()
    {
        if (Harvestable&&Input.GetMouseButtonDown(0)|| Harvestable && Input.GetKeyDown(KeyCode.Space))
        {
            if (seed_int >= seed_MAX)
            {
                //告訴gm玩家收成蘿蔔加多少
                Debug.Log(crop_number);
                Planting_system.GetComponent<Planting_system>().harvest(crop_number, Harvest_quantity);//告訴gm玩家收成蘿蔔加多少
                //land[land_No].GetComponent<Farming>().Seed_reply();//收割後使該土地可繼續種植
                Planting_system.GetComponent<Planting_system>().Renew_fields(land_No);//當玩家收割完之後更新田地資料
                harvestable_OBJ.SetActive(false);
                Destroy(this.gameObject);//收成完刪除
            }
        }
    }

    public int crop_number;//作物編號
    public void crop_int(int number)//抓取作物編號
    {
        crop_number = number;
    }

    bool Harvestable;//可收成
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Player"&& seed_int >= seed_MAX)
        {
            Harvestable = true;
            harvestable_OBJ.SetActive(true);
            Debug.Log(" Harvestable");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Harvestable)
            {
                Harvestable = false;
                harvestable_OBJ.SetActive(false);
            }
        }
    }

    public int Harvest_quantity;//收成數量

    public void seed_status(int status)//如果是讀取的作物檢查生長階段
    {
        seed_int = status;
        if (seed_int < seed_MAX)
        {
            if (seed_int > 0)
            {
                Crop_obj.sprite = Crop_growth[seed_int];//該階段作物圖
            }
            else
            {
                Crop_obj.sprite = Crop_growth[0];//該階段作物圖

            }
        }
        if (seed_int >= seed_MAX)
        {
           // seed_bool = true;
            Crop_obj.sprite = Crop_growth[seed_MAX];//該階段作物圖
            Harvest_quantity = Random.Range(1, 3);//決定玩家收成幾個
        }
    }

    public bool seed_bool;
    public void Cross_day()//判斷是否成長完成沒完成繼續生長
    {
        if (seed_int < seed_MAX)
        {
            seed_int++;
            Crop_obj.sprite = Crop_growth[seed_int];//該階段作物圖
            if (seed_int >= seed_MAX )
            {
                //seed_bool = true;
                Crop_obj.sprite = Crop_growth[seed_MAX];//該階段作物圖
               Harvest_quantity = Random.Range(1, 3);//決定玩家收成幾個
            }
        }
    }


    public void land_Number(int Number)//紀錄作物種在哪塊地上以方便收割時回復土地
    {
        land_No = Number;
    }
}
