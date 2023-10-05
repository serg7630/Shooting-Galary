using UnityEngine.SceneManagement;
using UnityEngine;

public class buttomEnterLevel : MonoBehaviour
{
    
    void Start()
    {
        
    }

   public void EnterLevel()
    {   
        string nameButtom = gameObject.name;
        if (Time.timeScale != 1) Time.timeScale = 1;
        
        StaticValueShowAds.ShowAds = true;
        StaticValueShowAds.ValForAds = 1;

        print(nameButtom);
        SceneManager.LoadScene(nameButtom);
        
    }
   
    void Update()
    {
        
    }
}
