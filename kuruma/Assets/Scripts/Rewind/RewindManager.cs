using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindManager : MonoBehaviour
{
    private static bool _isRewind = false;
    private static List<RewindObject> _rewindObjects = new List<RewindObject>();

    public static void Register(RewindObject obj)
    {
        if (!_rewindObjects.Contains(obj))
        {
            _rewindObjects.Add(obj);
        }
    }
    public static void Unregister(RewindObject obj)
    {
        if (_rewindObjects.Contains(obj))
        {
            _rewindObjects.Remove(obj);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _isRewind = true;
            foreach (var rewindObject in _rewindObjects)
            {
                rewindObject.StartRewind();
            }
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            _isRewind = false;
            foreach (var rewindObject in _rewindObjects)
            {
                rewindObject.StartRecord();
            }
        }
    }
}
