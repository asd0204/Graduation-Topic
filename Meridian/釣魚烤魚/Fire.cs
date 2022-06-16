using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    //處理烤魚烤火相關事件

    RectTransform rt;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        Grilled_fish = GameObject.Find("烤魚系統");
    }

    [Header("烤魚系統")]
    public GameObject Grilled_fish;
    bool Make_fire;
    // Update is called once per frame
    void Update()
    {
        rt.anchoredPosition3D += new Vector3(0, speed, 0); // 設定 座標 Pos X, Pos Y, Pos Z
        if (rt.anchoredPosition3D.y>700&& Make_fire==false)
        {
            Grilled_fish.GetComponent<Grilled_fish>().fire();
            Make_fire = true;
        }
        if (rt.anchoredPosition3D.y > 1500)
        {
            Destroy(this.gameObject);
        }
    }
}
