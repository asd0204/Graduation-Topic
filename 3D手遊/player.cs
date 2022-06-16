using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour {

    [SerializeField,Header("引力物體")] Transform earthTransform;
    [SerializeField, Header("玩家重力")] float gravityScale;
    [SerializeField, Header("玩家速度")] float speed;
    [SerializeField, Header("跳躍力度")] float Jump_speed;
    [SerializeField, Header("跳躍撥放器")] AudioSource Jump_Source;
    Rigidbody Player_rigidbody;//mass質量   
    AudioSource Move_Source;//轉動音效
    float x,z;


    private void Start () 
    {
        Time.timeScale = 1f;  //時間暫停
        Player_rigidbody = GetComponent<Rigidbody>();
        Move_Source = GetComponent<AudioSource>();
        InvokeRepeating("Time_time", 1f, 1f);//計時
    }

    int Seconds;//秒數
    int Minute;//分鐘
    int Hours;//小時
    float Total_hours;//總秒數
    [SerializeField, Header("所用時間")] Text Time_text;
    void Time_time()//時間計算
    {
        Seconds++;
        Total_hours++;
        Time_text.text = string.Format("{0:D2}:{1:D2}:{2:D2}", Hours, Minute, Seconds);
        if (Seconds==59)
        {
            Minute++;
            Seconds = 0;
        }

        if (Minute==59)
        {
            Hours++;
            Minute = 0;
        }
    }

    // Update is called once per frame
    private void Update () 
    {
        // PC_Move();
        CP_Move();
        Move();
        Rays();
    }

    private void PC_Move()//電腦移動
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
    }

    private void CP_Move()//手機移動
    {
        x = Input.acceleration.x;
        z = -Input.acceleration.z;
    }

    private void Move()//移動跳躍
    {
        transform.Translate(0, 0, speed * Time.deltaTime * z);
        transform.Rotate(0, 120 * Time.deltaTime * x,0);
        if (x!=0||z!=0)Move_Source.Play();//轉動音效
        else Move_Source.Stop();
        if (Input.GetButtonDown("Jump"))//跳躍
        {
            Jump();
        }
    }

    bool Jump_bool;
    public void Jump()//跳躍
    {
        if (Jump_bool)
        {
            Player_rigidbody.AddForce(transform.up * Jump_speed, ForceMode.VelocityChange);
            Jump_Source.Play();
        }
    }

    private void FixedUpdate()
    {
        if (Planetary_gravity_bool)
        {
            Planetary_gravity();
        }
    }

    bool Planetary_gravity_bool=true;//判斷是否開啟引力
    void Planetary_gravity()//星球引力
    {
        transform.rotation = Quaternion.FromToRotation(transform.up, transform.position - earthTransform.position) * transform.rotation;//旋轉
        Player_rigidbody.AddForce((earthTransform.position - transform.position).normalized * 10 * gravityScale, ForceMode.Acceleration);//引力物體引力
    }

    [SerializeField, Header("射線長度")] float rayLength;
    [SerializeField, Header("攝影機點")] GameObject Camera_point;
    [SerializeField, Header("跳躍按鈕")] GameObject Jump_obj;
    RaycastHit hit;//射線
    private void Rays()//射線偵測跳躍
    {
        if (Physics.Raycast(transform.position, -transform.up, out hit, rayLength))
        {
            Jump_bool = true; Camera_point.SetActive(false); Jump_obj.SetActive(true);     }
        else { Jump_bool = false; Camera_point.SetActive(true); Jump_obj.SetActive(false); }
    }

    void OnDrawGizmos()//顯示射線
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay (transform.position, -transform.up*rayLength);
    }


    public void Stand()//站立
    {
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }





    [SerializeField, Header("死亡次數")] int Number_deaths;
    [SerializeField, Header("抵達提示")] GameObject arrival;
    [SerializeField, Header("抵達文字")] Text arrival_text;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="重力點")
        {
            Planetary_gravity_bool = false;
            Player_rigidbody.useGravity = true;
            Stand();
        }
        if (other.gameObject.tag == "死亡區域")
        {
            Number_deaths++;
            transform.position =new Vector3(0, 77, 0);
            Planetary_gravity_bool = true;
            Player_rigidbody.useGravity = false;
            Stand();
        }

        if (other.gameObject.tag == "通關")
        {
            arrival.SetActive(true);
            CancelInvoke();
            arrival_text.text = "抵達終點" + "\n" + "所用總時間："+ string.Format("{0:D2}:{1:D2}:{2:D2}", Hours, Minute, Seconds) + "\n"+"死亡次數："+ Number_deaths;
            PlayerPrefs.SetFloat("總秒數", Total_hours);
            PlayerPrefs.SetInt("死亡次數", Number_deaths);
            //   Time.timeScale = 0f;  //時間暫停
            if (Ranking_bool==false)
            {
                Ranking_bool = true;
                Ranking();
            }
              
        }
    }



    bool Ranking_bool;
     public string[] player_text;
     public float[] Ranking_time;
     public int[] Ranking_deaths;

     void Ranking()//排行
     {
         for (int i = 0; i < Ranking_time.Length; i++)
         {
             player_text[i]= PlayerPrefs.GetString("第" + i.ToString() + "名稱");
             Ranking_time[i]= PlayerPrefs.GetFloat("第" + i.ToString()+"名總秒數");
             Ranking_deaths[i] = PlayerPrefs.GetInt("第" + i.ToString() + "名死亡次數");
         }
         Compare();
     }


     int Compare_int;//排名
     void Compare()//比較死亡成績
     {   
         for (int i = 0; i < Ranking_time.Length; i++)//比較死亡次數與時間
         {
            if (Total_hours< Ranking_time[i]|| Ranking_time[i]==0)
             {
                 Compare_int = i;//紀錄需要更新的名次                 
                 New_Ranking();//更新排行
                 return;
             }
            
        }
         arrival_text.text = "抵達終點" + "\n" + "真可惜沒能上榜呢~" + "\n" + "所用總時間：" + "\n" + string.Format("{0:D2}:{1:D2}:{2:D2}", Hours, Minute, Seconds) + "\n" + "死亡次數：" + Number_deaths;
         Time.timeScale = 0f;  //時間暫停
     }


     [SerializeField, Header("玩家名稱")] Text New_player_text;
     [SerializeField, Header("玩家名稱輸入")] GameObject New_player_text_Obj;
     void New_Ranking()//更新排行
     {
         
         for (int i = Ranking_time.Length-2; i >= Compare_int; i--)
         {
            PlayerPrefs.SetString("第" + (i + 1).ToString() + "名稱", player_text[i]);
            PlayerPrefs.SetFloat("第" + (i + 1).ToString() + "名總秒數", Ranking_time[i]);
            PlayerPrefs.SetInt("第" + (i + 1).ToString() + "名死亡次數", Ranking_deaths[i]);
        }
        
        arrival_text.text = "抵達終點" + "\n" + "喔呀？上榜了呢！" + "\n" + "所用總時間" + "\n" + string.Format("{0:D2}:{1:D2}:{2:D2}", Hours, Minute, Seconds) + "\n" + "死亡次數：" + Number_deaths;
         New_player_text_Obj.SetActive(true);
         PlayerPrefs.SetFloat("第" + Compare_int.ToString() + "名總秒數", Total_hours);
         PlayerPrefs.SetInt("第" + Compare_int.ToString() + "名死亡次數", Number_deaths);
         Time.timeScale = 0f;  //時間暫停
     }

     public void Save_update()//保存更新
     {
        player_text[Compare_int] = New_player_text.GetComponent<Text>().text;
         PlayerPrefs.SetString("第" + Compare_int.ToString() + "名稱", player_text[Compare_int]);
         SceneManager.LoadScene(0);
     }//*/

    public void Quit_game()
    {
        SceneManager.LoadScene(0);
    }

}
