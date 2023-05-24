using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //이거보다 그냥 바로 넣어버리는게 더 깔끔하다
    //생각해보니까 fixed time step을 짧게해주면 연산이 빨라져서 좀 더 자연스러움
    //그냥 코드로 하는걸로

    GameObject player;
    public float cameraSpeed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        { 
            Vector2 dir = player.transform.position - this.transform.position;
            Vector2 moveVector = dir * cameraSpeed * Time.deltaTime;
            this.transform.Translate(moveVector);
        }
    }
}
