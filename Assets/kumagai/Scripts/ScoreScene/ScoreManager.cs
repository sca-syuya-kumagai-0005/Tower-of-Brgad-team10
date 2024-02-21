using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static float allRate;
    public static int allTurn;
    public static float battleTime;
    public static float allBattleTime;
    public static int skillScore;
    public static float skillSelectTime;
    public static bool addScoreFlag;
    [SerializeField]
    private GameObject scoreSheet;
    [SerializeField]
    private GameObject sheetUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        battleTime+=Time.deltaTime;
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

    public static IEnumerator ScoreCoroutine()
    {
        yield break;
    }
}
