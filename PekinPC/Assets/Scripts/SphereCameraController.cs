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
        transform.position = (target.position + rotation * dir);
        transform.LookAt(target);
    }
    public void ChangeTarget(Transform trans)
    {
        target = trans;
        currentY = 45f;
        currentX = 90f;
        distance = 2.5f;
    }
}