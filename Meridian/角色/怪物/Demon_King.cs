
using UnityEngine;

public class Demon_King : MonoBehaviour
{
    //處理魔王血量及受傷

    [Header("生命值")]
    public float Blood;//速度

    [Header("受傷音效")]
    public AudioSource Hurt_sound;

    public AudioClip[] Hurt_Clip;

    [Header("史萊姆")]
    public GameObject[] slime;

    [Header("玩家")]
    public GameObject player;

    [Header("生成位置")]
    public GameObject[] Generate_location;

    [Header("傷害值")]
    public float damage;
    [Header("速度")]
    public float speed;//速度
    AudioSource sour;//走路音效

    [Header("背景音樂")]
    public music_system Music_System;

    private Cinemachine.CinemachineCollisionImpulseSource MyInpulse;

    private void Start()
    {
        MyInpulse = GetComponent<Cinemachine.CinemachineCollisionImpulseSource>();
        MyInpulse.GenerateImpulse();
        Invoke("MyInpulse_GenerateImpulse", 2f);
        Music_System= GameObject.Find("背景音樂").GetComponent< music_system>();
        Music_System.beat_king();
        Hurt_sound.Play();
        player = GameObject.Find("玩家");
        Generate_location = GameObject.FindGameObjectsWithTag("草原怪生成點");
        sour = GetComponent<AudioSource>();//抓取走路音效撥放器
        dir = new Vector3(Random.Range(180, -180), Random.Range(180, -180), 0);//初始方向   
    }

    void MyInpulse_GenerateImpulse()//魔王出現畫面震動關閉
    {
        MyInpulse.enabled = false;
    }

    public void Injuried(float harm)//怪受傷
    {
        if (harm>=20)//受超過25的傷害時召喚小怪
        {
            Call();
            this.GetComponent<SpriteRenderer>().color = new Color(0f, 255f, 0f, 255f);
        }
        else { this.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f, 255f); }
        Blood -= harm;
        Hurt_sound.clip = Hurt_Clip[1];
        Hurt_sound.Play();
        Invoke("monster_color", 0.2f);
    }

    void Call()//召喚
    {
        MyInpulse.enabled = true;
        if (Blood>0)
        {
            for (int i = 0; i < Random.Range(5, 10); i++)
            {
                Instantiate(slime[Random.Range(0, slime.Length)], Generate_location[Random.Range(0, Generate_location.Length)].transform.position, Generate_location[Random.Range(0, Generate_location.Length)].transform.rotation);
                if (Blood < 100)
                {
                    Blood += 10;
                }
                else { Blood = 100; }

                EnemySpawner.EnemyLiveCount++;
            }
        }

    }

    void monster_color()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 255f, 255f);
        MyInpulse.enabled = false;
    }

    private void Update()
    {
        if (Blood <= 0)
        {
            EnemySpawner.EnemyLiveCount--;
            Destroy(this.gameObject);
        }
        if (track == 0)
        {
            mobile();
        }

    }
    public int track;
    public void track_int(int int_int)
    {
        track = int_int;
    }

    public Vector3 dir;//this.gameobject的方向
    void mobile()//移動
    {
        this.transform.localPosition -= dir.normalized * speed * Time.deltaTime;
    }

    [Header("攻擊音效")]
    public AudioSource Attack_sound;//攻擊音效  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")//攻擊到玩家
        {
            player.gameObject.GetComponent<player>().Injuried(damage);//給予傷害
            this.gameObject.GetComponent<Transform>().position += new Vector3(10, 10, 0) * Time.deltaTime;
            Attack_sound.Play();//播放攻擊音效
        }
        if (collision.gameObject.tag == "牆" || collision.gameObject.tag == "河" || collision.gameObject.tag == "怪")//遊走時碰到牆壁
        {
            this.gameObject.GetComponent<Transform>().position += new Vector3(10, 10, 0) * Time.deltaTime;
            dir = new Vector3(Random.Range(180, -180), Random.Range(180, -180));//初始方向 
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")//攻擊到玩家
        {
            player.gameObject.GetComponent<player>().Injuried(damage);//給予傷害
            Attack_sound.Play();//播放攻擊音效
        }

    }
}
