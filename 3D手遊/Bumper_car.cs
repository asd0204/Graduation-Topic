using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper_car : MonoBehaviour
{
    public Vector3 dir;//this.gameobject的方向
    [Header("速度")]
    public float speed;//速度
    // Start is called before the first frame update
    void Start()
    {
        dir = new Vector3(Random.Range(180, -180), 0, Random.Range(180, -180));//初始方向 
        InvokeRepeating("Mobile_dir", 10f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        Mobile();
    }

    void Mobile()//移動
    {
       // this.transform.localPosition -= dir.normalized * speed * Time.deltaTime;
        transform.Translate(dir*speed * Time.deltaTime);
    }

    void Mobile_dir()
    {
        dir = new Vector3(Random.Range(180, -180), 0, Random.Range(180, -180));//初始方向
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "地板" || collision.gameObject.tag != "Player")//攻擊到玩家
        {
           // this.gameObject.GetComponent<Transform>().position += new Vector3(10, 10, 0) * Time.deltaTime;
            dir = new Vector3(Random.Range(180, -180), 0, Random.Range(180, -180));//初始方向 
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "地板")//攻擊到玩家
        {
            // this.gameObject.GetComponent<Transform>().position += new Vector3(10, 10, 0) * Time.deltaTime;
            dir = new Vector3(Random.Range(180, -180), 0, Random.Range(180, -180));//初始方向 
        }
    }
}
