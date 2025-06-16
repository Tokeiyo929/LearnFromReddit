using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ShowLevelInfo : MonoBehaviour
{
    [SerializeField] private Button levelButton;
    [SerializeField] private GameObject levelInfo;
    [SerializeField] private Sprite highlightImage;
    [SerializeField] private Sprite defaultImage;

    private void OnEnable()
    {
        if (levelButton != null)
            levelButton.image.sprite = highlightImage;
        if (levelInfo != null)
            levelInfo.SetActive(true);
    }
    private void OnDisable()
    {
        if (levelButton != null)
            levelButton.image.sprite = defaultImage;
        if (levelInfo != null)
            levelInfo.SetActive(false);
    }
}
