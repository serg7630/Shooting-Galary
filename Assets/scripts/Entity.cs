using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Rigidbody))]

public class Entity : MonoBehaviour
{
    private Rigidbody _rb;
    private int[,] _cubesInfo;
    private Vector3 _cubesInfoStartPosition;
    private Cube[] _cubes;

    private GameObject main_go;

    [SerializeField] GameObject ParentCubeTank;

    private void Awake()
    {
        main_go = this.gameObject;
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezePositionZ |
            RigidbodyConstraints.FreezeRotationX /*|
                                                  * RigidbodyConstraints.FreezeRotationZ*/ |
                                                  RigidbodyConstraints.FreezeRotationY;
        _rb.mass = transform.childCount;
        CollectCubes();
        RecalculateCubes();
    }

    private void CollectCubes()
    {
        Vector3 min = Vector3.one * float.MaxValue;
        Vector3 max = Vector3.one * float.MinValue;

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            min = Vector3.Min(min, child.localPosition);
            max = Vector3.Max(max, child.localPosition);
        }

        Vector2Int delta = Vector2Int.RoundToInt(max - min);
        _cubesInfo = new int[delta.x + 1, delta.y + 1];
        _cubesInfoStartPosition = min;
        _cubes = GetComponentsInChildren<Cube>();

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Vector2Int grid = GridPosition(child.localPosition);
            _cubesInfo[grid.x, grid.y] = i + 1;
            _cubes[i].Id = i + 1;
        }
    }

    private void RecalculateCubes()
    {
        List<int> freeCubeIds = new List<int>();
        for (int i = 0; i < _cubes.Length; i++)
        {
            if (_cubes[i] != null)
            {
                freeCubeIds.Add(_cubes[i].Id);
            }
        }

        if (freeCubeIds.Count == 0)
        {
            Destroy(gameObject);
            return;
        }

        List<CubeGroup> groups = new List<CubeGroup>();
        int currentGroup = 0;

        while (freeCubeIds.Count > 0)
        {
            groups.Add(new CubeGroup());
            int id = freeCubeIds[0];
            groups[currentGroup].Cubes.Add(id);
            freeCubeIds.Remove(id);
            checkCube(id);
            currentGroup++;

            void checkCube(int id)
            {
                Vector2Int gridPosition = GridPosition(_cubes[id - 1].transform.localPosition);

                checkNeighbor(Vector2Int.up);
                checkNeighbor(Vector2Int.right);
                checkNeighbor(Vector2Int.down);
                checkNeighbor(Vector2Int.left);

                void checkNeighbor(Vector2Int direction)
                {
                    int id = GetNeighbor(gridPosition, direction);
                    if (freeCubeIds.Remove(id))
                    {
                        groups[currentGroup].Cubes.Add(id);
                        checkCube(id);
                    }
                }
            }
        }

        if (groups.Count < 6)
            return;

        for (int i = 1; i < groups.Count; i++)
        {
            GameObject entity = new GameObject("Entity");
            var firstCube = _cubes[groups[i].Cubes[0] - 1].transform;
            entity.transform.SetPositionAndRotation(firstCube.position, firstCube.rotation);

            foreach (int id in groups[i].Cubes)
            {
                _cubes[id - 1].transform.parent = entity.transform;
            }
            //Debug.LogError(main_go.gameObject.name);
            entity.AddComponent<Entity>();

            //if (entity.transform.childCount >= 20)
            //{ 


            //    entity.transform.parent = main_go.transform;
            //    //Debug.LogError(entity.transform.root.tag);
            //    if (entity.transform.root.tag == "EnemyTank")
            //    {

            //        MooveTank MT = entity.AddComponent<MooveTank>();
                   
            //    }


            //}
        }


        CollectCubes();
    }


    public void DetouchCube(Cube cube, GameObject parent)
    {
        if (parent == null) parent = ParentCubeTank;
        Vector2Int grid = GridPosition(cube.transform.localPosition);
        _cubesInfo[grid.x, grid.y] = 0;
        _cubes[cube.Id - 1] = null;

        
        var rb = cube.gameObject.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
        

        RecalculateCubes();
        cube.transform.SetParent (/*parent.transform*/null);
    }



    private Vector2Int GridPosition(Vector3 localPosition)
    {
        return Vector2Int.RoundToInt(localPosition - _cubesInfoStartPosition);
    }

    private int GetNeighbor(Vector2Int position, Vector2Int direction)
    {
        Vector2Int gridPosition = position + direction;
        if (gridPosition.x < 0 || gridPosition.x >= _cubesInfo.GetLength(0)
            || gridPosition.y < 0 || gridPosition.y >= _cubesInfo.GetLength(1))
            return 0;

        return _cubesInfo[gridPosition.x, gridPosition.y];
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;

        Gizmos.matrix = transform.localToWorldMatrix;
        for (int x = 0; x < _cubesInfo.GetLength(0); x++)
        {
            for (int y = 0; y < _cubesInfo.GetLength(1); y++)
            {
                Vector3 position = _cubesInfoStartPosition + new Vector3(x, y, 0);
                if (_cubesInfo[x, y] == 0)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawSphere(position, 0.1f);
                }
                else
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(position, 0.2f);
                }

            }
        }
    }

    [ContextMenu("Randomize Position Z")]
    private void RandomizePositionZ()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Vector3 localPosition = child.localPosition;
            localPosition.z = Random.Range(-0.15f, 0.15f);
            child.localPosition = localPosition;
        }
    }
}

public class CubeGroup
{
    public List<int> Cubes = new List<int>();
}

//public class Entity : MonoBehaviour
//{
//    private Rigidbody _rb;
//    [SerializeField] public int[,] _infoCubes;
//    private Vector3 _cubenInfoStartPosition;
//    private Cube[] _cubes;
//    private GameObject main_go;


//    private void Awake()
//    {
//        main_go = this.gameObject;
//        //Debug.LogError(main_go.name);
//        _rb = GetComponent<Rigidbody>();
//        _rb.constraints = RigidbodyConstraints.FreezeRotationY |
//            RigidbodyConstraints.FreezeRotationX /*|*/
//            //RigidbodyConstraints.FreezeRotationZ |
//            /*RigidbodyConstraints.FreezePositionZ*/;
//        _rb.mass = transform.childCount;
//        CollectCubes();
//        RecalculatorCube();
//    }

//    void CollectCubes()
//    {
//        Vector3 min = Vector3.one * float.MaxValue;
//        Vector3 max = Vector3.one * float.MinValue;
//        for (int i = 0; i < transform.childCount; i++)
//        {
//            Transform child = transform.GetChild(i);
//            min = Vector3.Min(min, child.localPosition);
//            max = Vector3.Max(max, child.localPosition);
//        }

//        //print(max - min);
//        Vector2Int delta = Vector2Int.RoundToInt(max - min);
//        //print(delta);
//         _infoCubes = new int[delta.x + 1, delta.y + 1];
//        _cubenInfoStartPosition = min;
//        _cubes = GetComponentsInChildren<Cube>();

//        for (int i = 0; i < transform.childCount; i++)
//        {
//            Transform Child = transform.GetChild(i);
//            Vector2Int grid = gridPosition(Child.localPosition);
//            //print(grid);
//            _infoCubes[grid.x, grid.y] = i + 1;
//            _cubes[i].ID = i + 1;
//        }


//    }

//    private int GetNeighbor(Vector2Int position, Vector2Int directions)
//    {
//        Vector2Int gridposition = position + directions;
//        if (gridposition.x<0||gridposition.x>=_infoCubes.GetLength(0)
//            || gridposition.y<0||gridposition.y>=_infoCubes.GetLength(1))
//        {
//            return 0;
//        }
//        return _infoCubes[gridposition.x, gridposition.y];
//    }
//    public void RecalculatorCube()
//    {
//        List<int> FreeCubes = new List<int>();
//        for (int i = 0; i < _cubes.Length; i++)
//        {
//            if (_cubes[i]!=null)
//            {
//                FreeCubes.Add(_cubes[i].ID);

//            }
//        }
//        if (FreeCubes.Count==0)
//        {
//            Destroy(gameObject);
//            return;
//        }
//        List<CubeGroup> cubesGroup = new List<CubeGroup>();
//        int currentGroup = 0;

//        while (FreeCubes.Count>0)
//        {
//            cubesGroup.Add(new CubeGroup());
//            int id = FreeCubes[0];
//            cubesGroup[currentGroup].Cubes.Add(id);
//            FreeCubes.Remove(id);
//            ChekCube(id);
//            currentGroup++;

//            void ChekCube(int id)
//            {
//                Vector2Int gridposition = gridPosition(_cubes[id - 1].transform.localPosition);

//                ChekNeighbor(Vector2Int.up);
//                ChekNeighbor(Vector2Int.right);
//                ChekNeighbor(Vector2Int.left);
//                ChekNeighbor(Vector2Int.down);

//                void ChekNeighbor(Vector2Int directions)
//                {
//                    int id = GetNeighbor(gridposition, directions);
//                    if (FreeCubes.Remove(id))
//                    {
//                        cubesGroup[currentGroup].Cubes.Add(id);
//                        ChekCube(id);
//                    }
//                }
//            }
//        }
//        if (cubesGroup.Count < 2)
//            return;

//        for (int i = 0; i < cubesGroup.Count; i++)
//        {
//            Debug.LogError("Dead Tank");
//            GameObject entity = new GameObject("Entity");
//            var firstCube = _cubes[cubesGroup[i].Cubes[0] - 1].transform;
//            entity.transform.SetPositionAndRotation(firstCube.position, firstCube.rotation);
//            foreach (int id in cubesGroup[i].Cubes)
//            {
//                _cubes[id - 1].transform.parent = entity.transform;
//            }
//            entity.AddComponent<Entity>();
//            entity.tag = "EnemyTank";
//            //Debug.LogError(entity.transform.childCount);

//            if (entity.transform.childCount >= 60)
//            {


//                entity.transform.parent = main_go.transform;
//                //Debug.LogError(entity.transform.root.tag);
//                if (entity.transform.root.tag == "EnemyTank")
//                {

//                    MooveTank MT = entity.AddComponent<MooveTank>();
//                    //Debug.LogError(main_go.gameObject.name);
//                }


//            }




//        }
//        CollectCubes();
//    }

//    public void DetoucheCube(Cube cube)
//    {
//        Vector2Int grid = gridPosition(cube.transform.localPosition);
//        _infoCubes[grid.x, grid.y] = 0;
//        _cubes[cube.ID - 1] = null;

//        cube.transform.parent = null;
//        cube.GetComponent<Cube>().AddCoin();
//        var RB = cube.gameObject.AddComponent<Rigidbody>();
//        //RB.constraints = RigidbodyConstraints.FreezePositionZ;
//        RecalculatorCube();
//    }
//    private Vector2Int gridPosition(Vector3 localPosition)
//    {
//        return Vector2Int.RoundToInt(localPosition - _cubenInfoStartPosition);
//    }
//    private void FixedUpdate()
//    {
//        Vector3 pos = transform.position;
//        //print(pos.z);
//        //pos.z = 0;
//        //transform.position = pos;
//    }
//}
//class CubeGroup
//{
//    public List<int> Cubes = new List<int>();

//}

