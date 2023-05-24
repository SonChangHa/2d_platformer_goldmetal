using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMove : MonoBehaviour
{
    Vector3 moveVec;
    float runtime;
    public float height;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        runtime += Time.deltaTime;
        moveVec = new Vector3(transform.position.x, Mathf.Sin(runtime) * height, 0);
        transform.position = moveVec;
    }
}
