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
    private int[] EnemySkill;
    [SerializeField]
    int[] ReaperSkill;
    public static bool enemyMove;
    [SerializeField]private bool tmpEM;
    [SerializeField] private Image enemyMoveGageImage;
    [SerializeField]private GameObject partyChara;
    private int skillNumber;
    public static bool skillSet;
    public static bool skillOK;
    private int moveUpTurn;
    private float moveUpcorrection=1;
    public static int atkUpTurn;
    public static float atkUpcorrection=1;
    private Image[] charaAlive;
    float Damage;
    // Start is called before the first frame update
    private void Awake()
    {
        CharaMoveGage.ActTime[0]=1;
    }
    void Start()
    {
        switch (CharaMoveGage.enemyName)
        { 
            case "追いはぎ狼":
                {
                   　CharaMoveGage.ActTime[0]=1;
                    EnemySkill=WolfSkill;
                }
                break;
            case "死神":
                {
                    CharaMoveGage.ActTime[0]=15;
                    EnemySkill=ReaperSkill;
                }
                break;
        }
        charaAlive =new Image[partyChara.transform.childCount];

    }

    // Update is called once per frame
    void Update()
    {
       
       PartyCharaAlive();
       tmpEM=enemyMove;
       protEnemyMove();
        if (CharaMoveGage.MoveChar[0]!=null)
        { 
            if(GameManager.state==GameManager.BattleState.moveWait&&CharaMoveGage.MoveChar[0].CompareTag("Enemy"))
            {
                    if (CharaMoveGage.MoveChar[0].CompareTag("Enemy"))
                    {
                        enemyMove = true;
                        if(SkillStorage.poisonFlag) 
                        {
                            int charaNumber=SkillStorage.DoctorNumber;
                            float pAtk = PlayerEditorManager.PlayerInfo.Player_ATK[charaNumber];
                            SkillStorage.addDamage = (pAtk * SkillStorage.rate) *SkillStorage. atkBuff;
                            float ehp = EnemyManager.EnemyInfo.Enemy_HP[0] - SkillStorage.addDamage;
                            EnemyManager.EnemyInfo.Enemy_HP[0] = ehp;
                            EnemyManager.debugHPBer.fillAmount = ehp / EnemyManager.maxEnemyHP[0];
                            SkillStorage.poisonFlag=false;
                        }
                    }
            }
        }
    }
    void protEnemyMove()//プロト版でのエネミーの行動 どのスキルを使用するかの抽選
    {
        
        if (GameManager.state == GameManager.BattleState.enemyStay&&CharaMoveGage.MoveChar[0].name!=null&&!SkillStorage.sleep)
        {
           // EnemyBuff();
            int MaxSkill = 0;
            for (int i = 0; i < 4; i++)
            {
                MaxSkill += EnemySkill[i];
            }
            int move=Random.Range(1,MaxSkill+1);
            for(int i=0;i<5;i++)
            {
                move-=EnemySkill[i];
                if(move<=0)
                {
                    if(!skillSet)
                    {
                        skillNumber = i;
                        SkillSet();
                        if(GameManager.moveEnd)
                        {
                            skillOK = true;
                            skillSet = true;
                        }
                    }
                    break;
                }
            }
        }
        else if(GameManager.state == GameManager.BattleState.enemyStay && CharaMoveGage.MoveChar[0].name != null && SkillStorage.sleep)
        {
            Debug.Log("眠って動けない");
            GameManager.moveEnd = true;
            CharaMoveGage.ActTime[0] = 8 * moveUpcorrection;
            SkillStorage.enemyActTime = CharaMoveGage.ActTime[0];
            CharaMoveGage.alpha -= 1;
            skillOK = true;
            skillSet = true;
            SkillStorage.comparText=CharaMoveGage.enemyName+"は眠りから覚めた";
            StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
            SkillStorage.sleep = false;

            
        }
    }

    void WolfSkill1()
    {
        Debug.Log("噛みつき");
        SkillStorage.comparText="噛みつきを繰り出してきた\nダメージを受けた";
        StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        bool flg = false;
        int target = 0;


        if (!flg)
        {
            target = EnemyAttackTarget();//対象の抽選
            if (charaAlive[target].fillAmount>0)
            {
                flg=true;
            }
        }
        if(flg)
        { 
            int eAtk=(int)(EnemyManager.EnemyInfo.Enemy_ATK[0]*atkUpcorrection*richardSkill3Buff);
            Damage = eAtk;
            DamageCutController(target);
            DamageReflection(Damage);
            PlayerEditorManager.PlayerInfo.Player_HP[target]-=(int)Damage;
            float hp = PlayerEditorManager.PlayerInfo.Player_HP[target];
            PlayerManager.playerHPBer[target].fillAmount=hp/PlayerEditorManager.MaxHP[target];
            CharaMoveGage.ActTime[0]= 8* moveUpcorrection;
            SkillStorage.enemyActTime = CharaMoveGage.ActTime[0];
            GameManager.moveEnd=true;
        }
    }

    void WolfSkill2()
    {
        
        int target=0;
        Debug.Log("噛みつき");
        SkillStorage.comparText = "二度噛みを繰り出してきた\n二回のダメージを受けた";
        StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        for (int i=0;i<2;i++)
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
                CharaMoveGage.ActTime[0] = 3*moveUpcorrection;
                SkillStorage.enemyActTime = CharaMoveGage.ActTime[0];
                int eAtk = (int)(EnemyManager.EnemyInfo.Enemy_ATK[0] * atkUpcorrection * richardSkill3Buff);
                Damage = eAtk;
                DamageCutController(target);
                DamageReflection(Damage);
                PlayerEditorManager.PlayerInfo.Player_HP[target] -= (int)Damage;
                float hp = PlayerEditorManager.PlayerInfo.Player_HP[target];
                PlayerManager.playerHPBer[target].fillAmount = hp / PlayerEditorManager.MaxHP[target];
            }
        }
        GameManager.moveEnd = true;
    }
    void WolfSkill3()
    {
        SkillStorage.comparText = "俊足を繰り出してきた\n敵の行動速度が上昇した";
        StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        moveUpTurn =5;
        moveUpcorrection = 0.75f;
        Debug.Log(moveUpcorrection);
        CharaMoveGage.ActTime[0] = 10*moveUpcorrection;
        SkillStorage.enemyActTime = CharaMoveGage.ActTime[0];
        GameManager.moveEnd = true;
    }
    void WolfSkill4()
    {
        atkUpTurn=2;
        SkillStorage.comparText = "咆哮を繰り出してきた\n敵の攻撃力が上昇した";
        StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        CharaMoveGage.ActTime[0] = 8  * moveUpcorrection;
        SkillStorage.enemyActTime = CharaMoveGage.ActTime[0];
        GameManager.moveEnd = true;
    }


    void ReaperSkill1()
    {
        bool flg = false;
        int target = 0;
        SkillStorage.comparText = "命狩りの鎌を繰り出してきた\nダメージを受けた\nさらに敵の与ダメージが上昇した";
        StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        if (!flg)
        {
            target = EnemyAttackTarget();//対象の抽選
            if (charaAlive[target].fillAmount > 0)
            {
                flg = true;
            }
        }
        if (flg)
        {
            int eAtk = (int)(EnemyManager.EnemyInfo.Enemy_ATK[0] * atkUpcorrection * richardSkill3Buff);
            Damage = eAtk;
            DamageCutController(target);
            DamageReflection(Damage);
            PlayerEditorManager.PlayerInfo.Player_HP[target] -= (int)Damage;
            float hp = PlayerEditorManager.PlayerInfo.Player_HP[target];
            PlayerManager.playerHPBer[target].fillAmount = hp / PlayerEditorManager.MaxHP[target];
            CharaMoveGage.ActTime[0] = 8 * moveUpcorrection;
            CharaMoveGage.ActTime[0] = 8 * atkUpcorrection;
            SkillStorage.enemyActTime = CharaMoveGage.ActTime[0];
            atkUpcorrection=1.15f;
            atkUpTurn=2;
            GameManager.moveEnd = true;
        }
    }

    void ReaperSkill2()
    {
        int target = 0;
        SkillStorage.comparText = "命狩りの連撃を繰り出してきた\nダメージを二回受けた";
        StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        bool flg = false;
            if (!flg)
            {
                target = EnemyAttackTarget();
                if (charaAlive[target].fillAmount > 0)
                {
                    flg = true;
                }
            }
            if (flg)
            {
                CharaMoveGage.ActTime[0] = 20 * moveUpcorrection;
                SkillStorage.enemyActTime = CharaMoveGage.ActTime[0];
                int eAtk = (int)(EnemyManager.EnemyInfo.Enemy_ATK[0] * atkUpcorrection * richardSkill3Buff);
                Damage = eAtk;
                DamageCutController(target);
                DamageReflection(Damage);
                PlayerEditorManager.PlayerInfo.Player_HP[target] -= (int)Damage;
                float hp = PlayerEditorManager.PlayerInfo.Player_HP[target];
                PlayerManager.playerHPBer[target].fillAmount = hp / PlayerEditorManager.MaxHP[target];
        }
        
        GameManager.moveEnd = true;
    }

    void ReaperSkill3()
    {
        SkillStorage.comparText = "死期呼びを繰り出してきた\n味方全体が最大HPに応じて\nダメージを受けた";
        StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        for (int i=0;i<PlayerEditor.PlayerName.Length;i++)
        {
            
            float hp = PlayerEditorManager.PlayerInfo.Player_HP[i];
            if(hp>0)
            {
                Debug.Log("BBBB");
                PlayerEditorManager.PlayerInfo.Player_HP[i] -= (int)(PlayerEditorManager.MaxHP[i] / 10f*atkUpcorrection);
                hp=PlayerEditorManager.PlayerInfo.Player_HP[i];
                PlayerManager.playerHPBer[i].fillAmount = hp / PlayerEditorManager.MaxHP[i];
                CharaMoveGage.ActTime[0] = 25 * moveUpcorrection;
            }
        }
        GameManager.moveEnd = true;
    }

    void ReaperSkill4()
    {
        SkillStorage.comparText = "鎌研ぎを繰り出してきた\n敵の攻撃力が上昇した";
        StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        EnemyManager.EnemyInfo.Enemy_ATK[0]*=1.1f;
        CharaMoveGage.ActTime[0] = 13 * moveUpcorrection;
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
                GameObject hpobj=mobj.transform.Find("HPGreen").gameObject;
               charaAlive[i]=hpobj.GetComponent<Image>();
            }
        }
    }
    void SkillSet()
    {
        string eN=CharaMoveGage.enemyName;
        if(eN=="追いはぎ狼")
        { 
           switch(skillNumber)
           {
                case 0:
                    {
                        WolfSkill1();
                
                    }
                    break;
                case 1:
                    {
                        WolfSkill2();
                    }
                    break;
                case 2:
                    {
                        WolfSkill3();
                    }
                    break;
                case 3:
                    {
                        WolfSkill4();
                    }
                    break;
            }
        }
        if(eN=="死神")
        {
                switch (skillNumber)
                {
                    case 0:
                        {
                            ReaperSkill1();

                        }
                        break;
                    case 1:
                        {
                            ReaperSkill2();
                        }
                        break;
                    case 2:
                        {
                            ReaperSkill3();
                        }
                        break;
                    case 3:
                        {
                            ReaperSkill4();
                        }
                        break;
                }
        }
    }
    void DamageCutController(int target)
    {
        
        if(partyChara.transform.GetChild(target).gameObject.name=="ゴードン"&&SkillStorage.gordonBreakerTime>0) {
            Damage=0;
        }
        else if(SkillStorage.rinBreakerTime>0)
        {
            Damage*=SkillStorage.rinBreaker;
        }
        else if(partyChara.transform.GetChild(target).gameObject.name == "ゴードン" && SkillStorage.DameCutTime > 0) {
            Damage = Damage * (0.01f * (100 - SkillStorage.DameCutPar));
        }
        if(SkillStorage.atkDownTime>0)
        {
            Damage=Damage-SkillStorage.atkDownDeBuff;
            Debug.Log(SkillStorage.atkDownDeBuff);
        }
        if(Damage<0) 
        {
            Damage=0;
        }
    }
    

    private int richardSkill3Buff;

    int EnemyAttackTarget()
    {
        int MaxHate=0;
        
        for(int i=0;i<PlayerEditor.PlayerName.Length;i++)
        {
            if (GameManager.aliveFlag[i + 1])
            {
                MaxHate += PlayerEditorManager.PlayerInfo.Player_Hate[i];
            }
        }
        Debug.Log(MaxHate);
        int target=Random.Range(0,MaxHate+1);
        for(int i=0;i<partyChara.transform.childCount;i++)
        {
            if (GameManager.aliveFlag[i + 1])
            {
                target -= PlayerEditorManager.PlayerInfo.Player_Hate[i];
            }
           
            if(target<=0)
            {
                if(partyChara.transform.GetChild(target).gameObject.name =="リチャード"&&SkillStorage.richardSkill3Avoidance)
                {
                    richardSkill3Buff=0;
                }
                else
                {
                    richardSkill3Buff=1;
                }
                Debug.Log("ターゲットは"+i);
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
   
