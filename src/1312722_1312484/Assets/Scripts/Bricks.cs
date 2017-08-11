using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    class Bricks
    {
        public static float _decsLevel = 0.005f;
        private static Bricks _instance = null;
        private GameObject[][] _objectMatrix;
        private object[][] _synLocks;
        private static object _synLock = new object();
        private Bricks()
        {
            ToaDoGach tdg = ToaDoGach.getInstance();
            _objectMatrix = new GameObject[tdg.getM()][];
            _synLocks = new object[tdg.getN()][];
            for (int i = 0; i < tdg.getN(); i++)
            {
                _objectMatrix[i] = new GameObject[tdg.getM()];
                _synLocks[i] = new object[tdg.getM()];
                for(int j = 0; j < tdg.getM(); j++)
                {
                    _synLocks[i][j] = new object();
                }
            }
        }

        public void reload()
        {
            lock (_synLock)
            {
                _instance = new Bricks();
            }
        }

        public static Bricks getInstance()
        {
            lock (_synLock)
            {
                if (_instance == null)
                {
                    _instance = new Bricks();
                }
            }
            return _instance;
        }

        public void set(Vector2 p, GameObject obj)
        {
            lock (_synLocks[(int)p.x][(int)p.y])
            {
                _objectMatrix[(int)p.x][(int)p.y] = obj;
            }
        }

        public GameObject get(Vector2 p)
        {
            return _objectMatrix[(int)p.x][(int)p.y];
        }

        public void onStep(Vector2 p)
        {
            lock (_synLocks[(int)p.x][(int)p.y])
            {
                GameObject obj = this.get(p);
                Color color = obj.GetComponentInParent<Renderer>().material.color;
                color.b = color.b - _decsLevel;
                color.g = color.g - _decsLevel;
                obj.GetComponentInParent<Renderer>().material.color = color;
            }
        }
    }
}
