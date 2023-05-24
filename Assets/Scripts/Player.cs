using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager gameManager;
    Rigidbody2D rb;
    SpriteRenderer sr;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").transform.GetComponent<GameManager>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(transform.position.y < -10)
        {
            Dead();
        }
    }

    //여기부터 ㄱㄱㄱㄱㄱ
    void GetHit(Transform targetPos)
    {
        //8 == damagedplayer
        gameObject.layer = 8;
        sr.color = new Color(1, 1, 1, 0.4f);

        //rb.velocity = new Vector2(0,0);
        int dir = transform.position.x - targetPos.position.x > 0 ? 1 : -1;
        rb.AddForce(new Vector2(dir, 1) * 7, ForceMode2D.Impulse);
        Invoke("OutHit", 3);
    }

    void OutHit()
    {
        //8 == damagedplayer
        gameObject.layer = 6;
        sr.color = new Color(1, 1, 1, 1f);
    }

    void Dead()
    {
        gameManager.GameOver();
        Destroy(gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Spike")
        {
            GetHit(collision.transform);
        }
        if (collision.gameObject.name == "Finish")
        {
            gameManager.StageMoved();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gold")
        {
            gameManager.GetCoin(10);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Silver")
        {
            gameManager.GetCoin(5);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Bronze")
        {
            gameManager.GetCoin(1);
            Destroy(collision.gameObject);
        }
    }
}
