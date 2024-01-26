using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EXPManager : MonoBehaviour
{
    [SerializeField]
    private int[] tmpLv = new int[4];
    private int enemyCount = 1;
    private int GetEXP = 0;
    [SerializeField]
    private int[] LvUpCount = new int[4];
    private bool GetEXPFlag = false;
    public static int[] OverEXP=new int[4];
    [SerializeField]
    private int[] tmpOver=new int[4];
    private bool flag;
    public static int a=5;
    [SerializeField]
    private  Text[] playerLv;
    [SerializeField]
    private GameObject obj;
    private bool EXPGetFlag;
    // Start is called before the first frame update
    void Awake()
    {
        //for (int i = 0; i < 4; i++)
        //{
        //    PlayerEditorManager.Lv[i] = 99;
        //}
    }
    void Start()
    {
        playerLv=new Text[PlayerEditor.PlayerName.Length];
        
        flag=false;
        GetEXPFlag = false;
        EXPGetFlag=false;
    }

    // Update is called once per frame
    void Update()
    {
       
        if(!flag)
        {
            for (int i = 0; i < 4; i++)
            {
                if(PlayerEditor.PlayerName[i] != "" && PlayerEditor.PlayerName[i] != null) {
                    a =(int)OverEXP[i];
                    float b=a;
                    OverEXP[i]=Mathf.Abs(OverEXP[i]);
                    obj= GameObject.Find("MainCanvas").gameObject.transform.Find("PlayerManager").gameObject;
                    playerLv[i] =GameObject.Find("MainCanvas").gameObject.transform.Find("PlayerManager").gameObject.transform.Find("partyChar").GetChild(i).gameObject.
                        transform.Find("HP").gameObject.transform.Find("Lv").GetComponent<Text>();
                    playerLv[i].text = "Lv"+PlayerEditorManager.Lv[i].ToString();
                    if (OverEXP[i]!=0)
                    {
                        PlayerEditorManager.PlayerInfo.Player_EXP[i] = OverEXP[i];
                    }
                }
            }
            flag=true;
        }
        if(!EXPGetFlag)
        { 
            StartCoroutine(LvJudge()); 
        }
      
    }

    IEnumerator  LvJudge()
    {

        if (GameManager.state == GameManager.BattleState.start)
        {
            for (int i = 0; i < 4; i++)
            {
                LvUpCount[i] = 0;
                tmpLv[i] = PlayerEditorManager.Lv[i];
            }

        }
        if (GameManager.state == GameManager.BattleState.reSult)
        {
            if (!GetEXPFlag)
            {
                for (int j = 0; j < enemyCount; j++)
                {
                    GetEXP += EnemyManager.EnemyInfo.Enemy_EXP[j];//älìæëçåoå±ílÇéZèo
                }
                GetEXPFlag=true;
            }

            for (int i = 0; i < 4; i++)
            {
                int EXP = GetEXP;
                while (EXP != 0)
                {
                    EXP -= PlayerEditorManager.PlayerInfo.Player_EXP[i];
                    int exp = EXP;

                    if (exp >= 0)
                    {
                        PlayerEditorManager.Lv[i] += 1;
                        LvUpCount[i]++;
                       PlayerEditorManager. PlayerStatas(PlayerEditor.playerDatas[i], i);
                        Debug.Log("EXPÇÕ"+EXP);
                    }
                    else
                    {
                        Debug.Log("EXPÇÕ"+EXP);
                        OverEXP[i] =EXP;
                        EXP = 0;
                        break;
                    }
                }
                Debug.Log(LvUpCount[i] + "Lvè„è∏ÇµÇ‹ÇµÇΩ");
            }
            yield return null;
            EXPGetFlag=true;
        }
    }

    void PlayerAlilveJudge()
    {
    }
}
