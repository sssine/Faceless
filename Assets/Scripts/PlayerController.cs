using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform playerCamera ;
    [SerializeField] public float mouseSensitivity = 4;
    private float cameraPitch = 0.0f;
    [SerializeField] private float speed = 6.0f;
    CharacterController controller;
    private float moveSmoothing = 0.2f;
    private float mouseSmoothing = 0.02f;
    private Vector2 currentMouseDelta = Vector2.zero;
    private Vector2 currentMouseDeltaVelocity = Vector2.zero;
    Vector2 currentDirection = Vector2.zero;
    Vector2 currentDirectionVelocity = Vector2.zero;
    bool isSprinting;
    private float enrageMeter = 0f;

    [SerializeField] private GameObject beacon;
    [SerializeField] private Transform beaconDrop;

    private void Update()
    {
        UpdateMouseLook();
        UpdateMovement();
        if (Input.GetButtonDown("Sprint"))
        {
            
            isSprinting = true;
            
        }    
        if (Input.GetButtonUp("Sprint"))
        {
            isSprinting = false;
        }
        if(isSprinting)
        {
            speed = 12f;
            enrageMeter += Time.deltaTime;
        }
        if(!isSprinting)
        {
            speed = 6f;
            enrageMeter -= Time.deltaTime;
        }
        if (enrageMeter >= 5)
        {
            GameObject.Instantiate(beacon, beaconDrop);
            Debug.Log("BOOOOOOOOOOOOOOOOO");
            enrageMeter = 0;
        }
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UpdateMouseLook()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, mouseDelta, ref currentMouseDeltaVelocity, mouseSmoothing);
        cameraPitch -= mouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);
        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * mouseDelta.x * mouseSensitivity);
    }
    void UpdateMovement()
    {
        Vector2 InputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        InputDirection.Normalize();
        currentDirection = Vector2.SmoothDamp(currentDirection, InputDirection, ref currentDirectionVelocity, moveSmoothing);
        Vector3 velocity = (transform.forward * InputDirection.y + transform.right * InputDirection.x) * speed;
        controller.Move(velocity * Time.deltaTime);
    }

   
















    //private static PlayerController _instance;
    //public static PlayerController Instance
    //{
    //    get { return _instance;  }
    //}
    //private InputMaster controls;
    //private void Awake()
    //{
    //    if(_instance != null && _instance != this)
    //    {
    //        Destroy(this.gameObject);
    //    }
    //    else 
    //    {
    //        _instance = this;
    //    }
    //    controls = new InputMaster();

    //}

    //public Vector2 GetPlayerMovement()
    //{
    //    return controls.PlayerInput.Movement.ReadValue<Vector2>();
    //}
    //public Vector2 GetMouseDelta()
    //{
    //    return controls.PlayerInput.Look.ReadValue<Vector2>();
    //}
    //private void OnEnable()
    //{
    //    controls.Enable();
    //}
    //private void OnDisable()
    //{
    //    controls.Disable();
    //}
}
