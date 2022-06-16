using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{

    //每日怪生成系統

    //目前怪存活的數量
    public static int EnemyLiveCount = 0;

    [Header("怪物波數")]
    public Monster_everynight[] monster_everynight;//生成的怪物件、總共多少怪、多久生成


    [Header("生成位置")]
    public GameObject[] Generate_location;

    public GameObject monster_TEXT_obj;
    public Text monster_TEXT;

    int DAY_int;//音效
    // Start is called before the first frame update
    void Start()
    {
        DAY_int = GM.DAY_int;
        Generate_location = GameObject.FindGameObjectsWithTag("草原怪生成點");
        monster_TEXT_obj.SetActive(true);
        StartCoroutine(SpawerEnemy());
    }


    private void Update()
    {
        if (EnemyLiveCount==0)
        {
            this.gameObject.SetActive(false);
        }
        monster_TEXT.text = "剩餘魔族數量：" + EnemyLiveCount.ToString();
        if (EnemyLiveCount==0)
        {
            monster_TEXT_obj.SetActive(false);
        }
    }

    private IEnumerator SpawerEnemy()
    {

        //抓取所有波數
        foreach (Monster_everynight everynight in monster_everynight)
        {
            for (int i = 0; i < everynight.EnemyCount; i++)
            {
                //生成怪
                Instantiate(everynight.EnemyPrefab, Generate_location[Random.Range(0, Generate_location.Length)].transform.position, Generate_location[Random.Range(0, Generate_location.Length)].transform.rotation);
                //場中怪物的數量
                EnemyLiveCount++;
                //不是最後一波就暫停
                if (i != monster_everynight.Length - 1)
                {
                    //生成一個怪後暫停多少秒再繼續生成
                    yield return new WaitForSeconds(everynight.interval);
                }
            }
            //如果場上怪>5就不出下一波怪物
            while (EnemyLiveCount > 5)
            {
                yield return 0;
            }
            //生成每一波怪之間的時間間隔
            yield return new WaitForSeconds(0.2f);
        }
    }
}
