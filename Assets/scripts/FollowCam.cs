using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public GameObject POI;
    float easing = 0.1f;
    float camZ;
    [SerializeField] float camPosZ;
    Saw saw;
    Vector3 distance;
   [SerializeField] private GameObject Player;

    void Awake()
    {
        camZ = transform.position.z;

    }

   
    void FixedUpdate()
    {
        
        if (POI == null)
        {
            Invoke("projectNull", 1f);
            //
        }
        else
        {
            distance = POI.transform.position;
            if (POI.tag=="Project")
            {
                if (POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    Invoke("CameraNull", 1f);
                    return;
                  
                }
                
            }
        }
        //Vector3 distance = POI.transform.position;
        distance = Vector3.Lerp(transform.position, distance, easing);
        distance.x = Mathf.Max(902, distance.x);
        distance.y = Mathf.Max(22, distance.y);
        float posProjY = distance.y-7;
        camPosZ = posProjY;
        float positionZ= camZ - camPosZ;
        distance.z = positionZ;
        

        transform.position = distance;
        
    }
    void projectNull()
    {
        distance = Player.transform.position;
    }
    void CameraNull()
    {
        
        saw = POI.GetComponent<Saw>();
        saw.explosion(saw.radiusexplorer);

        POI = null;
        saw.DestroyProjectile();
    }
}
