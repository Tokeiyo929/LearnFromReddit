using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideMap : MonoBehaviour
{
    [SerializeField] GameObject Map3D;
    [SerializeField] Camera PlayerCamera;
    [SerializeField] Camera MiniMapCamera;
    [SerializeField] GameObject LeftButton;
    [SerializeField] GameObject MiniMap;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Map3D.gameObject.SetActive(!Map3D.gameObject.activeSelf);
        }
        if (Map3D.gameObject.activeSelf)
        {
            PlayerCamera.gameObject.SetActive(false);
            MiniMapCamera.gameObject.SetActive(false);
            MiniMap.SetActive(false);
            LeftButton.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            PlayerCamera.gameObject.SetActive(true);
            MiniMapCamera.gameObject.SetActive(true);
            MiniMap.SetActive(true);
            LeftButton.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
