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
    private int moveUpTurn;
    private float moveUpcorrection;
    private int atkUpTurn;
    private float atkUpcorrection;
    // Start is called before the first frame update
    void Start()
    {
        CharaMoveGage.ActTime[0]=1;
    }

    // Update is called once per frame
    void Update()
    {
       
        tmpEM=enemyMove;
        if(GameManager.moveEnd)
        {
            Debug.Log("TRUE�ɂȂ���");
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
    void protEnemyMove()//�v���g�łł̃G�l�~�[�̍s�� �ǂ̃X�L�����g�p���邩�̒��I
    {
        
        if (GameManager.state == GameManager.BattleState.enemyStay&&CharaMoveGage.MoveChar[0].name!=null)
        {
            EnemyBuff();
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
                        Debug.Log(i + "�Ԗڂ̃X�L�����g�p");
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
        Debug.Log("���݂�");
        int target=Random.Range(1,4);//�Ώۂ̒��I
        PlayerEditorManager.PlayerInfo.Player_HP[target]-= EnemyManager.EnemyInfo.Enemy_standardATK;
        float hp = PlayerEditorManager.PlayerInfo.Player_HP[target];
        PlayerManager.playerHPBer[target].fillAmount=hp/PlayerEditorManager.MaxHP[target];
        CharaMoveGage.ActTime[0]=8*moveUpcorrection;
        enemyMoveGageImage.fillAmount=0;
        GameManager.moveEnd=true;
    }

    void EnemySkill2()
    {
        Debug.Log("��x����");
        for(int i=0;i<2;i++)
        { 
            int target = Random.Range(1, 4);//�Ώۂ̒��I
            CharaMoveGage.ActTime[0] = 11*moveUpcorrection;
            PlayerEditorManager.PlayerInfo.Player_HP[target] -= EnemyManager.EnemyInfo.Enemy_standardATK;
            float hp = PlayerEditorManager.PlayerInfo.Player_HP[target];
            PlayerManager.playerHPBer[target].fillAmount = hp / PlayerEditorManager.MaxHP[target]; CharaMoveGage.ActTime[0] = 8;
            enemyMoveGageImage.fillAmount = 0;
        }
        GameManager.moveEnd = true;
    }
    void EnemySkill3()
    {
        Debug.Log("�r��");
        moveUpTurn=5;
        moveUpcorrection = 0.75f;
        Debug.Log(moveUpcorrection);
        CharaMoveGage.ActTime[0] = 10*moveUpcorrection;
        enemyMoveGageImage.fillAmount = 0;
        GameManager.moveEnd = true;
    }
    void EnemySkill4()
    {
        Debug.Log("���K");atkUpTurn=2;
        CharaMoveGage.ActTime[0] = 8*moveUpcorrection;
        enemyMoveGageImage.fillAmount = 0;
        GameManager.moveEnd = true;
    }
    void EnemyBuff()
    {
        //�s�����x�������͍U���o�t������ꍇ�A�s�����x�㏸�̃o�t�͎g��Ȃ�
        if(moveUpTurn>=1||atkUpTurn!=0)
        {
            WolfSkill[2]=0;
            
        }
        else
        {
            WolfSkill[2]=40;
            moveUpcorrection=1f;
        }
        //�U���o�t���d�ˊ|�����Ȃ�
        if(atkUpTurn!=0)
        {
            WolfSkill[3]=0;
        }
        else
        {
            WolfSkill[3]=10;
        }
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
