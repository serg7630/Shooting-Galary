using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerCastle : MonoBehaviour
{
    [SerializeField] GameObject _castleThis;
    [SerializeField] GameObject _parentForCube;
    [SerializeField] Vector3 _castlePosition;

    public GameObject[] Castles;

    [SerializeField] GameObject[] castleForDestroy;
    void Start()
    {

        _castleThis = transform.GetChild(0).gameObject;
        _castlePosition = _castleThis.transform.position;
        _parentForCube = GameObject.Find("ParentForCube");
        ArreyCastle(_castleThis);
    }

   
    public void detouhCastle()
    {
        int a = 0;
        GameObject[] allChildren = new GameObject[_castleThis.transform.childCount];

        //Find all child obj and store to that array
        foreach (Transform child in _castleThis.transform)
        {
            allChildren[a] = child.gameObject;
            a += 1;
        }

        //Now destroy them
        foreach (GameObject child in allChildren)
        {
            //AddCoins();
            child.GetComponentInParent<Entity>().DetouchCube(child.GetComponent<Cube>(), _parentForCube);
        }
    }

    void ArreyCastle(GameObject castle)
    {
        castleForDestroy = new GameObject[_castleThis.transform.childCount];
        int a = 0;
        foreach (Transform child in _castleThis.transform)
        {
            castleForDestroy[a] = child.gameObject;
            a += 1;
        }
    }

    public void invocationDeastroy(float timeDes)
    {
        Invoke("DestroyCastle", timeDes);
    }
    private void DestroyCastle(/*GameObject[] DesCastl=*/ )
    {

        foreach (GameObject castleBlok in castleForDestroy)
        {
            Destroy(castleBlok);
        }
        castleForDestroy = new GameObject[0];
        Destroy(_castleThis);
        _castleThis = null;
        MakeCastle();
    }
    void Update()
    {
        
    }
    void MakeCastle()
    {
        int RND = Random.Range(0, Castles.Length);
        _castleThis = Instantiate(Castles[RND],this.gameObject.transform) as GameObject;
        _castlePosition.y =0f;
        _castleThis.transform.position = _castlePosition;
        Camera.main.GetComponent<Score>().AddCastle(_castleThis);
        ArreyCastle(_castleThis);
    }
}
