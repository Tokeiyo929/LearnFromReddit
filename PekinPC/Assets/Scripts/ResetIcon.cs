using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetIcon : MonoBehaviour
{
    private void LateUpdate()
    {
        Vector3 euler = transform.eulerAngles;
        euler.x = 0f;
        transform.eulerAngles = euler;
    }
}
