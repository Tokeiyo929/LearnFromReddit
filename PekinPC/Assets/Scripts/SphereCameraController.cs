using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCameraController : MonoBehaviour
{

    public Transform target;

    [SerializeField] float distance = 20f;
    [SerializeField] float maxDistance = 20f;
    [SerializeField] float minDistance = 1f;

    [SerializeField] float zoomSpeed = 5f;
    [SerializeField] float rotateSpeed = 10f;

    float smoothTime = 0.3f;
    Vector3 desiredPosition;
    Quaternion desiredRotation;

    Vector3 lastMousePosition;
    float currentY = 25f;
    float currentX = 90f;

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

        desiredPosition = (target.position + rotation * dir);
        desiredRotation = Quaternion.LookRotation(target.position - desiredPosition);

        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime / smoothTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime / smoothTime);
    }
}