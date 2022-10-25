using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 摄像机的旋转
/// </summary>


public class CameraControl : MonoBehaviour
{
    public float mouseSensitivity = 100f;//视线灵敏度
    public Transform playerBody;//玩家的位置
    public float yRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //隐藏光标
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yRotation -= mouseY;//将上下旋转轴值进行累加（鼠标反转

        yRotation = Mathf.Clamp(yRotation, -80f, 80f);//旋转角度设置

        transform.localRotation =Quaternion.Euler(yRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up*mouseX);//横向旋转


    }
}
