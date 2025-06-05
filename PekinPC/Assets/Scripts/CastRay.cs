using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastRay : MonoBehaviour
{
    private GameObject LastObj = null;
    private GameObject currentObj;
    private bool wasHitting = false;
    [SerializeField] private LayerMask onlyLayer;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, onlyLayer))
        {
            //Debug.DrawRay(ray.origin, ray.direction * hitInfo.distance, Color.green);
            currentObj = hitInfo.collider.gameObject;
            //SetOuline(currentObj, true);

            if (wasHitting)
            {
                if(currentObj == LastObj)
                {
                    RayEventManager.TriggerObjectStayHit(hitInfo);
                }
                else
                {
                    RayEventManager.TriggerNoObjectHit();
                    RayEventManager.TriggerObjectHit(currentObj, hitInfo);
                    LastObj = currentObj;
                }
            }
            else
            {
                RayEventManager.TriggerObjectHit(currentObj, hitInfo);
                LastObj = currentObj;
                wasHitting = true;
            }
        }
        else
        {
            //Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
            if (wasHitting)
            {
                RayEventManager.TriggerNoObjectHit();
                //SetOuline(LastObj, false);
                LastObj = null;
                wasHitting = false;
            }
        }
    }
}
