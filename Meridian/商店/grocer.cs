using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class grocer : MonoBehaviour
{
    //處理雜貨店相關

    [Header("店鋪介面")]
    public GameObject shop_interface;

    [Header("種田管理器")]
    public GameObject Planting;

    [Header("烤魚系統")]
    public GameObject fish;

    [Header("雞舍系統")]
    public GameObject Chicken;

    [Header("遊戲管理器")]
    public GameObject GM_system;

    [Header("玩家錢錢")]
    public int money;

    [Header("玩家錢錢文字")]
    public Text playmoney_text;

    [Header("玩家")]
    public GameObject play_obj;

    [Header("音效")]
    public AudioSource sour;//音效

    [Header("購買音效")]
    public AudioClip Buy_Clip;//音效
    [Header("UI音效")]
    public AudioClip ui_Clip;//音效

    //public GameObject a,b,c;
    private void Start()
    {
        Planting = GameObject.Find("田地系統");
        fish = GameObject.Find("烤魚系統");
        Chicken = GameObject.Find("雞舍系統");
        GM_system = GameObject.Find("GM系統");
        sour = GetComponent<AudioSource>();
        //Instantiate(a, b.transform, false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")//當玩家碰到櫃子開啟店鋪介面
        {
            shop_interface.SetActive(true);
            Sales_window.SetActive(false);
            money = player.money;
            playmoney_text.text = money.ToString();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")//當玩家離開櫃子關閉店鋪介面
        {
            shop_interface.SetActive(false);
        }
    }

    public void Closeshop_interface()//關閉店鋪介面
    {
        shop_interface.SetActive(false);
        sour.clip = ui_Clip;
        sour.Play();
    }


    //購買程式

    [Header("購買確認窗")]
    public GameObject Purchase_confirmation;
    [Header("購買文字")]
    public Text Purchase_text;
    [Header("商品編號")]
    public int Product_Number;
    public void commodity(int commodity_int)//商店商品選項
    {
        Purchase_confirmation.SetActive(true);
        Product_Number = commodity_int;
        sour.clip = ui_Clip;
        sour.Play();
        switch (commodity_int)
        {
            case 0://白蘿蔔
                Purchase_text.text = "是否購買白蘿蔔種子?";
                break;
            case 1://紅蘿蔔
                Purchase_text.text = "是否購買紅蘿蔔種子?";
                break;
            case 2://藍蘿蔔
                Purchase_text.text = "是否購買藍蘿蔔種子?";
                break;
            case 3://玉米
                Purchase_text.text = "是否購買玉米種子?";
                break;
            case 4://鑽石玉米
                Purchase_text.text = "是否購買鑽石玉米種子?";
                break;
            case 5://月亮
                Purchase_text.text = "是否購買夕顏種子?";
                break;
            case 6://太陽
                Purchase_text.text = "是否購買朝露種子?";
                break;

        }
        Purchase_quantity = 1;
        quantity_text.text = Purchase_quantity.ToString();
        money_text.color = new Color(0f, 0f, 0f, 255f);
        Amount_Quote();
    }

    public void commodity_no()//關閉購買確認窗
    {
        Purchase_confirmation.SetActive(false);
        sour.clip = ui_Clip;
        sour.Play();
    }

    [Header("購買數量文字")]
    public Text quantity_text;
    [Header("購買金額文字")]
    public Text money_text;
    int Purchase_quantity=1;//購買數量
    public void increase_decrease(int quantity_int)//增減數量
    {
        sour.clip = ui_Clip;
        sour.Play();
        if (Purchase_quantity<99&& Purchase_quantity >= 1)
        {
            Purchase_quantity = Purchase_quantity + quantity_int;
            if (Purchase_quantity == 0)
            {
                Purchase_quantity = 1;
            }
            quantity_text.text = Purchase_quantity.ToString();
            Amount_Quote();
        }

    }

    void Amount_Quote()//估價
    {
        switch (Product_Number)
        {
            case 0://白蘿蔔
                if (money >= 10 * Purchase_quantity)
                {
                    money_text.text = "所需金額：" + (10 * Purchase_quantity).ToString();
                    money_text.color = new Color(0f, 0f, 0f, 255f);
                }
                else
                {
                    money_text.text = "所需金額：" + (10 * Purchase_quantity).ToString();
                    money_text.color = new Color(255f, 0f, 0f, 255f);
                }
                break;
            case 1://紅蘿蔔
                if (money >= 20 * Purchase_quantity)
                {
                    money_text.text = "所需金額：" + (20 * Purchase_quantity).ToString();
                    money_text.color = new Color(0f, 0f, 0f, 255f);
                }
                else
                {
                    money_text.text = "所需金額：" + (20 * Purchase_quantity).ToString();
                    money_text.color = new Color(255f, 0f, 0f, 255f);
                }
                break;
            case 2://藍蘿蔔
                if (money >= 30 * Purchase_quantity)
                {
                    money_text.text = "所需金額：" + (30 * Purchase_quantity).ToString();
                    money_text.color = new Color(0f, 0f, 0f, 255f);
                }
                else
                {
                    money_text.text = "所需金額：" + (30 * Purchase_quantity).ToString();
                    money_text.color = new Color(255f, 0f, 0f, 255f);
                }
                break;
            case 3://玉米
                if (money >= 40 * Purchase_quantity)
                {
                    money_text.text = "所需金額：" + (40 * Purchase_quantity).ToString();
                    money_text.color = new Color(0f, 0f, 0f, 255f);
                }
                else
                {
                    money_text.text = "所需金額：" + (40 * Purchase_quantity).ToString();
                    money_text.color = new Color(255f, 0f, 0f, 255f);
                }
                break;
            case 4://鑽石玉米
                if (money >= 50 * Purchase_quantity)
                {
                    money_text.text = "所需金額：" + (50 * Purchase_quantity).ToString();
                    money_text.color = new Color(0f, 0f, 0f, 255f);
                }
                else
                {
                    money_text.text = "所需金額：" + (50 * Purchase_quantity).ToString();
                    money_text.color = new Color(255f, 0f, 0f, 255f);
                }
                break;
            case 5://月亮
                if (money >= 60 * Purchase_quantity)
                {
                    money_text.text = "所需金額：" + (60 * Purchase_quantity).ToString();
                    money_text.color = new Color(0f, 0f, 0f, 255f);
                }
                else
                {
                    money_text.text = "所需金額：" + (60 * Purchase_quantity).ToString();
                    money_text.color = new Color(255f, 0f, 0f, 255f);
                }
                break;
            case 6://太陽
                if (money >= 70 * Purchase_quantity)
                {
                    money_text.text = "所需金額：" + (70 * Purchase_quantity).ToString();
                    money_text.color = new Color(0f, 0f, 0f, 255f);
                }
                else
                {
                    money_text.text = "所需金額：" + (70 * Purchase_quantity).ToString();
                    money_text.color = new Color(255f, 0f, 0f, 255f);
                }
                break;

        }
    }

    public void purchase_window()//購買商店商品
    {
        if (Purchase_quantity > 0)
        {
            switch (Product_Number)
            {
                case 0://白蘿蔔
                    if (money >= 10 * Purchase_quantity)
                    {
                        Planting.GetComponent<Planting_system>().harvest_seed(Product_Number, Purchase_quantity);
                        play_obj.GetComponent<player>().money_system(-(10 * Purchase_quantity));
                        Purchase_confirmation.SetActive(false);
                        sour.clip = Buy_Clip;
                        sour.Play();
                    }
                    else { 
                        money_text.text = "你的錢錢不夠喔~在努力賺錢吧~"; 
                        sour.clip = ui_Clip;
                        sour.Play();
                    }
                    break;
                case 1://紅蘿蔔
                    if (money >= 20 * Purchase_quantity)
                    {
                        Planting.GetComponent<Planting_system>().harvest_seed(Product_Number, Purchase_quantity);
                        play_obj.GetComponent<player>().money_system(-(20 * Purchase_quantity));
                        Purchase_confirmation.SetActive(false);
                        sour.clip = Buy_Clip;
                        sour.Play();
                    }
                    else { 
                        money_text.text = "你的錢錢不夠喔~在努力賺錢吧~";
                        sour.clip = ui_Clip;
                        sour.Play();
                    }
                    break;
                case 2://藍蘿蔔
                    if (money >= 30 * Purchase_quantity)
                    {
                        Planting.GetComponent<Planting_system>().harvest_seed(Product_Number, Purchase_quantity);
                        play_obj.GetComponent<player>().money_system(-(30 * Purchase_quantity));
                        Purchase_confirmation.SetActive(false);
                        sour.clip = Buy_Clip;
                        sour.Play();
                    }
                    else { 
                        money_text.text = "你的錢錢不夠喔~在努力賺錢吧~";
                        sour.clip = ui_Clip;
                        sour.Play();
                    }
                    break;
                case 3://玉米
                    if (money >= 40 * Purchase_quantity)
                    {
                        Planting.GetComponent<Planting_system>().harvest_seed(Product_Number, Purchase_quantity);
                        play_obj.GetComponent<player>().money_system(-(40 * Purchase_quantity));
                        Purchase_confirmation.SetActive(false);
                        sour.clip = Buy_Clip;
                        sour.Play();
                    }
                    else {
                        money_text.text = "你的錢錢不夠喔~在努力賺錢吧~";
                        sour.clip = ui_Clip;
                        sour.Play();
                    }
                    break;
                case 4://鑽石玉米
                    if (money >= 50 * Purchase_quantity)
                    {
                        Planting.GetComponent<Planting_system>().harvest_seed(Product_Number, Purchase_quantity);
                        play_obj.GetComponent<player>().money_system(-(50 * Purchase_quantity));
                        Purchase_confirmation.SetActive(false);
                        sour.clip = Buy_Clip;
                        sour.Play();
                    }
                    else {
                        money_text.text = "你的錢錢不夠喔~在努力賺錢吧~";
                        sour.clip = ui_Clip;
                        sour.Play();
                    }
                    break;
                case 5://月亮
                    if (money >= 60 * Purchase_quantity)
                    {
                        Planting.GetComponent<Planting_system>().harvest_seed(Product_Number, Purchase_quantity);
                        play_obj.GetComponent<player>().money_system(-(60 * Purchase_quantity));
                        Purchase_confirmation.SetActive(false);
                        sour.clip = Buy_Clip;
                        sour.Play();
                    }
                    else {
                        money_text.text = "你的錢錢不夠喔~在努力賺錢吧~";
                        sour.clip = ui_Clip;
                        sour.Play();
                    }
                    break;
                case 6://太陽
                    if (money >= 70 * Purchase_quantity)
                    {
                        Planting.GetComponent<Planting_system>().harvest_seed(Product_Number, Purchase_quantity);
                        play_obj.GetComponent<player>().money_system(-(70 * Purchase_quantity));
                        Purchase_confirmation.SetActive(false);
                        sour.clip = Buy_Clip;
                        sour.Play();
                    }
                    else { 
                        money_text.text = "你的錢錢不夠喔~在努力賺錢吧~";
                        sour.clip = ui_Clip;
                        sour.Play();
                    }
                    break;

            }
            money = player.money;
            playmoney_text.text = money.ToString();
        }
        else { 
            money_text.text = "你還未輸入購買數量唷~";
            sour.clip = ui_Clip;
            sour.Play();
        }

    }




    //販售程式




    [Header("販售視窗")]
    public GameObject Sales_window;

    [Header("白蘿蔔")]
    public GameObject White_radish;

    [Header("紅蘿蔔")]
    public GameObject carrot;

    [Header("藍蘿蔔")]
    public GameObject Blue_radish;

    [Header("玉米")]
    public GameObject corn;

    [Header("鑽米")]
    public GameObject Diamond_corn;

    [Header("月亮")]
    public GameObject moon;

    [Header("太陽")]
    public GameObject sun;



    [Header("一般雞蛋")]
    public GameObject Regular_egg;
    [Header("銀色雞蛋")]
    public GameObject Silver_egg;
    [Header("金色雞蛋")]
    public GameObject Golden_egg;



    [Header("新鮮烤魚")]
    public GameObject Number_fish;
    [Header("香香烤魚")]
    public GameObject Baked_successfully;
    [Header("失敗烤魚")]
    public GameObject Baking_failed;


    public void Sales_no()//不銷售物品
    {
        Sales_window.SetActive(false);
        sour.clip = ui_Clip;
        sour.Play();
    }
        public void Sales()
    {
        Sales_window.SetActive(true);
        Check_available();
        sour.clip = ui_Clip;
        sour.Play();
    }

    void Check_available()//檢查可銷售物品
    {
        //作物
        if (Planting_system.White_radish_int > 0)
        {
            White_radish.SetActive(true);
        }
        else { White_radish.SetActive(false); }

        if (Planting_system.carrot_int > 0)
        {
            carrot.SetActive(true);
        }
        else { carrot.SetActive(false); }

        if (Planting_system.Blue_radish_int > 0)
        {
            Blue_radish.SetActive(true);
        }
        else { Blue_radish.SetActive(false); }

        if (Planting_system.corn_int > 0)
        {
            corn.SetActive(true);
        }
        else { corn.SetActive(false); }

        if (Planting_system.Diamond_corn_int > 0)
        {
            Diamond_corn.SetActive(true);
        }
        else { Diamond_corn.SetActive(false); }

        if (Planting_system.moon_int > 0)
        {
            moon.SetActive(true);
        }
        else { moon.SetActive(false); }

        if (Planting_system.sun_int > 0)
        {
            sun.SetActive(true);
        }
        else { sun.SetActive(false); }



        //蛋
        if (Chicken_coop.Golden_int > 0)
        {
            Golden_egg.SetActive(true);
        }
        else { Golden_egg.SetActive(false); }

        if (Chicken_coop.Silver_int > 0)
        {
            Silver_egg.SetActive(true);
        }
        else { Silver_egg.SetActive(false); }

        if (Chicken_coop.Regular_int > 0)
        {
            Regular_egg.SetActive(true);
        }
        else { Regular_egg.SetActive(false); }


        //魚
        if (Grilled_fish.Baked_successfully_int > 0)
        {
            Baked_successfully.SetActive(true);
        }
        else { Baked_successfully.SetActive(false); }

        if (Grilled_fish.Baking_failed_int > 0)
        {
            Baking_failed.SetActive(true);
        }
        else { Baking_failed.SetActive(false); }

        if (Grilled_fish.Number_fish_int > 0)
        {
            Number_fish.SetActive(true);
        }
        else { Number_fish.SetActive(false); }
    }

    [Header("販售確認窗")]
    public GameObject Sales_confirmation;
    [Header("販售文字")]
    public Text Sales_text;
    [Header("現有庫存文字")]
    public Text in_stock;

    public void confirmation_no()//不販售
    {
        Sales_confirmation.SetActive(false);
    }

    public void Confirm_sale(int Confirm_int)//確認販售物
    {
        Sales_confirmation.SetActive(true);
        Product_Number = Confirm_int;
        sour.clip = ui_Clip;
        sour.Play();
        switch (Confirm_int)
        {
            case 0://白蘿蔔
                Sales_text.text = "販售白蘿蔔";
                in_stock.text = "現有庫存：" + Planting_system.White_radish_int.ToString();
                break;
            case 1://紅蘿蔔
                Sales_text.text = "販售紅蘿蔔";
                in_stock.text = "現有庫存：" + Planting_system.carrot_int.ToString();
                break;
            case 2://藍蘿蔔
                Sales_text.text = "販售藍蘿蔔";
                in_stock.text = "現有庫存：" + Planting_system.Blue_radish_int.ToString();
                break;
            case 3://玉米
                Sales_text.text = "販售玉米";
                in_stock.text = "現有庫存：" + Planting_system.corn_int.ToString();
                break;
            case 4://鑽石玉米
                Sales_text.text = "販售鑽石玉米";
                in_stock.text = "現有庫存：" + Planting_system.Diamond_corn_int.ToString();
                break;
            case 5://月亮
                Sales_text.text = "販售夕顏";
                in_stock.text = "現有庫存：" + Planting_system.moon_int.ToString();
                break;
            case 6://太陽
                Sales_text.text = "販售朝露";
                in_stock.text = "現有庫存：" + Planting_system.sun_int.ToString();
                break;
            case 7://一般蛋
                Sales_text.text = "販售一般雞蛋";
                in_stock.text = "現有庫存：" + Chicken_coop.Regular_int.ToString();
                Debug.Log(Chicken_coop.Regular_int);
                break;
            case 8://銀蛋
                Sales_text.text = "販售銀色雞蛋";
                in_stock.text = "現有庫存：" + Chicken_coop.Silver_int.ToString();
                break;
            case 9://金蛋
                Sales_text.text = "販售金色雞蛋";
                in_stock.text = "現有庫存：" + Chicken_coop.Golden_int.ToString();
                break;
            case 10://新鮮魚肉
                Sales_text.text = "販售新鮮魚肉";
                in_stock.text = "現有庫存：" + Grilled_fish.Number_fish_int.ToString();
                break;
            case 11://香香烤魚
                Sales_text.text = "販售香香烤魚";
                in_stock.text = "現有庫存：" + Grilled_fish.Baked_successfully_int.ToString();
                break;
            case 12://失敗烤魚
                Sales_text.text = "販售失敗烤魚";
                in_stock.text = "現有庫存：" + Grilled_fish.Baking_failed_int.ToString();
                break;

        }
        volumeint_text.text = Sales_volume.ToString();
        Valuation();
    }

    [Header("銷售數量文字")]
    public Text volumeint_text;
    [Header("銷售金額文字")]
    public Text volume_text;
    int Sales_volume;//蕭售數量

    public void Valuation_increase_decrease(int quantity_int)//增減數量
    {
        sour.clip = ui_Clip;
        sour.Play();
        switch (Product_Number)
            {
                case 0://白蘿蔔
                    if (Sales_volume < Planting_system.White_radish_int || Sales_volume == Planting_system.White_radish_int && quantity_int == -1)
                    {
                        Sales_volume = Sales_volume + quantity_int;
                        volumeint_text.text = Sales_volume.ToString();
                        Valuation();
                    }
                    break;
                case 1://紅蘿蔔
                    if (Sales_volume < Planting_system.carrot_int || Sales_volume == Planting_system.carrot_int && quantity_int == -1)
                    {
                        Sales_volume = Sales_volume + quantity_int;
                        volumeint_text.text = Sales_volume.ToString();
                        Valuation();
                    }
                    break;
                case 2://藍蘿蔔
                    if (Sales_volume < Planting_system.Blue_radish_int || Sales_volume == Planting_system.Blue_radish_int && quantity_int == -1)
                    {
                        Sales_volume = Sales_volume + quantity_int;
                        volumeint_text.text = Sales_volume.ToString();
                        Valuation();
                    }
                    break;
                case 3://玉米
                    if (Sales_volume < Planting_system.corn_int || Sales_volume == Planting_system.corn_int && quantity_int == -1)
                    {
                        Sales_volume = Sales_volume + quantity_int;
                        volumeint_text.text = Sales_volume.ToString();
                        Valuation();
                    }
                    break;
                case 4://鑽石玉米
                    if (Sales_volume < Planting_system.Diamond_corn_int || Sales_volume == Planting_system.Diamond_corn_int && quantity_int == -1)
                    {
                        Sales_volume = Sales_volume + quantity_int;
                        volumeint_text.text = Sales_volume.ToString();
                        Valuation();
                    }
                    break;
                case 5://月亮
                    if (Sales_volume < Planting_system.moon_int || Sales_volume == Planting_system.moon_int && quantity_int == -1)
                    {
                        Sales_volume = Sales_volume + quantity_int;
                        volumeint_text.text = Sales_volume.ToString();
                        Valuation();
                    }
                    break;
                case 6://太陽
                    if (Sales_volume < Planting_system.sun_int || Sales_volume == Planting_system.sun_int && quantity_int == -1)
                    {
                        Sales_volume = Sales_volume + quantity_int;
                        volumeint_text.text = Sales_volume.ToString();
                        Valuation();
                    }
                    break;
            case 7://一般蛋
                if (Sales_volume < Chicken_coop.Regular_int || Sales_volume == Chicken_coop.Regular_int && quantity_int == -1)
                {
                    Sales_volume = Sales_volume + quantity_int;
                    volumeint_text.text = Sales_volume.ToString();
                    Valuation();
                }
                break;
            case 8://銀蛋
                if (Sales_volume < Chicken_coop.Silver_int || Sales_volume == Chicken_coop.Silver_int && quantity_int == -1)
                {
                    Sales_volume = Sales_volume + quantity_int;
                    volumeint_text.text = Sales_volume.ToString();
                    Valuation();
                }
                break;
            case 9://金蛋
                if (Sales_volume < Chicken_coop.Golden_int || Sales_volume == Chicken_coop.Golden_int && quantity_int == -1)
                {
                    Sales_volume = Sales_volume + quantity_int;
                    volumeint_text.text = Sales_volume.ToString();
                    Valuation();
                }
                break;
            case 10://新鮮魚肉
                if (Sales_volume < Grilled_fish.Number_fish_int || Sales_volume == Grilled_fish.Number_fish_int && quantity_int == -1)
                {
                    Sales_volume = Sales_volume + quantity_int;
                    volumeint_text.text = Sales_volume.ToString();
                    Valuation();
                }
                break;
            case 11://香香烤魚
                if (Sales_volume < Grilled_fish.Baked_successfully_int || Sales_volume == Grilled_fish.Baked_successfully_int && quantity_int == -1)
                {
                    Sales_volume = Sales_volume + quantity_int;
                    volumeint_text.text = Sales_volume.ToString();
                    Valuation();
                }
                break;
            case 12://失敗烤魚
                if (Sales_volume < Grilled_fish.Baking_failed_int || Sales_volume == Grilled_fish.Baking_failed_int && quantity_int == -1)
                {
                    Sales_volume = Sales_volume + quantity_int;
                    volumeint_text.text = Sales_volume.ToString();
                    Valuation();
                }
                break;

        }

    }

    void Valuation()//估價
    {
        if (Sales_volume <0)
        {
            Sales_volume = 0;
            volumeint_text.text = Sales_volume.ToString();
        }
        else
        {
            switch (Product_Number)
            {
                case 0://白蘿蔔
                    volume_text.text = "銷售金額：" + (30 * Sales_volume).ToString();
                    break;
                case 1://紅蘿蔔
                    volume_text.text = "銷售金額：" + (124 * Sales_volume).ToString();
                    break;
                case 2://藍蘿蔔
                    volume_text.text = "銷售金額：" + (288 * Sales_volume).ToString();
                    break;
                case 3://玉米
                    volume_text.text = "銷售金額：" + (528 * Sales_volume).ToString();
                    break;
                case 4://鑽石玉米
                    volume_text.text = "銷售金額：" + (850 * Sales_volume).ToString();
                    break;
                case 5://月亮
                    volume_text.text = "銷售金額：" + (1260 * Sales_volume).ToString();
                    break;
                case 6://太陽
                    volume_text.text = "銷售金額：" + (1764 * Sales_volume).ToString();
                    break;
                case 7://一般蛋
                    volume_text.text = "銷售金額：" + (10 * Sales_volume).ToString();
                    break;
                case 8://銀蛋
                    volume_text.text = "銷售金額：" + (50 * Sales_volume).ToString();
                    break;
                case 9://金蛋
                    volume_text.text = "銷售金額：" + (100 * Sales_volume).ToString();
                    break;
                case 10://新鮮魚肉
                    volume_text.text = "銷售金額：" + (12 * Sales_volume).ToString();
                    break;
                case 11://香香烤魚
                    volume_text.text = "銷售金額：" + (28 * Sales_volume).ToString();
                    break;
                case 12://失敗烤魚
                    volume_text.text = "銷售金額：" + (8 * Sales_volume).ToString();
                    break;

            }
        }

    }

    public void items_sold()
    {
        sour.clip = ui_Clip;
        sour.Play();
        if (Sales_volume > 0)
        {
            switch (Product_Number)
            {
                case 0://白蘿蔔
                    play_obj.GetComponent<player>().money_system(+(30 * Sales_volume));
                    Planting.GetComponent<Planting_system>().harvest(Product_Number, -Sales_volume);
                    break;
                case 1://紅蘿蔔
                    play_obj.GetComponent<player>().money_system(+(124 * Sales_volume));
                    Planting.GetComponent<Planting_system>().harvest(Product_Number, -Sales_volume);
                    break;
                case 2://藍蘿蔔
                    play_obj.GetComponent<player>().money_system(+(288 * Sales_volume));
                    Planting.GetComponent<Planting_system>().harvest(Product_Number, -Sales_volume);
                    break;
                case 3://玉米
                    play_obj.GetComponent<player>().money_system(+(528 * Sales_volume));
                    Planting.GetComponent<Planting_system>().harvest(Product_Number, -Sales_volume);
                    break;
                case 4://鑽石玉米
                    play_obj.GetComponent<player>().money_system(+(850 * Sales_volume));
                    Planting.GetComponent<Planting_system>().harvest(Product_Number, -Sales_volume);
                    break;
                case 5://月亮
                    play_obj.GetComponent<player>().money_system(+(1260 * Sales_volume));
                    Planting.GetComponent<Planting_system>().harvest(Product_Number, -Sales_volume);
                    break;
                case 6://太陽
                    play_obj.GetComponent<player>().money_system(+(1764 * Sales_volume));
                    Planting.GetComponent<Planting_system>().harvest(Product_Number, -Sales_volume);
                    break;
                case 7://一般蛋
                    play_obj.GetComponent<player>().money_system(+(10 * Sales_volume));
                    Chicken.GetComponent<Chicken_coop>().harvest(Product_Number, -Sales_volume);
                    break;
                case 8://銀蛋
                    play_obj.GetComponent<player>().money_system(+(50 * Sales_volume));
                    Chicken.GetComponent<Chicken_coop>().harvest(Product_Number, -Sales_volume);
                    break;
                case 9://金蛋
                    play_obj.GetComponent<player>().money_system(+(100 * Sales_volume));
                    Chicken.GetComponent<Chicken_coop>().harvest(Product_Number, -Sales_volume);
                    break;
                case 10://新鮮魚肉
                    play_obj.GetComponent<player>().money_system(+(12 * Sales_volume));
                    fish.GetComponent<Grilled_fish>().harvest(Product_Number, -Sales_volume);
                    break;
                case 11://香香烤魚
                    play_obj.GetComponent<player>().money_system(+(28 * Sales_volume));
                    fish.GetComponent<Grilled_fish>().harvest(Product_Number, -Sales_volume);
                    break;
                case 12://失敗烤魚
                    play_obj.GetComponent<player>().money_system(+(8 * Sales_volume));
                    fish.GetComponent<Grilled_fish>().harvest(Product_Number, -Sales_volume);
                    break;

            }
            Sales_confirmation.SetActive(false);
            Sales_volume = 0;
            volumeint_text.text = Sales_volume.ToString();
            Check_available();
            money = player.money;
            playmoney_text.text = money.ToString();
            sour.clip = Buy_Clip;
            sour.Play();
        }
        else { volume_text.text = "你還未輸入販售數量唷~"; }
    }

    [Header("黑框內容"), TextArea]
    public string[] text_content;
    [Header("黑框文字")]
    public Text content_text;
    public void Introduce(int introduce_int)
    {
        content_text.text = text_content[introduce_int];
    }
}
