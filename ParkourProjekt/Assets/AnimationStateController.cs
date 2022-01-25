using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    [SerializeField] private PlayerMovment2 MovementSystem;

    Animator animator;
    int isWalkingHash;
    int isWalkingBackwardsHash;
    int isWalkingRightHash;
    int isWalkingLeftHash;
    int isRunningHash;

    int isCrouchingHash;
    int isCrouchedWalkingHash;
    int isCrouchedWalkingBackwardsHash;
    int isCrouchedWalkingRightHash;
    int isCrouchedWalkingLeftHash;

    int isSlidingHash;
    int isFallingHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isWalkingBackwardsHash = Animator.StringToHash("isWalkingBackwards");
        isWalkingRightHash = Animator.StringToHash("isWalkingRight");
        isWalkingLeftHash = Animator.StringToHash("isWalkingLeft");
        isRunningHash = Animator.StringToHash("isRunning");

        isCrouchingHash = Animator.StringToHash("isCrouching");
        isCrouchedWalkingHash = Animator.StringToHash("isCrouchedWalking");
        isCrouchedWalkingBackwardsHash = Animator.StringToHash("isCrouchedWalkingBackwards");
        isCrouchedWalkingRightHash = Animator.StringToHash("isCrouchedWalkingRight");
        isCrouchedWalkingLeftHash = Animator.StringToHash("isCrouchedWalkingLeft");

        isSlidingHash = Animator.StringToHash("isSliding");
        isFallingHash = Animator.StringToHash("isFalling");
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isWalkingBackwards = animator.GetBool(isWalkingBackwardsHash);
        bool isWalkingRight = animator.GetBool(isWalkingRightHash);
        bool isWalkingLeft = animator.GetBool(isWalkingLeftHash);
        bool isRunning = animator.GetBool(isRunningHash);
        bool isCrouching = animator.GetBool(isCrouchingHash);
        bool isCrouchedWalking = animator.GetBool(isCrouchedWalkingHash);
        bool isCrouchedWalkingBackwards = animator.GetBool(isCrouchedWalkingBackwardsHash);
        bool isCrouchedWalkingRight = animator.GetBool(isCrouchedWalkingRightHash);
        bool isCrouchedWalkingLeft = animator.GetBool(isCrouchedWalkingLeftHash);
        bool isFalling = animator.GetBool(isFallingHash);
        bool isSliding = animator.GetBool(isSlidingHash);

        bool forwardPressed = Input.GetKey("w");
        bool backwardPressed = Input.GetKey("s");
        bool rightPressed = Input.GetKey("d");
        bool leftPressed = Input.GetKey("a");
        bool runPressed = Input.GetKey("left shift");
        bool crouchPressed = Input.GetKey("left ctrl");

        // if the player presses the key "w", the character starts to walk
        if (!isWalking && forwardPressed)
        {
            // then set isWalking to true
            animator.SetBool(isWalkingHash, true);
        }

        // if the player does not press the key "w", the characters stops walking
        if (isWalking && !forwardPressed)
        {
            // then set isWalking to false
            animator.SetBool(isWalkingHash, false);
        }

        

        // if the player presses the key "s", the character starts to walk backwards
        if (!isWalkingBackwards && backwardPressed)
        {
            // then set isWalkingBackwards to true
            animator.SetBool(isWalkingBackwardsHash, true);
        }

        // if the player does not press the key "s", the characters stops walking backwards
        if (isWalkingBackwards && !backwardPressed)
        {
            // then set isWalkingBackwards to false
            animator.SetBool(isWalkingBackwardsHash, false);
        }



        // if the player presses the key "d", the character starts to walk right
        if (!isWalkingRight && rightPressed)
        {
            // then set isWalkingRight to true
            animator.SetBool(isWalkingRightHash, true);
        }

        // if the player does not press the key "d", the characters stops walking right
        if (isWalkingRight && !rightPressed)
        {
            // then set isWalkingRight to false
            animator.SetBool(isWalkingRightHash, false);
        }



        // if the player presses the key "a", the character starts to walk left
        if (!isWalkingLeft && leftPressed)
        {
            // then set isWalkingBackwards to true
            animator.SetBool(isWalkingLeftHash, true);
        }

        // if the player does not press the key "a", the characters stops walking left
        if (isWalkingLeft && !leftPressed)
        {
            // then set isWalkingBackwards to false
            animator.SetBool(isWalkingLeftHash, false);
        }



        // if the player presses the key "w" and the key "left shift", the character starts to run
        if (!isRunning && (forwardPressed && runPressed))
        {
            // then set isRunning to true
            animator.SetBool(isRunningHash, true);
        }

        // if the player is running and stops running or stops walking
        if (isRunning && (!forwardPressed || !runPressed))
        {
            // then set isRunning to false
            animator.SetBool(isRunningHash, false);
        }



        // if the player presses the key "ctrl", the character starts to crouch
        if (!isCrouching && crouchPressed)
        {
            // then set isCrouching to true
            animator.SetBool(isCrouchingHash, true);
        }

        // if the player does not press the key "ctrl", the characters stops crouching
        if (isCrouching && !crouchPressed)
        {
            // then set isCrouching to false
            animator.SetBool(isCrouchingHash, false);
        }



        // if the player presses the key "w" and the key "left ctrl", the character starts to crouchwalk
        if (isWalking && (forwardPressed && crouchPressed))
        {
            // then set isCrouchedWalking to true
            animator.SetBool(isCrouchedWalkingHash, true);
        }

        // if the player is crouchwalking and stops crouchwalking or stops crouching
        if (isWalking && (!forwardPressed || !crouchPressed))
        {
            // then set isCrouchedWalking to false
            animator.SetBool(isCrouchedWalkingHash, false);
        }



        // if the player presses the key "s" and the key "left ctrl", the character starts to crouchwalk backwards
        if (isWalkingBackwards && (backwardPressed && crouchPressed))
        {
            // then set isCrouchedWalkingBackwardsg to true
            animator.SetBool(isCrouchedWalkingHash, true);
        }

        // if the player is crouchwalking backwards and stops crouchwalking backwards or stops crouching backwards
        if (isWalkingBackwards && (!backwardPressed || !crouchPressed))
        {
            // then set isCrouchedWalkingBackwards to false
            animator.SetBool(isCrouchedWalkingHash, false);
        }



        // if the player presses the key "d" and the key "left ctrl", the character starts to crouchwalk to the right
        if (isWalkingRight && (rightPressed && crouchPressed))
        {
            // then set isCrouchedWalkingRight to true
            animator.SetBool(isCrouchedWalkingRightHash, true);
        }

        // if the player is crouchwalking to the right and stops crouchwalking right or stops crouching
        if (isWalkingRight && (!rightPressed || !crouchPressed))
        {
            // then set isCrouchedWalkingRight to false
            animator.SetBool(isCrouchedWalkingRightHash, false);
        }



        // if the player presses the key "a" and the key "left ctrl", the character starts to crouchwalk to the left
        if (isWalkingLeft && (leftPressed && crouchPressed))
        {
            // then set isCrouchedWalkingRight to true
            animator.SetBool(isCrouchedWalkingLeftHash, true);
        }

        // if the player is crouchwalking to the left and stops crouchwalking to the right or stops crouching
        if (isWalkingLeft && (!leftPressed || !crouchPressed))
        {
            // then set isCrouchedWalkingRight to false
            animator.SetBool(isCrouchedWalkingLeftHash, false);
        }



        // if the player is running and presses "ctrl", the character starts to slide
        if (!isSliding && (forwardPressed && runPressed && crouchPressed))
        {
            // then set isRunning to true
            animator.SetBool(isSlidingHash, true);
        }

        // if the player is not pressing "w", "shift" or "ctrl", the character stops sliding
        if (isSliding && (!forwardPressed || !runPressed || !crouchPressed))
        {
            // then set isRunning to false
            animator.SetBool(isSlidingHash, false);
        }



        // if the player is currently falling, the falling animation starts
        if (!MovementSystem.isGrounded)
        {
            // then set isJumping to true
            animator.SetBool(isFallingHash, true);
        }

        // if the player is not in the air, the characters stops falling
        if (MovementSystem.isGrounded)
        {
            // then set isJumping to false
            animator.SetBool(isFallingHash, false);
        }



        // if the player is touching a wall, the characters starts to wallrun
        if (MovementSystem.hitWall)
        {
            // then set isRunning to true and isFalling to false
            animator.SetBool(isRunningHash, true);
            animator.SetBool(isFallingHash, false);
        }
    }
}
