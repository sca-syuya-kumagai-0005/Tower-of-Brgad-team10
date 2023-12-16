using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
     int[] a=new int [4];
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<4;i++)
        {
            a[i]=5;
        }
        flag=false;
        
        GetEXPFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!flag)
        {
            for (int i = 0; i < 4; i++)
            {
                Debug.Log(OverEXP[i]*-1);
                OverEXP[i]*=-1;
                tmpOver=OverEXP;
                PlayerEditorManager.PlayerInfo.Player_EXP[i] = tmpOver[i];
            }
            flag=true;
        }
        LvJudge();
    }

    void LvJudge()
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

            for (int i = 0; i < PlayerEditor.PlayerName.Length; i++)
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
                    }
                    else
                    {
                        Debug.Log("EXPÇÕ"+EXP);
                        OverEXP[i] = EXP;
                        EXP = 0;
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
