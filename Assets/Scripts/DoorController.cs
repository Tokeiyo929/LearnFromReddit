using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openAngle = -90f;
    public float closeAngle = 0f;
    public bool isOpen = false;

    public void ToggleDoor()
    {
        isOpen = !isOpen;
    }
}
