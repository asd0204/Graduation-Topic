using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grassland_system : MonoBehaviour
{
    //處理草原事件

    [Header("天數")]
    public int DAY_int;

    public GameObject[] monster;

    public void into_the_night()//進入夜晚
    {
        DAY_int = GM.DAY_int;
        if (DAY_int!=0)
        {
            monster[DAY_int - 1].SetActive(true);
        }
    }
}
[System.Serializable]  //實現序列化 可以在屬性面板展開
public class Monster_everynight
{
    [Header("要生成的怪")]
    [SerializeField] public GameObject EnemyPrefab;

    [Header("要生成的數量")]
    [SerializeField] public int EnemyCount;

    [Header("生成的間隔時間")]
    [SerializeField] public float interval;
}
