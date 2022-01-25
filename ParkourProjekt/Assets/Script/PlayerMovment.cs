using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    private Rigidbody PlayerRb;

    private float MoveX;
    private float MoveY;


    private float PlayerHeight = 1;

    private Vector3 Movment;

    [Header("Speed")]
    [SerializeField] private float NormalSpeed;
    [SerializeField] private float MaxSpeed;
    [Tooltip("How quick the running will be")]
    [SerializeField] private float RunSpeed;
    private float PlayerSpeed;


    [Header("Keys")]
    [Tooltip("The key for running")]
    [SerializeField] private KeyCode RunKey;


    private bool isGrounded;
    private bool onSlideFloor = false;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        PlayerRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.localScale = new Vector3(1, PlayerHeight, 1);
        RaycastHit GroundOBJ;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight + 0.1f);

        if(Physics.Raycast(transform.position, Vector3.down, out GroundOBJ, PlayerHeight + 0.1f) && GroundOBJ.transform.tag == "SlidingFloor")
        {
            onSlideFloor = true;
        }


        if (Input.GetKey(RunKey))
        {
            PlayerSpeed = RunSpeed;
        }
        else
        {
            PlayerSpeed = NormalSpeed;
        }

        

        MoveX = Input.GetAxis("Horizontal");
        MoveY = Input.GetAxis("Vertical");

        Movment = (transform.right * MoveX + transform.forward * MoveY);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (onSlideFloor)
        {
            
        }
        PlayerRb.velocity = Movment.normalized * PlayerSpeed + new Vector3(0,PlayerRb.velocity.y, 0);

        print(isGrounded);
    }
}
