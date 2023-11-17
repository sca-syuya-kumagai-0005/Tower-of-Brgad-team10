using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMove : MonoBehaviour
{
    int[] WolfSkill={40,10,40,10 };
    public static bool enemyMove;
    [SerializeField]private bool tmpEM;
    [SerializeField] private Image enemyMoveGageImage;
    private int skillNumber;
    public static bool skillSet;
    public static bool skillOK;
    // Start is called before the first frame update
    void Start()
    {
        CharaMoveGage.ActTime[0]=2;
    }

    // Update is called once per frame
    void Update()
    {
        tmpEM=enemyMove;
        if(GameManager.moveEnd)
        {
            Debug.Log("TRUEになった");
        }
       protEnemyMove();
        if(GameManager.state==GameManager.BattleState.moveWait&&Input.GetKeyDown(KeyCode.Return))
        {
            
            if(CharaMoveGage.MoveChar[0]!=null)
            { 
                if (CharaMoveGage.MoveChar[0].name == "Enemy")
                {
                    enemyMove = true;
                }
            }
        }
    }
    void protEnemyMove()//プロト版でのエネミーの行動 どのスキルを使用するかの抽選
    {
        
        if (GameManager.state == GameManager.BattleState.enemyStay&&CharaMoveGage.MoveChar[0].name!=null)
        {
            int MaxSkill = 0;
            for (int i = 0; i < 4; i++)
            {
                MaxSkill += WolfSkill[i];
            }
            int move=Random.Range(0,MaxSkill+1);
            for(int i=1;i<5;i++)
            {
                move-=WolfSkill[i-1];
                if(move<=0)
                {
                   
                    if(!skillSet)
                    {
                        skillNumber = i;
                        SkillSet();
                        Debug.Log(i + "番目のスキルを使用");
                        skillOK=true;
                        skillSet =true;
                    }
                    
                    break;
                }
            }
            
        }
    }

    void EnemySkill1()
    {
        int target=Random.Range(1,4);//対象の抽選
        Debug.Log("target");
        Debug.Log("OK");
        PlayerEditorManager.PlayerInfo.Player_HP[target]-= EnemyManager.EnemyInfo.Enemy_standardATK;
        PlayerManager.playerHPBer[target].fillAmount=PlayerEditorManager.PlayerInfo.Player_HP[target]/PlayerEditorManager.MaxHP[target];
        CharaMoveGage.ActTime[0]=8;
        enemyMoveGageImage.fillAmount=0;
        GameManager.moveEnd=true;
    }

    void EnemySkill2()
    {
        int target = Random.Range(1, 4);//対象の抽選
        Debug.Log("target");
        PlayerEditorManager.PlayerInfo.Player_HP[target] -= EnemyManager.EnemyInfo.Enemy_standardATK;
        PlayerManager.playerHPBer[target].fillAmount = PlayerEditorManager.PlayerInfo.Player_HP[target] / PlayerEditorManager.MaxHP[target]; CharaMoveGage.ActTime[0] = 8;
        enemyMoveGageImage.fillAmount = 0;
        Debug.Log("OK");
        GameManager.moveEnd = true;
    }
    void EnemySkill3()
    {
        int target = Random.Range(1, 4);//対象の抽選
        Debug.Log("target");
        PlayerEditorManager.PlayerInfo.Player_HP[target] -= EnemyManager.EnemyInfo.Enemy_standardATK;
        PlayerManager.playerHPBer[target].fillAmount = PlayerEditorManager.PlayerInfo.Player_HP[target] / PlayerEditorManager.MaxHP[target]; CharaMoveGage.ActTime[0] = 8;
        enemyMoveGageImage.fillAmount = 0;
        Debug.Log("OK");
        GameManager.moveEnd = true;
    }
    void EnemySkill4()
    {
        int target = Random.Range(1, 4);//対象の抽選
        Debug.Log("target");
        PlayerEditorManager.PlayerInfo.Player_HP[target] -= EnemyManager.EnemyInfo.Enemy_standardATK;
        PlayerManager.playerHPBer[target].fillAmount = PlayerEditorManager.PlayerInfo.Player_HP[target] / PlayerEditorManager.MaxHP[target]; CharaMoveGage.ActTime[0] = 8;
        enemyMoveGageImage.fillAmount = 0;
        Debug.Log("OK");
        GameManager.moveEnd = true;
    }
    void SkillSet()
    {
       switch(skillNumber)
        {
            case 1:
                {
                    EnemySkill1();
                }
                break;
            case 2:
                {
                    EnemySkill2();
                }
                break;
            case 3:
                {
                    EnemySkill3();
                }
                break;
            case 4:
                {
                    EnemySkill4();
                }
                break;
        }
    }
}
