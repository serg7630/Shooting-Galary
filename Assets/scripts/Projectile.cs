using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public PlayerWeapon PW;
    public ActiveButtobExplosion _aBX;
    public GameObject TrailSmoke;

    void Start()
    {
        //TrailSmoke.SetActive(false);
    }

    
    void Update()
    {
        if (transform.position.y <= -20)
        {
            FollowCam.POI = null;
            Destroy(gameObject,1f);
        }
    }
    public void ProjectileNull()
    {
        //_aBX.ExplosionProjectile();
        //PW._aiming = false;
    }
    public void SmokeActive()
    {
        TrailSmoke.SetActive(true);
    }
}
