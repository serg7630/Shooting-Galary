using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlinshotTurrel : MonoBehaviour
{
    static public SlinshotTurrel S;

    [SerializeField] GameObject projectPrefab;

    [SerializeField] GameObject _launcPoint;
    Vector3 _launcPos;
    [SerializeField] GameObject _projectile;
    [SerializeField] bool aiming;
    [SerializeField] Rigidbody RB;
    [SerializeField] float velosityMult = 10f;
    Vector3 mousDelta;

    void Start()
    {
        S = this;
        _launcPoint = GameObject.Find("Halo");
        _launcPos = _launcPoint.transform.position;
        _launcPoint.SetActive(false);
    }

   public void SetlauncPos()
    {
        _launcPos = _launcPoint.transform.position;
    }
   
    void Update()
    {
        if (!aiming) return;
        Vector3 mousPos3D = Input.mousePosition;
        mousPos3D.z = -Camera.main.transform.position.z;
        Vector3 mousPos2D = Camera.main.ScreenToWorldPoint(mousPos3D);
         mousDelta =mousPos2D- _launcPos  ;
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (mousDelta.magnitude>maxMagnitude)
        {
            mousDelta.Normalize();
            mousDelta *= maxMagnitude;
            

           

        }
        Vector3 projPos = _launcPos + mousDelta;
        _projectile.transform.position = projPos;
        FireTurret();
        if(Input.GetMouseButton(0)) Invoke("MakeProjectile", 0.5f);

       

    }
   public void FireTurret()
    {       aiming = false;
            RB.isKinematic = false;
            RB.velocity = -mousDelta* velosityMult;
            RB.mass = 0.1f;
            Destroy(_projectile, 3f);
            //FollowCam.POI = _projectile;
            _projectile = null;

    }
    private void OnMouseDown()
    {
        MakeProjectile();
    }
    void MakeProjectile()
    {
        aiming = true;
        _projectile = Instantiate(projectPrefab) as GameObject;
        _projectile.transform.position = _launcPos;
        RB = _projectile.GetComponent<Rigidbody>();
        RB.isKinematic = true;
    }
    private void OnMouseEnter()
    {
        Debug.Log("Mousr");
        _launcPoint.SetActive(true);
    }
    private void OnMouseExit()
    {
        _launcPoint.SetActive(false);
    }
}
