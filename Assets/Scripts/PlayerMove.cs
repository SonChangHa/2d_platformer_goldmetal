using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float resistanceValue;
    Rigidbody2D rigid;
    float h;
    //float rayLength = 1.0f;

    public float jumpSpeed;

    bool isJump = false;

    Animator anim;

    GameObject contactPlaform; //���� ������ �ٴ� �÷���
    Vector3 distance;
    

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

        //input�� update���� �ؾ���. fixed�� �����ӿ����� ������� �ʴ� ��찡 ����.
        if (Input.GetButtonUp("Horizontal"))
        {
            //Ű���� �� ���� ������ ���̱� ����. �̵������� ���͸� ����ȭ�� �������� ����.
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            jump();
        }

        anim.SetBool("isJump", isJump);

        //�����̴� ���ǿ� ���� + �̵����� �ƴҶ�
        if(contactPlaform != null && !isJump)
        {
            transform.position = new Vector3(transform.position.x, (contactPlaform.transform.position - distance).y, 0);
        }

        //JumpCheck();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //�Է�
        h = Input.GetAxisRaw("Horizontal");
        anim.SetBool("isRun", h != 0);
        move();
    }

    void jump()
    {
        rigid.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        isJump = true;
    }

    void move()
    {
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        //magnitude�� ������ ��ü�� üũ�ϱ⶧���� ���ϵ� ������. ���� �����ÿ��� �¿�� ������������ ���� �߻�.
        ////�÷��̾��� ���� �ӵ��� �ִ� �ӵ����� Ŭ��
        //if (rigid.velocity.magnitude > maxSpeed)
        //    // �ִ� �ӵ��� �ӵ��� ����, ���׿����ڷ� �¿� Ȯ��

        //    rigid.velocity = new Vector2(maxSpeed * rigid.velocity.x > 0 ? 1 : -1, rigid.velocity.y);


        // �� ������ ���밪���� �ذ�
        if (Mathf.Abs(rigid.velocity.x) > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed * rigid.velocity.x > 0 ? 1 : -1, rigid.velocity.y);
    }

    //����ĳ��Ʈ�� �ϸ� 2�������� ��. ������ �����Ҷ��� �ٴڿ� ��� ��� �����̶�.
    /*
    void JumpCheck()
    {
        //��ġ�����ϱ� �������� �÷��̾ ���̾�� ����
        int layerMask = -1 - (1 << LayerMask.NameToLayer("Player"));

        Debug.DrawRay(transform.position, -transform.up * rayLength, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, rayLength, layerMask);

        if(hit.collider != null)
        {
            if (hit.collider.tag == "Ground")
                isJump = false;
            else
                //�� �κ��� �־�� �̴� ������ �ȵ�, ������ �����Ҷ��� �ٴڿ� ����ؼ� üũ�� ����� �ȵ�.
                isJump = true;
        }

    }
    */

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�浹 ���� == contacts ��, ������ �浹���� ������ 1���� ������.
        ContactPoint2D contact = collision.contacts[0];
        //���� �浹������ ���� ���͸� Ȯ��
        Vector2 normal = contact.normal;
        // ���� �����̸� ���� �����, ���� ��ü �����̹Ƿ� ���� ��ü�κ��� �÷��̾ ���� �־�� ���� 
        if (normal == Vector2.up)
        {
            if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "MovedGround")
            {
                isJump = false;
            }
        }
        //�ٴڰ� ���� ������ �� �ֵ��� ���ִ� �ڵ�
        if (collision.gameObject.tag == "MovedGround")
        {
            //�ٴ��� �̵��� �÷��̾� �̵��� ����ȭ
            contactPlaform = collision.gameObject;
            distance = contactPlaform.transform.position - transform.position;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //���� ���� ���� �ʱ�ȭ
        contactPlaform = null;
    }
}
