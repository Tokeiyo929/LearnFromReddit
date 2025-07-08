using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideMap : MonoBehaviour
{
    [SerializeField] GameObject Map3D;
    [SerializeField] Camera PlayerCamera;
    [SerializeField] GameObject LeftBar;
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
            Map3D.SetActive(!Map3D.activeSelf);
            UpdateGameState(Map3D.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && Map3D.activeSelf)
        {
            Map3D.SetActive(false);
            UpdateGameState(false);
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        if (Input.GetKeyUp(KeyCode.LeftAlt) && !Map3D.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void UpdateGameState(bool mapActive)
    {
        PlayerCamera.gameObject.SetActive(!mapActive);
        MiniMap.SetActive(!mapActive);
        LeftBar.SetActive(mapActive);
        Cursor.lockState = mapActive ? CursorLockMode.Confined : Cursor.lockState = CursorLockMode.Locked;
    }
}
