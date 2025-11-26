using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
[RequireComponent(typeof(AudioSource))]
public class PlayerAudioController : MonoBehaviour
{
    public AudioSource walkingSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        walkingSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void PlaySound(AudioClip clip)
    {
        walkingSource.PlayOneShot(clip);
    }
}
