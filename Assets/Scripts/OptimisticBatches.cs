using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptimisticBatches : MonoBehaviour
{
    [SerializeField] GameObject fisrtFloorDevices;
    [SerializeField] GameObject secondFloorDevices;
    [SerializeField] GameObject thirdFloorDevices;
    [SerializeField] Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.y > -2.49f && playerTransform.position.y < 2.1f)
        {
            secondFloorDevices.gameObject.SetActive(true);
        }
        if (playerTransform.position.y > 2.1f || playerTransform.position.y < -2.49f)
        {
            secondFloorDevices.gameObject.SetActive(false);
        }
        if (playerTransform.position.y > -0.79f)
        {
            fisrtFloorDevices.gameObject.SetActive(false);
        }
        if (playerTransform.position.y < -0.79f)
        {
            fisrtFloorDevices.gameObject.SetActive(true);
        }
        if (playerTransform.position.y > 0.51f)
        {
            thirdFloorDevices.gameObject.SetActive(true);
        }
        if(playerTransform.position.y < 0.51f)
        {
            thirdFloorDevices.gameObject.SetActive(false);
        }
    }
}
