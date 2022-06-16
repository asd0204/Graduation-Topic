using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Knights_System : MonoBehaviour
{
    //處理騎士團相關

    [Header("玩家錢錢")]
    public int money;

    [Header("玩家錢錢文字")]
    public Text playmoney_text;

    [Header("玩家")]
    public GameObject play_obj;

    [Header("店鋪介面")]
    public GameObject shop_interface;

    [Header("音效")]
    public AudioSource sour;//音效

    [Header("購買音效")]
    public AudioClip Buy_Clip;//音效
    [Header("UI音效")]
    public AudioClip ui_Clip;//音效

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")//當玩家碰到騎士開啟店鋪介面
        {
            shop_interface.SetActive(true);
            Sales_window.SetActive(false);
            money = player.money;
            playmoney_text.text = money.ToString();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")//當玩家離開騎士關閉店鋪介面
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
    [Header("商品編號")]
    public int Product_Number;
     [Header("購買確認窗")]
     public GameObject Purchase_confirmation;
     [Header("購買文字")]
     public Text Purchase_text;

     public void commodity(int commodity_int)//商店商品選項
     {
         Purchase_confirmation.SetActive(true);
         Product_Number = commodity_int;
            sour.clip = ui_Clip;
        sour.Play();
         switch (commodity_int)
         {
            case 1://巨大箭矢
                Purchase_text.text = "是否購買巨大箭矢?";
                break;
            case 2://輕羽箭矢
                Purchase_text.text = "是否購買輕羽箭矢?";
                break;
            case 3://掃除箭矢
                Purchase_text.text = "是否購買掃除箭矢?";
                break;
            case 4://破魔箭矢
                Purchase_text.text = "是否購買破魔箭矢?";
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
     int Purchase_quantity = 1;//購買數量
     public void increase_decrease(int quantity_int)//增減數量
     {
            sour.clip = ui_Clip;
        sour.Play();
         if (Purchase_quantity < 99 && Purchase_quantity >= 1)
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
            case 1://巨大箭矢
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
            case 2://輕羽箭矢
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
            case 3://掃除箭矢
                if (money >= 100 * Purchase_quantity)
                {
                    money_text.text = "所需金額：" + (100 * Purchase_quantity).ToString();
                    money_text.color = new Color(0f, 0f, 0f, 255f);
                }
                else
                {
                    money_text.text = "所需金額：" + (100 * Purchase_quantity).ToString();
                    money_text.color = new Color(255f, 0f, 0f, 255f);
                }
                break;
            case 4://破魔箭矢
                if (money >= 200 * Purchase_quantity)
                {
                    money_text.text = "所需金額：" + (200 * Purchase_quantity).ToString();
                    money_text.color = new Color(0f, 0f, 0f, 255f);
                }
                else
                {
                    money_text.text = "所需金額：" + (200 * Purchase_quantity).ToString();
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
                case 1://巨大箭矢
                    if (money >= 50 * Purchase_quantity)
                    {
                        play_obj.GetComponent<player>().weapon_table(Product_Number, Purchase_quantity);
                        play_obj.GetComponent<player>().money_system(-(50 * Purchase_quantity));
                        Purchase_confirmation.SetActive(false);
                    }
                    else { money_text.text = "你的錢錢不夠喔~在努力賺錢吧~"; }
                    break;
                case 2://輕羽箭矢
                    if (money >= 50 * Purchase_quantity)
                    {
                        play_obj.GetComponent<player>().weapon_table(Product_Number, Purchase_quantity);
                        play_obj.GetComponent<player>().money_system(-(50 * Purchase_quantity));
                        Purchase_confirmation.SetActive(false);
                    }
                    else { money_text.text = "你的錢錢不夠喔~在努力賺錢吧~"; }
                    break;
                case 3://掃除箭矢
                    if (money >= 100 * Purchase_quantity)
                    {
                        play_obj.GetComponent<player>().weapon_table(Product_Number, Purchase_quantity);
                        play_obj.GetComponent<player>().money_system(-(100 * Purchase_quantity));
                        Purchase_confirmation.SetActive(false);
                    }
                    else { money_text.text = "你的錢錢不夠喔~在努力賺錢吧~"; }
                    break;
                case 4://破魔箭矢
                    if (money >= 200 * Purchase_quantity)
                    {
                        play_obj.GetComponent<player>().weapon_table(Product_Number, Purchase_quantity);
                        play_obj.GetComponent<player>().money_system(-(200 * Purchase_quantity));
                        Purchase_confirmation.SetActive(false);
                    }
                    else { money_text.text = "你的錢錢不夠喔~在努力賺錢吧~"; }
                    break;

            }
             money = player.money;
             playmoney_text.text = money.ToString();
         }
         else { money_text.text = "你還未輸入購買數量唷~"; }

     }



    //販售程式




    [Header("販售視窗")]
    public GameObject Sales_window;

    [Header("不知名的礦石")]
    public GameObject unknown;

    [Header("詭異的礦石")]
    public GameObject weird;

    [Header("魔力礦石")]
    public GameObject Magic;

    public void Sales_no()//不銷售物品
    {
        Sales_window.SetActive(false);
        Weapon_Upgrade_false();
        sour.clip = ui_Clip;
        sour.Play();
    }
    public void Sales()
    {
        Sales_window.SetActive(true);
        Weapon_Upgrade_false();
        Check_available();
        sour.clip = ui_Clip;
        sour.Play();
    }

    void Check_available()//檢查可銷售物品
    {
        //礦石
        if (player.unknown_ore_int > 0)
        {
            unknown.SetActive(true);
        }
        else { unknown.SetActive(false); }

        if (player.weird_ore_int > 0)
        {
            weird.SetActive(true);
        }
        else { weird.SetActive(false); }

        if (player.Magic_ore_int > 0)
        {
            Magic.SetActive(true);
        }
        else { Magic.SetActive(false); }


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
        sour.clip = ui_Clip;
        sour.Play();
    }

    public void Confirm_sale(int Confirm_int)//確認販售物
    {
        Sales_confirmation.SetActive(true);
        Product_Number = Confirm_int;
        sour.clip = ui_Clip;
        sour.Play();
        switch (Confirm_int)
        {
            case 3://不知名的礦石
                Sales_text.text = "是否販售不知名的礦石?";
                in_stock.text = "現有庫存：" + player.unknown_ore_int.ToString();
                break;
            case 4://詭異的礦石
                Sales_text.text = "是否販售詭異的礦石?";
                in_stock.text = "現有庫存：" + player.weird_ore_int.ToString();
                break;
            case 5://魔力礦石
                Sales_text.text = "是否販售魔力礦石?";
                in_stock.text = "現有庫存：" + player.Magic_ore_int.ToString();
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
            case 3://不知名的礦石
                if (Sales_volume < player.unknown_ore_int || Sales_volume == player.unknown_ore_int && quantity_int == -1)
                {
                    Sales_volume = Sales_volume + quantity_int;
                    volumeint_text.text = Sales_volume.ToString();
                    Valuation();
                }
                break;
            case 4://詭異的礦石
                if (Sales_volume < player.weird_ore_int || Sales_volume == player.weird_ore_int && quantity_int == -1)
                {
                    Sales_volume = Sales_volume + quantity_int;
                    volumeint_text.text = Sales_volume.ToString();
                    Valuation();
                }
                break;
            case 5://魔力礦石
                if (Sales_volume < player.Magic_ore_int || Sales_volume == player.Magic_ore_int && quantity_int == -1)
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
        if (Sales_volume < 0)
        {
            Sales_volume = 0;
            volumeint_text.text = Sales_volume.ToString();
        }
        else
        {
            switch (Product_Number)
            {
                case 3://不知名的礦石
                    volume_text.text = "銷售金額：" + (100 * Sales_volume).ToString();
                    break;
                case 4://詭異的礦石
                    volume_text.text = "銷售金額：" + (200 * Sales_volume).ToString();
                    break;
                case 5://魔力礦石
                    volume_text.text = "銷售金額：" + (300 * Sales_volume).ToString();
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
                case 3://不知名的礦石
                    play_obj.GetComponent<player>().money_system(+(100 * Sales_volume));
                    play_obj.GetComponent<player>().number_beads(Product_Number, -Sales_volume);
                    break;
                case 4://詭異的礦石
                    play_obj.GetComponent<player>().money_system(+(200 * Sales_volume));
                    play_obj.GetComponent<player>().number_beads(Product_Number, -Sales_volume);
                    break;
                case 5://魔力礦石
                    play_obj.GetComponent<player>().money_system(+(300 * Sales_volume));
                    play_obj.GetComponent<player>().number_beads(Product_Number, -Sales_volume);
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




    //武器升級系統


    [Header("武器升級系統")]
    public GameObject Weapon_Upgrade_System;
    bool Weapon_Upgrade_bool;
    int Bow_Arrow_level;//弓箭等級
    [Header("武器現在與進化圖")]
    public Image weapon_illustration;
    [Header("武器升級文字")]
    public Text weapon_text;
    [Header("武器進化選項")]
    public GameObject weapon_evolution;
    [Header("弓箭圖")]
    public Sprite[] bow_arrow_illustration;
    public void Weapon_Upgrade_true()//顯示武器升級系統
    {
        if (Weapon_Upgrade_bool==false)
        {
            Weapon_Upgrade_bool = true;
            Weapon_Upgrade_System.SetActive(true);
            Bow_Arrow_level = player.Bow_Arrow_level;//抓取玩家弓箭等級
            switch (Bow_Arrow_level)
            {
                case 0:
                    weapon_text.text = "升級所需金額：500";
                    weapon_evolution.SetActive(true);
                    break;
                case 1:

                    weapon_text.text = "升級所需金額：1000";
                    weapon_evolution.SetActive(true);
                    break;
                case 2:
                    weapon_text.text = "箭矢已達最高級";
                    weapon_evolution.SetActive(false);
                    break;
            }
        }
        else
        {
            Weapon_Upgrade_false();
        }
       

    }

    void Weapon_Upgrade_false()//關閉武器升級系統
    {
        Weapon_Upgrade_System.SetActive(false);
        Weapon_Upgrade_bool = false;
    }

    [Header("升級提視窗")]
    public GameObject prompt_window;
    [Header("升級提視文字")]
    public Text prompt_text;
    public void Confirm_upgrade()//確認升級
    {
        prompt_window.SetActive(true);
        prompt_text.text = "升級後將無法還原，是否繼續升級？";
    }

    public void Confirm_upgrade_false()//確認升級
    {
        prompt_window.SetActive(false);
    }

    public void Weapon_upgrade()//升級
    {
        switch (Bow_Arrow_level)
        {
            case 0:
                if (money >= 500)
                {
                    play_obj.GetComponent<player>().Weapon_Upgrade();
                    play_obj.GetComponent<player>().money_system(-500);
                    prompt_window.SetActive(false);
                    sour.clip = Buy_Clip;
                    sour.Play();
                    Weapon_Upgrade_true();
                }
                else { prompt_text.text = "你的錢錢不夠喔~在努力賺錢吧~";  }
                break;
            case 1:
                if (money >= 1000)
                {
                    play_obj.GetComponent<player>().Weapon_Upgrade();
                    play_obj.GetComponent<player>().money_system(-1000);
                    prompt_window.SetActive(false);
                    sour.clip = Buy_Clip;
                    sour.Play();
                    Weapon_Upgrade_true();
                }
                else { prompt_text.text = "你的錢錢不夠喔~在努力賺錢吧~"; }
                break;
        }
        money = player.money;
        playmoney_text.text = money.ToString();
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
