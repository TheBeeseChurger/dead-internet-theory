using UnityEngine;
using UnityEngine.UI;

public class FilterManager : MonoBehaviour
{
    [SerializeField] private Image anomaly;

    private RectTransform rect;
    private float brightnessValue;
    private float contrastValue;
    private float saturationValue;
    private bool activeAnomaly = false;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void SetAnomaly(DataEntry entry)
    {
        activeAnomaly = entry.filterAnomaly;

        if (!activeAnomaly) anomaly.color = new(1f, 1f, 1f, 0f);
        else RandomizeAnomaly();
    }

    private void RandomizeAnomaly()
    {
        var anomalyRect = anomaly.rectTransform;

        var rand = Random.Range(1f, 1.8f);
        anomalyRect.localScale = new Vector3(rand, rand, 0f);

        rand = Random.Range(-32f, 32f);
        anomalyRect.localEulerAngles = new Vector3(0f, 0f, rand);

        var randW = Random.Range(0f + (anomalyRect.rect.width * 1.5f), rect.rect.width - (anomalyRect.rect.width * 1.5f));
        var randH = Random.Range(0f + (anomalyRect.rect.height * 1.5f), rect.rect.height - (anomalyRect.rect.height * 1.5f));
        anomalyRect.anchoredPosition = new(randW, randH);

        brightnessValue = Random.Range(-0.5f, 0.5f);
        contrastValue = Random.Range(1f, 2f);
        saturationValue = Random.Range(0f, 2f);
    }

    public void RevealAnomaly(float brightness, float contrast, float saturation)
    {
        if (!activeAnomaly) return;

        var brightnessRange = 0.25f;
        var contrastRange = 0.5f;
        var saturationRange = 0.4f;

        float brightnessDiff = Mathf.Abs(brightnessValue - brightness);
        float contrastDiff = Mathf.Abs(contrastValue - contrast);
        float saturationDiff = Mathf.Abs(saturationValue - saturation);

        float briT = brightnessDiff > brightnessRange ? 0f : 1 - (brightnessDiff / brightnessRange);
        float conT = contrastDiff > contrastRange ? 0f : 1 - (contrastDiff / contrastRange);
        float satT = saturationDiff > saturationRange ? 0f : 1 - (saturationDiff / saturationRange);

        float final = (briT + conT + satT) / 3f;

        anomaly.color = new(1f, 1f, 1f, final);
    }
}
