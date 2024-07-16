using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    //���������Ա
    private Animator ani;
    private Rigidbody2D rbody;

    private Vector3 dir = Vector3.zero;
    private int HP = 1;

    void Start()
    {
        //��ȡ���
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
        //��û
        if (transform.position.y < -1.15f)
        {
            Destroy(gameObject);
            Audio.Instance.AudioDie();//����GG��Ч
            Debug.Log("��ˮ��û��" + gameObject.name);
            return;
        }

        //��ɲ���
        transform.Translate(dir * Time.deltaTime * 0.5f);
        if (rbody.velocity.y == 0 || rbody.velocity.y < -0.5f)
        {
            //rbody.velocity = new Vector2(rbody.velocity.x, rbody.velocity.y + 0.6f);
            rbody.velocity = Vector2.up * 1f;
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




    }

    void SearchPlayer()
    {
        if (!GameObject.FindWithTag("Player") || HP==0 )
        {
            return;
        }

        dir = GameObject.FindWithTag("Player").transform.position - transform.position;//�����������
        dir = dir.normalized;

        //��ת
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
                Debug.Log(collision.gameObject.name + "�ȱ���" + gameObject.name);
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