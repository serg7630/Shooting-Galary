using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int Health;
    public int NumberLives;
    public Image[] Lives; 
    void Start()
    {
        healtManager();
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    public void HealtIncrement()
    {
        NumberLives++;
        healtManager();
    }
    public void HealthDicrement()
    {
        NumberLives--;
        if(NumberLives<=0)GameMeneger.S.GameOver();
        healtManager();
    }
    void healtManager()
    {
        for (int i = 0; i < Lives.Length; i++)
        {
            if (i < NumberLives)
            {
                Lives[i].enabled = true;
            }
            else
            {
                Lives[i].enabled = false;
            }
        }
        //Invoke("healtManager", 0.5f);
    }
}
