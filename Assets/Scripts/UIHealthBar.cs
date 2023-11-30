using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public static UIHealthBar Instance { get; private set; }

    public Image Mask;

    private float originalSize;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        originalSize = Mask.rectTransform.rect.width;
    }

    public void SetValue(float ratio)
    {
        Mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * ratio);
    }

}
