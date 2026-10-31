using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheck : MonoBehaviour
{
    [SerializeField] GameObject rockChecker;
    [SerializeField] GameObject rockObject;
    [SerializeField] GameObject rockFallen;

    RockCheck rockScript;
    // Start is called before the first frame update
    void Start()
    {
        rockScript = rockChecker.GetComponent<RockCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            if (rockScript.rockFell )
            {
                rockObject.SetActive(false);
                rockFallen.SetActive(true);
            }
        }
    }
}
