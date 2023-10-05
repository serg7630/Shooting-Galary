using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public bool _aiming;


    [SerializeField] float recharg = 1f;
    [SerializeField] private Transform _towerTank;
    [SerializeField] private Transform _point;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private ActiveButtobExplosion ABX;
    [SerializeField] float velosityMult = 500f;
    private GameObject _projectile;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)&&!_aiming)
        {
            
            _aiming = true;
            Quaternion RotationBulet = _point.rotation;
            _projectile = Instantiate(_projectilePrefab,_point.position,RotationBulet);
            
            
            Rigidbody rb = _projectile.GetComponent<Rigidbody>();
            rb.AddForce ( _projectile.transform.right * velosityMult,ForceMode.Impulse);

            _projectile.GetComponent<Projectile>()._aBX = ABX;
            ABX.Projectile = _projectile;
            _projectile.GetComponent<Projectile>().PW = this;

            Invoke("Rechard", recharg);
        }
    }

    void Rechard()
    {
        _aiming = false;
    }


   
}
