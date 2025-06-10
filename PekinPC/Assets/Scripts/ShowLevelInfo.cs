using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowLevelInfo : MonoBehaviour
{
    [SerializeField] private Button levelButton;
    [SerializeField] private TextMeshProUGUI levelInfo;

    private Color32 defaultColor = new Color32(255, 255, 255, 255); 
    private Color32 highlightColor = new Color32(255, 102, 0, 255); 

    private void OnEnable()
    {
        if (levelButton != null)
            levelButton.image.color = highlightColor;
        if (levelInfo != null)
            levelInfo.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        if (levelButton != null)
            levelButton.image.color = defaultColor;
        if (levelInfo != null)
            levelInfo.gameObject.SetActive(false);
    }
}
