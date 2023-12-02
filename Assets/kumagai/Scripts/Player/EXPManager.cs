using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPManager : MonoBehaviour
{
    [SerializeField]
    private int[] tmpLv;
    private int enemyCount=1;
    private int GetEXP=0;
    [SerializeField]
    private int[] LvUpCount=new int[4];
    private bool GetEXPFlag=false;
    // Start is called before the first frame update
    void Start()
    {
        GetEXPFlag=false;
    }

    // Update is called once per frame
    void Update()
    {
        LvJudge();
    }

    void LvJudge()
    {
        
        if (GameManager.state == GameManager.BattleState.start)
        {
            for(int i=0;i<4;i++)
            {
                LvUpCount[i] = 0;
                tmpLv[i] = PlayerEditorManager.Lv[i];
            }
            
        }
        if(GameManager.state==GameManager.BattleState.reSult)
        {
            if(!GetEXPFlag)
            {
                for (int j = 0; j < enemyCount; j++)
                {
                    GetEXP += EnemyManager.EnemyInfo.Enemy_EXP[j];//älìæëçåoå±ílÇéZèo
                }
            }
            
            for (int i=0;i<PlayerEditor.PlayerName.Length;i++)
            {
                int EXP=GetEXP;
                while(EXP!=0)
                {
                    EXP -= PlayerEditorManager.PlayerInfo.Player_EXP[i];
                    int exp= EXP;
                   
                    if (exp>=0)
                    {
                        PlayerEditorManager.Lv[i]+=1;
                        LvUpCount[i]++;
                    }
                    else 
                    {
                        PlayerEditorManager.PlayerInfo.Player_EXP[i]+=exp;
                        EXP=0;
                    }
                }
                Debug.Log(LvUpCount[i] + "Lvè„è∏ÇµÇ‹ÇµÇΩ");
            }
           
        }
    }

    void PlayerAlilveJudge()
    {
    }
}
