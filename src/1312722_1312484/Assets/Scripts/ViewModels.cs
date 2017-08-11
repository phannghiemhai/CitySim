using UnityEngine;
using Assets;
using System.Collections;
using UnityEngine.SceneManagement;

public class ViewModels : MonoBehaviour
{
    private int _curCamIdx;
    private int _n;

    // Use this for initialization
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void AddVisitor()
    {
        ToaDoGach tdg = ToaDoGach.getInstance();
        Global glb = Global.getInstance();
        Vector2 p = tdg.getRandomExitPos();
        Instantiate(glb.getRandGameObject(glb._visitors), 
            tdg.MaTran2ToaDo(p), new Quaternion());
    }

    public void ChangeSpeed(float s)
    {
        Global.getInstance()._speed = s;
    }


    public void SwitchCamera()
    {
        Global.getInstance().SwitchCamera();
    }

    public void Stop()
    {
        SceneManager.LoadScene("Setting");
    }
}
