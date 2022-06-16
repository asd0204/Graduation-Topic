using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class monster_blood : MonoBehaviour
{
    //處理怪物血量及受傷

    [Header("生命值")]
    public float Blood;//速度

    [Header("怪死後生成的寶物")]
    public GameObject beads; //要生成的物件。

    [Header("怪死後生成的礦物")]
    public GameObject[] mineral; //要生成的物件。

    [Header("受傷音效")]
    public AudioSource Hurt_sound;

    [Header("巨大史萊姆")]
    public GameObject giant_slime;

    [Header("史萊姆")]
    public GameObject[] slime;

    [Header("玩家")]
    public GameObject player;

    [Header("召喚史萊姆點")]
    public GameObject Summon_Slime;

    [Header("草原系統")]
    public GameObject grassland;

    int beads_int;//掉落物機率
    int index;//場景編號


    private void Start()
    {
        player = GameObject.Find("玩家");
        Summon_Slime = GameObject.Find("草原");
        index = SceneManager.GetActiveScene().buildIndex;
        if (index==2)
        {
            grassland = GameObject.Find("草原系統");
        }
    }

    public void KING_Injuried()//玩家打擊史萊姆完成功時觸發
    {
        Blood -= Blood*0.25f;
        beads_int = Random.Range(1, 100);
        Hurt_sound.Play();

    }

    public void KING_Instantiate()//玩家打擊史萊姆完失敗時觸發
    {
        Blood += Blood * 0.25f;
        for (int i = 0; i < Random.Range(5, 10); i++)
        {
            Instantiate(slime[Random.Range(0, slime.Length)], Summon_Slime.transform.position, Summon_Slime.transform.rotation);
            if (index == 2)
            {
                EnemySpawner.EnemyLiveCount++;
            }
        }
    }

    public void Injuried(float harm)//怪受傷
    {
        Blood -= harm;
        beads_int = Random.Range(1, 100);
        Hurt_sound.Play();
        this.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f, 255f);
        Invoke("monster_color", 0.2f);
    }

    [Header("節點面板")]
    public GameObject[] King_Skills;
    [Header("史萊姆王")]
    static public GameObject King_slime;
    [Header("史萊姆大招提醒")]
    public GameObject King_text;
    [Header("史萊姆王大絕")]
    public bool Blood_bool;
    void monster_color()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 255f);
        if (Blood >0&&Blood <= 20 && Blood_bool == false)//史萊姆王生命低於或等於50時開啟
        {
            if (this.tag == "史萊姆王")
            {
                King_Skills[0] = GameObject.Find("節點");
                King_Skills[1] = King_Skills[0].transform.GetChild(0).gameObject;
                King_text= King_Skills[0].transform.GetChild(1).gameObject;
                Blood_bool = true;
                King_slime = this.gameObject;
                King_text.SetActive(true);
                King_Skills[1].SetActive(true);
                King_Skills[1].GetComponent<King_Slime_Skills>().ASDW();
            }
        }
    }



    private void Update()
    {
        if (Blood<=0)
        {
            if (this.tag=="巨大史萊姆")
            {
                for (int i = 0; i < Random.Range(1,3); i++)
                {
                    Instantiate(slime[Random.Range(0, slime.Length)], this.transform.position,this.transform.rotation);
                    if (index == 2)
                    {
                        EnemySpawner.EnemyLiveCount++;
                    }
                }
            }
            if (this.tag == "史萊姆王")
            {
                player.GetComponent<player>().poisoning_true();//中毒
                for (int i = 0; i < Random.Range(1, 3); i++)
                {
                    Instantiate(giant_slime, this.transform.position, this.transform.rotation);
                    if (index == 2)
                    {
                        EnemySpawner.EnemyLiveCount++;
                    }
                }
            }
            if (this.tag=="毒牙蝙蝠")
            {
                player.GetComponent<player>().poisoning_true();//中毒
            }
            if (beads_int < 20&& index==2)//莊園怪掉落物
            {
                Instantiate(beads, this.transform.position, this.transform.rotation);
            }
            if (index == 7)//迷宮怪掉落物
            {
                if (beads_int < 20)
                {
                    beads_int = Random.Range(1, 100);
                    mineral_generation();//稀有礦物
                }
                else
                {
                    Instantiate(mineral[0], this.transform.position, this.transform.rotation);
                }
            }
            if (index==2)
            {
                EnemySpawner.EnemyLiveCount--;
            }
            Destroy(this.gameObject);
        }
    }

    void mineral_generation()//掉落礦物
    {
        if (beads_int>20&& beads_int<60)
        {
            Instantiate(mineral[1], this.transform.position, this.transform.rotation);
        }
        if (beads_int<20)
        {
            Instantiate(mineral[2], this.transform.position, this.transform.rotation);
        }
    }
}
