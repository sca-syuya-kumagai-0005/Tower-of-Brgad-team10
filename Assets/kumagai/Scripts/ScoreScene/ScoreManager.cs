using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    [SerializeField]
    private Text lastScoreText;
    [SerializeField]
    private Text floorText;

    // Start is called before the first frame update
    void Start()
    {
        battleTime=0;
        allRate=0;
        averageRate=0;
        flag=false;
    }
    bool flag;
    // Update is called once per frame
    void Update()
    {
        basicScoreText.text=basicScore.ToString();
        rateBonusText.text=rateScore.ToString();
        turnBonusText.text=turnBonus.ToString();
        lastScoreText.text=lastScore.ToString();
        floorText.text=(FloarManager.nowFloar-1).ToString();
        if(GameManager.state!=GameManager.BattleState.reSult)
        {
            battleTime += Time.deltaTime;
        }
        if(GameManager.GameOver||GameManager.GameClear)//スコアの計算をここで行う
        {
            if (!flag)
            {
                basicScore=skillScore+enemyScore;
                Debug.Log(skillScore);
                averageRate = allRate / allTurn;
                if (averageRate >= 1)
                {
                    rateScore +=(int)((basicScore) * 0.5f);
                }
                else if (averageRate >= 0.8f)
                {
                    rateScore +=(int) ((basicScore) * 0.3f);
                }
                else if (averageRate >= 0.6f)
                {
                    rateScore +=(int)((basicScore) * 0.05f);
                }
                if(allTurn<=7)
                {
                    turnBonus+=basicScore*0.7f;
                }
                if(GameManager.GameOver)
                {
                    lastScore=(int)(basicScore+rateScore+turnBonus);
                    StartCoroutine(ScoreOpen());
                }
                flag=true;
            }
        }
        if(GameManager.GameOver)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                rateScore=0;
                basicScore=0;
                turnBonus=0;
                SceneManager.LoadScene("TitleScene");
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

    private IEnumerator ScoreOpen()
    {
        if(GameManager.GameOver)
        {
            yield return new WaitForSeconds(1f);
            scoreSheet.SetActive(true);
        }
    }
}
