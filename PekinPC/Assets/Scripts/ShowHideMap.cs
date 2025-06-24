using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideMap : MonoBehaviour
{
    [SerializeField] Camera MapCamera;
    [SerializeField] Camera PlayerCamera;
    [SerializeField] GameObject LeftButton;
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
            MapCamera.gameObject.SetActive(!MapCamera.gameObject.activeSelf);
        }
        if (MapCamera.gameObject.activeSelf)
        {
            PlayerCamera.gameObject.SetActive(false);
            LeftButton.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            PlayerCamera.gameObject.SetActive(true);
            LeftButton.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
