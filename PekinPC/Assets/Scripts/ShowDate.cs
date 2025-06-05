using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShowDate : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI dateText;
    [SerializeField] TMPro.TextMeshProUGUI timeText;
    // Start is called before the first frame update
    void Start()
    {
        UpdateDate();
        InvokeRepeating("UpdateTime", 0f, 1f);
    }

    void UpdateDate()
    {
        DateTime now = DateTime.Now;
        dateText.text = now.ToString("yyyyƒÍMM‘¬dd»’dddd");
    }
    void UpdateTime()
    {
        DateTime now = DateTime.Now;
        timeText.text = now.ToString("HH:mm:ss");
    }
}
