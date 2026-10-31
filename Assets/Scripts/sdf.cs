using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class sdf : MonoBehaviour
{
    private CharacterController controller;
    private Rigidbody rb;
    public Vector3 playerVelocity;
    public Vector3 windVector;
    public bool groundedPlayer;
    public bool dead;
    public float walkspeed = 2.70f;
    public float sprintSpeed = 8.0f;
    public float crouchedSpeed = 1.3f;
    public float playerSpeed = 2.70f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
    public bool isWindy;
    public bool onWindZone;
    public bool windStarted;
    public float windSpeed = 2f;
    public bool leftWindZone;



    [SerializeField] Transform respawnPoint;
    [SerializeField] GameObject cutsceneCamera;
    [SerializeField] GameObject cutsceneCamera2;
    [SerializeField] GameObject model;
    [SerializeField] GameObject windParticles;


    public bool isMoving = false;
    public bool isJumping = false;
    public bool isRunning = false;
    public bool isCrouching = false;


    Animator animator;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        rb = gameObject.GetComponent<Rigidbody>();
        animator = model.GetComponent<Animator>(); ;
        dead = false;

    }
    void Respawn()
    {

        dead = false;
            gameObject.transform.position = respawnPoint.position;
            Physics.SyncTransforms();
        
    }

    void Update()
    {
        



        if (isWindy && onWindZone)
        {
            playerVelocity.x -= windSpeed * Time.deltaTime;
            if(playerVelocity.x <= -7)
            {
                playerVelocity.x = -7f;
            }
        }
        

        
        if (groundedPlayer)
        {
            isJumping = false;
            playerVelocity.y = -gravityValue * Time.deltaTime;
        }


        if (!isWindy || !onWindZone)
        {
            animator.SetBool("isWinding", false);
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            controller.Move(move * Time.deltaTime * playerSpeed);

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
                               
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    if (!Input.GetKey(KeyCode.LeftControl))
                    {
                        isMoving = true;
                        animator.SetBool("isWalking", true);
                        isRunning = false;
                        animator.SetBool("isRunning", false);
                        isCrouching = false;
                        controller.height = 2.59f;


                    }
                    else if (Input.GetKey(KeyCode.LeftControl))
                    {
                        animator.SetBool("crouchWalk", true);
                        animator.SetBool("isCrouching", true);
                        animator.SetBool("isWalking", false);
                        animator.SetBool("isRunning", false);
                        isMoving = false;
                        isCrouching = true;
                        controller.height = 1.4f;
                    }
                }
                else if (Input.GetKey(KeyCode.LeftShift))
                {
                    if (!Input.GetKey(KeyCode.LeftControl))
                    {
                        isMoving = false;
                        animator.SetBool("isWalking", false);
                        isRunning = true;
                        animator.SetBool("isRunning", true);
                        isCrouching = false;
                        controller.height = 2.59f;



                    }
                    else if (Input.GetKey(KeyCode.LeftControl))
                    {
                        animator.SetBool("isCrouching", true);
                        animator.SetBool("isWalking", false);
                        animator.SetBool("isRunning", false);
                        playerSpeed = crouchedSpeed;
                        isRunning = false;
                        isCrouching = true;
                        controller.height = 1.4f;



                    }
                }
            }
            else
            {
                isMoving = false;
                isRunning = false;
                isCrouching = false;


                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("crouchWalk", false);
                controller.height = 2.59f;


            }


            if (Input.GetKeyDown(KeyCode.P))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                playerSpeed = sprintSpeed;

                controller.height = 2.59f;

            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                playerSpeed = walkspeed;
                controller.height = 2.59f;


                if (Input.GetKey(KeyCode.LeftControl))
                {

                    playerSpeed = crouchedSpeed;
                    animator.SetBool("isCrouching", true);
                    animator.SetBool("isWalking", false);
                    animator.SetBool("isRunning", false);
                    controller.height = 1.4f;

                }
            }

            if (Input.GetKeyDown(KeyCode.LeftControl) && !(Input.GetKey(KeyCode.LeftShift)))
            {
                playerSpeed = crouchedSpeed;
                animator.SetBool("isCrouching", true);
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", false);
                controller.height = 1.4f;

            }
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                playerSpeed = walkspeed;
                animator.SetBool("isCrouching", false);
                animator.SetBool("isWalking", true);
                controller.height = 2.59f;

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    playerSpeed = sprintSpeed;
                    animator.SetBool("isCrouching", false);
                    animator.SetBool("isRunning", true);
                    controller.height = 2.59f;

                }
            }


            // Changes the height position of the player..
            if (Input.GetButton("Jump") && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                isJumping = true;
                isMoving = false;
                animator.SetBool("isJumping", true);
                controller.height = 2.59f;

            }
            if (!groundedPlayer)
            {
                isJumping = false;
                animator.SetBool("isJumping", false);

            }
        }
        else
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isCrouching", false);
            animator.SetBool("pushing", false);
            animator.SetBool("isWinding", true);
            

        }



        if (!groundedPlayer)
        {
            playerVelocity.y += gravityValue * Time.deltaTime;
            if(playerVelocity.y <= -10f)
            {
                dead = true;
                Debug.Log("Fall Damage");
                Respawn();
            }
        }
            controller.Move(playerVelocity * Time.deltaTime);
          groundedPlayer = controller.isGrounded;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wind"))
        { 
            onWindZone = true;
            Debug.Log("In Windzone");
            if (!windStarted) 
            { 
                StartCoroutine("Wind");               
            }
            leftWindZone = false;

        }

        if (other.CompareTag("Respawn"))
        {
            respawnPoint = other.gameObject.transform;
        }
        if (other.CompareTag("Water"))
        {
            Respawn();
            Debug.Log("Dead by water");
        }
        if (other.CompareTag("CutsceneFinal"))
        {
            cutsceneCamera.SetActive(true);
        }
        if (other.CompareTag("CutsceneFinal2"))
        {
            cutsceneCamera2.SetActive(true);
        }
        if (other.CompareTag("Safezone"))
        {
            Debug.Log("Out of Safezone");

            leftWindZone = true;
            onWindZone = false;
        }

        if (other.CompareTag("Pushable"))
        {
            animator.SetBool("pushing", true);
            playerSpeed = crouchedSpeed;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wind"))
        {
            Debug.Log("Out of Wind");
            onWindZone = false;
            playerVelocity.x = 0;
            leftWindZone = true;

        }
        if (other.CompareTag("Safezone"))
        {
            Debug.Log("Out of Safezone");
            onWindZone = true;
            leftWindZone = false;
        }
        if (other.CompareTag("Pushable"))
        {
            animator.SetBool("pushing", false);
            
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Pushable"))
        {
            animator.SetBool("pushing", true);
            playerSpeed = crouchedSpeed;
        }
    }

    IEnumerator Wind()
    {
        if (!windStarted)
            windStarted = true;
        yield return new WaitForSeconds(2);
        isWindy = true;
        windParticles.SetActive(true);
            Debug.Log("Wind starts");
        yield return new WaitForSeconds(5);
            Debug.Log("Wind stops");
        windParticles.SetActive(false);
        isWindy = false;
        playerVelocity.x = 0;
        yield return new WaitForSeconds(5);
        StartCoroutine("Wind");
    }
}
