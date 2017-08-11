using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Assets
{
    class BFS
    {
        public const int UNDEFINED = -1;
        public static Vector2[] dir = {new Vector2(1, 0),
                                        new Vector2(-1, 0),
                                        new Vector2(0, 1),
                                        new Vector2(0, -1),
                                        new Vector2(-1, -1),
                                        new Vector2(-1, 1),
                                        new Vector2(1, 1),
                                        new Vector2(1, -1)};
        private int _n;
        private int _m;
        private int[][] _step;
        private MMatrix _map;
        private Vector2 _s;
        private Vector2 _t;
        private bool _hasResult;
        public BFS(int n, int m, MMatrix map, Vector2 s, Vector2 t)
        {
            _n = n;
            _m = m;
            _map = map;
            _s = s;
            _t = t;
            _step = new int[_n][];
            for(int i = 0; i < _n; i++)
            {
                _step[i] = new int[_m];
                for(int j = 0; j < _m; j++)
                {
                    _step[i][j] = UNDEFINED;
                }
            }
            _hasResult = false;
        }

        private bool isUndefined(Vector2 p)
        {
            return _step[(int)p.x][(int)p.y] == UNDEFINED;
        }

        private void setStep(Vector2 p, int stepCount)
        {
            try
            {
                _step[(int)p.x][(int)p.y] = stepCount;
            } catch (Exception e)
            {
                //Debug.Log(p);
            }
        }

        private int getStep(Vector2 p)
        {
            return _step[(int)p.x][(int)p.y];
        }

        private bool isEndState(Vector2 p)
        {
            return p.Equals(this.getEndState());
        }

        private Vector2 getStartState()
        {
            return _s;
        }

        private Vector2 getEndState()
        {
            return _t;
        }

        private MMatrix getMap()
        {
            return _map;
        }

        private bool __findPath()
        {
            if (this.isEndState(this.getStartState()))
            {
                return true;
            }
            Queue<Vector2> queue = new Queue<Vector2>();
            this.setStep(_s, 0);
            queue.Enqueue(this.getStartState());
            while(queue.Count > 0)
            {
                Vector2 u = queue.Dequeue();
                Vector2[] dir_alt = this.getShuffleDirs();
                for(int i = 0; i < dir_alt.Length; i++)
                {
                    Vector2 v = u + dir_alt[i];
                    if (this.getMap().isEmpty(v) && this.isUndefined(v))
                    {
                        this.setStep(v, this.getStep(u) + 1);
                        queue.Enqueue(v);
                    }
                    if (this.isEndState(v))
                    {
                        this.setStep(v, this.getStep(u) + 1);
                        return true;
                    }
                }
            }
            return false;
        }

        public bool findPath()
        {
            bool res = this.__findPath();
            _hasResult = res;
            return res;
        }

        public bool hasResult()
        {
            return _hasResult;
        }

        public Stack<Vector2> getPath()
        {
            if (!hasResult())
                return null;
            Stack<Vector2> res = new Stack<Vector2>();
            Vector2 s = new Vector2(this.getStartState().x, this.getStartState().y);
            Vector2 t = new Vector2(this.getEndState().x, this.getEndState().y);
            res.Push(t);
            while (!t.Equals(s))
            {
                Vector2[] dir_alt = this.getShuffleDirs();
                for (int i = 0; i < dir_alt.Length; i++)
                {
                    Vector2 tmp = t + dir_alt[i];
                    if (this.getMap().isEmpty(tmp) 
                        && this.getStep(tmp) + 1 == this.getStep(t))
                    {
                        t = tmp;
                        res.Push(tmp);
                        break;
                    }
                }
            }
            return res;
        }

        private Vector2[] getShuffleDirs()
        {
            return dir.OrderBy(x => Global.getInstance().rand.Next()).ToArray();
        }
    }
}
