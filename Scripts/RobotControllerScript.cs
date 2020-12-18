using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotControllerScript : MonoBehaviour
{
    //机器人控制
    public Transform axis1_tf, axis2_tf, axis3_tf;      //各个轴的Transform
    public float axis1_angle, axis2_angle, axis3_angle; //各个轴的角度
    private Quaternion axis1_rot, axis2_rot, axis3_rot; //转换更新Transform

    //正运动学
    private Matrix4x4 T01, T12, T23;    
    private Vector4 hand_pos_world;     
    private Vector4 hand_pos_local;     

    //UI
    public UnityEngine.UI.Text transform_label;
    public UnityEngine.UI.Text forward_K_label;
    public Transform hand_transform;
    // Start is called before the first frame update
    void Start()
    {
        //初始化：齐次坐标变换矩阵
        T01 = Matrix4x4.identity;
        T12 = Matrix4x4.identity;
        T23 = Matrix4x4.identity;

        //在最后一个关节的坐标系中设置手臂尖端的局部坐标（这不会改变）
        hand_pos_local = new Vector4(0f, 3f, 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        

        //根据输入的角度进行计算
        axis1_rot = Quaternion.AngleAxis(axis1_angle, Vector3.up);
        axis2_rot = Quaternion.AngleAxis(axis2_angle, Vector3.right);
        axis3_rot = Quaternion.AngleAxis(axis3_angle, Vector3.right);

        axis1_tf.localRotation = axis1_rot;
        axis2_tf.localRotation = axis2_rot;
        axis3_tf.localRotation = axis3_rot;



        //齐次坐标变换矩阵
        T01.SetTRS(new Vector3(0f, 1f, 0f),
                    Quaternion.AngleAxis(axis1_angle, Vector3.up),
                    new Vector3(1f, 1f, 1f));

        T12.SetTRS(new Vector3(0f, 1f, 0f),
                    Quaternion.AngleAxis(axis2_angle, Vector3.right),
                    new Vector3(1f, 1f, 1f));

        T23.SetTRS(new Vector3(0f, 3f, 0f),
                    Quaternion.AngleAxis(axis3_angle, Vector3.right),
                    new Vector3(1f, 1f, 1f));

        //正运动学计算 手臂的位置
        hand_pos_world = T01 * T12 * T23 * hand_pos_local;

        //UI上表示
        transform_label.text = "Transform X:" + hand_transform.position.x.ToString("F2") +
                        " Y:" + hand_transform.position.y.ToString("F2") +
                        " Z:" + hand_transform.position.z.ToString("F2");

        forward_K_label.text = "Forward K X:" + hand_pos_world.x.ToString("F2") +
                        " Y:" + hand_pos_world.y.ToString("F2") +
                        " Z:" + hand_pos_world.z.ToString("F2");

    
}
}
