using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment2 : MonoBehaviour
{
    private Rigidbody PlayerRb;
    private CharacterController PlayerController;

    private float MoveX;
    private float MoveY;

    public GameObject HitBall;

    private float PlayerHeight = 1;

    private Vector3 Movment;
    private Vector3 SlidingMovment;

    [Header("Speed")]
    [SerializeField] private float NormalSpeed;
    [SerializeField] private float MaxSpeed;
    [Tooltip("How quick the running will be")]
    [SerializeField] private float RunSpeed;
    [SerializeField] private float FallSpeed;
    private float PlayerSpeed;
    [SerializeField] private float JumpForce;


    [Header("Keys")]
    [Tooltip("The key for running")]
    [SerializeField] private KeyCode RunKey;
    [SerializeField] private KeyCode CrouchKey;


    [Header("Drag")]
    [SerializeField] private float WalkRunDrag;
    [SerializeField] private float fallingDrag;
    [SerializeField] private float GlidingDrag;
    [SerializeField] private float StopDrag;
    [SerializeField] private float CrouchSlideDrag;
    Vector3 Wallspeed = Vector3.zero;


    public bool isGrounded;
    private bool onSlideFloor;
    private bool FallGrounded;
    private bool OnWallrunWallRight;
    private bool OnWallrunWallLeft;
    [HideInInspector] public bool hitWall = false;
    private bool MovmentActive = true;
    private bool RunActive = true;
    private bool isCrouchSliding = false;

    RaycastHit GroundOBJ;
    RaycastHit WallHit;
    RaycastHit VaultHit;

    private float VaultHitHeight = 0;

    private float jumpCooldownTimer;
    [SerializeField] private float jumpCooldown = 0.1f;

    private AudioSource Sounds;

    [Header("Sounds")]

    public AudioClip Wallrunfootsteps;
    public AudioClip Slide;
    public AudioClip MonsterEncounter;
    private float SoundTimer = 0;
    private float wallRuntimer = 0;




    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        Sounds = GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.Locked;
        PlayerRb = GetComponent<Rigidbody>();
        PlayerController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        GetInputs();
        CheckIsGrounded();
        DragManaging();
        Crouch();
        Vault();
        Run();


        transform.localScale = new Vector3(1, PlayerHeight, 1);
        Movment = (transform.right * MoveX + transform.forward * MoveY);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        WallRun();
        Move();
    }

    public void GetInputs()
    {
        MoveX = Input.GetAxis("Horizontal");
        MoveY = Input.GetAxis("Vertical");
    }

    public void Move()
    {
        PlayerRb.velocity = Vector3.ClampMagnitude(PlayerRb.velocity, 18);
        jumpCooldownTimer += Time.fixedDeltaTime;
        if (MovmentActive)
        {
            if (onSlideFloor)
            {
                SlidingMovment = Vector3.Scale(GroundOBJ.transform.right, Movment.normalized);
                PlayerRb.AddForce(GroundOBJ.transform.forward * 50, ForceMode.Force);
                PlayerRb.AddForce(SlidingMovment * 30, ForceMode.Force);
            }
            else
            {
                PlayerRb.AddForce(Movment.normalized * PlayerSpeed, ForceMode.Force);
            }

        }

    }

    public void CheckIsGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight + 0.1f);

        FallGrounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight + 3);

        if (Physics.Raycast(transform.position, gameObject.transform.right, 1))
        {
            OnWallrunWallRight = Physics.Raycast(transform.position, gameObject.transform.right, out WallHit, 1);
        }
        else
        {
            OnWallrunWallRight = false;
        }
        if (Physics.Raycast(transform.position, gameObject.transform.right * -1, 1))
        {
            OnWallrunWallLeft = Physics.Raycast(transform.position, gameObject.transform.right * -1, out WallHit, 1);
        }
        else
        {
            OnWallrunWallLeft = false;
        }
        Debug.DrawRay(transform.position, gameObject.transform.right * -1, Color.red);



        if (Physics.Raycast(transform.position, Vector3.down, out GroundOBJ, PlayerHeight + 0.1f) && GroundOBJ.transform.tag == "SlidingFloor")
        {
            onSlideFloor = true;
        }
        else
        {
            onSlideFloor = false;
        }
    }

    public void Crouch()
    {
        if (Input.GetKeyDown(CrouchKey))
        {
            PlayerHeight = 0.5f;
            transform.position += Vector3.down * 0.5f;
        }
        else if (Input.GetKeyUp(CrouchKey))
        {
            PlayerHeight = 1;
            transform.position += Vector3.up * 0.5f;
        }
        if (PlayerRb.velocity.magnitude > 3 && Input.GetKey(RunKey) && Input.GetKey(CrouchKey) && isGrounded)
        {
            RunActive = false;
            if (!isCrouchSliding)
            {
                isCrouchSliding = true;
                MovmentActive = false;
                PlayerRb.AddForce(transform.forward * 10, ForceMode.Impulse);

                GetComponent<AudioSource>().Play();
            }

        }
        else
        {
            isCrouchSliding = false;
        }
    }

    public void Vault()
    {
        if (Input.GetButtonDown("Jump") && isGrounded && jumpCooldownTimer > jumpCooldown)
        {
            if (Physics.Raycast(transform.position - new Vector3(0, 0.2f, 0), gameObject.transform.forward, 4))
            {
                Physics.Raycast(transform.position - new Vector3(0, 0.2f, 0), gameObject.transform.forward, out VaultHit, 4);
                VaultHitHeight = VaultHit.transform.GetComponent<MeshFilter>().mesh.bounds.size.y * VaultHit.transform.lossyScale.y;
                if (VaultHitHeight < 2)
                {
                    PlayerRb.velocity = new Vector3(PlayerRb.velocity.x, 0, PlayerRb.velocity.z);

                    float jumpneeded = Mathf.Sqrt((VaultHitHeight * -2 * Physics.gravity.y));

                    PlayerRb.AddForce((transform.up) * jumpneeded + Vector3.up * 1, ForceMode.Impulse);
                    jumpCooldownTimer = 0;

                }
                else
                {
                    PlayerRb.velocity = new Vector3(PlayerRb.velocity.x, 0, PlayerRb.velocity.z);
                    PlayerRb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
                    jumpCooldownTimer = 0;
                }
            }
            else
            {
                PlayerRb.velocity = new Vector3(PlayerRb.velocity.x, 0, PlayerRb.velocity.z);
                PlayerRb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
                jumpCooldownTimer = 0;
            }
        }
    }

    public void Run()
    {
        if (!isGrounded)
        {
            PlayerSpeed = FallSpeed;
            RunActive = false;
        }
        else if (Input.GetKey(RunKey) && RunActive && isGrounded)
        {
            PlayerSpeed = RunSpeed;
        }
        else
        {
            PlayerSpeed = NormalSpeed;
            if (!Input.GetKey(CrouchKey))
            {
                RunActive = true;
            }
        }
    }

    public void DragManaging()
    {
        if (isGrounded == true && MoveX == 0 && MoveY == 0 && PlayerRb.velocity.magnitude >= 0.1f)
        {
            PlayerRb.drag = StopDrag;
        }
        else
        {
            PlayerRb.drag = WalkRunDrag;
        }


        if (!isGrounded)
        {
            PlayerRb.drag = fallingDrag;
        }

        if (onSlideFloor)
        {
            PlayerRb.drag = GlidingDrag;
        }

        if (isCrouchSliding)
        {
            PlayerRb.drag = CrouchSlideDrag;
        }

    }


    public void WallRun()
    {
        wallRuntimer += Time.deltaTime;
        SoundTimer += Time.fixedDeltaTime;
        if (OnWallrunWallRight && !isGrounded && jumpCooldownTimer > 0.2 && WallHit.transform.tag != "NonWallrun" || OnWallrunWallLeft && !isGrounded && jumpCooldownTimer > 0.2 && WallHit.transform.tag != "NonWallrun")
        {
            if (!hitWall && wallRuntimer > 3)
            {
                Wallspeed = Vector3.Scale(PlayerRb.velocity, WallHit.transform.forward) + new Vector3(0, 5, 0);
                hitWall = true;
                //PlayerRb.isKinematic = true;
                //PlayerController.enabled = true;
                MovmentActive = false;
                if (OnWallrunWallLeft)
                {
                    gameObject.transform.eulerAngles = WallHit.transform.transform.right * 200 + PlayerRb.transform.rotation.eulerAngles;
                }
                if (OnWallrunWallRight)
                {
                    gameObject.transform.eulerAngles = WallHit.transform.transform.right * 200 + PlayerRb.transform.rotation.eulerAngles;
                }
            }

            if (SoundTimer > 0.4f && hitWall)
            {
                Sounds.clip = Wallrunfootsteps;
                SoundTimer = 0;
                Sounds.Play();
                if (Sounds.isPlaying)
                {
                    print("Played WallrunSound");
                }

            }


            //PlayerController.Move(speed + new Vector3(0, -1, 0));
            PlayerRb.velocity = Wallspeed -= new Vector3(0, 0.1f, 0);
            if (Input.GetButton("Jump"))
            {
                OnWallrunWallRight = false;
                OnWallrunWallLeft = false;
                wallRuntimer = 2.5f;
                //PlayerRb.velocity = PlayerRb.velocity + new Vector3(PlayerRb.velocity.x, 0, PlayerRb.velocity.z);
                PlayerRb.AddForce(WallHit.normal * 20 + Vector3.up * 5, ForceMode.Impulse);
            }
        }
        else
        {
            if (hitWall&&!Input.GetButton("Jump"))
            {
                wallRuntimer = 0;
            }
            Wallspeed = Vector3.zero;
            hitWall = false;
            if (!Input.GetKey(CrouchKey) && PlayerRb.velocity.sqrMagnitude < 3)
            {
                MovmentActive = true;
            }
        }
    }
}