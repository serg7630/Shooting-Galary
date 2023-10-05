using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBSleep : MonoBehaviour
{
    Rigidbody RB;
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        if (RB != null) RB.Sleep();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject go = collision.gameObject;
        if (go.tag == "Project") RB.WakeUp();
    }
}
