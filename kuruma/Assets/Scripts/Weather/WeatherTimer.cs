using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherTimer : MonoBehaviour
{
    [SerializeField]float timer;
    public float[] timesForWeatherStates = new float[7];
    private int currentStateIndex = 0;

    public delegate void TimeExpiredHandler();
    public event TimeExpiredHandler OnTimeExpired;

    // Start is called before the first frame update
    void Start()
    {
        currentStateIndex = 0;
        timer = timesForWeatherStates[currentStateIndex];
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            CycleTimer();
            OnTimeExpired?.Invoke();
        }
    }
    void CycleTimer()
    {
        SetTimer(currentStateIndex + 1);
    }
    void SetTimer(int nextStateIndex)
    {
        currentStateIndex = nextStateIndex % timesForWeatherStates.Length;
        timer = timesForWeatherStates[currentStateIndex];
    }
    public void SetTimer(float time)
    {
        timer = time;
    }
    public float GetCurrentTime()
    {
        return timer;
    }
}
