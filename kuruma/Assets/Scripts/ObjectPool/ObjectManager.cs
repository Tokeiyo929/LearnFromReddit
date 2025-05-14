using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public ObjectPool _objectPool;
    public float _speed = 10f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        GameObject obj = _objectPool.GetObject();
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.forward * _speed;
        }
        StartCoroutine(DeObject(obj));
    }
    IEnumerator DeObject(GameObject gb)
    {
        yield return new WaitForSeconds(2f);
        _objectPool.ReturnObject(gb);
    }
}
