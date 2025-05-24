using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMiniCamera : MonoBehaviour
{
    [System.Serializable]
    public class cameraZone
    {
        public float minY;
        public float maxY;
        public float cameraY;
    }

    [SerializeField] GameObject player;
    [SerializeField] List<cameraZone> cameraZones;

    // Update is called once per frame
    void Update()
    {
        float currentPlayerYPosition = player.transform.position.y;
        foreach(var _cameraZone in cameraZones)
        {
            if (currentPlayerYPosition >= _cameraZone.minY && currentPlayerYPosition <= _cameraZone.maxY)
            {
                transform.position = new Vector3(transform.position.x, _cameraZone.cameraY, transform.position.z);
                break;
            }
        }
    }
}
