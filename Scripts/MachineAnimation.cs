using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineAnimation : MonoBehaviour
{
    public Rigidbody rigidbody1;
    private Vector3 speed = new Vector3(0, -2, 0);         //声明下落速度
    int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.tag == "main" && i == 0)
        {
            rigidbody1.MovePosition(rigidbody1.position + speed * Time.deltaTime);  //向下运动
        }
        if (i == 1 && this.transform.tag == "main")                //表示机械手要上升
        {
            rigidbody1.MovePosition(rigidbody1.position - speed * Time.deltaTime);//向上运动
        }
        if (Input.GetKey(KeyCode.S))                   //为返回键添加监听
        {
            Application.Quit();                       //退出程序
        }
    }
    void OnCollisionEnter(Collision coll)               //碰撞监听事件
    {
        if (coll.collider.tag == "ball")                      //与球发生触碰
        {
            i = 1;                                      //表示上升状态
            this.rigidbody1.freezeRotation = false;                 //冻结机械手
        }
    }
}
