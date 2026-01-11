using UnityEngine;

public class PrinterManager : MonoBehaviour
{
    [SerializeField] private GameObject photoPrefab;
    [SerializeField] private float spawnDelay = 3.0f;

    [SerializeField] private GameObject photoSpawnLocation;

    private bool _printing = false;
    private GameObject currentPhotograph;

    public async void PrintPhoto(DataEntry data)
    {
        if (_printing) return;
        if (currentPhotograph != null) Destroy(currentPhotograph);
        _printing = true;

        // Handle delay
        await Awaitable.WaitForSecondsAsync(spawnDelay);

        // Check if photo should print wrong
        var photo =  !data.printAnomaly ? data.sourceData.defaultPhoto : data.sourceData.warpedPrint;

        // Spawn Photograph
        var inst = Instantiate(photoPrefab);
        inst.transform.position = photoSpawnLocation.transform.position;

        // Set photo to result of check
        inst.GetComponent<PhotographSetter>().ChangePhoto(photo);

        // Save ref to instance
        currentPhotograph = inst;
    }
}
