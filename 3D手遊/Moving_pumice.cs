using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_pumice : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    [SerializeField, Header("速度")] float speed;
    private void Move()
    {
        if (rotation_float == 0)
        {
            transform.position += new Vector3(speed*Time.deltaTime, 0, 0);
        }
        else { transform.position += new Vector3(-speed * Time.deltaTime, 0, 0); }
    }

    float rotation_float=0;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag!="Player")
        {
            if (rotation_float == 1)
            {
                rotation_float = 0;
            }
            else { rotation_float = 1; }
        }
    }
}
