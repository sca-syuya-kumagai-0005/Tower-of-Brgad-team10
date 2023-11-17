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
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        tmpEM=enemyMove;
       protEnemyMove();
        if(GameManager.state==GameManager.BattleState.moveWait&&Input.GetKeyDown(KeyCode.Return))
        {
            SkillSet();
            skillNumber=0;
            if (CharaMoveGage.MoveChar[0].name == "Enemy")
            {
                enemyMove = true;
            }
        }
    }
    void protEnemyMove()//プロト版でのエネミーの行動 どのスキルを使用するかの抽選
    {
        
        if (GameManager.state == GameManager.BattleState.enemyStay&&CharaMoveGage.MoveChar[0].name==null)
        {
            int sn;
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
                    Debug.Log(i+"番目のスキルを使用");
                    skillNumber=i;
                    return ;
                }
            }
            
        }
    }

    void EnemySkill1()
    {
        enemyMoveGageImage.fillAmount=0;
        
    }

    void EnemySkill2()
    {
        enemyMoveGageImage.fillAmount = 0;

    }
    void EnemySkill3()
    {
        enemyMoveGageImage.fillAmount = 0;

    }
    void EnemySkill4()
    {
        enemyMoveGageImage.fillAmount = 0;

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
