using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    [Header("移动参数")]
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float gravity = 9.81f;
    public float jumpHeight = 3f;
    public float airControlMultiplier = 0.5f; // 空中控制系数

    [Header("视角参数")]
    public float sensitivityX = 5f;
    public float sensitivityY = 5f;
    public float rightClickSensitivityX = 5f;
    public float rightClickSensitivityY = 5f;
    public float minimumY = -60f;
    public float maximumY = 60f;
    public KeyCode rotateKey = KeyCode.Mouse1;
    public float smoothTime = 0.1f;

    [Header("引用")]
    public Transform cameraTransform;

    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private Vector2 currentRotation;
    private Vector2 rotationVelocity;
    private bool isRotating = false;
    private bool isJumping = false; // 跟踪跳跃状态

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (cameraTransform == null)
            cameraTransform = GetComponentInChildren<Camera>().transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // 处理视角控制
        HandleCameraRotation();

        // 处理角色移动（无地面检测）
        HandleMovement();
    }

    private void HandleCameraRotation()
    {
        // 右键按下/释放切换旋转状态
        if (Input.GetKeyDown(rotateKey))
        {
            isRotating = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (Input.GetKeyUp(rotateKey))
        {
            isRotating = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // 获取鼠标输入
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // 根据是否右键点击使用不同的灵敏度
        float useSensitivityX = isRotating ? rightClickSensitivityX : sensitivityX;
        float useSensitivityY = isRotating ? rightClickSensitivityY : sensitivityY;

        // 计算目标旋转
        Vector2 targetRotation = new Vector2(
            currentRotation.x + mouseX * useSensitivityX,
            currentRotation.y + mouseY * useSensitivityY
        );

        // 限制垂直旋转角度
        targetRotation.y = Mathf.Clamp(targetRotation.y, minimumY, maximumY);

        // 平滑过渡
        currentRotation = Vector2.SmoothDamp(
            currentRotation,
            targetRotation,
            ref rotationVelocity,
            smoothTime
        );

        // 应用旋转 - 水平旋转角色，垂直旋转相机
        transform.localRotation = Quaternion.Euler(0, currentRotation.x, 0);
        cameraTransform.localRotation = Quaternion.Euler(-currentRotation.y, 0, 0);
    }

    private void HandleMovement()
    {
        // 获取输入
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 计算移动方向
        Vector3 moveInput = new Vector3(horizontal, 0, vertical);
        Vector3 worldMoveDirection = transform.TransformDirection(moveInput);

        // 应用速度
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        // 空中控制减弱
        float controlMultiplier = isJumping ? airControlMultiplier : 1f;
        worldMoveDirection *= currentSpeed * controlMultiplier;

        // 处理跳跃
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            moveDirection.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
        }

        // 应用重力（可选：如果不需要重力，注释此行）
        moveDirection.y -= gravity * Time.deltaTime;

        // 合并移动方向
        Vector3 finalMoveDirection = worldMoveDirection;
        finalMoveDirection.y = moveDirection.y;

        // 执行移动
        controller.Move(finalMoveDirection * Time.deltaTime);

        // 检测是否触底（替代地面检测）
        if (controller.collisionFlags == CollisionFlags.Below)
        {
            isJumping = false;
            moveDirection.y = 0; // 重置垂直速度
        }
    }
}