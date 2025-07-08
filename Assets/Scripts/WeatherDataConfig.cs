using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "WeatherDataConfig", menuName = "DataConfig/WeatherDataConfig")]
public class WeatherDataConfig : ScriptableObject
{
    public List<NameResponseID> weatherDataConfigs = new List<NameResponseID>();
}
[Serializable]
public class NameResponseID
{
    
    public string weatherTypeNameCN;  
    public int nameResponseID; 
}