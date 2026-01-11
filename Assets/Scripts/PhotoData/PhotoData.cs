using UnityEngine;

[CreateAssetMenu(fileName = "PhotoData", menuName = "Scriptable Objects/PhotoData")]
public class PhotoData : ScriptableObject
{
    public Texture2D defaultPhoto;
    public Texture2D warpedPrint;

    public bool contentViolation;
    public bool guaranteeFilterAnomaly;
    public bool noFilterAnomaly;
    public bool guaranteePrintAnomaly;
    public bool noPrintAnomaly;
    public bool guaranteeScanAnomaly;
    public bool noScanAnomaly;
}
