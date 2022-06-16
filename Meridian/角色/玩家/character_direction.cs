using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class character_direction : MonoBehaviour
{
    //處理人物面向滑鼠

    private Camera mainCamera;
    Animator aor;

    [Header("換箭音效播放")]
    public AudioSource sour;//音效

    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
        aor = GetComponent<Animator>();
        // arr.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (track==0)
        {
            archery_animation();
            direction();
        }

    }

    void direction()//玩家方向
    {
        //將對象轉換為屏幕空間
        Vector3 transformScreenPosition = mainCamera.WorldToScreenPoint(transform.position);
        //使用物體在屏幕空間的z作為鼠標在屏幕空間的z，然後計算鼠標的世界位置 
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transformScreenPosition.z));
        //從對像到鼠標的方向
        Vector3 dir = (mouseWorldPosition - transform.position).normalized;
        //將方向設置為對象的局部軸
        transform.up = dir;
    }

    [Header("箭")]
    public GameObject[] arrow;
    [Header("初始箭")]
    public GameObject arr;
    [Header("箭生成位置")]
    public GameObject Arrow_spawn;

    public int Number;//箭矢編號
    public int Cross_bool;//日夜

    [Header("換箭音效")]
    public AudioClip arrows_Clip;
    public void Arrow_type(int Arrow_Number)
    {
        Cross_bool = GM.Cross_bool;
        Number = Arrow_Number;
        sour.clip = arrows_Clip;
        sour.Play();
    }

    public void archery()//射箭
    {
        Instantiate(arrow[Number], Arrow_spawn.transform);
        //生成(Box, 物件的位置:生成點的位置, 物件的旋轉值:生成點的旋轉值);
    }

    void archery_animation()//播放射箭動畫
    {
        if (Input.GetMouseButtonDown(1)|| Input.GetKeyDown(KeyCode.Space))//滑鼠右鍵或空白射箭
        {
            this.GetComponent<player>().weapon_table(Number, -1);
            aor.SetBool("射擊動畫", true);
        }
    }

    public void archery_animation_end()//結束射箭動畫
    {
        aor.SetBool("射擊動畫", false);
    }


    public int track;
    public void track_int(int int_int)
    {
        track = int_int;
    }
}