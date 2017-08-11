using UnityEngine;
using System.Collections;
using Assets;

public class GameInit : MonoBehaviour
{
    public GameObject[] _bricks;
    public GameObject[] _visitors;
    public GameObject[] _stores;
    public GameObject[] _empties;
    public GameObject[] _walls;
    // Use this for initialization
    void Start()
    {
        Global global = Global.getInstance();
        ToaDoGach.getInstance().reload();
        Bricks.getInstance().reload();
        ToaDoGach tdg = ToaDoGach.getInstance();
        Bricks bricks = Bricks.getInstance();
        this.AddGlobalModel();
        GameObject cams = GameObject.Find("Cams");
        global._cams = (Camera[])GameObject.Find("Cams").transform.GetComponentsInChildren<Camera>();
        for(int i = 0; i < global._cams.Length; i++)
        {
            Camera cam = global._cams[i];
            int h = Mathf.Max(tdg.getN(), tdg.getM());
            Debug.Log(tdg.getN());
            Debug.Log(tdg.getM());
            Vector2 centerPoint = new Vector2(tdg.getN() / 2, tdg.getM() / 2);
            Vector3 lookAtVector = tdg.MaTran2ToaDo(centerPoint);
            Vector3 p = new Vector3(0, 0, 0);
            if (cam.name.Equals("T_Cam"))
            {
                p = new Vector3(lookAtVector.x, h, lookAtVector.z);
                global._curCamIdx = i;
                cam.enabled = true;
            }
            else
            {
                if (cam.name.Equals("TL_Cam"))
                {
                    p = tdg.MaTran2ToaDo(new Vector3(0, 0));
                    p.y = h / 3;
                }
                else if (cam.name.Equals("BR_Cam"))
                {
                    p = tdg.MaTran2ToaDo(new Vector3(tdg.getN() - 1, tdg.getM() - 1));
                    p.y = h / 3;
                }
                cam.enabled = false;
            }
            cam.transform.position = p;
            cam.transform.LookAt(lookAtVector);
        }
        createMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void AddGlobalModel()
    {
        Global global = Global.getInstance();
        global._bricks = _bricks;
        global._visitors = _visitors;
        global._stores = _stores;
        global._empties = _empties;
        global._walls = _walls;
    }

    private void createMap()
    {
        ToaDoGach tdg = ToaDoGach.getInstance();
        for (int i = 0; i < tdg.getN(); i++)
        {
            for (int j = 0; j < tdg.getM(); j++)
            {
                Vector2 p = new Vector2(i, j);
                int idx = tdg.getStoreIdx(p);
                GameObject clone = Global.getInstance().getClone(idx, 
                    tdg.MaTran2ToaDo(p), new Quaternion());
                if (idx == 0)
                    Bricks.getInstance().set(p, clone);
            }
        }
    }
}

