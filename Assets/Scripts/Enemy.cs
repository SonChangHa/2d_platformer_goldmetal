using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int forward = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        JumpCheck();
    }

    void JumpCheck()
    {
        Debug.DrawRay(new Vector2(transform.position.x * forward, transform.position.y), -transform.up, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x * forward, transform.position.y), -transform.up, 1);

        if (hit.collider != null)
        {

        }

    }
}
