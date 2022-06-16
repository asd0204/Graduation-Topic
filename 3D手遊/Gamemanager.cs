using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    float Total_hours;
    float Total_hours_max;
    int Number_deaths;
    int Number_deaths_max;
    public GameObject Highest_record_OBJ;
    public GameObject DeleteAll_OBJ;
    public Text Highest_record;
    int Seconds;//秒數
    int Minute;//分鐘
    int Hours;//小時
    private void Start()
    {
        DeleteAll_OBJ.SetActive(false);
        Highest_record_OBJ.SetActive(false);
        Total_hours = PlayerPrefs.GetFloat("總秒數");
        Total_hours_max = PlayerPrefs.GetFloat("最低總秒數");
        Number_deaths= PlayerPrefs.GetInt("死亡次數");
        Number_deaths_max = PlayerPrefs.GetInt("最低死亡次數");

        if (Total_hours_max==0 && Number_deaths_max==0 && Total_hours != 0)
        {
            DeleteAll_OBJ.SetActive(true);
            Highest_record_OBJ.SetActive(true);
            Seconds = (int)Mathf.Ceil(Total_hours % 60);//秒數無條件進位取餘值
            Minute = (int)Mathf.Ceil(Mathf.Floor(Total_hours / 60) % 60);//秒數無條件捨去
            Hours = (int)Mathf.Floor(Total_hours / 3600);//無條件捨去
            Highest_record.text = "最高紀錄" + "\n" + "時間：" + string.Format("{0:D2}:{1:D2}:{2:D2}", Hours, Minute, Seconds) + "\n" + "死亡次數：" + Number_deaths;
            PlayerPrefs.SetFloat("最低總秒數", Total_hours);
            PlayerPrefs.SetInt("最低死亡次數", Number_deaths);
        }

        if (Total_hours < Total_hours_max && Number_deaths <= Number_deaths_max && Total_hours != 0)
        {
            Highest_record_OBJ.SetActive(true);
            DeleteAll_OBJ.SetActive(true);
            Seconds = (int)Mathf.Ceil(Total_hours % 60);//秒數無條件進位取餘值
            Minute = (int)Mathf.Ceil(Mathf.Floor(Total_hours / 60) % 60);//秒數無條件捨去
            Hours = (int)Mathf.Floor(Total_hours / 3600);//無條件捨去
            Highest_record.text = "最高紀錄" + "\n" + "時間：" + string.Format("{0:D2}:{1:D2}:{2:D2}", Hours, Minute, Seconds) + "\n" +"死亡次數："+ Number_deaths;
            PlayerPrefs.SetFloat("最低總秒數", Total_hours);
            PlayerPrefs.SetInt("最低死亡次數", Number_deaths);
        }
        else if(Total_hours_max!=0)
        {
            Highest_record_OBJ.SetActive(true);
            DeleteAll_OBJ.SetActive(true);
            Seconds = (int)Mathf.Ceil(Total_hours_max % 60);//秒數無條件進位取餘值
            Minute = (int)Mathf.Ceil(Mathf.Floor(Total_hours_max / 60) % 60);//秒數無條件捨去
            Hours = (int)Mathf.Floor(Total_hours_max / 3600);//無條件捨去
            Highest_record.text = "最高紀錄" + "\n" + "時間：" + string.Format("{0:D2}:{1:D2}:{2:D2}", Hours, Minute, Seconds) + "\n" + "死亡次數：" + Number_deaths_max;
        }
    }


    public void Game()//開始遊戲
    {
        SceneManager.LoadScene(1);
    }


    [SerializeField, Header("遊戲玩法")] GameObject Gameplay_obj;
    bool Gameplay_bool;
    public void Gameplay()//遊戲玩法
    {
        if (Gameplay_bool)
        {
            Gameplay_bool = false;
            Gameplay_obj.SetActive(false);
        }
        else
        {
            Gameplay_bool = true;
            Gameplay_obj.SetActive(true);
        }
    }


    [SerializeField, Header("排行榜")] GameObject Ranking_obj;
    bool Ranking_bool;
    public void Ranking()//排行榜
    {
        if (Ranking_bool)
        {
            Ranking_bool = false;
            Ranking_obj.SetActive(false);
        }
        else
        {
            Ranking_bool = true;
            Ranking_obj.SetActive(true);
        }
    }


    [SerializeField, Header("音量設定")] GameObject Volume_obj;
    bool Volume_bool;
    public void Volume()//音量設定
    {
        if (Volume_bool)
        {
            Volume_false();
        }
        else
        {
            Volume_true();
        }
    }

    public void Volume_true()
    {
        Volume_bool = true;
        Volume_obj.SetActive(true);
        Volume_obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0F, 0F);
    }

    void Volume_false()
    {
        Volume_bool = false;
        Volume_obj.SetActive(false);
    }


    public void Quit_game()//離開遊戲
    {
        Application.Quit();
    }

    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
}
