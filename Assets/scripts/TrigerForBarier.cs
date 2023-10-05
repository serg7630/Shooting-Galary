using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerForBarier : MonoBehaviour
{
   private MooveTank MT;
    void Start()
    {
        MT = transform.root.GetComponent<MooveTank>();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        if (go.tag=="Player")
        {
            int RND = Random.Range(2, 6);
            reversspeed();
            Invoke("reversspeed", RND);
        }
        
    }
    void reversspeed()
    {
        MT.reversSpeed();
    }
    void Update()
    {
        
    }
}
