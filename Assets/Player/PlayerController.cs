using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    //声明组件成员
    private Animator ani;
    private Rigidbody2D rbody;
    private int HP=1;

    void Start()
    {
        //获取组件
        ani = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        //GG
        if (HP == 0)
        {
            if (transform.position.y < -5f)
            {
                Destroy(gameObject);
                Debug.Log("Debug.Log(SceneManager.LoadScene(0)");
                SceneManager.LoadScene(0);
            }
            return;
        }

        //淹没
        if (transform.position.y < -1.15f)
        {
            Debug.Log("河水淹没了" + gameObject.name);
            HP = 0;
            //ani.SetBool("isDie", true);//GG动画过渡标识
            Audio.Instance.AudioDie();//调用GG音效
            GetComponent<CapsuleCollider2D>().enabled = false;//取消碰撞
            GetComponent<CircleCollider2D>().enabled = false;
            //rbody.velocity = Vector2.up * 0.1f;//GG跳
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        rbody.velocity = new Vector2(horizontal, rbody.velocity.y);//更改水平速度，垂直速度不变

        //翻转
        if (horizontal > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (horizontal < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        //地面
        if (rbody.velocity.y == 0)
        {
            ani.SetBool("isIdle", true);
            //跑步
            if (horizontal == 0)
            {
                ani.SetBool("isRun", false);
            }
            else
            {
                ani.SetBool("isRun", true);
            }

        }
        else
        {
            ani.SetBool("isIdle", false);
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

        //起飞操作
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            //rbody.velocity = new Vector2(rbody.velocity.x, 0.5f);
            rbody.velocity = new Vector2(rbody.velocity.x, rbody.velocity.y+0.015f);

        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (collision.gameObject.transform.position.y> transform.position.y)
            {
                Debug.Log(collision.gameObject.name + "踩爆了"+ gameObject.name);
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
