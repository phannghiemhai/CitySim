using System;
using System.IO;
using UnityEngine;

namespace Assets
{
    class Global
    {
        private static Global _instance;
        public float _speed = 0.05f;
        public System.Random rand;
        public Camera[] _cams;
        public int _curCamIdx;
        public GameObject[] _bricks;
        public GameObject[] _visitors;
        public GameObject[] _stores;
        public GameObject[] _empties;
        public GameObject[] _walls;
        public String _mapDir;
        public String _attractiveLevelDir;

        private Global()
        {
            rand = new System.Random((int) DateTime.Now.Millisecond);
            _mapDir = "map.txt";
            _attractiveLevelDir = "attractiveLevel.txt";
        }

        public static Global getInstance()
        {
            if (_instance == null)
            {
                _instance = new Global();
            }
            return _instance;
        }


        public GameObject getClone(int idx, Vector3 p, Quaternion q)
        {
            GameObject clone;
            switch (idx)
            {
                case 0:
                    clone = this.getRandGameObject(_bricks);
                    break;
                case -1:
                    clone = this.getRandGameObject(_empties);
                    break;
                case -2:
                    clone = this.getRandGameObject(_walls);
                    break;
                default:
                    {
                        if (idx > 0)
                        {
                            clone = this.getRandGameObject(_stores);
                        }
                        else
                        {
                            clone = this.getRandGameObject(_empties);
                        }
                    }
                    break;
            }
            return (GameObject)UnityEngine.Object.Instantiate(clone, p, clone.transform.rotation);
        }

        public GameObject getRandGameObject(GameObject[] objs)
        {
            return objs[rand.Next(0, objs.Length)];
        }

        public void SwitchCamera()
        {
            _curCamIdx = (_curCamIdx + 1) % _cams.Length;
            for(int i = 0; i < _cams.Length; i++)
            {
                if(i == _curCamIdx)
                {
                    _cams[i].enabled = true;
                } else
                {
                    _cams[i].enabled = false;
                }
            }
        }

        public static float getDistance(Vector2 a, Vector2 b)
        {
            return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
        }

        public static int[][] readMatrixIntFromFile(string fileDir)
        {
            int n, m;
            int[][] T;

            try
            {
                string line;
                StreamReader reader = new StreamReader(fileDir);

                using (reader)
                {
                    //doc n
                    line = reader.ReadLine();
                    n = int.Parse(line);

                    //doc m
                    line = reader.ReadLine();
                    m = int.Parse(line);

                    T = new int[n][];
                    for (int i = 0; i < n; i++)
                    {
                        T[i] = new int[m];
                    }

                    int idx = 0;

                    do
                    {
                        line = reader.ReadLine();
                        string[] tmp = line.Split(' ');
                        for (int i = 0; i < m; i++)
                        {
                            T[idx][i] = int.Parse(tmp[i]);
                        }

                        idx++;
                    }
                    while (idx < n);

                    reader.Close();

                    return T;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}\n", e.Message);
                throw;
            }
        }

        public static float[][] readMatrixFloatFromFile(string fileDir)
        {
            int n, m;
            float[][] T;

            try
            {
                string line;
                StreamReader reader = new StreamReader(fileDir);

                using (reader)
                {
                    //doc n
                    line = reader.ReadLine();
                    n = int.Parse(line);

                    //doc m
                    line = reader.ReadLine();
                    m = int.Parse(line);

                    T = new float[n][];
                    for (int i = 0; i < n; i++)
                    {
                        T[i] = new float[m];
                    }

                    int idx = 0;

                    do
                    {
                        line = reader.ReadLine();
                        string[] tmp = line.Split(' ');
                        for (int i = 0; i < m; i++)
                        {
                            T[idx][i] = float.Parse(tmp[i]);
                        }

                        idx++;
                    }
                    while (idx < n);

                    reader.Close();

                    return T;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}\n", e.Message);
                throw;
            }
        }

        public static void writeMatrixToFile(String fileDir, int[][] ToaDo)
        {
            String[] str = new String[ToaDo.Length + 2];
            str[0] = ToaDo.Length.ToString();
            str[1] = ToaDo[0].Length.ToString();
            for (int i = 0; i < ToaDo.Length; i++)
            {
                str[i + 2] = "";
                for (int j = 0; j < ToaDo[i].Length; j++)
                {
                    str[i + 2] += ToaDo[i][j].ToString("D2") + " ";
                }
            }
            System.IO.File.WriteAllLines(fileDir, str);
        }

        public static void writeMatrixToFile(String fileDir, float[][] ToaDo)
        {
            String[] str = new String[ToaDo.Length + 2];
            str[0] = ToaDo.Length.ToString();
            str[1] = ToaDo[0].Length.ToString();
            for (int i = 0; i < ToaDo.Length; i++)
            {
                str[i + 2] = "";
                for (int j = 0; j < ToaDo[i].Length; j++)
                {
                    str[i + 2] += ((int) ToaDo[i][j]).ToString("D2") + " ";
                }
            }
            System.IO.File.WriteAllLines(fileDir, str);
        }

        public static FileInfo[] ListFiles(String Location)
        {
            //FIND ALL FILES IN FOLDER 
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Location);
            return dir.GetFiles();
        }

        public static DirectoryInfo[] ListFolders(String Location)
        {
            //FIND ALL FILES IN FOLDER 
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Location);
            return dir.GetDirectories();
        }
    }
}
