using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public static float allRate;
    public static float averageRate;
    public static float rateScore;
    public static int allTurn;
    public static float turnBonus;
    public static float battleTime;
    public static float allBattleTime;
    public static int skillScore;
    public static float skillSelectTime;
    public static bool addScoreFlag;
    public static float lastScore;
    public static float enemyScore;
    public static float basicScore;
    [SerializeField]
    private GameObject scoreSheet;
    [SerializeField]
    private Text basicScoreText;
    [SerializeField]
    private Text rateBonusText;
    [SerializeField]
    private Text turnBonusText;

    // Start is called before the first frame update
    void Start()
    {
        battleTime=0;
        flag=false;
    }
    bool flag;
    // Update is called once per frame
    void Update()
    {
        basicScoreText.text=basicScore.ToString();
        rateBonusText.text=rateScore.ToString();
        turnBonusText.text=turnBonus.ToString();

        if(GameManager.state!=GameManager.BattleState.reSult)
        {
            battleTime += Time.deltaTime;
        }
        else//スコアの計算をここで行う
        {
            if (!flag)
            {
                basicScore=skillScore+enemyScore;
                averageRate = allRate / allTurn;
                if (averageRate >= 1)
                {
                    rateScore += (basicScore) * 0.5f;
                }
                else if (averageRate >= 0.8f)
                {
                    rateScore += (basicScore) * 0.3f;
                }
                else if (averageRate >= 0.6f)
                {
                    rateScore += (basicScore) * 0.05f;
                }
                if(allTurn<=7)
                {
                    turnBonus+=basicScore*0.7f;
                }
                flag=true;
            }
            
        }
        
      

    }

    public static void PlayerScore(float rate,int skillscore)//プレイヤーがスキルを使うと呼ばれる関数
    {
        if(!addScoreFlag)
        {
            allRate += rate;
            allTurn++;
            skillScore += skillscore;
            addScoreFlag=true;
        }
    }
}
