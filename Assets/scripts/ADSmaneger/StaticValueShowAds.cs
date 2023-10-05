using UnityEngine.SceneManagement;
using UnityEngine;

public class StaticValueShowAds : MonoBehaviour
{
    static public StaticValueShowAds S;
    static public bool ShowAds;
    static public int ValForAds;
    //public GameObject PanelRevu;

    [Header("количесвто монет")]
    string CoinsPlayer = "Coins";
    [SerializeField] int _coins;
    void Start()
    {
        if(S==null) S = this;
        _coins= PlayerPrefs.GetInt(CoinsPlayer);
        Debug.LogError("startScriptADS");
    }


    public int Coins
    {
        get { return _coins; }
        set { _coins = value; }
    }
   public void PlusShowAdsVal()
    {
        ValForAds++;
    }
   public void loadScenLevels()
    {
        SceneManager.LoadScene("LevelsMenu");
    }
    //public void ShowReveu()
    //{
    //    PanelRevu.SetActive(true);
    //}
    //public void HideReveu()
    //{
    //    PanelRevu.SetActive(false);
    //}
    void Update()
    {
        
    }
   public void setCoin(int coins)
    {
        PlayerPrefs.SetInt(CoinsPlayer, coins);
        //Debug.LogError(coins+ " playerPrefer");
    }
    public int getCoin()
    {
       return Coins=PlayerPrefs.GetInt(CoinsPlayer);

    }
    public void resetCoins()
    {
        PlayerPrefs.SetInt(CoinsPlayer, 1);
        getCoin();
        //Debug.LogError(1 + "     playerPrefer");
    }
}
