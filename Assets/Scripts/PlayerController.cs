using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;

    [SerializeField] private float walkingSpeed;
    [SerializeField] private float angularWalkingSpeed;
    [SerializeField] private float runningSpeed;
    [SerializeField] private float runningAngularSpeed;
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private TextMeshProUGUI loseText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Button restartButton;
    [SerializeField] private float maxTime;

    private float timeRemaining;
    private float speed;
    private float angularSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        timeRemaining = maxTime;
        speed = walkingSpeed;
        angularSpeed = angularWalkingSpeed;
    }

    void Update()
    {
        if (timeRemaining > 0) 
        { 
            timeRemaining -= Time.deltaTime;
            Debug.Log("Time" + timeRemaining);
        } else
        {
            Debug.Log("Time is over!");
            FinishGame(false);
        }
        DisplayTime(timeRemaining);
    }

    void FixedUpdate()
    {
        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");

        if (moveZ > 0)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetBool("isRunning", true);
            speed = runningSpeed;
            angularSpeed = runningAngularSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.SetBool("isRunning", false);
            speed = walkingSpeed;
            angularSpeed = angularWalkingSpeed;
        }
        rb.velocity = transform.forward * moveZ * speed;
        rb.angularVelocity = transform.up * moveX * angularSpeed;

        rb.velocity = transform.forward * moveZ * speed;
        rb.angularVelocity = transform.up * moveX * angularSpeed;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Treasure"))
        {
            FinishGame(true);
        }   
    }

    void FinishGame(Boolean playerWin)
    {
        Debug.Log("Finish");
        winText.gameObject.SetActive(playerWin);
        loseText.gameObject.SetActive(!playerWin);
        restartButton.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    void DisplayTime(float time) 
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    
}
