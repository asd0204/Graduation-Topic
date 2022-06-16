using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music_system : MonoBehaviour
{
    //處理背景音樂等

    //單例設計模式用於在專案內宣告自身
    //static public music_system Music_System = null;

    AudioSource THIS_Source;//抓取自身撥放器

    private void Awake()
    {
         THIS_Source = GetComponent<AudioSource>();//抓取自身撥放器
    }

    [Header("首頁音樂")]
    public AudioClip Home_Clip;
    public void Home_Music()//首頁音樂切換
    {
        THIS_Source.clip= Home_Clip;
        THIS_Source.Play();
    }

    [Header("農場白天音樂")]
    public AudioClip FarmDay_Clip;
    public void FarmDay_Music()//農場白天音樂切換
    {
        THIS_Source.clip = FarmDay_Clip;
        THIS_Source.Play();
    }

    [Header("農場夜晚音樂")]
    public AudioClip FarmEvening_Clip;
    public void FarmEvening_Music()//農場夜晚音樂切換
    {
        THIS_Source.clip = FarmEvening_Clip;
        THIS_Source.Play();
    }

    [Header("農場戰鬥音樂")]
    public AudioClip Fighting_Clip;
    public void Fighting_Music()//農場戰鬥音樂切換
    {
        THIS_Source.clip = Fighting_Clip;
        THIS_Source.Play();
    }

    [Header("打魔王音樂")]
    public AudioClip beat_king_Clip;
    public void beat_king()
    {
        THIS_Source.clip = beat_king_Clip;
        THIS_Source.Play();
    }

    [Header("雜貨店音樂")]
    public AudioClip Grocery_Clip;
    public void Grocery_Music()//雜貨店音樂切換
    {
        THIS_Source.clip = Grocery_Clip;
        THIS_Source.Play();
    }

    [Header("教堂音樂")]
    public AudioClip Church_Clip;
    public void Church_Music()//教堂音樂切換
    {
        THIS_Source.clip = Church_Clip;
        THIS_Source.Play();
    }

    [Header("騎士團音樂")]
    public AudioClip Knights_Clip;
    public void Knights_Music()//騎士團音樂切換
    {
        THIS_Source.clip = Knights_Clip;
        THIS_Source.Play();
    }

    [Header("森林音樂")]
    public AudioClip Forest_Clip;
    public void Forest_Music()//森林音樂切換
    {
        THIS_Source.clip = Forest_Clip;
        THIS_Source.Play();
    }

    [Header("小遊戲音樂")]
    public AudioClip game_Clip;
    public void game_Music()//騎士團音樂切換
    {
        THIS_Source.clip = game_Clip;
        THIS_Source.Play();
    }
}
