using UnityEngine;

[CreateAssetMenu(fileName = "PhotoData", menuName = "Scriptable Objects/PhotoData")]
public class PhotoData : ScriptableObject
{
    public Texture2D defaultPhoto;
    public Texture2D warpedPrint;
}
