using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Wingling : MonoBehaviour
{
    private NavMeshAgent wingling;
    private Animator wingAnim;
    public GameObject Player;
    public float distanceFromPlayer = 3f;
    private AudioSource _audioSource;
    bool isPlaying;

    public AudioClip[] _walkClips;
    public AudioClip[] _walkClips2;
    public AudioClip[] roar;
    bool isClose = false;

    // Start is called before the first frame update
    void Start()
    {
        wingling = GetComponent<NavMeshAgent>();
        wingAnim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        if (distance < distanceFromPlayer)
        {
            if (isClose)
            {
                int randomnmr3 = Random.Range(0, roar.Length);

                _audioSource.PlayOneShot(roar[randomnmr3]);
                isClose = false;
            }

            Vector3 directionPlayer = transform.position - Player.transform.position;
            Vector3 newPos = transform.position + directionPlayer;

            wingling.SetDestination(newPos);
            wingAnim.SetBool("run", true);
            if(!isPlaying)
            PlaySound();
        }
        else
            wingAnim.SetBool("run", false);
        isClose = true;
    }

    void PlaySound()
    {
        isPlaying = true;
        int randomnmr = Random.Range(0, _walkClips.Length);
        int randomnmr2 = Random.Range(0, _walkClips2.Length);

        _audioSource.PlayOneShot(_walkClips[randomnmr]);
        StartCoroutine("WaitForSoundEnd");
        _audioSource.PlayOneShot(_walkClips2[randomnmr2]);
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
