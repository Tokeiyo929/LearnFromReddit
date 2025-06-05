using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEditor.PackageManager;
using UnityEngine;

public class RayResponse : MonoBehaviour
{
    [SerializeField]
    float cursorHoverOffset = 0.05f;
    public Canvas mainCanvas;
    public GameObject cursorPrefab;
    
    private GameObject currentCursor = null;
    private GameObject currentShownObj = null;
    private GameObject lastOutlineObject;

    public Transform carryParent;
    private void OnEnable()
    {
        RayEventManager.OnObjectStayHit += UpdateCursorPosition;
        RayEventManager.OnObjectStayHit += PickObject;
        RayEventManager.OnObjectHit += HandleObjectHit;
        RayEventManager.OnNoObjectHit += HandleNoObjectHit;
    }
    private void OnDisable()
    {
        RayEventManager.OnObjectStayHit -= UpdateCursorPosition;
        RayEventManager.OnObjectStayHit -= PickObject;
        RayEventManager.OnObjectHit -= HandleObjectHit;
        RayEventManager.OnNoObjectHit -= HandleNoObjectHit;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && currentShownObj != null)
        {
            SetCanvasEnabled(currentShownObj, true);
        }
    }
    void HandleObjectHit(GameObject _obj, RaycastHit _hitInfo)
    {
        ShowCanvas(_obj);
        ShowOutline(_obj);
        CreateOrUpdateCursor(_hitInfo);
    }
    void HandleNoObjectHit()
    {
        HideCanvas();
        HideOutline();
    }

    #region Canvas相关
    void ShowCanvas(GameObject _obj)
    {
        if(currentShownObj != null && currentShownObj != _obj)
        {
            SetCanvasEnabled(currentShownObj, false);
        }
        currentShownObj = _obj;
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
    void SetCanvasEnabled(GameObject _obj, bool isEnabled)
    {
        if (mainCanvas == null) return;
        Transform targetTransform = mainCanvas.transform.Find(_obj.name);
        if (targetTransform == null) return;
        GameObject targetObject = targetTransform.gameObject;
        targetObject.SetActive(isEnabled);
    }
    #endregion

    #region Cursor相关
    void UpdateCursorPosition(RaycastHit _hitInfo)
    {
        if(currentCursor != null)
        {
            PositionCursor(_hitInfo);
        }
    }
    void CreateOrUpdateCursor(RaycastHit _hitInfo)
    {
        if (cursorPrefab == null) return;
        if (currentCursor == null)
        {
            currentCursor = Instantiate(cursorPrefab);
        }
        PositionCursor(_hitInfo);
        currentCursor.SetActive(true);
    }
    void PositionCursor(RaycastHit _hitInfo)
    {
        currentCursor.transform.position = _hitInfo.point + _hitInfo.normal * cursorHoverOffset;
        currentCursor.transform.LookAt(Camera.main.transform.position);
        currentCursor.transform.Rotate(0, 180, 0);
    }
    #endregion

    #region Outline相关
    void ShowOutline(GameObject _obj)
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
    #endregion

    void PickObject(RaycastHit _hitInfo)
    {
        if (_hitInfo.collider.gameObject.layer != 9)
            return;
        GameObject _obj = _hitInfo.collider.gameObject;
        if (Input.GetMouseButtonDown(0))
        {
            _obj.transform.parent = carryParent;
            _obj.transform.localPosition = Vector3.zero;
            _obj.transform.localEulerAngles = Vector3.zero;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _obj.transform.parent = null;
            _obj.transform.localEulerAngles = new Vector3(0, _obj.transform.localEulerAngles.y, 0);
        }
    }
}
