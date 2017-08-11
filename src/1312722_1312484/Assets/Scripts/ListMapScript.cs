using UnityEngine;
using System.Collections;
using System.IO;
using Assets;
using UnityEngine.UI;

public class ListMapScript : MonoBehaviour {
    private Dropdown _listMap;
    string _homeDir;
    private DirectoryInfo[] _listDirs;
    private int _curIdx;
	// Use this for initialization
	void Start () {
        _homeDir = "./maps";
        _listDirs = Global.ListFolders(_homeDir);
        _listMap = GetComponentInParent<Dropdown>();
        System.Collections.Generic.List<string> _listDirStrs = new System.Collections.Generic.List<string>();
        foreach (DirectoryInfo info in _listDirs)
        {
            _listDirStrs.Add(info.Name);
        }
        _listMap.AddOptions(_listDirStrs);
        this.ChooseMap(0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChooseMap(int idx)
    {
        Global glb = Global.getInstance();
        _curIdx = idx;
        glb._mapDir = this.getDir(_homeDir, _listDirs[idx].Name, "map.txt");
        glb._attractiveLevelDir = this.getDir(_homeDir, _listDirs[idx].Name, "attractiveLevel.txt");
    }

    private string getDir(string homeDir, string dir, string fileName)
    {
        return homeDir + "/" + dir + "/" + fileName;
    }
}
