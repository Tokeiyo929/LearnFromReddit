using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayResponse : MonoBehaviour
{
    public Canvas mainCanvas;
    GameObject currentShownObj = null;
    public GameObject cursorPrefab;
    GameObject currentCursor = null;
    RaycastHit currentHitInfo;
    private void OnEnable()
    {
        RayEventManager.OnObjectHit += ShowCanvas;
        RayEventManager.OnNoObjectHit += HideCanvas;
        RayEventManager.OnObjectStayHit += UpdateCursorPosition;
    }
    private void OnDisable()
    {
        RayEventManager.OnObjectHit -= ShowCanvas;
        RayEventManager.OnNoObjectHit -= HideCanvas;
        RayEventManager.OnObjectStayHit -= UpdateCursorPosition;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && currentShownObj != null)
        {
            SetCanvasEnabled(currentShownObj, true);
        }
    }
    void ShowCanvas(GameObject _obj, RaycastHit _hitInfo)
    {
        if(currentShownObj != null && currentShownObj != _obj)
        {
            SetCanvasEnabled(currentShownObj, false);
        }
        currentShownObj = _obj;
        CreateOrUpdateCursor(_hitInfo);
    }
    void HideCanvas()
    {
        if (currentShownObj != null)
        {
            SetCanvasEnabled(currentShownObj, false);
            currentShownObj = null;
        }
        if(currentCursor != null)
        {
            currentCursor.SetActive(false);
        }
    }
    void UpdateCursorPosition(RaycastHit _hitInfo)
    {
        currentHitInfo = _hitInfo;
        if(currentCursor != null)
        {
            currentCursor.transform.position = _hitInfo.point;
            currentCursor.transform.rotation = Quaternion.FromToRotation(Vector3.up, _hitInfo.normal);
        }
    }
    void CreateOrUpdateCursor(RaycastHit _hitInfo)
    {
        if (cursorPrefab == null)
            return;
        if (currentCursor == null)
        {
            currentCursor = Instantiate(cursorPrefab, _hitInfo.point, Quaternion.identity);
            currentCursor.transform.rotation = Quaternion.FromToRotation(Vector3.up, _hitInfo.normal);
        }
        else
        {
            currentCursor.transform.position = _hitInfo.point;
            currentCursor.transform.rotation = Quaternion.FromToRotation(Vector3.up, _hitInfo.normal);
        }
        currentCursor.SetActive(true);
    }
    void SetCanvasEnabled(GameObject _obj, bool isEnabled)
    {
        if (mainCanvas == null)
            return;
        Transform targetTransform = mainCanvas.transform.Find(_obj.name);
        if (targetTransform == null)
            return;
        GameObject targetObject = targetTransform.gameObject;
        targetObject.SetActive(isEnabled);
    }
}
