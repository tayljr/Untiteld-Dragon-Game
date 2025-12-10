using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
[RequireComponent(typeof(AudioSource))]
public class PlayerAudioController : MonoBehaviour
{
    public AudioClip[] walkingSounds;
    public AudioSource walkingSource;

    public AudioSource PunchSource;
    public float delay = 0.4f;

    private Coroutine walkRoutine;

    bool CanWalk,isGliding,IsFalling,IsClimbing = false;
    
    void Start()
    {
    }
    private void Update()
    {
        IsFalling = GetComponent<PlayerAnimation>().IsFalling;
        isGliding = GetComponent<PlayerAnimation>().IsGliding;
        IsClimbing = GetComponent <PlayerAnimation>().IsClimbing;
        if (isGliding || IsFalling || IsClimbing)
        {
            CanWalk = false;
            StopWalking();
        }
        else
            CanWalk = true;
    }
    public void StartWalking()
    {
        if (walkRoutine == null && CanWalk)
            walkRoutine = StartCoroutine(WalkLoop());
    }

    public void StopWalking()
    {
        if (walkRoutine != null)
        {
            StopCoroutine(walkRoutine);
            walkRoutine = null;
        }
    }

    private IEnumerator WalkLoop()
    {
        while (true)
        {
            int index = Random.Range(0, walkingSounds.Length);
            walkingSource.PlayOneShot(walkingSounds[index]);
            yield return new WaitForSeconds(delay);
        }
    }
    public void PlaySound(AudioClip clip)
    {
        walkingSource.PlayOneShot(clip);
    }
    public void PunchShit()
    {
        PunchSource.Play();
    }
}
