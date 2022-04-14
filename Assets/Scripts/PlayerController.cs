using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip jumpSound;
    public AudioClip coinTakenSound;
    public AudioClip airTimeSound;
    public AudioClip deathSound;
    public float jumpForce = 500;
    public bool isGrounded = true;

    private GameManager gameManager;
    private GameObject focalPoint;
    private Rigidbody playerRb;
    private AudioSource playerSounds;
    private float vertical;
    private float moveSpeed = 2000;
    private float yLimit = 5;
    private float maxSpeed = 100;

    void Start()
    {
        Physics.gravity = new Vector3(0, -20f, 0);
        playerSounds = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        focalPoint = GameObject.Find("Focal Point");
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!gameManager.isGameover)
        {
            Movement();
            Jump();
        }
        OutOfBounds();
    }

    void Movement()
    {
        vertical = Input.GetAxis("Vertical");

        if (vertical >= 0 && isGrounded)
        {
            playerRb.drag = 0.5f;

            playerRb.AddForce(focalPoint.transform.forward * vertical * moveSpeed);
            playerRb.velocity = Vector3.ClampMagnitude(playerRb.velocity, maxSpeed);
        }
        else if (vertical < 0 && isGrounded)
        {
            playerRb.drag += 0.05f;
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerSounds.PlayOneShot(jumpSound, 1);
            StartCoroutine(JumpScore());
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            playerRb.drag = 0;
        }
    }

    void OutOfBounds()
    {
        if (transform.position.y < -yLimit)
        {
            gameManager.health--;
            if (gameManager.round1 == true && gameManager.health > 0)
            {
                gameManager.Round1();
            }
            else if (gameManager.round2 == true && gameManager.health > 0)
            {
                gameManager.Round2();
            }
            else if (gameManager.round3 == true && gameManager.health > 0)
            {
                gameManager.Round3();
            }
            else if (gameManager.round4 == true && gameManager.health > 0)
            {
                gameManager.Round4();
            }
            else if (gameManager.health == 0)
            {
                playerSounds.PlayOneShot(deathSound, 1);
                gameManager.isGameover = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameManager.health--;
            if (gameManager.round1 == true && gameManager.health > 0)
            {
                gameManager.Round1();
            }
            else if (gameManager.round2 == true && gameManager.health > 0)
            {
                gameManager.Round2();
            }
            else if (gameManager.round3 == true && gameManager.health > 0)
            {
                gameManager.Round3();
            }
            else if (gameManager.round4 == true && gameManager.health > 0)
            {
                gameManager.Round4();
            }
            else if (gameManager.health == 0)
            {
                playerSounds.PlayOneShot(deathSound, 1);
                gameManager.isGameover = true;
            }
        }
        else if (collision.gameObject.CompareTag("Finish1"))
        {
            gameManager.round1Complete = true;
        }
        else if (collision.gameObject.CompareTag("Finish2"))
        {
            gameManager.round2Complete = true;
        }
        else if (collision.gameObject.CompareTag("Finish3"))
        {
            gameManager.round3Complete = true;
        }
        else if (collision.gameObject.CompareTag("Finish4"))
        {
            gameManager.round4Complete = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            playerSounds.PlayOneShot(coinTakenSound, 1);
            Destroy(other.gameObject);
            gameManager.UpdateScore(20);
        }
        else if (other.CompareTag("PathFlags"))
        {
            Destroy(other.GetComponent<BoxCollider>());
            gameManager.UpdateScore(30);
        }
        else if (other.CompareTag("RingPath"))
        {
            Destroy(other.GetComponent<BoxCollider>());
            gameManager.UpdateScore(10);
        }
    }

    IEnumerator JumpScore()
    {
        yield return new WaitForSeconds(2);
        if(!isGrounded)
        {
            playerSounds.PlayOneShot(airTimeSound, 1);
            gameManager.UpdateScore(40);
        }
    }
}
