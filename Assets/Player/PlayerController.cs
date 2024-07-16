using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    //���������Ա
    private Animator ani;
    private Rigidbody2D rbody;
    private int HP=1;

    void Start()
    {
        //��ȡ���
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

        //��û
        if (transform.position.y < -1.15f)
        {
            Debug.Log("��ˮ��û��" + gameObject.name);
            HP = 0;
            //ani.SetBool("isDie", true);//GG�������ɱ�ʶ
            Audio.Instance.AudioDie();//����GG��Ч
            GetComponent<CapsuleCollider2D>().enabled = false;//ȡ����ײ
            GetComponent<CircleCollider2D>().enabled = false;
            //rbody.velocity = Vector2.up * 0.1f;//GG��
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        rbody.velocity = new Vector2(horizontal, rbody.velocity.y);//����ˮƽ�ٶȣ���ֱ�ٶȲ���

        //��ת
        if (horizontal > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (horizontal < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        //����
        if (rbody.velocity.y == 0)
        {
            ani.SetBool("isIdle", true);
            //�ܲ�
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

        //���Ҵ���
        if (transform.position.x < -1.85f)
        {
            transform.position = new Vector3(1.95f, transform.position.y, transform.position.z);
        }
        if (transform.position.x > 1.95f)
        {
            transform.position = new Vector3(-1.85f, transform.position.y, transform.position.z);
        }

        //��ɲ���
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
                Debug.Log(collision.gameObject.name + "�ȱ���"+ gameObject.name);
                HP = 0;
                ani.SetBool("isDie", true);//GG�������ɱ�ʶ
                Audio.Instance.AudioDie();//����GG��Ч
                GetComponent<CapsuleCollider2D>().enabled = false;//ȡ����ײ
                GetComponent<CircleCollider2D>().enabled = false;
                rbody.velocity = Vector2.up * 0.1f;//GG��
            }
        }
    }
}
