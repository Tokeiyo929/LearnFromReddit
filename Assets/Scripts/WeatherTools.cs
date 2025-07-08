using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniStorm;
using UnityEngine;
using UnityEngine.Networking;

#region 根据ip位置获取城市名称
public class CityData
{
    public int status;
    public string address;
    public Content content;
}
public class Content
{
    public string address;
    public Address_Detail address_detail;
    public Point point;
}
public class Address_Detail
{
    public string adcode;
    public string city;
    public int city_code;
    public string district;
    public string province;
    public string street;
    public string street_number;
}
public class Point
{
    public string x;
    public string y;
}
#endregion

#region 根据城市名称对应天气接口里的city_code
public class CityCode
{
    public int id;
    public int pid;
    public string city_code;
    public string city_name;
    public string post_code;
    public string area_code;
    public string ctime;
}
#endregion

#region 天气数据
public class WeatherData
{
    public string message;
    public int status;
    public string date;
    public string time;
    public CityInfo cityInfo;
    public WeathData data;
}
public class CityInfo
{
    public string city;
    public string citykey;
    public string parent;
    public string updateTime;
}
public class WeathData
{
    public string shidu;
    public double pm25;
    public double pm10;
    public string quality;
    public string wendu;
    public string ganmao;
    public WeathDetailData[] forecast;
    public WeathDetailData yesterday;
}
public class WeathDetailData
{
    public string date;
    public string high;
    public string low;
    public string ymd;
    public string week;
    public string sunrise;
    public string sunset;
    public double aqi;
    public string fx;
    public string fl;
    public string type;
    public string notice;
}
#endregion



public class WeatherTools : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cityNameText;
    [SerializeField] TextMeshProUGUI wetText;
    [SerializeField] TextMeshProUGUI weatherTypeText;
    [SerializeField] TextMeshProUGUI TempText;
    [SerializeField] WeatherDataConfig weatherDataConfig;


    public static bool isInitDic = false;
    public static Dictionary<string, string> posToId = new Dictionary<string, string>();
    
    string posUrl = "https://api.map.baidu.com/location/ip?ak=rBR6z4Y0bbPAuN5pwClkbzMhJAzqKzSt";
    string weatherUrl = "http://t.weather.sojson.com/api/weather/city/";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RequestCityName());
    }

    IEnumerator RequestCityName()
    {
        
        UnityWebRequest request = UnityWebRequest.Get(posUrl);
        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.Success)
        {
            CityData cityData = LitJson.JsonMapper.ToObject<CityData>(request.downloadHandler.text);
            //Debug.Log(cityData.content.address_detail.city);
            //Debug.Log(GetWeatherId(cityData.content.address_detail.city));
            string city_code = GetWeatherId(cityData.content.address_detail.city);
            StartCoroutine(RuquestWeatherData(city_code));
        }
    }

    IEnumerator RuquestWeatherData(string _city_code) 
    {
        UnityWebRequest request = UnityWebRequest.Get(weatherUrl + _city_code);
        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.Success)
        {
            //Debug.Log(request.downloadHandler.text);
            WeatherData t = LitJson.JsonMapper.ToObject<WeatherData>(request.downloadHandler.text);
            InitShowInfoCanvas(t);
            SetWeather(t);
        }
    }
    public static string GetWeatherId(string name)
    {
        string city_code = "";
        if (!isInitDic)
        {
            isInitDic = true;
            TextAsset city = Resources.Load<TextAsset>("city");
            List<CityCode> cityCode = LitJson.JsonMapper.ToObject<List<CityCode>>(city.text);
            foreach(CityCode t in cityCode)
            {
                posToId[t.city_name] = t.city_code;
            }
        }
        if (posToId.ContainsKey(name))
            return posToId[name];
        string shortName = name.Replace("市", "").Replace("区", "");
        if (posToId.ContainsKey(shortName))
            return posToId[shortName];
        return city_code;
    }
    private void InitShowInfoCanvas(WeatherData _t)
    {
        cityNameText.text = _t.cityInfo.city;
        wetText.text = ($"湿度：<color=#14FF00>{_t.data.shidu}</color>");
        weatherTypeText.text = ($"天气：<color=#14FF00>{ _t.data.forecast[0].type}</color>");
        TempText.text = ($"温度：<color=#14FF00>{_t.data.wendu}℃</color>");
    }
    private void SetWeather(WeatherData _t)
    {
        foreach (NameResponseID t in weatherDataConfig.weatherDataConfigs)
        {
            if(t.weatherTypeNameCN == _t.data.forecast[0].type)
            {
                UniStormManager.Instance?.ChangeWeatherWithTransition(UniStormSystem.Instance.AllWeatherTypes[t.nameResponseID]);
                break;
            }
        }
    }
}
