using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slinshot : MonoBehaviour
{
    static public Slinshot S;

    [SerializeField] GameObject projectPrefab;

    public Transform ProjectPosition;
    public Vector3 _launcPos;
    [SerializeField] GameObject _projectile;
    public bool aiming=false;
    public bool Mobileplatform=true;

    [SerializeField] private bool FireMobile;

    [SerializeField] Rigidbody RB;
    public float velosityMult=5000;
    public float rechardTime;

    [SerializeField] private ActiveButtobExplosion ABX;
    [SerializeField] private Score _score;

    [SerializeField]
    private TraectoryLine _traectoryLine;
    public float RbMass;
    public PlayerControl PL;

    private void Awake()
    {
        PL=GetComponent<PlayerControl>();
    }
    void Start()
    {
        //aiming = true;
        //_launcPoint = GameObject.Find("Halo");
        //SetlauncPos();
        //_launcPoint.SetActive(false);
        if (S == null) S = this;
    }
   //public void SetlauncPos()
   // {
        
   //     //_launcPos = _launcPoint.transform.position;
       
   //     //Invoke("launcPos", .2f);

   // }
   
    void Update()
    {

        if (Mobileplatform)
        {
            if (FireMobile && aiming != false) Fire();
            else { return; }
        }

        if (Input.GetMouseButton(0)&&aiming!=false)
        {
            if (Mobileplatform) return;
            Fire();
        }
      

    }
     public void Fire()
    {
        //Debug.LogError("firer");
        _projectile = Instantiate(projectPrefab) as GameObject;
        _projectile.transform.position = ProjectPosition.position;
        RB = _projectile.GetComponent<Rigidbody>();
        //RB.mass= RbMass;
        RB.AddForce(ProjectPosition.forward * velosityMult);
        _projectile.GetComponent<Saw>().GetScore = _score;
        _projectile.GetComponent<Projectile>()._aBX = ABX;
        _projectile.GetComponent<Projectile>().SmokeActive();
        //ABX.Projectile = _projectile;
        //ABX.StartProjectile();
        aiming = false;
        Invoke("recharge", rechardTime);
          _projectile = null;
    }
    

    private void OnMouseEnter()
    {
        //Debug.Log("Mousr");
        //_launcPoint.SetActive(true);
    }
    private void OnMouseExit()
    {
        //_launcPoint.SetActive(false);
    }
    public void stopControl()
    {
        PL.returnControl=false;
    }
    public void startControl()
    {
        PL.returnControl = true;
    }


    static public Vector3 LAUNC_POS
    {
        get
        {
            if (S == null) return Vector3.zero;
            return S._launcPos;
        }
    }
    void recharge()
    {
        aiming = true;
    }

    public void FireMoobileTry()
    {
        FireMobile = true;
    }
    public void FireMoobileFalse()
    {
        FireMobile = false;
    }
}
