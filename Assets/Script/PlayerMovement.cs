using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// control palyer's movement
/// </summary>

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    public float walkSpeed = 10f;//移动速度
    public float runSpeed = 15f;//奔跑速度
    public float speed;
    public Vector3 moveDriction;//设置移动方向
    public bool isRun;

    //跳跃
    public float jumpForce = 3f;//跳跃的力
    public Vector3 velocity;//力
    private bool isJump;
    
    public float gravity = -25f;//重力
    private Transform groundCheck;
    private float groundDistance = 0.1f;
    public LayerMask groundMash;
    private bool isGround;

    [SerializeField] private float slopeForce = 6.0f;
    [SerializeField] private float slopeForceRayLenth = 2.0f;

    /*设置键位*/
    [Header("Keyboard Setting")]
    [SerializeField]private KeyCode runInputName;//奔跑键位
    [SerializeField] private KeyCode jumpInputName ;//跳跃键位

    private void Start()
    {
        //获取player的CharacterController组件
        characterController = GetComponent<CharacterController>();
        runInputName = KeyCode.LeftShift;
        jumpInputName = KeyCode.Space;
        groundCheck = GameObject.Find("Player/CheckGround").GetComponent<Transform>();
  
    }

    private void Update()
    {
        CheckGround();
        Move();
    }

    public void Move()
    {
        //移动和奔跑
        float hor = Input.GetAxis("Horizontal");//获取水平轴轴体
        float ver = Input.GetAxis("Vertical");//获取垂直轴轴体
        isRun = Input.GetKey(runInputName);
        if (isRun)
        {
            speed = runSpeed;
        }else
        {
            speed = walkSpeed;
        }

        moveDriction = (transform.right * hor + transform.forward * ver).normalized;
        characterController.Move(moveDriction*speed*Time.deltaTime);

        //跳跃
        if (isGround == false)//不在地面上（空中）施加向下的重力
        {
            velocity.y += gravity * Time.deltaTime;
        }
        characterController.Move(velocity*Time.deltaTime);
        Jump();

        //如果在斜坡上移动
        if (OnSlope())
        {
            //增加一个向下的力
            characterController.Move(Vector3.down * characterController.height / 2 * slopeForce * Time.deltaTime);
        }

    }

    public void Jump()
    {
        isJump = Input.GetKey(jumpInputName);
        
        if (isJump && isGround)
        {
            velocity.y =Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }

    public void CheckGround()
    {
        //判断是否在地面上
        isGround = Physics.CheckSphere(groundCheck.position,groundDistance,groundMash);
        if (isGround && velocity.y <= 0)
        {
            velocity.y = -2f;
        }
    }

    //检测是否在平面上
    public bool OnSlope()
    {
        if (isJump)
            return false;
        //向下发出射线，检查是否在斜坡上
        if(Physics.Raycast(transform.position,new Vector3(0,-1,0),out RaycastHit hit, characterController.height/2*slopeForceRayLenth))
        {
            if(hit.normal != Vector3.up)
            {
                return true;
            }              
        }
        return false;
    }
}
