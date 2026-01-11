using UnityEngine;

[System.Serializable]
public class DataEntry
{
    public PhotoData sourceData;

    public bool contentViolation;
    public bool filterAnomaly;
    public bool printAnomaly;
    public bool scanAnomaly;

    private const float filterChance = 0.35f;
    private const float printChance = 0.7f;
    private const float scanChance = 0.4f;

    public DataEntry(PhotoData data)
    {
        sourceData = data;
        contentViolation = data.contentViolation;

        if (data.guaranteeFilterAnomaly && data.noFilterAnomaly) Debug.LogError("Filter Anomaly guaranteed and not");
        else if (data.guaranteeFilterAnomaly) filterAnomaly = true;
        else if (data.noFilterAnomaly) filterAnomaly = false;
        else
        {
            float rand = Random.Range(0.0f, 1.0f);
            filterAnomaly = rand > filterChance;
        }

        if (data.guaranteePrintAnomaly && data.noPrintAnomaly) Debug.LogError("Print Anomaly guaranteed and not");
        else if (data.guaranteePrintAnomaly) printAnomaly = true;
        else if (data.noPrintAnomaly) printAnomaly = false;
        else
        {
            float rand = Random.Range(0.0f, 1.0f);
            printAnomaly = rand > printChance;
        }

        if (data.guaranteeScanAnomaly && data.noScanAnomaly) Debug.LogError("Scan Anomaly guaranteed and not");
        else if (data.guaranteeScanAnomaly) scanAnomaly = true;
        else if (data.noScanAnomaly) scanAnomaly = false;
        else
        {
            float rand = Random.Range(0.0f, 1.0f);
            scanAnomaly = rand > scanChance;
        }
    }
}
