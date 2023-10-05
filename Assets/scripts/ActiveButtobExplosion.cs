using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveButtobExplosion : MonoBehaviour
{
    [SerializeField] private Button _explosion;
    public GameObject Projectile;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
   public void ExplosionProjectile()
    {
        _explosion.interactable = false;
    }
   public void StartProjectile()
    {
        _explosion.interactable = true;
    }
    public void Explosion()
    {
        //Saw saw = Projectile.GetComponent<Saw>();
        //saw.explosion(saw.radiusexplorer*2);
        //saw.DestroyProjectile();
    }
}
