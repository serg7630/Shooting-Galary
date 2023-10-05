using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public static LevelMenu S;

   public static  int LevelAvaileble;

    public Sprite SpriteActiveScene;
    public Sprite SpriteDontActiveScene;

    [SerializeField]  int AvailibleLevels;

    string KeyStringLevelsAvaileble = "KeyLevels";
    [SerializeField] int GetLevel;

    public static string MaxScene;
    void Start()
    {

        GetLevel = PlayerPrefs.GetInt(KeyStringLevelsAvaileble);
        //GetLevel = 1;
        AvailibleLevels = 1;
        if (AvailibleLevels < GetLevel)
        {
            AvailibleLevels = GetLevel;
        }
        // установка кнопок с уровнями
        LevelAvaileble = AvailibleLevels;
        for (int i = 0; i < transform.childCount; i++)
        {
            int levelButtom = i + 1;
            string NameButtom = (i+1).ToString();
            transform.GetChild(i).gameObject.name = "Level " + levelButtom/* .ToString() */;
            transform.GetChild(i).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = NameButtom;


            if (i<LevelAvaileble)
            {
                transform.GetChild(i).GetComponent<Button>().interactable = true;
                transform.GetChild(i).GetComponent<Image>().sprite = SpriteActiveScene;
                MaxScene = transform.GetChild(i).gameObject.name;
                //print(transform.GetChild(i));
            }
            else
            {
                transform.GetChild(i).GetComponent<Button>().interactable = false;
                transform.GetChild(i).GetComponent<Image>().sprite = SpriteDontActiveScene;
                //print(transform.GetChild(i));

                //print(i);
            }
        }
        S = this;
    }

    
    void Update()
    {
        
    }
    // увеличение количества доступных уровней
    public void LevelPlus(string ActiveScene)
    {
        if (ActiveScene == MaxScene)
        {
            AvailibleLevels++;
            SetLevels();
        }

    }

   //получем доступные уровни
    public void SetLevels()
    {
        PlayerPrefs.SetInt(KeyStringLevelsAvaileble, AvailibleLevels);
    }
    //выход в главное меню
    public void BackInMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void resetScene()
    {
        PlayerPrefs.SetInt(KeyStringLevelsAvaileble, 1);
    }
}
