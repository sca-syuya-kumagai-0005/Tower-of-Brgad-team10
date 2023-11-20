using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    int[] WolfSkill;
    public static bool enemyMove;
    [SerializeField]private bool tmpEM;
    [SerializeField] private Image enemyMoveGageImage;
    [SerializeField]private GameObject partyChara;
    private int skillNumber;
    public static bool skillSet;
    public static bool skillOK;
    private int moveUpTurn;
    private float moveUpcorrection=1;
    private int atkUpTurn;
    [SerializeField]
    private float atkUpcorrection;
    private Image[] charaAlive;
    // Start is called before the first frame update
    void Start()
    {
        CharaMoveGage.ActTime[0]=1;
        charaAlive=new Image[partyChara.transform.childCount];
    }

    // Update is called once per frame
    void Update()
    {
       PartyCharaAlive();
        tmpEM=enemyMove;
       protEnemyMove();
        if(CharaMoveGage.MoveChar[0]!=null)
        { 
            if(GameManager.state==GameManager.BattleState.moveWait&&CharaMoveGage.MoveChar[0].name=="Enemy")
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
           // EnemyBuff();
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
                        if(GameManager.moveEnd)
                        { 
                        skillOK=true;
                        skillSet =true;
                        }
                    }
                    
                    break;
                }
            }
            
        }
    }

    void EnemySkill1()
    {
        Debug.Log("噛みつき");
        bool flg = false;
        int target=0;


        if(!flg)
        {
            target = Random.Range(0, 1);//対象の抽選
            if (charaAlive[target].fillAmount>0)
            {   
                flg=true;
            }
        }
        if(flg)
        { 
            EnemyManager.EnemyInfo.Enemy_ATK[0]*=atkUpcorrection;
            PlayerEditorManager.PlayerInfo.Player_HP[target]-= (int)EnemyManager.EnemyInfo.Enemy_ATK[0];
            float hp = PlayerEditorManager.PlayerInfo.Player_HP[target];
            PlayerManager.playerHPBer[target].fillAmount=hp/PlayerEditorManager.MaxHP[target];
            CharaMoveGage.ActTime[0]=1*moveUpcorrection;
            enemyMoveGageImage.fillAmount=0;
            GameManager.moveEnd=true;
        }
    }

    void EnemySkill2()
    {
        bool flg=false;
        int target=0;
        Debug.Log("二度噛み");
        for(int i=0;i<2;i++)
        { 
            if(!flg)
            {
                target = Random.Range(1, charaAlive.Length);//対象の抽選
                if (charaAlive[target].fillAmount > 0)
                {
                    flg = true;
                }
            }
            if(flg)
            { 
                CharaMoveGage.ActTime[0] = 11*moveUpcorrection; 
                EnemyManager.EnemyInfo.Enemy_ATK[0] *= atkUpcorrection;
                PlayerEditorManager.PlayerInfo.Player_HP[target] -= (int)EnemyManager.EnemyInfo.Enemy_standardATK[0];
                float hp = PlayerEditorManager.PlayerInfo.Player_HP[target];
                PlayerManager.playerHPBer[target].fillAmount = hp / PlayerEditorManager.MaxHP[target]; 
                CharaMoveGage.ActTime[0] = 8;
                enemyMoveGageImage.fillAmount = 0;
            }
        }
        GameManager.moveEnd = true;
    }
    void EnemySkill3()
    {
        Debug.Log("俊足");
        moveUpTurn=5;
        moveUpcorrection = 0.75f;
        Debug.Log(moveUpcorrection);
        CharaMoveGage.ActTime[0] = 10*moveUpcorrection;
        enemyMoveGageImage.fillAmount = 0;
        GameManager.moveEnd = true;
    }
    void EnemySkill4()
    {
        Debug.Log("咆哮");atkUpTurn=2;
        CharaMoveGage.ActTime[0] = 8*moveUpcorrection;
        enemyMoveGageImage.fillAmount = 0;
        GameManager.moveEnd = true;
    }
    void EnemyBuff()
    {
        //行動速度もしくは攻撃バフがある場合、行動速度上昇のバフは使わない
        if(moveUpTurn>=1||atkUpTurn!=0)
        {
            WolfSkill[2]=0;
            
        }
        else
        {
            WolfSkill[2]=40;
            moveUpcorrection=1f;
        }
        //攻撃バフを重ね掛けしない
        if(atkUpTurn!=0)
        {
            WolfSkill[3]=0;
        }
        else
        {
            WolfSkill[3]=10;
        }
    }
    void PartyCharaAlive()
    {
        if(GameManager.state==GameManager.BattleState.start)
        { 
            for(int i=0;i<partyChara.transform.childCount;i++)
            {
               GameObject obj= partyChara.transform.GetChild(i).gameObject;
               GameObject mobj=obj.transform.Find("HP").gameObject;
               charaAlive[i]=mobj.GetComponent<Image>();
                Debug.Log(charaAlive);
            }
        }
    }
    void SkillSet()
    {
       switch(skillNumber)
        {
            case 1:
                {
                    while(!GameManager.moveEnd)
                    { 
                        EnemySkill1();
                    }
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
