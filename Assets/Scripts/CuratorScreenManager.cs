using UnityEngine;
using UnityEngine.UI;

public class CuratorScreenManager : MonoBehaviour
{
    [SerializeField] private Material photoMaterial;

    [SerializeField] private GameObject magnifySidebar;
    [SerializeField] private GameObject filterSidebar;
    [SerializeField] private GameObject printSidebar;
    [SerializeField] private GameObject scanSidebar;

    [SerializeField] private Slider filterBrightness;
    [SerializeField] private Slider filterContrast;
    [SerializeField] private Slider filterSaturation;

    public void OnMagnify()
    {
        if (magnifySidebar != null) magnifySidebar.SetActive(true);

        if (filterSidebar != null) filterSidebar.SetActive(false);
        if (printSidebar != null) printSidebar.SetActive(false);
        if (scanSidebar != null) scanSidebar.SetActive(false);
    }

    public void OnFilter()
    {
        if (filterSidebar != null) filterSidebar.SetActive(true);

        if (magnifySidebar != null) magnifySidebar.SetActive(false);
        if (printSidebar != null) printSidebar.SetActive(false);
        if (scanSidebar != null) scanSidebar.SetActive(false);
    }

    public void OnPrint()
    {
        if (printSidebar != null) printSidebar.SetActive(true);

        if (magnifySidebar != null) magnifySidebar.SetActive(false);
        if (filterSidebar != null) filterSidebar.SetActive(false);
        if (scanSidebar != null) scanSidebar.SetActive(false);
    }

    public void OnScan()
    {
        if (scanSidebar != null) scanSidebar.SetActive(true);

        if (magnifySidebar != null) magnifySidebar.SetActive(false);
        if (filterSidebar != null) filterSidebar.SetActive(false);
        if (printSidebar != null) printSidebar.SetActive(false);
    }

    public void OnBrightnessChanged(float value)
    {
        photoMaterial.SetFloat("_Brightness", value);
    }

    public void OnContrastChanged(float value)
    {
        photoMaterial.SetFloat("_Contrast", value);
    }

    public void OnSaturationChanged(float value)
    {
        photoMaterial.SetFloat("_Saturation", value);
    }
}
