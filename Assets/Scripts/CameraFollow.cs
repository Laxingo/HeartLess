using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;       
    public Vector3 offset;            
    public float delayTime = 0.5f;
    public float timeDelayed;
    private sdf playerScript;
    private float playerVelocity;

    void Start()
    {
        playerScript = player.GetComponent<sdf>();
        
       
    }
    void LateUpdate()
    {
        playerVelocity = playerScript.playerSpeed;
        timeDelayed = delayTime * playerVelocity;
        transform.position = Vector3.Lerp(transform.position , player.transform.position + offset, Time.deltaTime * timeDelayed);
        
    }
}