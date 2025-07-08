using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowardsCamera : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    void Update()
    {
        transform.eulerAngles = new Vector3(0, cameraTransform.eulerAngles.y, 0);
    }
}

