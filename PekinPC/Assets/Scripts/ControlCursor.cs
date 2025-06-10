using System.Collections;
using System.Collections.Generic;
using UniStorm.Utility;
using UnityEngine;

public class ControlCursor : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

}
