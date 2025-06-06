using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastMouseRay : MonoBehaviour
{
    private RaycastHit _hitInfo;
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    SphereCameraController sphereCameraController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray r = _camera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(r, out _hitInfo, 100f))
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                sphereCameraController.ChangeTarget(_hitInfo.transform);
            }
        }
    }
}
