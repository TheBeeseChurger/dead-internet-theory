using UnityEngine;

public class PhotographSetter : MonoBehaviour
{
    [SerializeField] private Material photograph;

    public void ChangePhoto(Texture2D photo)
    {
        photograph.SetTexture("_BaseMap", photo);
    }
}
