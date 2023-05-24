using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //�̰ź��� �׳� �ٷ� �־�����°� �� ����ϴ�
    //�����غ��ϱ� fixed time step�� ª�����ָ� ������ �������� �� �� �ڿ�������
    //�׳� �ڵ�� �ϴ°ɷ�

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
