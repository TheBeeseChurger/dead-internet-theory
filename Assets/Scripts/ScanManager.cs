using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScanManager : MonoBehaviour
{
    [SerializeField] private List<Sprite> anomalySprites;
    [SerializeField] private RectTransform photoRect;
    [SerializeField] private RectTransform anomalyMask;
    [SerializeField] private Image scanLineImage;
    [SerializeField] private Image anomalyImage;

    [Range(0f, 1f)]
    public float ScanProgress
    {
        get
        {
            return _scanProgress;
        }
        set
        {
            _scanProgress = value;
            OnScanProgessChange();
        }
    }

    private float _scanProgress;
    private float width;
    private float height;

    private void Start()
    {
        width = photoRect.rect.width;
        height = photoRect.rect.height;

        anomalyImage.sprite = anomalySprites[Random.Range(0, anomalySprites.Count)];

        ScanProgress = 0f;
    }

    private void OnScanProgessChange()
    {
        scanLineImage.rectTransform.anchoredPosition = new((_scanProgress * (width + 100)) - 50f, 0f);

        float halfWidth = width / 2;
        float maskX = Mathf.Clamp((_scanProgress * (width + 100)) - 50f - halfWidth, -halfWidth, halfWidth);
        anomalyMask.anchoredPosition = new(maskX, 0f);
        anomalyImage.rectTransform.anchoredPosition = new(halfWidth - maskX, 0f);
    }

    public void SetAnomaly(DataEntry data)
    {
        anomalyImage.sprite = anomalySprites[Random.Range(0, anomalySprites.Count)];

        var color = anomalyImage.color;
        anomalyImage.color = new(color.r, color.g, color.b, data.scanAnomaly ? 1f : 0f);
    }

    public void FadeScan(float fadeProgress)
    {
        var aColor = anomalyImage.color;
        var sColor = scanLineImage.color;

        if (anomalyImage.color.a > 0f) anomalyImage.color = new(aColor.r, aColor.g, aColor.b, fadeProgress);
        scanLineImage.color = new(sColor.r, sColor.g, sColor.b, fadeProgress);
    }

    public void ResetScan(DataEntry data)
    {
        ScanProgress = 0f;

        var color = anomalyImage.color;
        var sColor = scanLineImage.color;
        anomalyImage.color = new(color.r, color.g, color.b, data.scanAnomaly ? 1f : 0f);
        scanLineImage.color = new(sColor.r, sColor.g, sColor.b, 1f);
    }
}
