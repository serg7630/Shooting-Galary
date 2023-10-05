using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    static public PlayerControl S;
    [Header("ограничения для поворота орудия")]
    public Vector2 MinX;
    public Vector2 MinY;
    public float speedRotation=0.2f;

    float mouseSpeed = 10f;
    public bool returnControl=true;

    public float InputHorizont;
    public float InputVertical;

     public  bool MobilePlatform;
    [SerializeField] GameObject mobileMoveButtom;
    [SerializeField] VariableJoystick joystick;

    public void Awake()
    {
        mobileMoveButtom = GameObject.Find("Variable Joystick");

        if (!Application.isMobilePlatform)  //если не мобильная патформа
        {
            speedRotation = 0.5f;
            MobilePlatform = false;
            Slinshot.S.Mobileplatform = false;
            mobileMoveButtom.SetActive(false);
            Debug.LogError("isMobile");
        }
        if (S == null) S = this;
        Invoke("ChecEnabled", 1f);
        this.enabled = true;
    }
    public void Update()
    {
        //print(joystick.Horizontal);
        if (MobilePlatform)
        {
            InputHorizont = joystick.Horizontal * speedRotation;
            InputVertical = joystick.Vertical * speedRotation;
            return;
        }
        InputHorizont = Input.GetAxis("Horizontal")*speedRotation;
        InputVertical = Input.GetAxis("Vertical")*speedRotation;
        
    }
    private void FixedUpdate()
    {
        if (!returnControl) return;
        RotationPlayer();
    }
    public void RotationPlayer()
    {   

        Vector3 rotate = transform.eulerAngles;
        
        rotate.y = rotate.y+InputHorizont; 
        if (rotate.y<MinY.x & rotate.y> MinY.x-30f)rotate.y = MinY.x;
        if (rotate.y > MinY.y & rotate.y < MinY.y+50f)rotate.y = MinY.y;
        

        rotate.x = rotate.x-InputVertical;
        if (rotate.x < MinX.x & rotate.x > MinX.x-10f) rotate.x = MinX.x/* print("rotateX")*/;
        if (rotate.x > MinX.y & rotate.x < MinX.y+20f) rotate.x = MinX.y;

        transform.rotation = Quaternion.Euler(rotate);
    }

    public  void HideMobilePlatform()
    {
        mobileMoveButtom.SetActive(false);
    }
    public  void ShoweMobilePlatform()
    {
        mobileMoveButtom.SetActive(true);
    }
    void ChecEnabled()
    {
        if (this.enabled) { return; }
        else { 
            this.enabled = true;
            print("enabled try");
        }
    }
    void chekMobilePlatform()
    {
        
    }
}
