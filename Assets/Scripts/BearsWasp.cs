using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearsWasp : MonoBehaviour
{
    private AudioSource _audioSource;
    bool isPlaying;

    public AudioClip[] flying;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying)
        {
            PlaySound();
        }        
    }

    void PlaySound()
    {
        isPlaying = true;

        int randomnmr = Random.Range(0, flying.Length);


        _audioSource.PlayOneShot(flying[randomnmr]);
        StartCoroutine("WaitForSoundEnd");

    }

    IEnumerator WaitForSoundEnd()
    {
        while (_audioSource.isPlaying)
        {
            yield return 0.1f;
        }
        isPlaying = false;

    }
}
