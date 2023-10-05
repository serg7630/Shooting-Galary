using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MooveTank : MonoBehaviour
{
   [SerializeField] private float _speed ;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] float _maxMagnitude=6f;
    public float maxSpeed=1700;
    public float minSpeed=1500;

    public bool TankTurnedOver = false;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        float RangSpeed = Random.Range( minSpeed, maxSpeed);
        _speed = RangSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        

        
          
    }
    private void FixedUpdate()
    {
        if (TankTurnedOver) return;
        
        if (_rb.velocity.magnitude > _maxMagnitude)
        {
            //print(_rb.velocity.magnitude);
            _rb.velocity = _rb.velocity.normalized * _maxMagnitude;
        }
            Move(); 
    }
    private void Move()
    {

        
        
            float moveHorizontal = Input.GetAxis("Horizontal");

            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(1, 0.0f, moveVertical);

            _rb.AddForce(movement * -_speed);
        

    }
    public float Speed
    {
        get
        {
            return _speed;   
        }
        set
        {
            _speed = value;
            //if (_speed <= 0f)
            //{
            //    _speed = 0f;
            //}
        }
    }
   public void reversSpeed()
    {
        _speed *= -1;
    }
   
}
