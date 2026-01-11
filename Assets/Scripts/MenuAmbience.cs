using UnityEngine;

public class MenuAmbience : MonoBehaviour
{
    private AudioSource ambience;

    private void Awake()
    {
        ambience = GetComponent<AudioSource>();
        PlayAmbience();
    }

    private async void PlayAmbience()
    {
        await Awaitable.WaitForSecondsAsync(37f);

        await Awaitable.WaitForSecondsAsync(Random.Range(0f, 10f));

        if (ambience != null)
        {
            ambience.Play();
            PlayAmbience();
        }
    }
}
