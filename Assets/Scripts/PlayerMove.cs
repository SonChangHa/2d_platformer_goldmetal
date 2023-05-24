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

    GameObject contactPlaform; //현재 접촉한 바닥 플랫폼
    Vector3 distance;
    

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

        //input은 update에서 해야함. fixed는 프레임에따라 실행되지 않는 경우가 잦음.
        if (Input.GetButtonUp("Horizontal"))
        {
            //키에서 손 땔때 관성을 줄이기 위함. 이동방향의 벡터를 정규화해 절반으로 나눔.
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            jump();
        }

        anim.SetBool("isJump", isJump);

        //움직이는 발판에 닿음 + 이동중이 아닐때
        if(contactPlaform != null && !isJump)
        {
            transform.position = new Vector3(transform.position.x, (contactPlaform.transform.position - distance).y, 0);
        }

        //JumpCheck();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //입력
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

        //magnitude는 움직임 전체를 체크하기때문에 낙하도 포함함. 따라서 점프시에도 좌우로 움직여버리는 현상 발생.
        ////플레이어의 물리 속도가 최대 속도보다 클때
        //if (rigid.velocity.magnitude > maxSpeed)
        //    // 최대 속도로 속도를 고정, 삼항연산자로 좌우 확인

        //    rigid.velocity = new Vector2(maxSpeed * rigid.velocity.x > 0 ? 1 : -1, rigid.velocity.y);


        // 위 문제는 절대값으로 해결
        if (Mathf.Abs(rigid.velocity.x) > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed * rigid.velocity.x > 0 ? 1 : -1, rigid.velocity.y);
    }

    //레이캐스트로 하면 2단점프가 됨. 점프를 시작할때도 바닥에 잠깐 닿는 판정이라.
    /*
    void JumpCheck()
    {
        //위치설정하기 귀찮으니 플레이어만 레이어에서 제거
        int layerMask = -1 - (1 << LayerMask.NameToLayer("Player"));

        Debug.DrawRay(transform.position, -transform.up * rayLength, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, rayLength, layerMask);

        if(hit.collider != null)
        {
            if (hit.collider.tag == "Ground")
                isJump = false;
            else
                //이 부분이 있어야 이단 점프가 안됨, 점프를 시작할때도 바닥에 닿긴해서 체크가 제대로 안됨.
                isJump = true;
        }

    }
    */

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //충돌 지점 == contacts 즉, 수많은 충돌점중 최초의 1개를 가져옴.
        ContactPoint2D contact = collision.contacts[0];
        //최초 충돌지점의 법선 벡터를 확인
        Vector2 normal = contact.normal;
        // 위쪽 방향이면 점프 재수행, 닿은 물체 기준이므로 닿은 물체로부터 플레이어가 위에 있어야 적용 
        if (normal == Vector2.up)
        {
            if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "MovedGround")
            {
                isJump = false;
            }
        }
        //바닥과 같이 움직일 수 있도록 해주는 코드
        if (collision.gameObject.tag == "MovedGround")
        {
            //바닥의 이동과 플레이어 이동을 동기화
            contactPlaform = collision.gameObject;
            distance = contactPlaform.transform.position - transform.position;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //현재 접촉 발판 초기화
        contactPlaform = null;
    }
}
