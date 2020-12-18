using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_hing_joint : MonoBehaviour
{
    Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        this.body = this.GetComponent<Rigidbody>();
        this.body.AddForce(new Vector3(0, 0, 100));//给它一个z轴方向的力
        //为了观察到效果，要注意Anchor，Axis的设置
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
