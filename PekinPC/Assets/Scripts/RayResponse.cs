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
    GameObject lastOutlineObject;
    [SerializeField]
    float cursorHoverOffset = 0.05f;

    private void OnEnable()
    {
        RayEventManager.OnObjectHit += ShowCanvas;
        RayEventManager.OnNoObjectHit += HideCanvas;
        RayEventManager.OnObjectStayHit += UpdateCursorPosition;
        RayEventManager.OnObjectHit += ShowOutline;
        RayEventManager.OnNoObjectHit += HideOutline;
    }
    private void OnDisable()
    {
        RayEventManager.OnObjectHit -= ShowCanvas;
        RayEventManager.OnNoObjectHit -= HideCanvas;
        RayEventManager.OnObjectStayHit -= UpdateCursorPosition;
        RayEventManager.OnObjectHit -= ShowOutline;
        RayEventManager.OnNoObjectHit -= HideOutline;
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
            currentCursor.transform.position = _hitInfo.point + _hitInfo.normal * cursorHoverOffset;
            currentCursor.transform.LookAt(Camera.main.transform.position);
            currentCursor.transform.Rotate(0, 180, 0);
        }
    }
    void CreateOrUpdateCursor(RaycastHit _hitInfo)
    {
        if (cursorPrefab == null)
            return;
        if (currentCursor == null)
        {
            currentCursor = Instantiate(cursorPrefab, _hitInfo.point + _hitInfo.normal * cursorHoverOffset, Quaternion.identity);
            currentCursor.transform.LookAt(Camera.main.transform.position);
            currentCursor.transform.Rotate(0, 180, 0);
        }
        else
        {
            currentCursor.transform.position = _hitInfo.point + _hitInfo.normal * cursorHoverOffset;
            currentCursor.transform.LookAt(Camera.main.transform.position);
            currentCursor.transform.Rotate(0, 180, 0);
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
    void ShowOutline(GameObject _obj, RaycastHit _hitInfo)
    {
        SetOutline(_obj, true);
        lastOutlineObject = _obj;
    }
    void HideOutline()
    {
        SetOutline(lastOutlineObject, false);
    }
    void SetOutline(GameObject _gameObject, bool _bool)
    {
        if (_gameObject == null)
            return;
        Outline outline = _gameObject.GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = _bool;
        }
    }
}
