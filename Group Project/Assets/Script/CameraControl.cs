using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���������ת
/// </summary>


public class CameraControl : MonoBehaviour
{
    public float mouseSensitivity = 100f;//����������
    public Transform playerBody;//��ҵ�λ��
    public float yRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //���ع��
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yRotation -= mouseY;//��������ת��ֵ�����ۼӣ���귴ת

        yRotation = Mathf.Clamp(yRotation, -80f, 80f);//��ת�Ƕ�����

        transform.localRotation =Quaternion.Euler(yRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up*mouseX);//������ת


    }
}
