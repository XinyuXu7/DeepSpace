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
    public float walkSpeed = 10f;//�ƶ��ٶ�
    public float runSpeed = 15f;//�����ٶ�
    public float speed;
    public Vector3 moveDriction;//�����ƶ�����
    public bool isRun;

    //��Ծ
    public float jumpForce = 3f;//��Ծ����
    public Vector3 velocity;//��
    private bool isJump;
    
    public float gravity = -25f;//����
    private Transform groundCheck;
    private float groundDistance = 0.1f;
    public LayerMask groundMash;
    private bool isGround;

    [SerializeField] private float slopeForce = 6.0f;
    [SerializeField] private float slopeForceRayLenth = 2.0f;

    /*���ü�λ*/
    [Header("Keyboard Setting")]
    [SerializeField]private KeyCode runInputName;//���ܼ�λ
    [SerializeField] private KeyCode jumpInputName ;//��Ծ��λ

    private void Start()
    {
        //��ȡplayer��CharacterController���
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
        //�ƶ��ͱ���
        float hor = Input.GetAxis("Horizontal");//��ȡˮƽ������
        float ver = Input.GetAxis("Vertical");//��ȡ��ֱ������
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

        //��Ծ
        if (isGround == false)//���ڵ����ϣ����У�ʩ�����µ�����
        {
            velocity.y += gravity * Time.deltaTime;
        }
        characterController.Move(velocity*Time.deltaTime);
        Jump();

        //�����б�����ƶ�
        if (OnSlope())
        {
            //����һ�����µ���
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
        //�ж��Ƿ��ڵ�����
        isGround = Physics.CheckSphere(groundCheck.position,groundDistance,groundMash);
        if (isGround && velocity.y <= 0)
        {
            velocity.y = -2f;
        }
    }

    //����Ƿ���ƽ����
    public bool OnSlope()
    {
        if (isJump)
            return false;
        //���·������ߣ�����Ƿ���б����
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
