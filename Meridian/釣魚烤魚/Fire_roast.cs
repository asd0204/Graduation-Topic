using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Fire_roast : MonoBehaviour
{
    //處理烤魚的魚相關事件

    [Header("烤魚系統")]
    public GameObject Grilled_fish;
    [Header("烤魚熟度")]
    public int fire;
    [Header("烤魚圖")]
    public Sprite[] GrilledFish_illustration;
    Image Fish_Image;
   
    // Start is called before the first frame update
    void Start()
    {
        Grilled_fish = GameObject.Find("烤魚系統");
        Fish_Image = GetComponent<Image>();
    }



    float OffsetX;
    float OffsetY;

    public void BeginDrag()
    {
        OffsetX = transform.position.x - Input.mousePosition.x; ;
        OffsetY = transform.position.y - Input.mousePosition.y; ;
    }

    public void OnDrag()
    {
        transform.position = new Vector3(Input.mousePosition.x + OffsetX, Input.mousePosition.y + OffsetY);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "火")//觸碰火熟程度+1
        {
            Grilled_fish.GetComponent<Grilled_fish>().Fire_Clip();
            Maturity();//烤魚判斷
            Destroy(collision.gameObject);//刪除火
        }
        if (collision.tag == "收穫")//觸碰火熟程度+1
        {
            if (Fragrant_fish&& fire==10)
            {
                Grilled_fish.GetComponent<Grilled_fish>().successfully_text();
            }
            else if(Fragrant_fish && fire > 10) { Grilled_fish.GetComponent<Grilled_fish>().failed_text(); }
            Destroy(this.gameObject);//刪除魚
        }
    }

    bool Fragrant_fish;//可拿起烤魚
    void Maturity()
    {
        fire++;//烤魚熟度增加
        switch (fire)
        {
            case 5://五分熟
                Fish_Image.sprite = GrilledFish_illustration[0];
                break;
            case 10://全熟
                Fish_Image.sprite = GrilledFish_illustration[1];
                Fragrant_fish = true;
                break;
        }
        if (fire>10)//焦掉
        {
            Fragrant_fish = true;
            Fish_Image.sprite = GrilledFish_illustration[2];
        }
    }
}
