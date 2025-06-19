using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEditor.PackageManager;
using UnityEngine;

public class RayResponse : MonoBehaviour
{
    //[SerializeField]
    //float cursorHoverOffset = 0.05f;
    //public Canvas mainCanvas;
    //public GameObject cursorPrefab;

    //private GameObject currentCursor = null;
    //private GameObject currentShownObj = null;

    public Transform carryParent;
    private GameObject lastOutlineObject;
    public GameObject player;

    private void OnEnable()
    {
        RayEventManager.OnObjectStayHit += HandleObjectStayHit;
        RayEventManager.OnObjectHit += HandleObjectHit;
        RayEventManager.OnNoObjectHit += HandleNoObjectHit;
    }
    private void OnDisable()
    {
        RayEventManager.OnObjectStayHit -= HandleObjectStayHit;
        RayEventManager.OnObjectHit -= HandleObjectHit;
        RayEventManager.OnNoObjectHit -= HandleNoObjectHit;
    }
    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.F) && currentShownObj != null)
        //{
        //    SetCanvasEnabled(currentShownObj, true);
        //}
    }
    void HandleObjectHit(GameObject _obj, RaycastHit _hitInfo)
    {
        //ShowCanvas(_obj);
        ShowOutline(_obj);
        //CreateOrUpdateCursor(_hitInfo);
    }
    void HandleObjectStayHit(RaycastHit _hitInfo)
    {
        PickObject(_hitInfo);
        //UpdateCursorPosition(_hitInfo);
        OpenDoor(_hitInfo);
        OpenUI(_hitInfo);
    }
    void HandleNoObjectHit()
    {
        //HideCanvas();
        HideOutline();
    }

    #region Canvas���
    //void ShowCanvas(GameObject _obj)
    //{
    //    if(currentShownObj != null && currentShownObj != _obj)
    //    {
    //        SetCanvasEnabled(currentShownObj, false);
    //    }
    //    currentShownObj = _obj;
    //}
    //void HideCanvas()
    //{
    //    if (currentShownObj != null)
    //    {
    //        SetCanvasEnabled(currentShownObj, false);
    //        currentShownObj = null;
    //    }
    //    //if(currentCursor != null)
    //    //{
    //    //    currentCursor.SetActive(false);
    //    //}
    //}
    //void SetCanvasEnabled(GameObject _obj, bool isEnabled)
    //{
    //    if (mainCanvas == null) return;
    //    Transform targetTransform = mainCanvas.transform.Find(_obj.name);
    //    if (targetTransform == null) return;
    //    GameObject targetObject = targetTransform.gameObject;
    //    targetObject.SetActive(isEnabled);
    //}
    #endregion

    #region Cursor���
    //void UpdateCursorPosition(RaycastHit _hitInfo)
    //{
    //    if(currentCursor != null)
    //    {
    //        PositionCursor(_hitInfo);
    //    }
    //}
    //void CreateOrUpdateCursor(RaycastHit _hitInfo)
    //{
    //    if (cursorPrefab == null) return;
    //    if (currentCursor == null)
    //    {
    //        currentCursor = Instantiate(cursorPrefab);
    //    }
    //    PositionCursor(_hitInfo);
    //    currentCursor.SetActive(true);
    //}
    //void PositionCursor(RaycastHit _hitInfo)
    //{
    //    currentCursor.transform.position = _hitInfo.point + _hitInfo.normal * cursorHoverOffset;
    //    currentCursor.transform.LookAt(Camera.main.transform.position);
    //    currentCursor.transform.Rotate(0, 180, 0);
    //}
    #endregion

    #region Outline���
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

    #region Pick�������
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
    #endregion

    #region �������
    void OpenDoor(RaycastHit _hitInfo)
    {
        if (_hitInfo.collider.gameObject.layer != 10)
            return;
        
        if (Input.GetMouseButtonDown(0))
        {
            Transform _objTrans = _hitInfo.collider.gameObject.transform;
            DoorController doorController = _objTrans.GetComponent<DoorController>();

            if(doorController != null)
            {
                doorController.ToggleDoor();
                float targetAngle = doorController.isOpen ? doorController.openAngle : doorController.closeAngle;
                StartCoroutine(RotateDoorCoroutine(_objTrans, targetAngle, 0.3f));
            }
        }
        
    }
    IEnumerator RotateDoorCoroutine(Transform _objTrans, float _angle, float _duration)
    {
        Quaternion startRot = _objTrans.rotation;
        Quaternion endRot = Quaternion.Euler(_objTrans.eulerAngles.x, _angle, _objTrans.eulerAngles.z);
        float elapsed = 0f;
        while(elapsed < _duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / _duration); 
            _objTrans.rotation = Quaternion.Slerp(startRot, endRot, t);
            yield return null;
        }
        _objTrans.rotation = endRot;
        
    }
    #endregion

    void OpenUI(RaycastHit _hitInfo)
    {
        if (_hitInfo.collider.gameObject.layer != 11)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            player.GetComponent<DetectCollider>().enabled = false;
            StartCoroutine(MoveCamera(_hitInfo.collider.gameObject.transform.Find("Camera").position, _hitInfo.collider.gameObject.transform.Find("Camera").rotation));
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(MoveCameraLocal(new Vector3(0, 0.4f, 0)));
        }
    }
    IEnumerator MoveCamera(Vector3 _afterPos, Quaternion _afterRot)
    {
        float _duration = 0.3f;
        float passTime = 0;
        while(passTime < _duration)
        {
            player.transform.Find("Camera").position = Vector3.Slerp(player.transform.Find("Camera").position, _afterPos, passTime / _duration);
            player.transform.Find("Camera").rotation = Quaternion.Slerp(player.transform.Find("Camera").rotation, _afterRot, passTime / _duration);
            passTime += Time.deltaTime;
            yield return null;
        }

        player.transform.Find("Camera").position = _afterPos;
        player.transform.Find("Camera").rotation = _afterRot;
        
    }
    IEnumerator MoveCameraLocal(Vector3 _afterPos)
    {
        player.GetComponent<DetectCollider>().enabled = true;
        float _duration = 0.3f;
        float passTime = 0;
        while(passTime < _duration)
        {
            player.transform.Find("Camera").localPosition = Vector3.Slerp(player.transform.Find("Camera").localPosition, _afterPos, passTime / _duration);
            passTime += Time.deltaTime;
            yield return null;
        }
        player.transform.Find("Camera").localPosition = _afterPos;
        
        
    }
    
}
