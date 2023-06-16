using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private Transform playerTransform;
    private float movement;
    private bool toTheRight = true;
 

    [SerializeField] private GameObject playerHead;
    private Transform playerHeadTransform;
    [SerializeField] private Transform playerNeckTransform;
    

    private Vector3 facingDirection;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnPointBullet;
    private bool isReloading;
    [SerializeField] private float reloadTime = 0.5f;
    private float timer;

    private Animator playerAnim;
    private AudioSource playerAudio;
    [SerializeField] private AudioSource playerAudioForFootStep;

    private void Awake()
    {
        playerTransform = gameObject.GetComponent<Transform>();
        playerHeadTransform = playerHead.GetComponent<Transform>();
        playerAnim = gameObject.GetComponent<Animator>();
        playerAudio = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        Movement();
        HeadFollow();
        Flip();
        FlipHead();
        Shoot();
        Reload();
    }

    private void Flip()
    {
        if (movement > 0)
        {
            playerTransform.localScale = new Vector3(1, 1, 1);

            toTheRight = true;
        }
        else if (movement < 0)
        {
            playerTransform.localScale = new Vector3(-1, 1, 1);

            toTheRight = false;
        } 
    }

    private void FlipHead()
    {
        if (facingDirection.x > 0)
        {
            if (toTheRight)
            {
                HeadSetScale(1,1);
            }
            else
            {
                HeadSetScale(-1,1);
            }
        }
        else
        {
            if (toTheRight)
            {
                HeadSetScale(1, -1);
            }
            else
            {
                HeadSetScale(-1,-1);
            }
        }
    }

    private void HeadSetScale(float scaleHeadX, float scaleHeadY)
    {
        playerHeadTransform.localScale = new Vector3(scaleHeadX, scaleHeadY, 1);
        //splayerNeckTransform.localScale = new Vector3(scaleNeckX, 1, 1);
    }

    private void Movement()
    {
        movement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        playerTransform.Translate(movement, 0, 0);

        if (movement != 0)
        {
            playerAnim.SetBool("isWalking", true);
        }
        else
        {
            playerAnim.SetBool("isWalking", false);
        }
    }

    private void HeadFollow()
    {
        var worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        facingDirection = worldMousePosition - playerHeadTransform.position;
        var aimAngle = Mathf.Atan2(facingDirection.y, facingDirection.x);
     

        if (aimAngle < 0f)
        {
            aimAngle = Mathf.PI * 2 + aimAngle;
        }

        var aimDirectionZ = aimAngle * Mathf.Rad2Deg;

        playerHeadTransform.rotation = Quaternion.Euler(0, 0, aimDirectionZ);
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse2) && isReloading == false || Input.GetKeyDown(KeyCode.Space) && isReloading == false)
        {
            GameObject bullet = Instantiate(bulletPrefab, spawnPointBullet.position, spawnPointBullet.rotation);
            isReloading = true;
            playerAudio.Play();
        }       
    }

    private void Reload()
    {
        if (isReloading == true)
        {
            timer += Time.deltaTime;

            if (timer >= reloadTime)
            {
                isReloading = false;
                timer = 0;
            }
        }
    }

    public float GetMovementSpeed()
    {
        return movement;
    }

    public void PlaySoundFootStep()
    {
        playerAudioForFootStep.Play();
    }
}
