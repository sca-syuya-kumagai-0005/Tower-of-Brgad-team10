using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
using UnityEngine;

public class BreakerKeyJudge : MonoBehaviour
{
    [SerializeField]
    private bool[] tmpKeyFlag = new bool[8];
    public static bool[] KeyFlag = new bool[8];
    public static string[] AllKey = { "w", "a", "s", "d", "up", "down", "right", "left" };
    public static bool startFlag;
    [SerializeField]
    private GameObject Judge;
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
    [SerializeField]private GameObject nearObj;
    void ElseJudge()
    {
        if (GameManager.state == GameManager.BattleState.breakerCommand)
        {
            for (int i = 0; i < 8; i++)
            {
                GameObject obj;
                if (Input.GetKeyDown(AllKey[i]) && !startFlag)
                {
                    if (!KeyFlag[i])
                    {
                        for (int j=5;j<this.gameObject.transform.childCount;j++)
                        {
                            obj=transform.GetChild(j).gameObject;
                            Debug.Log("’Ê‰ß‚µ‚Ä‚¢‚Ü‚·");
                            CommandController CC=obj.GetComponent<CommandController>();
                            if(nearObj!=null)
                            {
                                CommandController nearObjCC=nearObj.GetComponent<CommandController>();
                                if(Math.Abs(nearObjCC.judgeDistance)>Math.Abs(CC.judgeDistance))
                                {
                                    nearObj=obj;
                                }
                            }
                            if(nearObj==null)
                            {
                                nearObj=obj;
                            }
                        }
                        if(nearObj.GetComponent<CommandController>()!=null)
                        {
                            CommandController C = nearObj.GetComponent<CommandController>();
                            C.OkFlag = true;

                        }
                        KeyFlag[CommandController.tmpi]=false;
                        BreakerEditor.commandDestroy += 1;
                        Destroy(nearObj);
                    }
                }
            }
        }
    }
}
