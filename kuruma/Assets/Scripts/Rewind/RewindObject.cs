using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindObject : MonoBehaviour
{
    private bool _isRewind = false;
    private Rigidbody _rb;
    private List<TransformData> _history = new List<TransformData>();

    [SerializeField]
    private float _recordTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        RewindManager.Register(this);
    }
    private void OnDestroy()
    {
        RewindManager.Unregister(this);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isRewind)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }
    private void Rewind()
    {
        if (_history.Count > 0)
        {
            TransformData data = _history[0];
            transform.position = data.position;
            transform.rotation = data.rotation;
            _history.RemoveAt(0);
        }
    }
    private void Record()
    {
        if(_history.Count > Mathf.Round(_recordTime / Time.fixedDeltaTime))
        {
            _history.RemoveAt(_history.Count - 1);
        }
        _history.Insert(0, new TransformData(transform.position, transform.rotation));
    }
    public void StartRewind()
    {
        _isRewind = true;
        if(_rb != null)
        {
            _rb.isKinematic = true;
        }
    }
    public void StartRecord()
    {
        _isRewind = false;
        if (_rb != null)
        {
            _rb.isKinematic = false;
        }
    }

    public struct TransformData
    {
        public Vector3 position;
        public Quaternion rotation;

        public TransformData(Vector3 pos, Quaternion rot)
        {
            position = pos;
            rotation = rot;
        }
    }
}
