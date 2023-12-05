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
        Debug.DrawRay(Judge.transform.position, new Vector3(100, 0, 0), Color.red, 100.0f);
    }

    [SerializeField]private GameObject obj;
    void ElseJudge()
    {
        RaycastHit2D hit;
       if(GameManager.state==GameManager.BattleState.command)
        { 
        for (int i = 0; i < 8; i++)
        {
            if (Input.GetKeyDown(AllKey[i]) && !startFlag)
            {
                if (!KeyFlag[i])
                {
                     hit = Physics2D.Raycast(Judge.transform.position, new Vector3(100, 0, 0), 100f);
                    if (hit.collider.gameObject.name != "judge")
                    {
                        Debug.Log(hit.collider);
                        obj = hit.collider.gameObject;
                         CommandController CC=obj.GetComponent<CommandController>();
                         CC.OkFlag=true;
                         //obj.SetActive(false);
                        Destroy(obj);
                        NotesEditor.commandDestroy+=1;
                    }


                }
            }
        }
        }
    }
}
