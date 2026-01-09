using UnityEngine;

public class PhotoManager : MonoBehaviour
{
    public PhotoData source;
    public RenderTexture photoRT;

    private void Update()
    {
        Graphics.Blit(source.defaultPhoto, photoRT);
    }
}
