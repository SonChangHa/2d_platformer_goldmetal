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

        /* magnitude�� ������ ��ü�� üũ�ϱ⶧���� ���ϵ� ������. ���� �����ÿ��� �¿�� ������������ ���� �߻�.
        //�÷��̾��� ���� �ӵ��� �ִ� �ӵ����� Ŭ��
        if (rigid.velocity.magnitude > maxSpeed)
            // �ִ� �ӵ��� �ӵ��� ����, ���׿����ڷ� �¿� Ȯ��
            rigid.velocity = new Vector2(maxSpeed * rigid.velocity.x > 0 ? 1 : -1, rigid.velocity.y);
        */

        // �� ������ ���밪���� �ذ�
        if (Mathf.Abs(rigid.velocity.x) > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed * rigid.velocity.x > 0 ? 1 : -1, rigid.velocity.y);


    }
}
