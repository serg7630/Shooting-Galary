using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stop_Tank : MonoBehaviour
{
    [SerializeField] MooveTank MT;
    
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //print("STOP");
        MT.TankTurnedOver = true;
    }
    private void OnTriggerExit(Collider other)
    {
        //print("drive");
        MT.TankTurnedOver = false;
    }

}
