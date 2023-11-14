using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandKeyManager : MonoBehaviour
{
    [SerializeField]
    private bool[] tmpKeyFlag = new bool[8];
    public static bool[] KeyFlag = new bool[8];
    public static string[] AllKey = { "w", "a", "s", "d", "up", "down", "right", "left" };
    public static bool startFlag;
    // Start is called before the first frame update
    void Start()
    {
        startFlag = false;
    }


    // Update is called once per frame
    void Update()
    {
        ElseJudge();
        tmpKeyFlag = KeyFlag;
    }

    void ElseJudge()
    {
        for (int i = 0; i < 8; i++)
        {
            if (Input.GetKeyDown(AllKey[i]) && startFlag)
            {
                if (!KeyFlag[i])
                {
                }
            }
        }
    }
}
