using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{

    public GameObject plane;
    public GameObject spawnPoint;
    public GameObject camera;
    private Rigidbody rb; // 缓存 Rigidbody 组件
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // // 启用陀螺仪
        // if (SystemInfo.supportsGyroscope) {
        //     Input.gyro.enabled = true;
        // } else {
        //     Debug.LogWarning("设备不支持陀螺仪！");
        // }
        //
        // if (!Application.HasUserAuthorization(UserAuthorization.WebCam)) {
        //     Application.RequestUserAuthorization(UserAuthorization.WebCam);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        // // 动态更新 Unity 的重力方向
        // if (SystemInfo.supportsGyroscope) {
        //     Vector3 deviceGravity = Input.gyro.gravity; // 获取设备重力方向
        //     Physics.gravity = deviceGravity * 9.81f;    // 更新 Unity 重力方向，保持现实一致
        // }
        
        // 获取 ARCamera 的 Transform
        Transform arCameraTransform = camera.transform;

        // 计算摄像头方向上的重力
        Vector3 worldGravity = Vector3.down; // Unity 世界的重力方向
        Vector3 adjustedGravity = arCameraTransform.rotation * worldGravity;

        // 应用到 Unity 的全局重力
        Physics.gravity = adjustedGravity * 9.81f; // 调整为现实重力值
        
        if (transform.position.y < plane.transform.position.y - 10 ||
            transform.position.y > plane.transform.position.y + 10) {
            transform.position = spawnPoint.transform.position;

            // 重置速度和角速度
            if (rb) {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}
