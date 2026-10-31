using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class StepsAudio : MonoBehaviour
{

    public AudioClip[] _walkClips;
    public AudioClip[] _walkClips2;
    public AudioClip[] _runningClips;
    public AudioClip[] _runningClips2;
    public AudioClip[] _crouchingClips;
    public AudioClip[] _crouchingClips2;
    public AudioClip[] _JumpClip;
    public AudioClip _windClip;
    public GameObject windZoneOBJ;

    private AudioSource _audioSource;
    private AudioSource _audioSource2;


    public GameObject playerObj;
    sdf movementScript;
    public bool isPlaying;
    public bool windPlaying;
    public bool fadedOut;
    bool stopsound = false;

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }



    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource2 = windZoneOBJ.GetComponent<AudioSource>();
        movementScript = playerObj.GetComponent<sdf>();
    }

    private void Update()
    {
        if (!stopsound)
        {
            if (movementScript.isMoving == true && !isPlaying)
            {
                if (!movementScript.isRunning)
                {
                    StartSound();
                }
            }
            else if (!movementScript.isMoving && !isPlaying)
            {
                if (movementScript.isRunning)
                {
                    StartRunSound();
                }else if (movementScript.isCrouching)
                {
                    StartCrouchSound();
                }
            }
            if (movementScript.isJumping == true)
            {
                int randomnmr = Random.Range(0, _JumpClip.Length);

                _audioSource.PlayOneShot(_JumpClip[randomnmr]);

            }
        }
        if (movementScript.isWindy && movementScript.onWindZone && !windPlaying)
        {
            stopsound = true;
            if (!movementScript.leftWindZone)
            {
                _audioSource2.volume = 1f;
                _audioSource2.PlayOneShot(_windClip);
                windPlaying = true;
                fadedOut = false;
            }

        }
        if (movementScript.leftWindZone && !fadedOut)
        {
            stopsound = false;
            StartCoroutine(FadeOut(_audioSource2, 2f));
            StopCoroutine(FadeOut(_audioSource2, 2f));

            windPlaying = false;
            fadedOut = true;
        }
    }


    // Use this for initialization
    public void StartSound()
    {
        isPlaying = true;
        int randomnmr = Random.Range(0, _walkClips.Length);
        int randomnmr2 = Random.Range(0, _walkClips2.Length);

        _audioSource.PlayOneShot(_walkClips[randomnmr]);
        StartCoroutine("WaitForSoundEnd");
        _audioSource.PlayOneShot(_walkClips2[randomnmr2]);
    }

    public void StartRunSound()
    {
        isPlaying = true;
        int randomnmr = Random.Range(0, _runningClips.Length);
        int randomnmr2 = Random.Range(0, _runningClips2.Length);

        _audioSource.PlayOneShot(_runningClips[randomnmr]);
        StartCoroutine("WaitForSoundEnd");
        _audioSource.PlayOneShot(_runningClips2[randomnmr]);
        //  Debug.Log(_runningClips[randomnmr].name);
        //Debug.Log(_runningClips2[randomnmr2].name);
    }

    public void StartCrouchSound()
    {
        isPlaying = true;
        int randomnmr = Random.Range(0, _crouchingClips.Length);
        int randomnmr2 = Random.Range(0, _crouchingClips2.Length);

        _audioSource.PlayOneShot(_crouchingClips[randomnmr]);
        StartCoroutine("WaitForSoundEnd");
        _audioSource.PlayOneShot(_crouchingClips2[randomnmr2]);
    }


    IEnumerator WaitForSoundEnd()
    {
        while (_audioSource.isPlaying)
        {
            yield return null;
        }
        isPlaying = false;

    }
}