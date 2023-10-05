using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{

    [SerializeField] int ValueAddCoins = 1;
    public bool One_hit=false;
    private Score _score;
    [SerializeField] private bool valueCoin;
    private bool _detouch;
    [SerializeField] int hit_numbers=1;
    private int _hitDamages;
    [SerializeField] bool _move_Object;
    [SerializeField] private bool _goal_tank;
    [SerializeField] private bool ParentCastle;

    public int Id { get; set; }

    private void Start()
    {
        _score = Camera.main.GetComponent<Score>();
    }
    public void Detouch(int HitDamage)
    {
        //
        _hitDamages = HitDamage;
        if (_detouch||One_hit)
        {
            return;
        }
        
        if (!hitCube()) return;

       Destroy(this.gameObject, 8f);
        //Debug.LogError(transform.root.gameObject.name);
        if (transform.root.gameObject.TryGetComponent<MooveTank>(out MooveTank MT))
        {
            if (_goal_tank)
            {
                //MT.Speed = 0;
            }
            else
            {
                //MT.Speed -= 50f;
            }
        }
        
        GetComponentInParent<Entity>().DetouchCube(this,null);
        AddCoin();

        float posZ = transform.position.z;
        Vector3 posCube = new Vector3(transform.position.x, transform.position.y, posZ - 1f);
        transform.position = posCube;
        
        setLayer();
    }
    void setLayer()
    {
        gameObject.layer = 8;
    }
    bool hitCube()
    {
        //print("hitCube");
        hit_numbers-=_hitDamages;
        if (hit_numbers <= 0)
        {
            _detouch = true;
            return true;
        }

        return false;
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (valueCoin) return;
    //    valueCoin = true;
    //    _score.AddCoins();
    //}
  public  void AddCoin()
    {
        if (!valueCoin) return;
        //Debug.LogError("addcoin");
        _score.AddCoins(ValueAddCoins);
    }

}
