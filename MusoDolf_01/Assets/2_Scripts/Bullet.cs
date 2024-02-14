using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    Rigidbody2D rigid;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // 총알에 능력치값 세팅 ( damage : 데미지, per : 관통력 , dir : 방향 )
    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        if(per >= 0)
        {
            rigid.velocity = dir * 15f;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 적에게 총알이 맞을때마다 관통력 -1 . . 0이하가 되면 active false
        if (!collision.CompareTag("Enemy") || per == -100)
            return;

        per--;

        if(per < 0) // 관통 이후의 로직을 조금 더 느슨하게 변경
        {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area") || per == -100)
            return;

        gameObject.SetActive(false);
    }
}