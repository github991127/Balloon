using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    //声明组件成员
    private Animator ani;
    private Rigidbody2D rbody;

    private Vector3 dir = Vector3.zero;
    private int HP = 1;

    void Start()
    {
        //获取组件
        ani = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        InvokeRepeating("SearchPlayer", 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        //GG
        if (HP == 0)
        {
            if (transform.position.y < -1.5f)
            {
                Destroy(gameObject);
            }
            return;
        }
        //淹没
        if (transform.position.y < -1.15f)
        {
            Destroy(gameObject);
            Audio.Instance.AudioDie();//调用GG音效
            Debug.Log("河水淹没了" + gameObject.name);
            return;
        }

        //起飞操作
        transform.Translate(dir * Time.deltaTime * 0.5f);
        if (rbody.velocity.y == 0 || rbody.velocity.y < -0.5f)
        {
            //rbody.velocity = new Vector2(rbody.velocity.x, rbody.velocity.y + 0.6f);
            rbody.velocity = Vector2.up * 1f;
        }

        //左右穿屏
        if (transform.position.x < -1.85f)
        {
            transform.position = new Vector3(1.95f, transform.position.y, transform.position.z);
        }
        if (transform.position.x > 1.95f)
        {
            transform.position = new Vector3(-1.85f, transform.position.y, transform.position.z);
        }




    }

    void SearchPlayer()
    {
        if (!GameObject.FindWithTag("Player") || HP==0 )
        {
            return;
        }

        dir = GameObject.FindWithTag("Player").transform.position - transform.position;//朝向玩家向量
        dir = dir.normalized;

        //翻转
        if (dir.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (dir.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.gameObject.transform.position.y > transform.position.y)
            {
                Debug.Log(collision.gameObject.name + "踩爆了" + gameObject.name);
                HP = 0;
                ani.SetBool("isDie", true);//GG动画过渡标识
                Audio.Instance.AudioDie();//调用GG音效
                GetComponent<CapsuleCollider2D>().enabled = false;//取消碰撞
                GetComponent<CircleCollider2D>().enabled = false;
                rbody.velocity = Vector2.up * 0.1f;//GG跳
            }
        }
    }
}