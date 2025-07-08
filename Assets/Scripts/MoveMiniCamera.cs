using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MoveMiniCamera;

public class MoveMiniCamera : MonoBehaviour
{
    [SerializeField] Transform playerTrans;

    void Update()
    {
        float currentPlayerYPosition = playerTrans.position.y;
        transform.position = new Vector3(playerTrans.position.x, currentPlayerYPosition + 2f, playerTrans.position.z);
    }
}
