using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer3D : MonoBehaviour
{
    CharacterController character;
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MoveTo(Transform _trans)
    {
        character.enabled = false;
        transform.position = _trans.position;
        character.enabled = true;
    }
    public void MoveTo(Vector3 _pos)
    {
        character.enabled = false;
        transform.position = _pos;
        character.enabled = true;
    }
}
