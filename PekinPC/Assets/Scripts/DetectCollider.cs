using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollider : MonoBehaviour
{
    //碰撞检测，打印碰撞物体名称
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("碰撞物体名称：" + other.gameObject.name);
    }

    private CharacterController character;
    private float speedRate = 4f;

    private void Start()
    {
        character = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        // 根据物体当前朝向计算移动方向
        Vector3 moveDirection = transform.forward * ver + transform.right * hor;
        Vector3 speed = moveDirection * speedRate;

        character.SimpleMove(speed);
    }

    //鼠标控制旋转
    private void LateUpdate()
    {
        float rotateSpeed = 100f;
        float rotateHorizontal = Input.GetAxis("Mouse X");
        float rotateVertical = Input.GetAxis("Mouse Y");

        // 更合理的旋转方式，避免z轴问题
        transform.Rotate(Vector3.up, rotateHorizontal * rotateSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.right, -rotateVertical * rotateSpeed * Time.deltaTime, Space.Self);

        // 锁定z轴旋转（如果需要）
        Vector3 euler = transform.eulerAngles;
        euler.z = 0;
        transform.eulerAngles = euler;
    }
}