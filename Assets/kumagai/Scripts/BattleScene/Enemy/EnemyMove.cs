using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using static MoveTextController;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]private Text EMT;
    [SerializeField]int[] WolfSkill;
    [SerializeField]int[] SuccubsSkill;
    [SerializeField]int[] GoblinSkill;
    [SerializeField]int[] OctopusPotSkill;
    [SerializeField]int[] KerberosSkill;
    [SerializeField]int[] DoragonSkill;
    [SerializeField]int[] PegasusSkill;
    [SerializeField]
    private int[] EnemySkill;
    [SerializeField]
    int[] ReaperSkill;
    [SerializeField]
    int[] StoneSkill;
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
        octopusPotSkill1Buff=1;
        octopusPotSkill4Buff=1;
        octopusPostSkill1Turn=0;
        octopusPotSkill4Turn=0;
        moveUpTurn = 0;
        moveUpcorrection = 1f;
        atkUpTurn=0;
        StonePoison = false;
        spTurn = 0;
        stoneSpeedDebuff = 1f;
        stoneSpeedTurn = 0;
        succubusSkill2Buff = 1.3f;
        succubusSkill2Turn = 5;
        goblinBuff=0;
        kerberosBuff=1;
        kerberosBuffTurn=0;
        doragonSkill1Buff=1;
        doragonSkill1Flag=false;
        PegasusFirstFlag=false;
        
    }
    void Start()
    {
        flag=false;
        switch (CharaMoveGage.enemyName)
        { 
            case "追いはぎ狼":
                {
                   　CharaMoveGage.ActTime[0]=8;
                    EnemySkill=WolfSkill;
                }
                break;
            case "死神":
                {
                    CharaMoveGage.ActTime[0]=15;
                    EnemySkill=ReaperSkill;
                }
                break;
            case "口だけの像":
                {
                    CharaMoveGage.ActTime[0]=10;
                    EnemySkill=StoneSkill;
                }
                break;
            case "サキュバス":
                {
                    CharaMoveGage.ActTime[0]=9;
                    EnemySkill=SuccubsSkill;
                }
                break;
            case "ゴブリン":
                {
                    CharaMoveGage.ActTime[0]=6;
                    EnemySkill=GoblinSkill;
                }
                break;
            case"タコ壺戦士":
                {
                    CharaMoveGage.ActTime[0]=12;
                    EnemySkill=OctopusPotSkill;
                }
                break;
            case"ケルベロス":
                {
                    CharaMoveGage.ActTime[0]=7;
                    EnemySkill=KerberosSkill;
                }
                break;
            case"ドラゴン":
                {
                    CharaMoveGage.ActTime[0]=8;
                    EnemySkill=DoragonSkill;
                }
                break;
            case "ペガサス":
                {
                    CharaMoveGage.ActTime[0]=0.1f;
                    EnemySkill=PegasusSkill;
                }
                break;
        }

        charaAlive =new Image[partyChara.transform.childCount];
    }

    bool flag=false;
    // Update is called once per frame
    void Update()
    {
       if(!flag)
        {
            StartCoroutine(moveTextCoroutine(CharaMoveGage.enemyName + "が現れた！"));
            flag =true;
        }
       if(succubusSkill2Turn<=0)
        {
            succubusSkill2Buff=1;
        }
       DoragonSkillPoint();
       PegasusSkillPoint();
       PartyCharaAlive();
       tmpEM=enemyMove;
       protEnemyMove();
        if(stoneSpeedTurn==0)
        {
            stoneSpeedDebuff=1;
        }
        if (CharaMoveGage.MoveChar.Count!=0)
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
    void protEnemyMove()//どのスキルを使用するかの抽選
    {
        
        if (GameManager.state == GameManager.BattleState.enemyStay&&CharaMoveGage.MoveChar[0].name!=null&&!SkillStorage.sleep)
        {
           // EnemyBuff();
           Debug.Log("AAAAAAAAAAAAAAAAAaa");
            int MaxSkill = 0;
            for (int i = 0; i < EnemySkill.Length; i++)
            {
                MaxSkill += EnemySkill[i];
                Debug.Log("BBBBBBBBBBB");
            }
            int move=Random.Range(1,MaxSkill+1);
            for(int i=0;i<EnemySkill.Length;i++)
            {
                move-=EnemySkill[i];
                if(move<=0)
                {
                    if(!skillSet)
                    {
                        Debug.Log("CCCCCCCC");
                        skillNumber = i;
                        SkillSet();
                        if(GameManager.moveEnd)
                        {
                            Debug.Log("DDDDDDD");
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
        SkillStorage.comparText = "追剥ぎ狼は力強く嚙みついた\n" + PlayerEditor.PlayerName[target] +"は"+Damage.ToString()+"ダメージを受けた";
        StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
    }

    void WolfSkill2()
    {
        
        int target=0;
        Debug.Log("噛みつき");
        int damagetext=0;
        
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
                CharaMoveGage.ActTime[0] = 11*moveUpcorrection;
                SkillStorage.enemyActTime = CharaMoveGage.ActTime[0];
                int eAtk = (int)(EnemyManager.EnemyInfo.Enemy_ATK[0] * atkUpcorrection * richardSkill3Buff);
                Damage = eAtk;
                DamageCutController(target);
                DamageReflection(Damage);
                PlayerEditorManager.PlayerInfo.Player_HP[target] -= (int)Damage;
                damagetext+=(int)Damage;
                float hp = PlayerEditorManager.PlayerInfo.Player_HP[target];
                PlayerManager.playerHPBer[target].fillAmount = hp / PlayerEditorManager.MaxHP[target];
            }
        }
        SkillStorage.comparText = "追剥ぎ狼は素早く二度噛みついた\n合計"+damagetext.ToString()+"のダメージを受けた";
        StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        GameManager.moveEnd = true;
    }
    void WolfSkill3()
    {
        SkillStorage.comparText = "追剥ぎ狼は華麗なステップを踏んだ\n行動ゲージの上昇量が増えた";
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
        SkillStorage.comparText = "追剥ぎ狼は甲高く遠吠えした\n攻撃力が少し上昇した";
        StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        CharaMoveGage.ActTime[0] = 8  * moveUpcorrection;
        SkillStorage.enemyActTime = CharaMoveGage.ActTime[0];
        GameManager.moveEnd = true;
    }


    void ReaperSkill1()
    {
        bool flg = false;
        int target = 0;
       
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
            SkillStorage.enemyActTime = CharaMoveGage.ActTime[0];
            atkUpcorrection=1.15f;
            atkUpTurn=2;
            GameManager.moveEnd = true;
        }
        SkillStorage.comparText = "死神は大きく鎌を振りかざした\n"+PlayerEditor.PlayerName[target]+"に\n"+Damage.ToString()+"のダメージ";
        StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
    }

    void ReaperSkill2()
    {
        int target = 0;
       
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
        SkillStorage.comparText = "死神は素早く鎌を振り回した\n" + PlayerEditor.PlayerName[target] + "に\n合計" + Damage.ToString() + "のダメージ";
        StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        GameManager.moveEnd = true;
    }

    void ReaperSkill3()
    {
        SkillStorage.comparText = "死神は不気味な言葉を口にした\n味方全体が最大HPに応じて\nダメージを受けた";
        StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        for (int i=0;i<PlayerEditor.PlayerName.Length;i++)
        {
            
            float hp = PlayerEditorManager.PlayerInfo.Player_HP[i];
            if(hp>0)
            {
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
        SkillStorage.comparText = "死神はおもむろに鎌を研ぎ始めた\n鎌の鋭さとオーラが研ぎ澄まされていく";
        StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        EnemyManager.EnemyInfo.Enemy_ATK[0]*=1.1f;
        CharaMoveGage.ActTime[0] = 13 * moveUpcorrection;
        GameManager.moveEnd = true;
    }


    void StoneSkill1()
    {
      
        bool flg = false;
        int target = 0;


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
            CharaMoveGage.ActTime[0] = 10 * moveUpcorrection;
            SkillStorage.enemyActTime = CharaMoveGage.ActTime[0];
            SkillStorage.comparText = "口だけの像は魂を吸出してきた\n"+PlayerEditor.PlayerName[target]+"に\n"+Damage.ToString()+"のダメージ";
            StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
            GameManager.moveEnd = true;
        }
       
    }

    void StoneSkill2()
    {
        SkillStorage.comparText = "体から力が抜けていく・・・・・・\nブレイカーゲージが減少した";
        StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        BreakerEditor.BreakerGageCount-=20;
        CharaMoveGage.ActTime[0] = 15 * moveUpcorrection;
        GameManager.moveEnd=true;
    }

    public static bool StonePoison;
    public static int spTurn=0;
    void StoneSkill3()
    {
        if(SkillStorage.DeInvalidTime>0)
        {
            SkillStorage.comparText="口だけの像は怪しげな霧を巻いた\nしかし効果がなかった";
            StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        }
        else
        {
            SkillStorage.comparText = "口だけの像は怪しげな霧を巻いた\n味方全体は毒に侵されてしまった";
            StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
            StonePoison = true;
            spTurn = 5;
        }
        SkillStorage.comparText= "口だけの像は怪しげな霧を巻いた\n味方全体は毒に侵されてしまった";
        StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        StonePoison =true;
        spTurn=5;
        CharaMoveGage.ActTime[0] = 10 * moveUpcorrection;
        GameManager.moveEnd = true;
    }
    public static void stonePoison()
    {
        if(StonePoison&&spTurn>0)
        {
            for(int i=0;i<4;i++)
            {
                float pHP=PlayerEditorManager.PlayerInfo.Player_HP[i];
                pHP-=pHP*0.05f;
                PlayerEditorManager.PlayerInfo.Player_HP[i]=(int)pHP;
            }
            spTurn--;
        }
    }
    public static float stoneSpeedDebuff=1;
    public static int stoneSpeedTurn=0;
    void  StoneSkill4()
    {
        CharaMoveGage.ActTime[0] = 15 * moveUpcorrection;

        if (SkillStorage.DeInvalidTime > 0)
        {
            SkillStorage.comparText = "口だけの像は怪しげな霧を巻いた\nしかし効果がなかった";
            StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        }
        else
        {
            stoneSpeedDebuff = 0.7f;
            stoneSpeedTurn = 8;
            SkillStorage.comparText = "口だけの像は不気味な領域を展開した\n行動速度が遅くなるのを感じる";
            StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        }
        
       
        GameManager.moveEnd = true;
    }

    void SuccubusSkill1()
    {
        bool flg = false;
        int target = 0;
      

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
            Damage = eAtk*succubusSkill2Buff;
            DamageCutController(target);
            DamageReflection(Damage);
            PlayerEditorManager.PlayerInfo.Player_HP[target] -= (int)Damage;
            float hp = PlayerEditorManager.PlayerInfo.Player_HP[target];
            PlayerManager.playerHPBer[target].fillAmount = hp / PlayerEditorManager.MaxHP[target];
            CharaMoveGage.ActTime[0] = 7 * moveUpcorrection;
            SkillStorage.enemyActTime = CharaMoveGage.ActTime[0];
            SkillStorage.comparText = "サキュバスは魂を吸い出してきた\n" + PlayerEditor.PlayerName[target] + "に\n" + Damage.ToString() + "のダメージ";
            StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
            succubusSkill2Turn--;
            GameManager.moveEnd = true;
        }
    }

    private float succubusSkill2Buff;
    private int succubusSkill2Turn;
    void SuccubusSkill2()
    {
        float judge=0;
        for(int i=0;i<4;i++)
        {
            if(TeamCharacter.charaName[i]=="レオン"|| TeamCharacter.charaName[i] == "ゴードン"|| TeamCharacter.charaName[i] == "リチャード"|| TeamCharacter.charaName[i] == "スティアータ")
            {
                judge+=65;
            }
            else
            {
                judge+=45;
            }
        }
        int rand=Random.Range(0,101);
        if(rand<=judge)
        {
            succubusSkill2Buff=1.3f;
            succubusSkill2Turn=5;
            SkillStorage.comparText="サキュバスはこちらを魅了してきた\nしばらくの間受けるダメージが増加する\n";
        }
        else
        {
            succubusSkill2Buff=1;
            succubusSkill2Turn=0;
            SkillStorage.comparText = "サキュバスはこちらを魅了してきた\nしかしうまくいかなかった\n";
        }
        CharaMoveGage.ActTime[0] = 8 * moveUpcorrection;
        SkillStorage.enemyActTime = CharaMoveGage.ActTime[0];
        StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        GameManager.moveEnd = true;
    }

    void SuccubusSkill3()
    {
        float eAtk=EnemyManager.EnemyInfo.Enemy_ATK[0];
        Damage=eAtk/2;
        if(succubusSkill2Turn>0)
        {
            Damage=eAtk/2+eAtk*2;
        }
        for(int i=0;i<4;i++)
        {
            PlayerEditorManager.PlayerInfo.Player_HP[i]-=(int)Damage;
            DamageReflection(Damage);
        }
        SkillStorage.comparText="冒険者たちの魂を吸い出してきた\n味方全体に平均"+Damage.ToString()+"のダメージ";
        CharaMoveGage.ActTime[0] = 10 * moveUpcorrection;
        SkillStorage.enemyActTime = CharaMoveGage.ActTime[0];
        StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        GameManager.moveEnd = true;
    }

    void SuccubusSkill4()
    {
        EnemyManager.EnemyInfo.Enemy_HP[0]+=EnemyManager.EnemyInfo.Enemy_ATK[0]+EnemyManager.EnemyInfo.Enemy_HP[0]+0.7f;
        SkillStorage.comparText = "サキュバスはお菓子をばらまいた\nサキュバスのHPが回復した";
        CharaMoveGage.ActTime[0] = 10 * moveUpcorrection;
        SkillStorage.enemyActTime = CharaMoveGage.ActTime[0];
        StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        GameManager.moveEnd = true;
    }

    void GoblinSkill1()
    {
        bool flg = false;
        int target = 0;
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
            Damage = eAtk * succubusSkill2Buff;
            DamageCutController(target);
            DamageReflection(Damage);
            PlayerEditorManager.PlayerInfo.Player_HP[target] -= (int)Damage;
            float hp = PlayerEditorManager.PlayerInfo.Player_HP[target];
            PlayerManager.playerHPBer[target].fillAmount = hp / PlayerEditorManager.MaxHP[target];
            GoblinBuffDamage();
            CharaMoveGage.ActTime[0] = 5 * moveUpcorrection;
            SkillStorage.enemyActTime = CharaMoveGage.ActTime[0];
            SkillStorage.comparText = "ゴブリンは斬りかかってきた\n" + PlayerEditor.PlayerName[target] + "に\n" + Damage.ToString() + "のダメージ";
            if(goblinBuff>0)
            {
                SkillStorage.comparText+="\nさらに仲間のゴブリンから追撃を受けた";
            }
            StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
            GameManager.moveEnd = true;
        }
    }
    
    public static int goblinBuff;
    void GoblinSkill2()
    {
        if(goblinBuff<10)
        {
            goblinBuff += 5;
        }
        CharaMoveGage.ActTime[0] = 5 * moveUpcorrection;
        SkillStorage.enemyActTime = CharaMoveGage.ActTime[0];
        SkillStorage.comparText = "ゴブリンは仲間を呼んだ\nゴブリンの攻撃時に追撃が来るようになった";
        StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        GameManager.moveEnd = true;
    }


    void GoblinBuffDamage()
    {
        bool flg = false;
        int target = 0;
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
            Damage = eAtk * succubusSkill2Buff;
            DamageCutController(target);
            DamageReflection(Damage);
            PlayerEditorManager.PlayerInfo.Player_HP[target] -= (int)Damage*goblinBuff;
            float hp = PlayerEditorManager.PlayerInfo.Player_HP[target];
            PlayerManager.playerHPBer[target].fillAmount = hp / PlayerEditorManager.MaxHP[target];
        }
    }
    public static float octopusPotSkill1Buff;
    public static float octopusPostSkill1Turn;
    void OctopusPotSkill1()
    {
        octopusPostSkill1Turn=6;
        octopusPotSkill1Buff=0.75f;
        CharaMoveGage.ActTime[0] = 10 * moveUpcorrection;
        SkillStorage.enemyActTime = CharaMoveGage.ActTime[0];
        SkillStorage.comparText = "タコ壺戦士は壺を取り替えた\n新しい壺により防御力が上昇する";
        StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        GameManager.moveEnd = true;
    }

    void OctopusPotSkill2()
    {
        bool flg = false;
        int target = 0;
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
            Damage = (int)(eAtk * succubusSkill2Buff);
            DamageCutController(target);
            DamageReflection(Damage);
            PlayerEditorManager.PlayerInfo.Player_HP[target] -= (int)Damage;
            float hp = PlayerEditorManager.PlayerInfo.Player_HP[target];
            PlayerManager.playerHPBer[target].fillAmount = hp / PlayerEditorManager.MaxHP[target];
            CharaMoveGage.ActTime[0] = 9 * moveUpcorrection;
            SkillStorage.enemyActTime = CharaMoveGage.ActTime[0];
            SkillStorage.comparText = "タコ壺戦士は斬りかかってきた\n" + PlayerEditor.PlayerName[target] + "に\n" + Damage.ToString() + "のダメージ";
            StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
            GameManager.moveEnd = true;
        }
    }

    void OctopusPotSkill3()
    {
        int rand=Random.Range(1,4);
        int allDamage=0;
        for(int i=0;i<rand;i++)
        {
            bool flg = false;
            int target = 0;
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
                Damage = eAtk * succubusSkill2Buff;
                allDamage+=(int)Damage;
                DamageCutController(target);
                DamageReflection(Damage);
                PlayerEditorManager.PlayerInfo.Player_HP[target] -= (int)Damage;
                float hp = PlayerEditorManager.PlayerInfo.Player_HP[target];
                PlayerManager.playerHPBer[target].fillAmount = hp / PlayerEditorManager.MaxHP[target];
            }

            CharaMoveGage.ActTime[0] = 15 * moveUpcorrection;
            SkillStorage.enemyActTime = CharaMoveGage.ActTime[0];
            SkillStorage.comparText = "タコ壺戦士は剣を振り回した\n合計" +allDamage.ToString()+  "のダメージ";
            StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
            GameManager.moveEnd = true;
        }
    }

    public static int octopusPotSkill4Turn;
    public static int octopusPotSkill4Buff;
    void OctopusPotSkill4()
    {

        octopusPotSkill4Turn = 1;
        octopusPotSkill4Buff = 0;
        CharaMoveGage.ActTime[0] = 20 * moveUpcorrection;
        SkillStorage.enemyActTime = CharaMoveGage.ActTime[0];
        SkillStorage.comparText = "タコ壺戦士は守りの体制を取った\nどんな攻撃も受け流しそうだ";
        StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        GameManager.moveEnd = true;
    }
    public static int kerberosBuff;
    public static int kerberosBuffTurn;
    void KerberosSkill1()
    {
        kerberosBuff=2;
        kerberosBuffTurn=10;
        CharaMoveGage.ActTime[0] = 6 * moveUpcorrection;
        SkillStorage.enemyActTime = CharaMoveGage.ActTime[0];
        SkillStorage.comparText="ケルベロスは天に向かって遠吠えした\n攻撃力がかなり上昇した";
        StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        GameManager.moveEnd = true;
    }
    
    void KerberosSkill2()
    {
        bool flg = false;
        int target = 0;
        for(int i=0;i<3;i++)
        {
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
                Damage = eAtk * kerberosBuff;
                DamageCutController(target);
                DamageReflection(Damage);
                PlayerEditorManager.PlayerInfo.Player_HP[target] -= (int)Damage;
                float hp = PlayerEditorManager.PlayerInfo.Player_HP[target];
                PlayerManager.playerHPBer[target].fillAmount = hp / PlayerEditorManager.MaxHP[target];
                GoblinBuffDamage();
                CharaMoveGage.ActTime[0] = 9 * moveUpcorrection;
                SkillStorage.enemyActTime = CharaMoveGage.ActTime[0];
                
            }
            SkillStorage.comparText = "ケルベロスは三つの頭で噛みついてきた\n" + "平均" + Damage.ToString() + "のダメージ";
            StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
            GameManager.moveEnd = true;
        }
    }

    public static int kerberosPoisonTurn;
    void KerberosSkill3()
    {
        kerberosPoisonTurn++;
        CharaMoveGage.ActTime[0] = 4 * moveUpcorrection;
        SkillStorage.enemyActTime = CharaMoveGage.ActTime[0];
        SkillStorage.comparText="ケルベロスは大きな舌で舐めまわしてきた\n冒険者たちは毒に侵されてしまった";
        StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        GameManager.moveEnd = true;
    }

    public static void KerberosPoison()
    {
        if(kerberosPoisonTurn>0)
        {
            for (int i = 0; i < 4; i++)
            {
                PlayerEditorManager.PlayerInfo.Player_HP[i] -= (int)EnemyManager.EnemyInfo.Enemy_ATK[0];
            }
        }
        kerberosPoisonTurn--;
    }
    
    private bool doragonSkill1Flag;
    public static float doragonSkill1Buff;
    void DoragonSkill1()
    {
        Debug.Log("skill1");
        CharaMoveGage.ActTime[0] = 4 * moveUpcorrection;
        SkillStorage.enemyActTime = CharaMoveGage.ActTime[0];
        if(!doragonSkill1Flag)
        {
            SkillStorage.comparText = "ドラゴンは障壁を展開した\n次の行動まで攻撃は効かなそうだ";
        }
        else
        {
            SkillStorage.comparText="ドラゴンは再度障壁を展開した\n次の行動まで攻撃は効かなそうだ";
            
        }
        doragonSkill1Flag = true;
        doragonSkill1Buff = 0;
        StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        GameManager.moveEnd = true;
    }

    void DoragonSkill2()
    {
        Debug.Log("skill2");
        int allDamage=0;
        for(int i=0;i<4;i++)
        {
            allDamage+=PlayerEditorManager.PlayerInfo.Player_HP[i]/2;
            float hp = PlayerEditorManager.PlayerInfo.Player_HP[i];
            hp/=2;
            PlayerManager.playerHPBer[i].fillAmount = hp / PlayerEditorManager.MaxHP[i];
            PlayerEditorManager.PlayerInfo.Player_HP[i]=(int)hp;
        }
        CharaMoveGage.ActTime[0] =15 * moveUpcorrection;
        SkillStorage.enemyActTime = CharaMoveGage.ActTime[0];
        if(doragonSkill1Flag)
        {
            SkillStorage.comparText = "ドラゴンは障壁を解除した\n";
            doragonSkill1Buff = 1;
            doragonSkill1Flag = false;
        }

        SkillStorage.comparText += "ドラゴンは口から業火を吐き出した\n味方全体に平均\n"+((int)allDamage/4).ToString()+"のダメージ";
        StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        GameManager.moveEnd = true;
    }

    void DoragonSkill3()
    {
        Debug.Log("skill3");
        int Count=0;
        bool flg=false;
        int target=0;
        for(int i=0;i<4;i++)
        {
            if(PlayerEditorManager.PlayerInfo.Player_HP[i]>0)
            {
                Count++;
            }
        }
        if(Count>2)
        {
            Count=2;
        }
        for (int i = 0; i < Count; i++)
        {
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
                Damage = (int)eAtk ;
                DamageCutController(target);
                DamageReflection(Damage);
                PlayerEditorManager.PlayerInfo.Player_HP[target] -= (int)Damage;
                float hp = PlayerEditorManager.PlayerInfo.Player_HP[target];
                PlayerManager.playerHPBer[target].fillAmount = hp / PlayerEditorManager.MaxHP[target];

            }
            CharaMoveGage.ActTime[0] = 10 * moveUpcorrection;
            SkillStorage.enemyActTime = CharaMoveGage.ActTime[0];
            if(doragonSkill1Flag)
            {
                SkillStorage.comparText="ドラゴンは障壁を解除した\n";
                doragonSkill1Buff=1;
                doragonSkill1Flag=false;
            }
        }
        SkillStorage.comparText += "ドラゴンは巨体を揺らし暴れ始めた\n" + "平均" + Damage.ToString() + "のダメージ";
        StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        GameManager.moveEnd = true;
    }

    void DoragonSkill4()
    {
        Debug.Log("skill4");
        EnemyManager.EnemyInfo.Enemy_HP[0]+=(int)EnemyManager.EnemyInfo.Enemy_HP[0]/10;
        CharaMoveGage.ActTime[0] = 5 * moveUpcorrection;
        SkillStorage.enemyActTime = CharaMoveGage.ActTime[0];
        if (doragonSkill1Flag)
        {
            SkillStorage.comparText = "ドラゴンは障壁を解除した\n";
            doragonSkill1Buff = 1;
            doragonSkill1Flag = false;
        }
        SkillStorage.comparText = "ドラゴンは羽休めを行った\nドラゴンのHPが少し回復した";
        StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        GameManager.moveEnd = true;
    }

    void DoragonSkillPoint()
    {
        if(CharaMoveGage.enemyName=="ドラゴン")
        {
            if (EnemyManager.EnemyInfo.Enemy_HP[0] / EnemyManager.maxEnemyHP[0] > 0.75f)
            {
                EnemySkill[3]=0;
            }
            else
            {
                EnemySkill[3]=10;
            }
            if(EnemyManager.EnemyInfo.Enemy_HP[0]/EnemyManager.maxEnemyHP[0]>0.15)
            {
                EnemySkill[1]=0;
            }
            else
            {
                EnemySkill[1]=45;
            }
        }
        
    }

    void PegasusSkill1()
    {
        bool flg=false;
        int target=0;
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
            int eAtk = (int)(EnemyManager.EnemyInfo.Enemy_ATK[0] * 2.5f* richardSkill3Buff);
            Damage = (int)eAtk;
            DamageCutController(target);
            DamageReflection(Damage);
            PlayerEditorManager.PlayerInfo.Player_HP[target] -= (int)Damage;
            float hp = PlayerEditorManager.PlayerInfo.Player_HP[target];
            PlayerManager.playerHPBer[target].fillAmount = hp / PlayerEditorManager.MaxHP[target];

        }
        CharaMoveGage.ActTime[0] = 10 * moveUpcorrection;
        SkillStorage.enemyActTime = CharaMoveGage.ActTime[0];
        SkillStorage.comparText += "「 我、今こそ万物を粛清せん 」\n" + PlayerEditor.PlayerName[target] + "に\n" + Damage.ToString() + "のダメージ";
        StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        GameManager.moveEnd = true;
    }

    void PegasusSkill2()
    {
        for(int i=0;i<4;i++)
        {
                int eAtk = (int)(EnemyManager.EnemyInfo.Enemy_ATK[0]  * richardSkill3Buff);
                Damage = (int)eAtk;
                DamageCutController(i);
                DamageReflection(Damage);
                PlayerEditorManager.PlayerInfo.Player_HP[i] -= (int)Damage;
                float hp = PlayerEditorManager.PlayerInfo.Player_HP[i];
                PlayerManager.playerHPBer[i].fillAmount = hp / PlayerEditorManager.MaxHP[i];
          
          
        }
        CharaMoveGage.ActTime[0] = 10 * moveUpcorrection;
        SkillStorage.enemyActTime = CharaMoveGage.ActTime[0];
        SkillStorage.comparText += "「 聞こえるか、\n汝に下す雷鳴と審判の音が 」\n平均" +Damage.ToString() + "のダメージ";
        StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        GameManager.moveEnd = true;
    }

    void PegasusSkill3()
    {
        int rand=0;
        List<int> oldrand=new List<int>();
        for(int i=0;i<3;i++)
        {
            bool flag=false;
            while(!flag)
            {
                rand = Random.Range(0, 5);
                for(int j=0;j<oldrand.Count;j++)
                {
                    if(oldrand[i]==rand)
                    {
                        flag=false;
                        break;
                    }
                    else
                    {
                        flag=true;
                    }
                }
                oldrand.Add(rand);
            }
            
            switch(rand)
            {
                case 0://口だけの像　スキル2
                    {
                        BreakerEditor.BreakerGageCount -= 20;
                    }
                    break;
                case 1:
                    {
                        StonePoison = true;
                        spTurn = 5;
                    }
                    break;
                case 2:
                    {
                        stoneSpeedDebuff = 0.7f;
                        stoneSpeedTurn = 8;
                    }
                    break;
                case 3:
                    {
                        float judge = 0;
                        for (int j = 0; j < 4; j++)
                        {
                            if (TeamCharacter.charaName[j] == "レオン" || TeamCharacter.charaName[j] == "ゴードン" || TeamCharacter.charaName[j] == "リチャード" || TeamCharacter.charaName[j] == "スティアータ")
                            {
                                judge += 65;
                            }
                            else
                            {
                                judge += 45;
                            }
                        }
                        int random = Random.Range(0, 101);
                        if (random <= judge)
                        {
                            succubusSkill2Buff = 1.3f;
                            succubusSkill2Turn = 5;
                        }
                        else
                        {
                            succubusSkill2Buff = 1;
                            succubusSkill2Turn = 0;
                        }
                    }
                    break;
                    case 4:
                    {
                        kerberosPoisonTurn++;
                    }
                    break;
            }
            CharaMoveGage.ActTime[0] = 10 * moveUpcorrection;
            SkillStorage.enemyActTime = CharaMoveGage.ActTime[0];
            SkillStorage.comparText= "「 天に昇りし者たちの\n憎悪を知れ 」\n倒した命の数々が\n背筋を伝う……";
            StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        }
        GameManager.moveEnd = true;
    }
    public static bool pegasusSkill4Flag;
    void PegasusSkill4()
    {
        for (int i = 0; i < 4; i++)
        {
            if(PlayerEditorManager.PlayerInfo.Player_HP[i]!=0)
            {
                PlayerEditorManager.PlayerInfo.Player_HP[i]=1;
            }
        }
        pegasusSkill4Flag = true;
        CharaMoveGage.ActTime[0] = 10 * moveUpcorrection;
        SkillStorage.enemyActTime = CharaMoveGage.ActTime[0];
        SkillStorage.comparText = "「 全てを、終焉へと導かん……！ 」\n味方全体のHPが１になった\nペガサス は大きく体勢を崩した";
        StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
        GameManager.moveEnd = true;
    }

    bool PegasusFirstFlag;
    void PegasusSkillPoint()
    {
        if(CharaMoveGage.enemyName=="ペガサス")
        {
            if (!PegasusFirstFlag)
            {
                EnemySkill[0] = 0;
                EnemySkill[1] = 0;
                PegasusFirstFlag = true;
            }
            else
            {
                EnemySkill[0]=35;
                EnemySkill[1]=40;
            }
            if(EnemyManager.EnemyInfo.Enemy_HP[0]/EnemyManager.maxEnemyHP[0]<0.05f)
            {
                EnemySkill[3]=150;
            }
        }
        
    }
    void PartyCharaAlive()
    {
        if(GameManager.state==GameManager.BattleState.start)
        { 
            for(int i=0;i<partyChara.transform.childCount;i++)
            {
               GameObject obj= partyChara.transform.GetChild(i).gameObject;
                if(obj.name != "NullPrefab(Clone)") { 
               GameObject mobj=obj.transform.Find("HP").gameObject;
                GameObject hpobj=mobj.transform.Find("HPGreen").gameObject;
               charaAlive[i]=hpobj.GetComponent<Image>();
                }
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
        if (eN == "口だけの像")
        {
            switch (skillNumber)
            {
                case 0:
                    {
                        StoneSkill1();

                    }
                    break;
                case 1:
                    {
                        StoneSkill2();
                    }
                    break;
                case 2:
                    {
                        StoneSkill3();
                    }
                    break;
                case 3:
                    {
                        StoneSkill4();
                    }
                    break;
            }
        }

        if(eN=="サキュバス")
        {
            switch (skillNumber)
            {
                case 0:
                    {
                        SuccubusSkill1();

                    }
                    break;
                case 1:
                    {
                        SuccubusSkill2();
                    }
                    break;
                case 2:
                    {
                        SuccubusSkill3();
                    }
                    break;
                case 3:
                    {
                        SuccubusSkill4();
                    }
                    break;
            }
        }
        if (eN == "ゴブリン")
        {
            switch (skillNumber)
            {
                case 0:
                    {
                        GoblinSkill1();
                    }
                    break;
                case 1:
                    {
                        GoblinSkill2();
                    }
                    break;
            }

        }
        if (eN == "タコ壺戦士")
        {
            switch (skillNumber)
            {
                case 0:
                    {
                        OctopusPotSkill1();

                    }
                    break;
                case 1:
                    {
                        OctopusPotSkill2();
                    }
                    break;
                case 2:
                    {
                        OctopusPotSkill3();
                    }
                    break;
                case 3:
                    {
                        OctopusPotSkill4();
                    }
                    break;
            }
        }
        if (eN == "ケルベロス")
        {
            switch (skillNumber)
            {
                case 0:
                    {
                        KerberosSkill1();

                    }
                    break;
                case 1:
                    {
                        KerberosSkill2();
                    }
                    break;
                case 2:
                    {
                        KerberosSkill3();
                    }
                    break;
            }
        }
        if (eN == "ドラゴン")
        {
            switch (skillNumber)
            {
                case 0:
                    {
                        DoragonSkill1();

                    }
                    break;
                case 1:
                    {
                        DoragonSkill2();
                    }
                    break;
                case 2:
                    {
                        DoragonSkill3();
                    }
                    break;
                case 3:
                    {
                        DoragonSkill4();
                    }
                    break;
            }
        }
        if(eN=="ペガサス")
        {
            switch (skillNumber)
            {
                case 0:
                    {
                        PegasusSkill1();

                    }
                    break;
                case 1:
                    {
                        PegasusSkill2();
                    }
                    break;
                case 2:
                    {
                        PegasusSkill3();
                    }
                    break;
                case 3:
                    {
                        PegasusSkill4();
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
            if(PlayerEditor.PlayerName[i] != "" && PlayerEditor.PlayerName[i] != null) {
                if (GameManager.aliveFlag[i + 1])
                {
                    MaxHate += PlayerEditorManager.PlayerInfo.Player_Hate[i];
                }
            }
        }
        Debug.Log(MaxHate);
        int target=Random.Range(0,MaxHate+1);
        for(int i=0;i<4;i++)
        {
            if(PlayerEditor.PlayerName[i] != "" && PlayerEditor.PlayerName[i] != null) {
                if (GameManager.aliveFlag[i + 1])
                {
                    target -= PlayerEditorManager.PlayerInfo.Player_Hate[i];
                }
           
                if(target<=0)
                {
                    if(partyChara.transform.GetChild(i).gameObject.name =="リチャード"&&SkillStorage.richardSkill3Avoidance)
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

   void poisonDamage()
    {
        
    }

}
   
