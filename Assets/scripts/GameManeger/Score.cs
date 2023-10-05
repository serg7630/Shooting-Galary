using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public TMP_Text CountTanksText;

    public TMP_Text MainCoins;

    public TMP_Text CountCastle;
    [SerializeField] int _fullCountCastle;
    [SerializeField]ManagerCastle MC;


    [SerializeField] GameObject _enemyCastle;
    int Coins;
    [SerializeField] List< GameObject>  _tanks=new List<GameObject>();
    [SerializeField] GameObject[] _countTanks;
    public GameObject go;
    private SpawnEnemy SE;
    public bool StopTank=false;

    bool deadCastle = false;

    public bool there_are_tanks=false;
    public bool there_are_Castle = false;
    void Start()
    {
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        if (activeScene == 0)
        {
            Debug.LogError("mainMenu");
        }
        //Coins = StaticValueShowAds.S.getCoin();
        if (there_are_Castle)
        {
            _fullCountCastle = CountCastleBric();
            print(_fullCountCastle + "  full count bric start");
        }
        
        SetCount();
        CountChild();
        ShowCoin();
        /*GameObject go = GameObject.Find("castleEnemy")*/;
        SE = go.GetComponent<SpawnEnemy>();
    }

    void SetCount()
    {
        int count = _tanks.Count;
        if (!there_are_tanks) return;
        CountTanksText.text = count.ToString();

    }
    void Update()
    {
        
    }

    public void CountChild()
    {
        //print("CountChild");
        if (!there_are_tanks) return;
        for (int i = 0; i < _tanks.Count; i++)
        {

            if (_tanks[i].transform.hierarchyCount < 60)
            {
                if (_tanks[i].TryGetComponent<MooveTank>(out MooveTank MT)) MT.enabled = false;
                int a = 0;

                //обходим весь обьект и открепляем кубы от сущности
                GameObject[] allChildren = new GameObject[_tanks[i].transform.childCount];

                //Find all child obj and store to that array
                foreach (Transform child in _tanks[i].transform)
                {
                    allChildren[a] = child.gameObject;
                    a += 1;
                }

                //Now destroy them
                foreach (GameObject child in allChildren)
                {
                    //AddCoins();
                    child.GetComponentInParent<Entity>().DetouchCube(child.GetComponent<Cube>(),null);
                }
                print("setCount");
                _tanks.RemoveAt(i);
                SetCount();
            }
        }

        getCountCastle();
        
    }
    public void getCountCastle()        //выводит процент от замка
    {   
        int countBric=CountCastleBric();
        //print(countBric + "  count bric");
        //print(_fullCountCastle + "  full count bric");
        int countBricPercent =    100* countBric/_fullCountCastle;
        //print(countBricPercent);
        //CountCastle.text = countBricPercent.ToString()+" %";
        if (deadCastle) return;
        if (countBricPercent <= 20)
        {

            deadCastle = true;
            AddCoins(_enemyCastle.GetComponent<ValueCastl>().valueCastl);
            //ManagerCastle MC = _enemyCastle.GetComponent<ManagerCastle>();
            _enemyCastle = null;
            MC.detouhCastle();
            MC.invocationDeastroy(5f);

            
        }

    }
    public void AddTank(GameObject tank)
    {
        _tanks.Add(tank);
        SetCount();
    }
    public void removeTank(GameObject tank)
    {
        _tanks.Remove(tank);
        SetCount();
    }
    public void AddCoins(int AddCoin)
    {
        Coins+=AddCoin;
        GameMeneger.S.SetValueCoins(Coins);
        ShowCoin();
    }
    public void ShowCoin()
    {
        //StaticValueShowAds.S.setCoin(Coins);
        ////Debug.LogError("setCoin");
        //MainCoins.text = Coins.ToString();

    }

    public int getCountChildren()
    {
        return _tanks.Count;
    }
    public void invokeCoundChild()
    {
        Invoke("CountChild", 0.5f);
    }


    int CountCastleBric()       //считает количесвто кирпичей в замке
    {
        if (_enemyCastle == null) return 0;
        //print(_enemyCastle.transform.hierarchyCount);
        return _enemyCastle.transform.hierarchyCount;
        
    }
    public void AddCastle(GameObject castle)
    {
        _enemyCastle = castle;
        _fullCountCastle = CountCastleBric();
        getCountCastle();
        deadCastle = false;
    }
}
