using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]private Text EMT;
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
    float Damage;
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
    void protEnemyMove()//�v���g�łł̃G�l�~�[�̍s�� �ǂ̃X�L�����g�p���邩�̒��I
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
            for(int i=0;i<5;i++)
            {
                move-=WolfSkill[i];
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
        Debug.Log("���݂�");
        EMT.text="�ǂ��͂��T�̊��݂�";
        bool flg = false;
        int target=0;


        if(!flg)
        {
            target = EnemyAttackTarget();//�Ώۂ̒��I
            if (charaAlive[target].fillAmount>0)
            {   
                flg=true;
            }
        }
        if(flg)
        { 
            EnemyManager.EnemyInfo.Enemy_ATK[0]*=atkUpcorrection;
            Damage = (int)EnemyManager.EnemyInfo.Enemy_ATK[0];
            DamageCutController(target);
            DamageReflection(Damage);
            PlayerEditorManager.PlayerInfo.Player_HP[target]-=(int)Damage;
            float hp = PlayerEditorManager.PlayerInfo.Player_HP[target];
            PlayerManager.playerHPBer[target].fillAmount=hp/PlayerEditorManager.MaxHP[target];
            CharaMoveGage.ActTime[0]= 8*SkillStorage.DeBuffSpeed * moveUpcorrection;
            enemyMoveGageImage.fillAmount=0;
            CharaMoveGage.ActTime[0] = 8*SkillStorage.DeBuffSpeed;
            GameManager.moveEnd=true;
        }
    }

    void EnemySkill2()
    {
        
        int target=0;
        Debug.Log("��x����");
        EMT.text="�ǂ��͂��T�̓�x����";
        for(int i=0;i<2;i++)
        {
            bool flg = false;
            if (!flg)
            {
                target = EnemyAttackTarget();
                if (charaAlive[target].fillAmount > 0)
                {
                    flg = true;
                }
            }
            if(flg)
            { 
                CharaMoveGage.ActTime[0] = 11*SkillStorage.DeBuffSpeed*moveUpcorrection; 
                EnemyManager.EnemyInfo.Enemy_ATK[0] *= atkUpcorrection;
                Damage = (int)EnemyManager.EnemyInfo.Enemy_ATK[0];
                DamageCutController(target);
                DamageReflection(Damage);
                PlayerEditorManager.PlayerInfo.Player_HP[target] -= (int)Damage;
                float hp = PlayerEditorManager.PlayerInfo.Player_HP[target];
                PlayerManager.playerHPBer[target].fillAmount = hp / PlayerEditorManager.MaxHP[target]; 
                enemyMoveGageImage.fillAmount = 0;
            }
        }
        GameManager.moveEnd = true;
    }
    void EnemySkill3()
    {
        Debug.Log("�r��");
        EMT.text="�ǂ��͂��T�̏r��";
        moveUpTurn=5;
        moveUpcorrection = 0.75f;
        Debug.Log(moveUpcorrection);
        CharaMoveGage.ActTime[0] = 10*SkillStorage.DeBuffSpeed * moveUpcorrection;
        enemyMoveGageImage.fillAmount = 0;
        GameManager.moveEnd = true;
    }
    void EnemySkill4()
    {
        Debug.Log("���K");atkUpTurn=2;
        EMT.text="�ǂ��͂��T�̙��K";
        CharaMoveGage.ActTime[0] = 8 * SkillStorage.DeBuffSpeed * moveUpcorrection;
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
            case 0:
                {
                    EnemySkill1();
                
                }
                break;
            case 1:
                {
                    EnemySkill2();
                }
                break;
            case 2:
                {
                    EnemySkill3();
                }
                break;
            case 3:
                {
                    EnemySkill4();
                }
                break;
        }
    }
    void DamageCutController(int target)
    {
        if (partyChara.transform.GetChild(target).gameObject.name == "�S�[�h��" && SkillStorage.DameCutTime > 0)
        {
            Damage = Damage * (0.01f * (100 - SkillStorage.DameCutPar));
        }
        if(SkillStorage.atkDownTime>0)
        {
            Damage=Damage-SkillStorage.atkDownDeBuff;
            Debug.Log(SkillStorage.atkDownDeBuff);
        }
    }

    int EnemyAttackTarget()
    {
        int MaxHate=PlayerEditorManager.PlayerInfo.Player_Hate.Sum();
        Debug.Log(MaxHate);
        int target=Random.Range(0,MaxHate+1);
        for(int i=0;i<partyChara.transform.childCount;i++)
        {
           target-=PlayerEditorManager.PlayerInfo.Player_Hate[i];
            if(target<0)
            {
                return i;
            }
        }
        return 0;
    }

    void DamageReflection(float atk)
    {
        if(SkillStorage.RefrectCount>0)
        { 
            int Damage=(int)((atk*0.02f)+SkillStorage.RefrectDamage);
            EnemyManager.EnemyInfo.Enemy_HP[0]-=Damage;
            SkillStorage.RefrectCount--;
        }
    }
}
   
