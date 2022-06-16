using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farming : MonoBehaviour
{
    //處理種田相關事件

    Collider2D Colli2d;
    [Header("種子按鈕")]
    public GameObject Planting_button;//當土地可種植時顯示
    public int seed_int;

    [Header("種子視窗")]
    public GameObject Planting_game;
    [Header("確認種植窗口")]
    public GameObject Confirmation_window;

    public int field_touch;

    // Start is called before the first frame update
    void Start()
    {
        Colli2d = GetComponent<Collider2D>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player"&& seed_int==0&& field_touch==0)//觸碰田地開啟種植選單
        {
            Planting_button.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")//離開田地關閉種植
        {
            Planting_button.SetActive(false);
            Planting_game.SetActive(false);//關閉種子選單
            Confirmation_window.SetActive(false);//關閉確認窗口
        }
    }

    int Seed_number;
    public void Seed_menu()//不可種植種子
    {
        seed_int = 1;
        //Colli2d.isTrigger = false;
        Planting_button.SetActive(false);
    }
    public void Seed_reply()//可種植種子
    {
        seed_int = 0;
        //Colli2d.isTrigger = true;
    }

    public void field_off()//夜晚不可觸碰田地
    {
        field_touch = 1;
    }

    public void field_open()//白天可觸碰田地
    {
        field_touch = 0;
    }
}
