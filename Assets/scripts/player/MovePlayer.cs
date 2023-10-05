using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
   [SerializeField] private Rigidbody _rB;
    [SerializeField] private float _speed;
    void Start()
    {
        _rB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(Horizontal, 0.0f, 0.0f);

        _rB.AddForce(movement * _speed);
    }
    void Update()
    {
        
    }
}
