using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowardsPlayer : MonoBehaviour
{
    Transform playerTransform;
    Canvas IntroduceUI;
    float distanceValve = 25f;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        IntroduceUI = gameObject.GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceSqr = (playerTransform.position - transform.position).sqrMagnitude;
        bool isActive = distanceSqr < distanceValve;
        
        if(IntroduceUI.enabled != isActive)
        {
            IntroduceUI.enabled = isActive;
        }

        if (isActive)
        {
            transform.eulerAngles = new Vector3(0, playerTransform.eulerAngles.y, 0);
        }
    }
}
