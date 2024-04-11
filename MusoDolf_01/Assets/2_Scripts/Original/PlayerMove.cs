using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    Rigidbody2D rigid;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // 입력받은 벡터 방향에 따라 실제로 플레이어 위치를 움직임(물리연산 FixedUpdate)
    void FixedUpdate()
    {   
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }
   

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
}
