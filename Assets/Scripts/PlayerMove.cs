using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float resistanceValue;
    Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        /* magnitude는 움직임 전체를 체크하기때문에 낙하도 포함함. 따라서 점프시에도 좌우로 움직여버리는 현상 발생.
        //플레이어의 물리 속도가 최대 속도보다 클때
        if (rigid.velocity.magnitude > maxSpeed)
            // 최대 속도로 속도를 고정, 삼항연산자로 좌우 확인
            rigid.velocity = new Vector2(maxSpeed * rigid.velocity.x > 0 ? 1 : -1, rigid.velocity.y);
        */

        // 위 문제는 절대값으로 해결
        if (Mathf.Abs(rigid.velocity.x) > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed * rigid.velocity.x > 0 ? 1 : -1, rigid.velocity.y);


    }
}
