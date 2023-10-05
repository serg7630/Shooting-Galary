using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankManeger : MonoBehaviour
{
    [SerializeField] Score _score;
    [SerializeField] bool HealtDicriment=false;
    void Start()
    {
        _score = Camera.main.GetComponent<Score>();
        _score.AddTank(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "dicrementHaelt"&&!HealtDicriment) {
            HealtDicriment = true;
            Camera.main.GetComponent<HealthManager>().HealthDicrement();
            //Debug.LogError("dicrementHaelt");
            RemoveTank();
            Destroy(this.gameObject,2f);
        }
    }
    private void RemoveTank()
    {
        _score.removeTank(this.gameObject);
    }
}
