using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideLevel : MonoBehaviour
{
    [SerializeField] GameObject fisrtFloor;
    [SerializeField] GameObject secondFloor;
    [SerializeField] GameObject thirdFloor;

    private const float secondFloorThreshold = 0.5f;
    private const float thirdFloorThreshold = 4.7f;
    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player")?.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float y = playerTransform.position.y;

        thirdFloor.SetActive(y > thirdFloorThreshold);
        secondFloor.SetActive(y > secondFloorThreshold && y < thirdFloorThreshold);
        fisrtFloor.SetActive(y < secondFloorThreshold);
    }
}
