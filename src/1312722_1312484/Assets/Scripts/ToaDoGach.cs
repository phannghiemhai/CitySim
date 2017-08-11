using UnityEngine;
using System.Collections;
using System;
using Assets;

public class ToaDoGach : MMatrix
{
    public static Vector2[] dir = {new Vector2(1, 0),
                                        new Vector2(-1, 0),
                                        new Vector2(0, 1),
                                        new Vector2(0, -1),
                                        new Vector2(-1, -1),
                                        new Vector2(-1, 1),
                                        new Vector2(1, 1),
                                        new Vector2(1, -1)};
    private static ToaDoGach _instance = null;
    private int _n; //width
    private int _m; //hieght
    private float[][] _d; //actractive level
    private int[][] ToaDo;
    private Vector3 ToaDoVienGach_0_0 = new Vector3(0.5f, 0.0f, 0.5f);
    private ArrayList _exitPositions;
    private static readonly object syncLock = new object();

    private ToaDoGach()
    {
        //KhoiTaoToaDo();
        ToaDo = Global.readMatrixIntFromFile(Global.getInstance()._mapDir);
        _d = Global.readMatrixFloatFromFile(Global.getInstance()._attractiveLevelDir);
        _n = ToaDo.Length;
        _m = ToaDo[0].Length;
        _exitPositions = new ArrayList();
        findExitPosition();
    }

    public void reload()
    {
        lock (syncLock)
        {
            _instance = new ToaDoGach();
        }
    }

    private void findExitPosition()
    {
        for(int i = 0; i < _n; i++)
        {
            Vector2 e1 = new Vector2(i, 0);
            if (this.isEmpty(e1))
                _exitPositions.Add(e1);
            Vector2 e2 = new Vector2(i, _m - 1);
            if (this.isEmpty(e2))
                _exitPositions.Add(e2);
        }

        for (int j = 0; j < _m; j++)
        {
            Vector2 e1 = new Vector2(0, j);
            if (this.isEmpty(e1))
                _exitPositions.Add(e1);
            Vector2 e2 = new Vector2(_n - 1, j);
            if (this.isEmpty(e2))
                _exitPositions.Add(e2);
        }
    }

    public static ToaDoGach getInstance()
    {
        lock(syncLock)
        {
            if (_instance == null)
            {
                _instance = new ToaDoGach();
            }
        }
        return _instance;
    }

    private void KhoiTaoToaDo()
    {
        int idx = 0;
        ToaDo = new int[49][];
        for (int i = 0; i < 49; i++)
        {
            ToaDo[i] = new int[50];
            for (int j = 0; j < 50; j++)
            {
                ToaDo[i][j] = 0;
            }
        }

        //Danh dau gian hang ben trai
        for (int i = 0; i < 45; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                ToaDo[i][j] = idx / 5 + 1;
            }
            for (int j = 3; j < 5; j++)
            {
                if (i % 5 != 2)
                    ToaDo[i][j] = idx / 5 + 1;
            }
            idx++;
        }

        //Danh dau gian hang ben phai
        for (int i = 0; i < 45; i++)
        {
            for (int j = 47; j < 50; j++)
            {
                ToaDo[i][j] = idx / 5 + 1;
            }
            for (int j = 45; j < 47; j++)
            {
                if (i % 5 != 2)
                    ToaDo[i][j] = idx / 5 + 1;
            }
            idx++;
        }

        //danh dau gian hang giua ben trai (trai)
        for (int i = 14; i < 39; i++)
        {
            for (int j = 14; j < 17; j++)
            {
                ToaDo[i][j] = idx / 5 + 1;
            }
            for (int j = 12; j < 14; j++)
            {
                if (i % 5 != 1)
                    ToaDo[i][j] = idx / 5 + 1;
            }
            idx++;
        }

        //danh dau gian hang giua ben trai (phai)
        for (int i = 14; i < 39; i++)
        {
            for (int j = 17; j < 20; j++)
            {
                ToaDo[i][j] = idx / 5 + 1;
            }
            for (int j = 20; j < 22; j++)
            {
                if (i % 5 != 1)
                    ToaDo[i][j] = idx / 5 + 1;
            }
            idx++;
        }



        //danh dau gian hang giua ben phai (trai)
        for (int i = 14; i < 39; i++)
        {
            for (int j = 30; j < 32; j++)
            {
                ToaDo[i][j] = idx / 5 + 1;
            }
            for (int j = 28; j < 30; j++)
            {
                if (i % 5 != 1)
                    ToaDo[i][j] = idx / 5 + 1;
            }
            idx++;
        }

        //danh dau gian hang giua ben phai (phai)
        for (int i = 14; i < 39; i++)
        {
            for (int j = 32; j < 35; j++)
            {
                ToaDo[i][j] = idx / 5 + 1;
            }
            for (int j = 35; j < 37; j++)
            {
                if (i % 5 != 1)
                    ToaDo[i][j] = idx / 5 + 1;
            }
            idx++;
        }


        //danh dau san khau (sankhau = 99)
        for (int i = 0; i < 10; i++)
        {
            for (int j = 15; j < 35; j++)
            {
                ToaDo[i][j] = 99;
            }
        }

    }

    private void InitAtractiveLevel()
    {
        _d = new float[this.getN()][];
        for (int i = 0; i < this.getN(); i++)
        {
            _d[i] = new float[this.getM()];
            for (int j = 0; j < this.getM(); j++)
            {
                if (this.getStoreIdx(new Vector2(i, j)) > 0)
                {
                    _d[i][j] = 1;
                    int countBound = 0;
                    int idx = -1;
                    for (int k = 0; k < dir.Length; k++)
                    {
                        Vector2 v = new Vector2(i, j) + dir[k];
                        if (this.isEmpty(v))
                        {
                            countBound += 1;
                            idx = k;
                        }
                    }
                    if (countBound == 1 && idx < 4)
                        _d[i][j] = Global.getInstance().rand.Next(10, 100);
                    else
                        _d[i][j] = 1;
                }
                else
                {
                    _d[i][j] = 0;
                }
            }
        }
    }

    public Vector3 MaTran2ToaDo(Vector2 p)
    {
        lock (syncLock)
        {
            Vector3 ToaDoGach = new Vector3(
                ToaDoVienGach_0_0.z + p.y,
                ToaDoVienGach_0_0.y,
                ToaDoVienGach_0_0.x + p.x);
            return ToaDoGach;
        }
    }
    
    public Vector2 ToaDo2MaTran(Vector3 p)
    {
        lock (syncLock)
        {
            Vector2 ToaDoTrenMaTran = new Vector2(
            (int)(p.z - ToaDoVienGach_0_0.z),
            (int)(p.x - ToaDoVienGach_0_0.x));
            return ToaDoTrenMaTran;
        }
    }

    public int getN()
    {
        return _n;
    }

    public int getM()
    {
        return _m;
    }

    public Vector2 getRandomExitPos()
    {
        int idx = Global.getInstance().rand.Next(0, _exitPositions.Count);
        return (Vector2) _exitPositions[idx];
    }

    public bool isValidPos(Vector2 p)
    {
        lock (syncLock)
        {
            if (p.x < 0 || p.x >= this.getN())
                return false;
            if (p.y < 0 || p.y >= this.getM())
                return false;
            return true;
        }
    }

    public int getStoreIdx(Vector2 p)
    {
        if (!isValidPos(p))
            return -1;
        return ToaDo[(int)p.x][(int)p.y];
    }

    public bool isEmpty(Vector2 p)
    {
        lock (syncLock)
        {
            if (!this.isValidPos(p))
                return false;
            return this.getStoreIdx(p) == 0;
        }
    }

    public float getActractiveLevel(Vector2 p)
    {
        return _d[(int)p.x][(int)p.y];
    }
}
