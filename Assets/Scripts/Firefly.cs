using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Firefly : MonoBehaviour
{
    [SerializeField] GameObject player;
    sdf playerScript;
    NavMeshAgent navvv;
    private AudioSource _audioSource;
    bool isPlaying;

    public AudioClip[] _walkClips;
    void Start()
    {
        navvv = this.GetComponent<NavMeshAgent>();
        playerScript = player.GetComponent<sdf>();
        _audioSource = GetComponent<AudioSource>();     
    }

    void Update()
    {
       navvv.destination = player.transform.position;

        if (playerScript.isMoving)
        {
            navvv.speed = 3.5f;
            navvv.stoppingDistance = 3f;

        }
        if (playerScript.isRunning)
        {
            navvv.speed = 8f;
            navvv.stoppingDistance = 5f;
        }
        if (!isPlaying)
            PlaySound();
    }
    void PlaySound()
    {
        isPlaying = true;
        int randomnmr = Random.Range(0, _walkClips.Length);
        

        _audioSource.PlayOneShot(_walkClips[randomnmr]);
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
