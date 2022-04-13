using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 500;

    private GameManager gameManager;
    private Rigidbody playerRb;
    private float horizontal;
    private float vertical;
    private float rotatePos;
    private float startRotation;
    private float turnSpeed = 50;
    private float moveSpeed = 2000;
    //private float rotateOffset = 5;
    //private float rotateLimit = 60;
    private float yLimit = 5;
    private float maxSpeed = 20;
    private bool isGrounded = true;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerRb = GetComponent<Rigidbody>();
        startRotation = transform.localEulerAngles.y;
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
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        rotatePos = transform.localEulerAngles.y;

        if (vertical >= 0 && isGrounded)
        {
            playerRb.drag = 0.5f;
            playerRb.AddRelativeForce(Vector3.forward * vertical * moveSpeed);
            playerRb.velocity = Vector3.ClampMagnitude(playerRb.velocity, maxSpeed);
        }
        else if (vertical < 0 && isGrounded)
        {
            playerRb.drag += 0.05f;
        }

        transform.Rotate(Vector3.up * Time.deltaTime * horizontal * turnSpeed);

        //if ((rotatePos <= startRotation + rotateLimit && rotatePos > 0) || (rotatePos > (360 + startRotation) - rotateLimit))
        //{
        //    transform.Rotate(Vector3.up * Time.deltaTime * horizontal * turnSpeed);
        //}
        //else if (rotatePos > rotateLimit && rotatePos < rotateLimit + rotateOffset)
        //{
        //    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, rotateLimit, transform.eulerAngles.z);
        //}
        //else if (rotatePos < 360 - rotateLimit && rotatePos > 360 - rotateLimit - rotateOffset)
        //{
        //    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 360 - rotateLimit, transform.eulerAngles.z);
        //}
        //else if (rotatePos > rotateLimit + rotateOffset && rotatePos < (360 - rotateLimit - rotateOffset))
        //{
        //    Debug.Log("Restart");
        //}
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            StartCoroutine(JumpScore());
            playerRb.AddRelativeForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, transform.eulerAngles.y, 0), 0.5f);
            playerRb.drag = 0;
        }
    }

    void OutOfBounds()
    {
        if (transform.position.y < -yLimit)
        {
            Debug.Log("Gameover");
            gameManager.isGameover = true;
            Destroy(gameObject);
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
                gameManager.isGameover = true;
            }
            Debug.Log("Dead");
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
            Destroy(other.gameObject);
            Debug.Log("Coin Points");
        }
        else if (other.CompareTag("PathFlags"))
        {
            Destroy(other.GetComponent<BoxCollider>());
            Debug.Log("Bonus Flag Points");
        }
        else if (other.CompareTag("RingPath"))
        {
            Destroy(other.GetComponent<BoxCollider>());
            Debug.Log("Bonus Ring Points");
        }
    }

    IEnumerator JumpScore()
    {
        yield return new WaitForSeconds(2);
        if(!isGrounded)
        {
            Debug.Log("Jump Score");
        }
    }
}
