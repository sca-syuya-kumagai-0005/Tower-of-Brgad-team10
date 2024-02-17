using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using static MoveTextController;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]private Text EMT;
    [SerializeField]
    int[] WolfSkill;
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
                CharaMoveGage.ActTime[0] = 1*moveUpcorrection;
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
            Damage = eAtk;
            DamageCutController(target);
            DamageReflection(Damage);
            PlayerEditorManager.PlayerInfo.Player_HP[target] -= (int)Damage;
            float hp = PlayerEditorManager.PlayerInfo.Player_HP[target];
            PlayerManager.playerHPBer[target].fillAmount = hp / PlayerEditorManager.MaxHP[target];
            CharaMoveGage.ActTime[0] = 10 * moveUpcorrection;
            SkillStorage.enemyActTime = CharaMoveGage.ActTime[0];
            SkillStorage.comparText = "口だけの像は魂を吸出してきた\n" + PlayerEditor.PlayerName[target] + "に\n" + Damage.ToString() + "のダメージ";
            StartCoroutine(MoveTextController.moveTextCoroutine(SkillStorage.comparText));
            GameManager.moveEnd = true;
        }
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
   
