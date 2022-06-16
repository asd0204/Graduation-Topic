using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class King_Slime_Skills : MonoBehaviour
{

    //史莱姆王技能


    AudioSource sour;//音效
    public int[] Node;//用於紀錄當前出現結點與下一個節點是否不同
    [Header("節點")]
    public GameObject[] Node_obj;
    [Header("生成父物件")]
    public Image Node_canv;

    public int key_intO;//隨機數

    [Header("史萊姆王")]
    public GameObject King_slime;


    [Header("要生成多少節點")]
    public int number_nodes;

    [Header("一般怪物")]
    public GameObject[] monster;
    [Header("巨大史萊姆")]
    public GameObject[] giant_slime;
    [Header("史萊姆王")]
    public GameObject[] king_slime;
    [Header("毒牙蝙蝠")]
    public GameObject[] fangs_bat;
    [Header("魔王")]
    public GameObject Demon;
    [Header("玩家")]
    public GameObject play;
    [Header("史萊姆大招提醒")]
    public GameObject King_text;

    private void Start()
    {
        sour = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (player.hp_float<0)
        {
            Delete_node();
        }
    }

    public void ASDW()
    {
        if (nodes_bool == false)
        {
            pause();
            Invoke("Slime_Skills", 5f);
            nodes_bool = true;
        }
    }

    void Slime_Skills()//史萊姆大招
    {
        King_text.SetActive(false);
        King_slime = monster_blood.King_slime;//紀錄發起大招的史萊姆
        number_nodes = Random.Range(5, Node_obj.Length);//判斷要生成多少節點
        click_int = number_nodes;
        InvokeRepeating("Generate_node", 0.2f, 3f);
    }

    void pause()//暫停所有怪及玩家
    {
        monster = GameObject.FindGameObjectsWithTag("怪");
        giant_slime = GameObject.FindGameObjectsWithTag("巨大史萊姆");
        king_slime = GameObject.FindGameObjectsWithTag("史萊姆王");
        fangs_bat = GameObject.FindGameObjectsWithTag("毒牙蝙蝠");
        play = GameObject.Find("玩家");
        play.GetComponent<player>().SPEED_FLOAT(0);
        play.GetComponent<character_direction>().track_int(1);
        for (int i = 0; i < monster.Length; i++)
        {
            monster[i].GetComponent<enemy>().track_int(1);
            monster[i].GetComponent<AIPath>().enabled = false;
        }
        for (int i = 0; i < giant_slime.Length; i++)
        {
            giant_slime[i].GetComponent<enemy>().track_int(1);
            giant_slime[i].GetComponent<AIPath>().enabled = false;
        }
        for (int i = 0; i < king_slime.Length; i++)
        {
            king_slime[i].GetComponent<enemy>().track_int(1);
            king_slime[i].GetComponent<AIPath>().enabled = false;
        }
        for (int i = 0; i < fangs_bat.Length; i++)
        {
            fangs_bat[i].GetComponent<enemy>().track_int(1);
            fangs_bat[i].GetComponent<AIPath>().enabled = false;
        }
    }

    void Resume()//解除暫停
    {
        play.GetComponent<player>().SPEED_FLOAT(10);
        play.GetComponent<character_direction>().track_int(0);
        for (int i = 0; i < monster.Length; i++)
        {
            monster[i].GetComponent<enemy>().track_int(0);
        }
        for (int i = 0; i < giant_slime.Length; i++)
        {
            giant_slime[i].GetComponent<enemy>().track_int(0);
        }
        for (int i = 0; i < king_slime.Length; i++)
        {
            king_slime[i].GetComponent<enemy>().track_int(0);
        }
        for (int i = 0; i < fangs_bat.Length; i++)
        {
            fangs_bat[i].GetComponent<enemy>().track_int(0);
        }
        nodes_bool = false;
        this.gameObject.SetActive(false);
    }


    public bool nodes_bool;

    void Generate_node()//生成節點
    {
        if (number_nodes > 0)
        {
            number_nodes--;
            Node_generation();
        }
        else { CancelInvoke("Generate_node"); }
    }

    void Node_generation()//判斷節點
    {
        Node[0] = Random.Range(0, Node_obj.Length);//判斷節點位置
        Instantiate(Node_obj[Node[0]], Node_canv.transform, false);
        Debug.Log("123");
    }

    public int click_int;
    public void click_success()//點擊成功
    {
        sour.Play();
        click_int--;
        if (click_int==0)
        {
            CancelInvoke("Generate_node");
            King_slime.GetComponent<monster_blood>().KING_Injuried();
            Resume();
            
        }
    }

    public void FailedEgg_Retrieval()//按失敗
    {
        CancelInvoke("Generate_node");
        King_slime.GetComponent<monster_blood>().KING_Instantiate();
        Resume();
        Delete_node();
       this.gameObject.SetActive(false);
    }

    void Delete_node()//刪除節點
    {
        Node_obj = GameObject.FindGameObjectsWithTag("節點");
        for (int i = 0; i < Node_obj.Length; i++)
        {
            Destroy(Node_obj[i].gameObject);
        }
        nodes_bool = false;
        this.gameObject.SetActive(false);
    }
}
