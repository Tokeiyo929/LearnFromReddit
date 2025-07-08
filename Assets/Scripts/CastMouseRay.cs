using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastMouseRay : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private RaycastHit _hitInfo;
    [SerializeField] MovePlayer3D movePlayer3D;
    Vector3 offsetTransform = new Vector3(0, 0, -2000);
    [SerializeField] LayerMask onlyLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray r = _camera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(r, out _hitInfo, 100f,onlyLayer))
        {

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 targetPosition = _hitInfo.collider.gameObject.transform.Find("TelePoint").transform.position ;
                //找到gameobject下名为cube的子物体
                //Transform offsetTransform = gameObject.transform.Find("Cube").transform;
                //Vector3 targetPosition = _hitInfo.collider.transform.position;
                targetPosition += offsetTransform;
                movePlayer3D.MoveTo(targetPosition);

            }
        }
    }
}
