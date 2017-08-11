using UnityEngine;
using System.Collections;

public class PlayManager : MonoBehaviour 
{
	public Animator[] playerGroup; 
	private string[] animClipNameGroup;
	private int currentNumber;
    private float[] velocities;
    private float[] currentVelocities;

    // Use this for initialization
    void Start () {

		animClipNameGroup = new string[] {
			"Teat_01",
			"Basic_Run_01",
			"Basic_Run_02",
			"Basic_Run_03",
			"Basic_Walk_01",
			"Basic_Walk_02",
			"Etc_Walk_Zombi_01"

		};

        velocities = new float[]
        {
            0,
            0.0f,
            0.012f,
            0.03f,
            0.01f,
            0.008f,
            0.005f
        };

		currentNumber = 4;

        currentVelocities = new float[]
        {
            velocities[currentNumber] + Random.Range(-0.005f, +0.005f),
            velocities[currentNumber] + Random.Range(-0.005f, +0.005f),
            velocities[currentNumber] + Random.Range(-0.005f, +0.005f),
            velocities[currentNumber] + Random.Range(-0.005f, +0.005f)
        };

		playerGroup = GameObject.Find ("PlayerGroup").transform.GetComponentsInChildren<Animator>();

		for(int i = 0; i < playerGroup.Length; i++)
		{
			playerGroup[i].speed = 1f;
			playerGroup[i].Play(animClipNameGroup[currentNumber]);
		}
	}

    static float v = 0.01f;

    void Update()
    {
        Random r = new Random();
        for (int i = 0; i < playerGroup.Length; i++)
        {
            Animator player = playerGroup[i];
            Vector3 b = player.transform.TransformDirection
                (new Vector3(0, 0, currentVelocities[i]));
            player.transform.position += b;
            player.transform.Rotate(0, 0.3f, 0);
        }
    }

    void OnGUI()
	{

        if (GUI.Button(new Rect(50, 50, 50, 50), "<"))
        {
            currentNumber--;

            if (currentNumber < 0)
            {
                currentNumber = animClipNameGroup.Length - 1;
            }

            for (int i = 0; i < playerGroup.Length; i++)
            {
                playerGroup[i].speed = 1f;
                playerGroup[i].Play(animClipNameGroup[currentNumber]);
            }

        }

        if (GUI.Button(new Rect(160, 50, 50, 50), ">"))
        {
            currentNumber++;

            if (currentNumber == animClipNameGroup.Length)
            {
                currentNumber = 0;
            }

            for (int i = 0; i < playerGroup.Length; i++)
            {
                playerGroup[i].speed = 1f;
                playerGroup[i].Play(animClipNameGroup[currentNumber]);
            }
        }

        GUI.Label(new Rect(240, 50, 200, 100), animClipNameGroup[currentNumber].ToString());

    }
}
