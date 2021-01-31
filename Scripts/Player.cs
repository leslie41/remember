using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class Player : MonoBehaviour
{
    public LayerMask layer;

    public Animator animator;
    public CharacterController2D controller;

    public float runSpeed = 40f;
    public Joystick joystick;

    private float horizontalMove = 0f;
    private float verticalMove = 0f;
    bool jump = false;


    void Start()
    {

    }
    private void Update()
    {



            if (joystick.Horizontal >= .3f)
            {
                animator.SetBool("isWalk", true);
                horizontalMove = runSpeed;
            }
            else if (joystick.Horizontal <= -.3f)
            {
                horizontalMove = -runSpeed;
                animator.SetBool("isWalk", true);
            }
            else
            {
                horizontalMove = 0f;
                animator.SetBool("isWalk", false);
            }


            verticalMove = joystick.Vertical;

            if (verticalMove >= .5f)
            {

                jump = true;
                animator.SetBool("isJump", true);


            }
            else
            {
                animator.SetBool("isJump", false);
            }

        }


    

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.deltaTime, false, jump);
        jump = false;
    }



}