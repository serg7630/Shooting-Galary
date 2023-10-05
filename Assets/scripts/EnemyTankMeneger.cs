using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankMeneger : MonoBehaviour
{
    [SerializeField] private Score _score;
    void Start()
    {
        _score.AddTank(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
