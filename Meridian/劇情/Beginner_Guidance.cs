using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Beginner_Guidance : MonoBehaviour
{
    //初學者指導系統

    [Header("莊園對話")]
    public GameObject Manor_Dialogue;

    [Header("雜貨店對話")]
    public GameObject Grocery_Conversation;

    [Header("騎士團對話")]
    public GameObject Knights_Dialogue;

    [Header("教堂對話")]
    public GameObject church_conversation;

    [Header("木橋對話")]
    public GameObject wooden_bridge_dialogue;    
    
    [Header("引導結束對話")]
    public GameObject Boot_ends;

    [Header("玩家")]
    public GameObject PLAYER;

    [Header("村長")]
    public GameObject village_head;

    [Header("黑幕")]
    public GameObject Shady;


    [Header("對話階段")]
    public int dialogue;


    [Header("背景音樂")]
    public music_system Music_System;

    [Header("農場位置")]
    public GameObject farm_Transfer;
    [Header("教堂位置")]
    public GameObject church_Transfer;
    [Header("雜物店位置")]
    public GameObject grocery_Transfer;
    [Header("騎士團位置")]
    public GameObject Knights_Transfer;
    [Header("木橋位置")]
    public GameObject wooden_bridge_location;

    [Header("農場攝影機")]
    public GameObject farm_Camera;
    [Header("教堂攝影機")]
    public GameObject church_Camera;
    [Header("雜物店攝影機")]
    public GameObject grocery_Camera;
    [Header("騎士團攝影機")]
    public GameObject Knights_Camera;

    private void Start()
    {
         Manor();
    }

    public void Manor()//莊園對話
    {
        Manor_Dialogue.SetActive(true);
        PLAYER.GetComponent<new_player>().SPEED_FLOAT(0);
    }

    public void wooden_bridge()//木橋對話
    {
        wooden_bridge_dialogue.SetActive(true);
        PLAYER.GetComponent<new_player>().SPEED_FLOAT(0);
    }

    public void Grocery()//雜貨對話
    {
        Grocery_Conversation.SetActive(true);
        PLAYER.GetComponent<new_player>().SPEED_FLOAT(0);
    }

    public void Knights()//騎士
    {
        Knights_Dialogue.SetActive(true);
        PLAYER.GetComponent<new_player>().SPEED_FLOAT(0);
    }

    public void church()//教堂
    {
        church_conversation.SetActive(true);
        PLAYER.GetComponent<new_player>().SPEED_FLOAT(0);
    }

    public void Manor_BAY()//莊園對話
    {
        Boot_ends.SetActive(true);
        PLAYER.GetComponent<new_player>().SPEED_FLOAT(0);
    }

    public void start_game()//正式進入遊戲
    {
        SceneManager.LoadScene(2);
    }

    public void jump_over()//跳過新手引導
    {
        Shady.SetActive(true);
        Invoke("start_game", 0.2F);
    }

    public void dialogue_INT()
    {
        dialogue ++;
        switch (dialogue)
        {
            case 1:
                village_head.transform.position = wooden_bridge_location.transform.position;
                break;
            case 2:
                village_head.transform.position = grocery_Transfer.transform.position;
                PLAYER.transform.position = grocery_Transfer.transform.position;//傳送至雜貨店位置
                farm_Camera.SetActive(false);
                church_Camera.SetActive(false);
                grocery_Camera.SetActive(true);
                Knights_Camera.SetActive(false);
                Shady.SetActive(true);
                Music_System.Grocery_Music();
                Invoke("Grocery", 0.2F);
                break;
            case 3:
                village_head.transform.position = Knights_Transfer.transform.position;
                PLAYER.transform.position = Knights_Transfer.transform.position;//傳送至騎士團位置
                farm_Camera.SetActive(false);
                church_Camera.SetActive(false);
                grocery_Camera.SetActive(false);
                Shady.SetActive(true);
                Knights_Camera.SetActive(true);
                Music_System.Knights_Music();
                Invoke("Knights", 0.2F);
                break;
            case 4:
                village_head.transform.position = church_Transfer.transform.position;
                PLAYER.transform.position = church_Transfer.transform.position;//傳送至教堂位置
                farm_Camera.SetActive(false);
                church_Camera.SetActive(true);
                grocery_Camera.SetActive(false);
                Knights_Camera.SetActive(false);
                Music_System.Church_Music();
                Shady.SetActive(true);
                Invoke("church", 0.2F);
                break;
            case 5:
                village_head.transform.position = farm_Transfer.transform.position;
                PLAYER.transform.position = farm_Transfer.transform.position;//傳送至莊園位置
                farm_Camera.SetActive(true);
                church_Camera.SetActive(false);
                grocery_Camera.SetActive(false);
                Knights_Camera.SetActive(false);
                Music_System.FarmDay_Music();
                Shady.SetActive(true);
                Invoke("Manor_BAY", 0.2F);
                break;
            case 6:

                Shady.SetActive(true);
                Invoke("start_game", 0.2F);
                break;
        }
    }
}
