using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets;
using System;

public class MyBehaviourScript : MonoBehaviour
{
    private string[] animClipNameGroup;
    private int currentNumber;
    private Animator player;
    private Vector3 target;
    private Vector3 currentTarget;
    private Stack<Vector2> stack;
    private HashSet<int> _passedStores;
    private int _numberStore;
    // Use this for initialization
    void Start()
    {

        animClipNameGroup = new string[] {
            "Teat_01",
            "Basic_Run_01",
            "Basic_Run_02",
            "Basic_Run_03",
            "Basic_Walk_01",
            "Basic_Walk_02",
            "Etc_Walk_Zombi_01"

        };
        _numberStore = Global.getInstance().rand.Next(10, 20);
        currentNumber = 4;
        _passedStores = new HashSet<int>();
        player = GetComponentInParent<Animator>();
        player.speed = Global.getInstance()._speed / 0.05f;
        player.Play(animClipNameGroup[currentNumber]);
        this.updateStack();
        this.updateTarget();
    }

    void Update()
    {
        player.speed = Global.getInstance()._speed / 0.05f;
        ToaDoGach tdg = ToaDoGach.getInstance();

        if (player.transform.position == target)
        {
            Bricks.getInstance().onStep(tdg.ToaDo2MaTran(target));
            this.updateTarget();
        }
        else
        {
            var heading = target - player.transform.position;
            var heading2d = new Vector2(heading.x, heading.z).normalized;
            var angle = Mathf.Atan2(heading2d.y, heading2d.x) * -Mathf.Rad2Deg + 90;
            player.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
            Vector3 wP = Vector3.MoveTowards(player.transform.position,
                target, Global.getInstance()._speed);
            player.transform.position = wP;
        }
    }

    private void updateTarget()
    {
        ToaDoGach tdg = ToaDoGach.getInstance();
        if (stack != null && stack.Count != 0)
        {
            Vector2 t = stack.Pop();
            int storeIdx = tdg.getStoreIdx(t);
            if (stack.Count > 0 || storeIdx == 0)
            {
                currentTarget = target = tdg.MaTran2ToaDo(t);
            }
            else if (storeIdx != 0)
            {
                _passedStores.Add(storeIdx);
                updateStack();
            }
        }
        else
        {
            currentTarget = target = player.transform.position;
            Destroy(player.gameObject);
            //player.transform.position = new Vector3(1000, 0, 0);
        }
//        Debug.Log(target);
    }

    private void updateStack()
    {
        ToaDoGach tdg = ToaDoGach.getInstance();
        Vector2 s = tdg.ToaDo2MaTran(player.transform.position);
        Vector2 t = this.getDestination(s);
//        Debug.Log("New target : " + t);
        if (t == new Vector2(32, 20))
        {
            Debug.Log("ll");
        }
        BFS bfs = new BFS(tdg.getN(), tdg.getM(), tdg, s, t);
        if (bfs.findPath())
            stack = bfs.getPath();
        else
            stack = null;

    }

    public bool isPassed(int storeIdx)
    {
        if (_passedStores.Count >= _numberStore)
            return true;
        return _passedStores.Contains(storeIdx);
    }

    public Vector2 getDestination(Vector2 s)
    {
        ToaDoGach tdg = ToaDoGach.getInstance();
        float[][] dPlayer = new float[tdg.getN()][];
        float maxA = -1;
        for (int i = 0; i < tdg.getN(); i++)
        {
            dPlayer[i] = new float[tdg.getM()];
            for (int j = 0; j < tdg.getM(); j++)
            {
                float tmp = tdg.getActractiveLevel(new Vector2(i, j));
                if (!this.isPassed(tdg.getStoreIdx(new Vector2(i, j))))
                    dPlayer[i][j] = tmp * 1000 / Global.getDistance(s, new Vector2(i, j));
                else
                    dPlayer[i][j] = -1;
                if (dPlayer[i][j] > maxA)
                    maxA = dPlayer[i][j];
            }
        }

        if (maxA <= 0)
        {
            return tdg.getRandomExitPos();
        }

        ArrayList lmaxItems = new ArrayList();
        for (int i = 0; i < tdg.getN(); i++)
        {
            for (int j = 0; j < tdg.getM(); j++)
            {
                if (dPlayer[i][j] == maxA)
                {
                    lmaxItems.Add(new Vector2(i, j));
                }
            }
        }

        int rIdx = Global.getInstance().rand.Next(0, lmaxItems.Count);
        return (Vector2)lmaxItems[rIdx];

    }

}
