using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CuratorScreenManager : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Material photoMaterial;
    [SerializeField] private DayData currentDay;
    [SerializeField] private int currentDayIndex = 0;
    [SerializeField] private PhotoManager photoManager;

    [Header("Sidebars")]
    [SerializeField] private GameObject magnifySidebar;
    [SerializeField] private GameObject filterSidebar;
    [SerializeField] private GameObject printSidebar;
    [SerializeField] private GameObject scanSidebar;

    [Header("Magnify Settings")]
    [SerializeField] private Slider magnifyZoomAmount;
    [SerializeField] private Canvas canvas;

    private bool isMagnifyMode = false;

    [Header("Filter Settings")]
    [SerializeField] private FilterManager filterManager;
    [SerializeField] private Slider filterBrightness;
    [SerializeField] private Slider filterContrast;
    [SerializeField] private Slider filterSaturation;

    [Header("Print Settings")]
    [SerializeField] private PrinterManager printerManager;

    [Header("Scan Settings")]
    [SerializeField] private ScanManager scanManager;
    [SerializeField] private float scanDuration;
    [SerializeField] private float fadeDuration;

    private DataEntry currentDataEntry;

    private void Start()
    {
        currentDayIndex--;
        ChangePhoto();
    }

    public void ChangePhoto()
    {
        currentDayIndex++;
        if (currentDayIndex >= currentDay.photos.Count) return;
        currentDataEntry = new DataEntry(currentDay.photos[currentDayIndex]);
        photoManager.source = currentDay.photos[currentDayIndex];

        scanManager.SetAnomaly(currentDataEntry);
        filterManager.SetAnomaly(currentDataEntry);
        UpdateFilterAnomaly();
    }

    public void OnMagnify()
    {
        if (magnifySidebar != null) magnifySidebar.SetActive(true);

        if (filterSidebar != null) filterSidebar.SetActive(false);
        if (printSidebar != null) printSidebar.SetActive(false);
        if (scanSidebar != null) scanSidebar.SetActive(false);

        magnifyZoomAmount.value = 1f;

        isMagnifyMode = true;

        ResetPhoto();
    }

    public void OnFilter()
    {
        if (filterSidebar != null) filterSidebar.SetActive(true);

        if (magnifySidebar != null) magnifySidebar.SetActive(false);
        if (printSidebar != null) printSidebar.SetActive(false);
        if (scanSidebar != null) scanSidebar.SetActive(false);

        filterBrightness.value = 0f;
        filterContrast.value = filterSaturation.value = 1f;

        isMagnifyMode = false;

        ResetPhoto();
    }

    public void OnPrint()
    {
        if (printSidebar != null) printSidebar.SetActive(true);

        if (magnifySidebar != null) magnifySidebar.SetActive(false);
        if (filterSidebar != null) filterSidebar.SetActive(false);
        if (scanSidebar != null) scanSidebar.SetActive(false);

        printerManager.PrintPhoto(currentDataEntry);

        isMagnifyMode = false;

        ResetPhoto();
    }

    public void OnScan()
    {
        if (scanSidebar != null) scanSidebar.SetActive(true);

        if (magnifySidebar != null) magnifySidebar.SetActive(false);
        if (filterSidebar != null) filterSidebar.SetActive(false);
        if (printSidebar != null) printSidebar.SetActive(false);

        isMagnifyMode = false;

        ResetPhoto();

        Scan();
    }

    public void OnBrightnessChanged(float value)
    {
        photoMaterial.SetFloat("_Brightness", value);
        UpdateFilterAnomaly();
    }

    public void OnContrastChanged(float value)
    {
        photoMaterial.SetFloat("_Contrast", value);
        UpdateFilterAnomaly();
    }

    public void OnSaturationChanged(float value)
    {
        photoMaterial.SetFloat("_Saturation", value);
        UpdateFilterAnomaly();
    }

    private void UpdateFilterAnomaly()
    {
        filterManager.RevealAnomaly(photoMaterial.GetFloat("_Brightness"), photoMaterial.GetFloat("_Contrast"), photoMaterial.GetFloat("_Saturation"));
    }

    public void OnZoomAmountChanged(float value)
    {
        photoMaterial.SetFloat("_ZoomAmount", value);
    }

    private void SetZoomCenter()
    {
        RectTransform rect = photoManager.GetComponent<RectTransform>();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rect,
            Mouse.current.position.ReadValue(),
            canvas.worldCamera,
            out Vector2 localPoint
        );

        Vector2 uv = new(
            (localPoint.x - rect.rect.xMin) / rect.rect.width,
            (localPoint.y - rect.rect.yMin) / rect.rect.height
        );

        photoMaterial.SetVector("_ZoomCenter", uv);
    }

    private void Update()
    {
        if (isMagnifyMode)
        {
            SetZoomCenter();
        }
    }

    private async void Scan()
    {
        scanManager.ScanProgress = 0f;
        await Awaitable.WaitForSecondsAsync(1f);
        var elapsed = 0f;
        while(scanManager.ScanProgress < 1f)
        {
            var t = elapsed / scanDuration;
            scanManager.ScanProgress = Mathf.Lerp(0f, 1f, t);
            elapsed += Time.deltaTime;
            await Awaitable.NextFrameAsync();
        }

        await Awaitable.WaitForSecondsAsync(1f);

        elapsed = 0f;
        while(elapsed < fadeDuration)
        {
            var t = elapsed / fadeDuration;
            scanManager.FadeScan(1f - t);
            elapsed += Time.deltaTime;
            await Awaitable.NextFrameAsync();
        }

        scanManager.ResetScan(currentDataEntry);
    }

    private void ResetPhoto()
    {
        photoMaterial.SetFloat("_Brightness", 0f);
        photoMaterial.SetFloat("_Contrast", 1f);
        photoMaterial.SetFloat("_Saturation", 1f);
        UpdateFilterAnomaly();

        photoMaterial.SetFloat("_ZoomAmount", 1f);
        photoMaterial.SetVector("_ZoomCenter", new Vector2());
    }
}
