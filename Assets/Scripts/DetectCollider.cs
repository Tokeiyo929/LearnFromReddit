using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollider : MonoBehaviour
{
    
    private CharacterController character;
    private float speedRate = 4f;

    public Transform cameraTransform;
    private float pitch = 0f;
    private float rotateSpeed = 100f;

    private void Start()
    {
        character = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * ver + transform.right * hor;
        Vector3 speed = moveDirection * speedRate;

        character.SimpleMove(speed);

        RotateView();
    }

    private void RotateView()
    {
        float rotateHorizontal = Input.GetAxis("Mouse X");
        float rotateVertical = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up, rotateHorizontal * rotateSpeed * Time.deltaTime, Space.World);
        
        pitch -= rotateVertical * rotateSpeed * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -80f, 80f);

        if(cameraTransform != null)
            cameraTransform.localEulerAngles = new Vector3(pitch, 0f, 0f);

    }
}