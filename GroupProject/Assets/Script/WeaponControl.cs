using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponControl : MonoBehaviour
{
    public Transform shootPoint;
    public int bulletsMag = 30;//一个弹匣的子弹数量
    public int range = 100;//射程
    public int bulletLeft = 300;//备弹
    public int currentBullet;//当前子弹数

    private bool gunShootInput;

    public float fireRate = 0.1f;//射速
    private float fireTime;//计时器

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
        gunShootInput = Input.GetMouseButton(0);//检测鼠标左键是否按下
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
    /// 射击
    /// </summary>
    public void GunFire()
    {
        //控制射速，如果计时器比射速还小，跳出方法（不射击
        //每帧减少射线执行的次数，从而降低射速
        //子弹为0时退出射击。
        if (fireTime < fireRate || currentBullet <= 0)
        {
            return;
        }
            
        //射击
        RaycastHit hit;
        Vector3 shootDirection = shootPoint.forward;//射击方向向前
        if(Physics.Raycast(shootPoint.position,shootDirection,out hit,range))//100码（range）的射程，如果击中物体，则会将信息存放到 hit 中
        {
            Debug.Log(hit.transform.name + "击中");
        }

        currentBullet--;//每次射击子弹减一
        UpdateAmmoUI();

        fireTime = 0;
    }

    /// <summary>
    /// 子弹UI
    /// </summary>
    public void UpdateAmmoUI()
    {
        AmmoTextUI.text = currentBullet + "/" + bulletLeft;
    }

    /// <summary>
    /// 换子弹
    /// </summary>
    public void Reload()
    {
        if(bulletLeft <=0) return;

        int bulletNeed = bulletsMag - currentBullet;//所需填装的弹药数量

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
       
        //bulletLeft -= bullectReduce;//减少的备弹
        currentBullet += bulletNeed;//当前子弹数
        UpdateAmmoUI();
    }
}
