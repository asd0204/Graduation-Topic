using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Sound_manager : MonoBehaviour
{
    static Sound_manager instance;
    public AudioMixer MusicMa;
    [Header("ESC設定鍵")]
    public GameObject set;//設定
    [Header("主頁設定鍵")]
    public GameObject[] Back;
    [Header("音效介面")]
    public GameObject musi;
    [Header("UI音效")]
    AudioSource sour;//音效

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            name = "音效管理物件";

        }
        else if (this != instance)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            Debug.Log("刪除場景" + sceneName + "的" + name);
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        sour = GetComponent<AudioSource>();//抓取自身音效撥放器
    }

    void SetMasterVolume(float volume)    // 控制主音量的函数
    {
        MusicMa.SetFloat("Master", volume);

    }

    void SetBackgroundVolume(float volume)    // 控制背景音乐音量的函数
    {
        MusicMa.SetFloat("背景", volume);

    }
    void SetSpecialVolume(float volume)    // 控制音效音量的函数
    {
        MusicMa.SetFloat("音效", volume);

    }
    private void Update()
    {
        //判斷是否在遊戲還是主畫面
        // Scene scene = SceneManager.GetActiveScene();
        int index = SceneManager.GetActiveScene().buildIndex;
        if (index == 0)
        {
            Back[0].SetActive(true);

        }
        else
        {
            Back[0].SetActive(false);
            escc();
        }
    }

  

    void escc()//暫停遊戲
    {
        if (Input.GetKeyDown("escape"))
        {
            //Time.timeScale = 0f;  //時間暫停
            musi.SetActive(true);
            set.SetActive(true);

        }
    }

    //點擊主頁設定
    public void th_set()
    {
        Back[1].SetActive(true);
        musi.SetActive(true);
        sour.Play();//播放音效
    }

    //點擊主頁設定返回
    public void fh_set()
    {
        Back[1].SetActive(false);
        musi.SetActive(false);
        sour.Play();//播放音效
    }

    //繼續遊戲
    public void GP_final()
    {
       // Time.timeScale = 1f;
        musi.SetActive(false);
        set.SetActive(false);
        sour.Play();//播放音效
    }

    //離開遊戲
    public void exit_game()
    {
        musi.SetActive(false);
        set.SetActive(false);
        sour.Play();//播放音效
        SceneManager.LoadScene(0);
    }

    [Header("操作玩法")]
    public GameObject How_play;
    bool How_play_bool;
    public void Gameplay()
    {
        if (How_play_bool == false)
        {
            How_play.SetActive(true);
            How_play_bool = true;
        }
        else
        {
            How_play.SetActive(false);
            How_play_bool = false;
        }

    }

}
