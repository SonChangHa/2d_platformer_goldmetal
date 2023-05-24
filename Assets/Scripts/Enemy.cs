using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int forward = -1;
    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(forward * speed * Time.deltaTime, 0));
        FindGround();
    }

    void FindGround()
    {
        //0.5만큼만 앞에 레이캐스트가 생성되게
        Debug.DrawRay(new Vector2(transform.position.x + forward, transform.position.y), -transform.up, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + forward, transform.position.y), -transform.up, 1);

        if (hit.collider == null)
        {
            forward *= -1;
        }

    }
}
