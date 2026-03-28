using UnityEngine;

/// <summary>
/// 相机控制器 - 支持鼠标拖拽旋转、滚轮缩放
/// </summary>
public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float zoomSpeed = 10f;
    [SerializeField] private float minDistance = 5f;
    [SerializeField] private float maxDistance = 100f;
    [SerializeField] private float smoothTime = 0.1f;

    [Header("Initial Position")]
    [SerializeField] private Vector3 targetPosition = Vector3.zero;
    [SerializeField] private float initialDistance = 30f;
    [SerializeField] private float initialYaw = 45f;
    [SerializeField] private float initialPitch = 30f;

    private float currentYaw;
    private float currentPitch;
    private float currentDistance;
    private float targetYaw;
    private float targetPitch;
    private float targetDistance;
    
    private float yawVelocity;
    private float pitchVelocity;
    private float distanceVelocity;

    private bool isDragging = false;

    private void Start()
    {
        // 初始化相机位置和角度
        targetYaw = initialYaw;
        targetPitch = initialPitch;
        targetDistance = initialDistance;
        
        currentYaw = targetYaw;
        currentPitch = targetPitch;
        currentDistance = targetDistance;
        
        UpdateCameraPosition();
    }

    private void Update()
    {
        HandleInput();
        SmoothUpdate();
    }

    private void HandleInput()
    {
        // 鼠标右键拖拽旋转
        if (Input.GetMouseButtonDown(1))
        {
            isDragging = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            float deltaX = Input.GetAxis("Mouse X");
            float deltaY = Input.GetAxis("Mouse Y");

            targetYaw += deltaX * rotationSpeed * 10f;
            targetPitch -= deltaY * rotationSpeed * 10f;
            
            // 限制俯仰角
            targetPitch = Mathf.Clamp(targetPitch, -89f, 89f);
        }

        // 滚轮缩放
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            targetDistance -= scroll * zoomSpeed;
            targetDistance = Mathf.Clamp(targetDistance, minDistance, maxDistance);
        }
    }

    private void SmoothUpdate()
    {
        // 平滑插值到目标值
        currentYaw = Mathf.SmoothDamp(currentYaw, targetYaw, ref yawVelocity, smoothTime);
        currentPitch = Mathf.SmoothDamp(currentPitch, targetPitch, ref pitchVelocity, smoothTime);
        currentDistance = Mathf.SmoothDamp(currentDistance, targetDistance, ref distanceVelocity, smoothTime);

        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        // 将球坐标转换为笛卡尔坐标
        float yawRad = currentYaw * Mathf.Deg2Rad;
        float pitchRad = currentPitch * Mathf.Deg2Rad;

        float x = targetPosition.x + currentDistance * Mathf.Cos(pitchRad) * Mathf.Sin(yawRad);
        float y = targetPosition.y + currentDistance * Mathf.Sin(pitchRad);
        float z = targetPosition.z + currentDistance * Mathf.Cos(pitchRad) * Mathf.Cos(yawRad);

        transform.position = new Vector3(x, y, z);
        transform.LookAt(targetPosition);
    }

    // 公共方法：重置相机位置
    public void ResetCamera()
    {
        targetYaw = initialYaw;
        targetPitch = initialPitch;
        targetDistance = initialDistance;
    }

    // 公共方法：设置目标位置
    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
    }
}
