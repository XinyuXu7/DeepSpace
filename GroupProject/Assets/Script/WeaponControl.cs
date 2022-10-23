using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponControl : MonoBehaviour
{
    public Transform shootPoint;
    public int bulletsMag = 30;//һ����ϻ���ӵ�����
    public int range = 100;//���
    public int bulletLeft = 300;//����
    public int currentBullet;//��ǰ�ӵ���

    private bool gunShootInput;

    public float fireRate = 0.1f;//����
    private float fireTime;//��ʱ��

    [Header("KeySet")]
    [SerializeField]private KeyCode reloadInputName;

    [Header("UI")]
    public Text AmmoTextUI;
    public Image CrossPoint;

    // Start is called before the first frame update
    void Start()
    {
        reloadInputName = KeyCode.R;
        currentBullet = bulletsMag;
        UpdateAmmoUI();
    }

    // Update is called once per frame
    void Update()
    {
        gunShootInput = Input.GetMouseButton(0);//����������Ƿ���
        if(gunShootInput)
        {
            GunFire();
        }

        if(fireTime < fireRate)
        {
            fireTime += Time.deltaTime;
        }

        if (Input.GetKeyDown(reloadInputName) && currentBullet < bulletsMag && bulletLeft > 0)
        {
            Reload();
        }
     }

    /// <summary>
    /// ���
    /// </summary>
    public void GunFire()
    {
        //�������٣������ʱ�������ٻ�С�����������������
        //ÿ֡��������ִ�еĴ������Ӷ���������
        //�ӵ�Ϊ0ʱ�˳������
        if (fireTime < fireRate || currentBullet <= 0)
        {
            return;
        }
            
        //���
        RaycastHit hit;
        Vector3 shootDirection = shootPoint.forward;//���������ǰ
        if(Physics.Raycast(shootPoint.position,shootDirection,out hit,range))//100�루range������̣�����������壬��Ὣ��Ϣ��ŵ� hit ��
        {
            Debug.Log(hit.transform.name + "����");
        }

        currentBullet--;//ÿ������ӵ���һ
        UpdateAmmoUI();

        fireTime = 0;
    }

    /// <summary>
    /// �ӵ�UI
    /// </summary>
    public void UpdateAmmoUI()
    {
        AmmoTextUI.text = currentBullet + "/" + bulletLeft;
    }

    /// <summary>
    /// ���ӵ�
    /// </summary>
    public void Reload()
    {
        if(bulletLeft <=0) return;

        int bulletNeed = bulletsMag - currentBullet;//������װ�ĵ�ҩ����

        //int bullectReduce = (bulletLeft >= bulletNeed) ? bulletNeed : bulletLeft;

      
        if(bulletLeft >= bulletNeed)
        {
            bulletLeft = bulletLeft - bulletNeed;
        }
        else
        {
            bulletLeft = 0;
            bulletsMag = currentBullet + bulletLeft;
        }
       
        //bulletLeft -= bullectReduce;//���ٵı���
        currentBullet += bulletNeed;//��ǰ�ӵ���
        UpdateAmmoUI();
    }
}
