using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{

    public float speed;
    float resetSpeed;
    public float dashSpeed;
    public float dashTime;

    PhotonView view;
    Animator anim;
    Health healthScript;

    LineRenderer rend;

    public float minX, maxX, minY, maxY;

    //sound
    public AudioSource _as;
    public AudioClip[] audioClipArray;
    public AudioClip[] secondaryAudioClipArray;

    //camerashake
    private Animator cameraAnim;

    //nicknames
    public TextMeshProUGUI nameDisplay;


    private void Start()
    {
        resetSpeed = speed;
        view = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();
        healthScript = FindObjectOfType<Health>();
        rend = FindObjectOfType<LineRenderer>();

        _as = GetComponent<AudioSource>();

        cameraAnim = Camera.main.GetComponent<Animator>();

        if (view.IsMine)
        {
            nameDisplay.text = PhotonNetwork.NickName;
        }
        else
        {
            nameDisplay.text = view.Owner.NickName;
        }
    }


    private void Update()
    {
        if (view.IsMine)
        {
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 moveAmount = moveInput.normalized * speed * Time.deltaTime;
            transform.position += (Vector3)moveAmount;

            //wrap
            Wrap();

            //dash move
            if(Input.GetKeyDown(KeyCode.Space) &&  moveInput != Vector2.zero)
            {
                PlaySoundDash();
                StartCoroutine(Dash());
            }

            if(moveInput == Vector2.zero)
            {
                anim.SetBool("isRunning", false);
            }
            else
            {
                anim.SetBool("isRunning", true);
            }

            rend.SetPosition(0, transform.position);

        }
        else
        {
            rend.SetPosition(1, transform.position);
        }

    }

    IEnumerator Dash()
    {
        speed = dashSpeed;
        yield return new WaitForSeconds(dashTime);
        speed = resetSpeed;
    }

    void Wrap()
    {
        if(transform.position.x < minX)
        {
            transform.position = new Vector2(maxX, transform.position.y);
        }

        if (transform.position.x > maxX)
        {
            transform.position = new Vector2(minX, transform.position.y);
        }

        if (transform.position.y < minY)
        {
            transform.position = new Vector2(transform.position.x, maxY);
        }

        if (transform.position.y > maxY)
        {
            transform.position = new Vector2(transform.position.x, minY);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (view.IsMine)
        {
            if (collision.tag == "Enemy")
            {
                PlayHurtSound();
                cameraAnim.SetTrigger("shake");
                healthScript.TakeDamage();


            }
        }
     
    }

    void PlaySoundDash()
    {
        _as.clip = audioClipArray[Random.Range(0, audioClipArray.Length)];
        _as.PlayOneShot(_as.clip);
    }

    void PlayHurtSound()
    {
        _as.clip = secondaryAudioClipArray[Random.Range(0, secondaryAudioClipArray.Length)];
        _as.PlayOneShot(_as.clip);
    }


}
