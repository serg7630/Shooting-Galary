using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public float Force = 100f;
    public bool exlorer = false;

    public float radiusexplorer = 2f;
    public Score GetScore;

    [SerializeField] int _hitDamages = 1;
    [SerializeField] private GameObject _explosion;


    private bool _showExplorer=false;

    public void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(exlorer) return;
        //if (collision.gameObject.tag == "Ground") return;


        //print("DestroyProjectile");
        
        //exlorer = true;
        explosion(radiusexplorer * 1f);
    }
   
    public void explosion(float radius)
    {

        UpdateScore();
        DestroyProjectile();
        ShowExplorer();
        Collider[] overLappedColliders = Physics.OverlapSphere(transform.position, radius);
        //print(overLappedColliders.Length);
        for (int i = 0; i < overLappedColliders.Length; i++)
        {
            if (overLappedColliders[i].TryGetComponent(out Cube cube))
            {
                //if (overLappedColliders[i].gameObject.tag == "Goal_tank") Debug.LogError("Goal_tank      "+overLappedColliders[i].gameObject.transform.root.name);
                //print(overLappedColliders[i].gameObject.name);
                cube.Detouch(_hitDamages);

            }
            if (exlorer) return;

            
        }
        
        this.GetComponent<SphereCollider>().radius *= radiusexplorer;
        exlorer = true;
        for (int i = 0; i < overLappedColliders.Length; i++)
        {

            Rigidbody rb = overLappedColliders[i].attachedRigidbody;

                if (rb)
                {
                    //Debug.LogError(rb.gameObject.name);
                    rb.AddExplosionForce(Force * 5, new Vector2(transform.position.x + 0.5f, transform.position.y - 0.5f), radius);

                }
        }
        
    }
    private void UpdateScore()
    {
        GetScore.invokeCoundChild();
    }
    public void DestroyProjectile()
    {
        
        GetComponent<Projectile>().ProjectileNull();
        Destroy(this.gameObject);
    }
    public void ShowExplorer()
    {
        if (_showExplorer) return;
        _showExplorer = true;
        GameObject EXP = Instantiate(_explosion) as GameObject;
        EXP.transform.position = this.transform.position;
        Destroy(EXP, 0.5f);
    }
   
}
