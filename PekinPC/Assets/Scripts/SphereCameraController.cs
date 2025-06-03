using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCameraController : MonoBehaviour
{

    [SerializeField] Transform target;

    [SerializeField] float distance = 10f;
    [SerializeField] float maxDistance = 30f;
    [SerializeField] float minDistance = 5f;

    [SerializeField] float zoomSpeed = 5f;
    [SerializeField] float rotateSpeed = 10f;

    Vector3 lastMousePosition;
    float currentY = 0f;
    float currentX = 0f;

    private void Start()
    {
        UpdateCameraPosition();
    }

    private void Update()
    {
        float scaleValue = Input.GetAxis("Mouse ScrollWheel");
        distance = Mathf.Clamp(distance - scaleValue * zoomSpeed, minDistance, maxDistance);
        if (Input.GetMouseButton(2))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            currentX += delta.x * rotateSpeed * 0.02f;
            currentY -= delta.y * rotateSpeed * 0.02f;
            currentY = Mathf.Clamp(currentY, -80, 80);
        }
        UpdateCameraPosition();
        lastMousePosition = Input.mousePosition;
    }

    void UpdateCameraPosition()
    {
        if (target == null)
            return;
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = (target.position + rotation * dir);
        transform.LookAt(target);
    }
}