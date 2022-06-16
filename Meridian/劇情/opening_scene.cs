using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class opening_scene : MonoBehaviour
{
    [Header("文字框")]
    public Text textLabel;//文字框

    [Header("頭像")]
    public Image[] Avatar;
    public Sprite[] Avatar_Sprite;

    [Header("文本文件")]
    public TextAsset textFile;//劇情本
    public int index;//劇情判斷

    [Header("文字速度")]
    public float textseed;
     bool textfn;


    [Header("初學者指導系統")]
    public GameObject Beginner;

    [Header("玩家")]
    public GameObject PLAYER;



    List<string> textList = new List<string>();


    private void Awake()
    {
        GetTextFormFile(textFile);//讀取文本
        PLAYER = GameObject.Find("玩家");
    }

    private void OnEnable()
    {
        //textLabel.text = textList[index];//起始第一句
        //index++;
       // textfn = true;
        StartCoroutine(SetTextUI());
    }

    private void Update()
    {
        if (Input.anyKeyDown && index == textList.Count - 1)//判斷文本是否跑完，跑完即歸0
        {
            index = 0;
            this.gameObject.SetActive(false);
            PLAYER.GetComponent<new_player>().SPEED_FLOAT(10);
            Beginner.GetComponent<Beginner_Guidance>().dialogue_INT();
            return;
        }
        if (Input.anyKeyDown && textfn)//按下任一鍵觸發文本下一句劇情
        {
            //textLabel.text = textList[index];
            index++;
            StartCoroutine(SetTextUI());
        }
    }

    void GetTextFormFile(TextAsset file)
    {
        textList.Clear();//將列表清空
        index = 0;//起始設為0
        var lineDate = file.text.Split('\n');//將文本切割成一行一行

        foreach (var line in lineDate)
        {
            textList.Add(line);
        }
    }

    IEnumerator SetTextUI()//執行協程讓字逐一跑出
    {
        textfn = false;
        textLabel.text = "";//清空對話欄
        switch (textList[index].Trim())//WIN系統需加.Trim()否則無法判斷AB而MAC則不用
        {
            case "NPC："://當A時頭像切換文[0]
                //Avatar[0].sprite = Avatar_Sprite[0];
                Avatar[1].color = new Color(0.5f, 0.5f, 0.5f);
                Avatar[0].color = new Color(1, 1, 1);
                index++;
                break;
            case "玩家：":
                Avatar[1].sprite = Avatar_Sprite[1];
                Avatar[0].color = new Color(0.5f, 0.5f, 0.5f);
                Avatar[1].color = new Color(1, 1, 1);
                index++;
                break;
            case "NPC1：":
                Avatar[1].sprite = Avatar_Sprite[0];
                Avatar[0].color = new Color(0.5f, 0.5f, 0.5f);
                Avatar[1].color = new Color(1, 1, 1);
                index++;
                break;
        }

        for (int i = 0; i < textList[index].Length; i++)//迴圈起始值<指定數值;每跑完一次迴圈+1
        {
            textLabel.text += textList[index][i];//讓字逐一累加出現
            yield return new WaitForSeconds(textseed); //控制出現速度
        }

        textfn = true;//下一句開關

    }
    public void this_false()
    {
        this.gameObject.SetActive(false);
    }
}
