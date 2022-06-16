using UnityEngine;
using UnityEngine.UI;

public class Ranking_system : MonoBehaviour
{

    public string[] Ranking_player;
    public float[] Ranking_time;
    public int[] Ranking_deaths;

    int Seconds;//秒數
    int Minute;//分鐘
    int Hours;//小時

    [SerializeField, Header("玩家名稱")] Text[] player_text;
    [SerializeField, Header("死亡次數")] Text[] deaths_text;
    [SerializeField, Header("所用時間")] Text[] Time_text;

    // Start is called before the first frame update
    void Start()
    {
       // PlayerPrefs.DeleteAll();
        Ranking_show();
    }

    void Ranking_show()
    {
        for (int i = 0; i < Ranking_time.Length; i++)
        {
            Ranking_player[i] = PlayerPrefs.GetString("第" + i.ToString() + "名稱");
            Ranking_time[i] = PlayerPrefs.GetFloat("第" + i.ToString() + "名總秒數");
            Ranking_deaths[i] = PlayerPrefs.GetInt("第" + i.ToString() + "名死亡次數");           
            if (Ranking_time[i] > 0)
            {
                Seconds = (int)Mathf.Ceil(Ranking_time[i] % 60);//秒數無條件進位取餘值
                Minute = (int)Mathf.Ceil(Mathf.Floor(Ranking_time[i] / 60) % 60);//秒數無條件捨去
                Hours = (int)Mathf.Floor(Ranking_time[i] / 3600);//無條件捨去
            }
            else{Seconds = 0;Minute = 0;Hours = 0;}
            player_text[i].text = Ranking_player[i];
            deaths_text[i].text = Ranking_deaths[i].ToString();
            Time_text[i].text = string.Format("{0:D2}:{1:D2}:{2:D2}", Hours, Minute, Seconds);
        }
    }

}
