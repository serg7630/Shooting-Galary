using UnityEngine.SceneManagement;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using UnityEngine.UI;

public class GameMeneger : MonoBehaviour
{
    public static GameMeneger S;


    [SerializeField] GameObject _buttomPauseUI;
    [SerializeField] GameObject _panelGameOver;
    [SerializeField] GameObject _panelFinish;
    [SerializeField] GameObject _panelPause;
    [SerializeField] GameObject MobileJoystic;

    [SerializeField] GameObject _panelVictory;
    [SerializeField] GameObject PauseOnStart;


    [SerializeField] GameObject _buttomExplosion;


    [Header("панель прогресса")]
    public Sprite VictorySprite;
    public Image DefaultImage;
    public Image VictoryImage;
    public float MaxScore;
    [SerializeField] Image _PanelBar;

    //[SerializeField] GameObject SoundBaground;
    [SerializeField] AudioSource BagroundSource;
    [SerializeField] bool MiusicInGame;
    [Header("поле с уровнем при паузе")]
    [SerializeField] TMPro.TextMeshProUGUI LevelName;
    
    int Coins;
    bool FinishActive;
    bool GameOverActive;
    public string ActiveScene;
    [SerializeField] string MaxScene;

    [Header("PlayerPrefs")]
    [SerializeField] int AvailibleLevels;
    string KeyStringLevelsAvaileble = "KeyLevels";
    [SerializeField] int GetLevel;

    [SerializeField] GameObject ButtomyJump;

    [SerializeField] PlayerControl PL;

    [SerializeField] Slinshot Sling;
    


    //[DllImport("__Internal")]
    //private static extern void ShowAds();

   
    void Start()
    {
        SetValueCoins(0);

        if (PL.enabled==false)PL.enabled=true;
       
        _buttomPauseUI = GameObject.Find("ButtonPause");
        //ButtomyJump = GameObject.Find("ButtonJump");
        GetLevel = PlayerPrefs.GetInt(KeyStringLevelsAvaileble);

        _buttomExplosion = GameObject.Find("ButtonExplosion");
        //CoinMeheger.text = Coins.ToString();
        if(S==null)S = this;

        //PauseOnStart = GameObject.Find("PauseOnStart");
        //if (PauseOnStart != null) PauseOnStart.SetActive(false);
        //PauseOnStarts();


        if (StaticValueShowAds.ShowAds == true)
        {
            print("pauseAds");
            PauseOnStarts();

        }
        int ValIndexAds = StaticValueShowAds.ValForAds;
        if (ValIndexAds % 4 == 0)
        {
            PauseOnStarts();
            print("show ok");

        }
        print(SceneManager.GetActiveScene().name);
        if (SceneManager.sceneCountInBuildSettings >= 1) LevelName.text = SceneManager.GetActiveScene().name;
        

    }

    public void SetValueCoins(int ValCoint)
    {
        if (ValCoint >= MaxScore)
        {
            DefaultImage.GetComponent<Image>().sprite = VictorySprite;
            GameFinish();


        }
        //print(ValCoint);
        float coinsBar = ValCoint;
        _PanelBar.fillAmount = coinsBar / MaxScore;
    }

    
    //загрузка с меню уровней
    public void LoadSceneInMenuLevel(string NameScene)
    {
        //int NumLevel = Convert.ToInt32(NameScene);
        SceneManager.LoadScene(NameScene);

    }
    //проигрыш
    public void GameOver()
    {
        if (MobileJoystic.activeInHierarchy == true) MobileJoystic.SetActive(false);
        if (Time.timeScale != 1) Time.timeScale = 1;

        if (FinishActive) return;
        _panelGameOver.SetActive(true);
        GameOverActive = true;
        Slinshot.S.aiming = false;
        Slinshot.S.stopControl();
        Camera.main.GetComponent<Score>().StopTank = true;
        //BagroundSource.Pause();

        if (PlayerControl.S.MobilePlatform == true)
        {
            PlayerControl.S.HideMobilePlatform();
        }
    }
    //рестарт
    public void Restart()
    {

        //StaticValueShowAds.S.PlusShowAdsVal();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    //финиш и победа
    public void GameFinish()
    {
        if (Time.timeScale != 1) Time.timeScale = 1;
        if(FinishActive) return;
        //BagroundSource.Pause();
        Slinshot.S.aiming = false;
        Slinshot.S.stopControl();
        _buttomPauseUI.SetActive(false);
        if (GameOverActive) return;
        _panelFinish.SetActive(true);
        
        FinishActive = true;
        ActiveScene = SceneManager.GetActiveScene().name;

        LevelPlus(ActiveScene);

        //PlayerMove.S.HideMobileJoystic();
    }
    //пауза
    public void Pause()
    {
        string ActiveScene = SceneManager.GetActiveScene().name;
        //LevelName.text = ActiveScene;
        _panelPause.SetActive(true);
        //Debug.LogError(PlayerControl.S.MobilePlatform);
        if (PL.MobilePlatform == true)
        {
            PL.HideMobilePlatform();
        }
        Slinshot.S.aiming = false;
        Slinshot.S.stopControl();
        Time.timeScale = 0f;
        BagroundManeger.S.GamePause = true;
        //_buttomExplosion.SetActive(false);
        _buttomPauseUI.SetActive(false);
        //BagroundSource.Pause();
    }
    //снятие с паузы
    public void continium()
    {
        //BagroundSource.Play();
        _buttomPauseUI.SetActive(true);
        //_buttomExplosion.SetActive(true);
        _panelPause.SetActive(false);
        if (PL.MobilePlatform == true)
        {
            PL.ShoweMobilePlatform();
        }
        Time.timeScale = 1;
        Slinshot.S.startControl();
        Slinshot.S.aiming = true;
        BagroundManeger.S.GamePause = false;
    }
    //выход в меню уровней
    public void loadSceneLevels()
    {
        //LevelPlus(ActiveScene);
        if (Time.timeScale < 1) Time.timeScale = 1;
        SceneManager.LoadScene("LevelsMenu");
    }


    //увеличение доступных уровней при победе
    public void LevelPlus(string ActiveScene)
    {
        getLastLevel();
        AvailibleLevels = GetLevel;
        MaxScene = LevelMenu.MaxScene;
        Debug.Log(MaxScene + "  max scene");
        Debug.Log(ActiveScene + "  active scene");
        Debug.Log(AvailibleLevels + "  доступная scene");
        Debug.Log(GetLevel + "  получаемая scene");
        Debug.Log(LevelMenu.LevelAvaileble + "  LevelMenu.LevelAvaileblee");
        if (ActiveScene == LevelMenu.MaxScene)
        {
            AvailibleLevels++;
            SetLevels();
        }

    }
    public void ContiniumVictory()
    {

    }

    //запуск последнего уровня
    public void StartTheLastLevel()
    {
        int Lev = GetLevel + 1;
        if (Lev == 1)
        {
            Lev = 2;
        }
        if (Lev == SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogError("allScenes");
            Lev--;
        }
        if (Time.timeScale < 1) Time.timeScale = 1;
        StaticValueShowAds.ShowAds = true;
        StaticValueShowAds.ValForAds = 1;

        SceneManager.LoadScene(Lev);
    }
    // получаем последний уровень
    public void getLastLevel()
    {
        GetLevel = PlayerPrefs.GetInt(KeyStringLevelsAvaileble);
        if (GetLevel == 0)
        {
            GetLevel = 1;
        }
    }

    //устанавливаем данные в преферс
    public void SetLevels()
    {
        PlayerPrefs.SetInt(KeyStringLevelsAvaileble, AvailibleLevels);
        PlayerPrefs.Save();
    }
    //сброс преферса только для теста
    public void PlayerPreferReset()
    {
        AvailibleLevels = 1;
        PlayerPrefs.SetInt(KeyStringLevelsAvaileble, AvailibleLevels);
        PlayerPrefs.Save();
        GetLevel = 0;
    }
    //public void CoinMeneger()
    //{
    //    Coins++;
    //    CoinMeheger.text = Coins.ToString();
    //}


    public void PauseOnStarts()
    {
        //if (SceneManager.GetActiveScene().buildIndex == 0)
        //{
        //    return;
        //}
        
        Time.timeScale = 0f;
        //ShowAds();
        //ButtomyJump.SetActive(false);
        Debug.Log("tryPause");
        if (PauseOnStart == null)
        {
            Invoke("PauseOnStarts", 0.5f);
            return;
        }
        if (Sling==null)
        {
            Invoke("PauseOnStarts", 0.5f);
            return;
        }
        Debug.Log("pauseOnStartActive");
        PauseOnStart.SetActive(true);
        Sling.aiming = false;
        Sling.stopControl();
        //Slinshot.S.aiming = false;
        //Slinshot.S.stopControl();

        _buttomPauseUI.SetActive(false);

        //StaticValueShowAds.ShowAds = false;

    }
    public void ExitPauseOnStarts()
    {
        PauseOnStart.SetActive(false);
        Time.timeScale = 1f;
        
        _buttomPauseUI.SetActive(true);
        Slinshot.S.aiming = true;
        Slinshot.S.startControl();
    }


    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            return;
        }
        //if (PauseOnStart.activeInHierarchy) Time.timeScale = 0;
    }
}
