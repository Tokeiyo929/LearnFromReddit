using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCameraController : MonoBehaviour
{
    public Transform target; // 球心目标
    public float distance = 5.0f; // 初始距离
    public float minDistance = 1.0f; // 最小距离
    public float maxDistance = 10.0f; // 最大距离
    public float zoomSpeed = 5.0f; // 缩放速度
    public float rotateSpeed = 5.0f; // 旋转速度

    private float currentX = 0.0f; // 水平旋转角度
    private float currentY = 0.0f; // 垂直旋转角度
    private Vector3 lastMousePosition; // 记录上一帧鼠标位置

    void Start()
    {
        // 初始化相机位置
        UpdateCameraPosition();
    }

    void Update()
    {
        // 鼠标滚轮控制距离
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance = Mathf.Clamp(distance - scroll * zoomSpeed, minDistance, maxDistance);

        // 鼠标中键控制旋转
        if (Input.GetMouseButton(2)) // 2代表鼠标中键
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            currentX += delta.x * rotateSpeed * 0.02f;
            currentY -= delta.y * rotateSpeed * 0.02f;

            // 限制垂直角度，避免翻转
            currentY = Mathf.Clamp(currentY, -80, 80);
        }

        UpdateCameraPosition();
        lastMousePosition = Input.mousePosition;
    }

    void UpdateCameraPosition()
    {
        if (target == null)
            return;

        // 计算球面坐标
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = target.position + rotation * dir;

        // 相机始终看向目标
        transform.LookAt(target.position);
    }
}