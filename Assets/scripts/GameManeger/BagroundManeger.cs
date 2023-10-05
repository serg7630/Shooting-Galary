using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagroundManeger : MonoBehaviour
{
  public static  BagroundManeger S;
    public bool GamePause = false;
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    private void OnApplicationFocus(bool focus)
    {
        //if (focus)
        //{
        //    if (GamePause == true)
        //    {
        //        return;
        //    }
        //    Time.timeScale = 1;
        //    Debug.Log("focus true");

        //}
        //else
        //{
        //    Time.timeScale = 0;
        //    Debug.Log("focus false");
        //}
    }
    void Start()
    {
        S = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
