using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    Vector3 playerPos;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        this.transform.position = new Vector3(playerPos.x, playerPos.y, playerPos.z + 2000);
    }
}
