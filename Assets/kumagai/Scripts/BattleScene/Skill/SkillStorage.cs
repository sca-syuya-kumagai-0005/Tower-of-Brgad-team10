using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerEditorManager;
using static MoveTextController;
using static BuffManager;

public class SkillStorage : MonoBehaviour
{
    public static string moveTextText;
    public static string DamageText;
    public static string targetText;
    public static string moveEffectText;
    public static string comparText;
    [SerializeField]
    private Text skilltext;
    public static float breakerRate;
    public  static float rate;
    [SerializeField]
    private float tmpRate;
    [SerializeField]
    private float pATKCorrect;
    public static int playerSkill3;
    [SerializeField]
    private float playerSkill3Buff;
    public static int CommandCount;
    public static float addDamage;
    [SerializeField]
    private int gordonHate;
    public static float enemyActTime=4;
    public static float atkBuff;
    public static IEnumerator croutine;
    [SerializeField]
    private Text moveMember;

    private void Start()
    {
        p2AtkUpTime=0;
        annSkill3Time=0;
        annBreakerTime=0;
        DameCutTime=0;
        atkDownTime=0;
        gordonBreakerTime=0;
       DeSpeedTime=0;DeInvalidTime=0;
        reCoveryTargetFlg=false;
        rinBreakerTime=0;
        MagicBarrelTime=0;
        DoctorAtkBuffTime=0;
        sleep=false;
        melodyBuffTime=0;
        imnTime=0;
        gabTime=0;
        richardSkill2Time=0;
        richardSkill3Time=0;
    }
    private void Update()
    {
        tmpRate=rate;
        tmpSleep=sleep;
        AddStatus();
        rate = NotesEditor.NotesOKCount / CommandCount;
        //if (CharaMoveGage.MoveChar==null||CharaMoveGage.MoveChar[0].name=="Enemy") {
        //    charaNumber=-1;
        //}
       ATKBuff();
        if (CharaMoveGage.MoveChar.Count!=0)
        { 
            CharaSet();
        } 
        if(CharaMoveGage.MoveChar.Count!=0)
        {
            CharNumberGet();
        }
            
        
        if(BreakerEditor.commandEnd)
        {
            breakerRate = BreakerEditor.NotesOKCount / CommandCount;
            CharaSet();
        }
        if(NotesEditor.commandEnd)
        {
            rate = NotesEditor.NotesOKCount / CommandCount;
            CharaSet();
        }
        if(GameManager.state==GameManager.BattleState.move)
        {

        }
        BuffTimeStorage();
        if (rate >= 1)
        {
            rate = 1;
        }
           
        
    }
    [SerializeField]
    private GameObject partyChara;
    public static int charaNumber;
    public static void PlayerSkillExecution()
    {
      
    }
    private const float p2AtkUpMaxTime=45f;
    private float p2AtkUpTime;
    private void PlayerSkill()
    {
       // moveMember.text =  "レオンは\nどうする？";
        switch (SkillSelection.SkillNumber)
        {
            case 0:
                {//スラッシュ
                   
                    if(GameManager.state==GameManager.BattleState.skillSelect)
                    { 
                        NotesEditor.skillName="スラッシュ";
                    }
                    if (GameManager.state==GameManager.BattleState.move)
                    {
                        float pAtk= PlayerInfo.Player_ATK[charaNumber] * atkBuff + atkStatusBuff;
                        addDamage=(pAtk*rate)*2;
                        Debug.Log(melodyBuff);
                        float ehp= EnemyManager.EnemyInfo.Enemy_HP[0]- addDamage;
                        EnemyManager.EnemyInfo.Enemy_HP[0] = ehp;
                        EnemyManager.debugHPBer.fillAmount=ehp/EnemyManager.maxEnemyHP[0];
                        DamageText=((int)(addDamage)).ToString()+"のダメージ";
                        targetText=EnemyNameGet.enemyNameText.ToString()+"に";
                        comparText="スラッシュを繰り出した"+"\n"+targetText+DamageText;
                        StartCoroutine(moveTextCoroutine(comparText));
                        GameManager.moveEnd=true;
                    }
                }
                break;
                case 1://闘志入魂
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "闘志入魂";
                    }
                    
                    if (GameManager.state == GameManager.BattleState.move)
                    { 
                    pATKCorrect = (NotesEditor.NotesOKCount / CommandCount);
                    p2AtkUpTime=p2AtkUpMaxTime;
                    comparText="闘志入魂を繰り出した\n味方全体の攻撃力が上昇した";
                       publicPBuffStorage.Add(0);
                        StartCoroutine(moveTextCoroutine(comparText));
                        GameManager.moveEnd=true;
                    }
                }
                break;
                case 2:
                {

                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "妨害工作";
                    }
                   if(GameManager.state==GameManager.BattleState.move)
                    { 
                        playerSkill3Buff =(rate*100/20f)/100;
                        playerSkill3=1;
                        publicEDeBuffStorage.Add(0);
                        comparText="妨害工作を繰り出した\n敵の被ダメージが上昇する";
                        StartCoroutine(moveTextCoroutine(comparText));
                        comparText = "";
                        GameManager.moveEnd = true;
                    }
                }
                break;
            case 3:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "応急手当";
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                       float php=PlayerInfo.Player_HP[charaNumber];
                        php+=(((rate*100*0.3f)/100))*PlayerEditorManager.MaxHP[charaNumber];
                        PlayerInfo.Player_HP[charaNumber]=(int)php;
                        PlayerManager.playerHPBer[charaNumber].fillAmount=PlayerInfo.Player_HP[charaNumber]/PlayerEditorManager.MaxHP[charaNumber];
                        comparText="応急手当を繰り出した\nレオンのHPが"+ (((rate * 100 * 0.3f) / 100)) * PlayerEditorManager.MaxHP[charaNumber]+"回復した";
                        StartCoroutine(moveTextCoroutine(comparText));
                        comparText = "";
                        GameManager.moveEnd = true;
                    }
                }
                break;
            case 4:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        BreakerEditor.skillName = "勇気の軌跡";
                        BreakerEditor.allTime = 65*(2f-addSpeed);
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        float pAtk = PlayerInfo.Player_ATK[charaNumber]*atkBuff;
                        float ehp = EnemyManager.EnemyInfo.Enemy_HP[0] - pAtk * (breakerRate)*GameManager.aliveCount + atkStatusBuff;
                        Debug.Log("ダメージは"+pAtk * breakerRate * GameManager.aliveCount);
                        EnemyManager.EnemyInfo.Enemy_HP[0] = ehp;
                        EnemyManager.debugHPBer.fillAmount = ehp / EnemyManager.maxEnemyHP[0];
                        targetText=EnemyNameGet.enemyNameText+"に";
                        comparText="勇気の軌跡を繰り出した\n" + targetText+ (int)(pAtk * (breakerRate) * GameManager.aliveCount)+"のダメージ";
                        StartCoroutine(moveTextCoroutine(comparText));
                        comparText = "";
                        BreakerEditor.BreakerGageCount = 0;
                        BreakerEditor.breakerGageMax = false;
                        GameManager.moveEnd = true;
                    }
                    break;
                }
        }
    }
    
    public static float addSpeed=1;
    public static int addSpeedTurn;
    [SerializeField]
    private float rateCorrection;
    public static float annaSKill3;
    public static float annaSkill3MaxTime;
    public static float annSkill3Time;
    public static float annBreakerMaxTime;
    public static float annBreakerTime;
    void AnnaSkill()
    {
        //moveMember.text="アンナリーナは\nどうする？";
        switch (SkillSelection.SkillNumber)
        {
            case 0:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "加速する未来";
                       
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        addSpeed=1.3f;
                        addSpeedTurn = 0;
                        for(float i=0.2f;i<5;i+=0.2f) 
                        {
                            if(rate>=i) {
                                addSpeedTurn++;
                            }
                        }
                        comparText="加速する未来を繰り出した\n"+"コマンドの流れる速度が変化した";
                        StartCoroutine(moveTextCoroutine(comparText));
                        comparText = "";

                        GameManager.moveEnd = true;
                    }
                    break;
                }
                case 1:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "減速する過去";
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        addSpeed =0.7f;
                        for(float i = 0.2f; i < 5; i += 0.2f) 
                        {
                            if(rate >= i) 
                            {
                                addSpeedTurn++;
                            }
                        }
                    }
                        comparText = "減速する過去を繰り出した\nコマンドの流れる速度が変化した";
                        StartCoroutine(moveTextCoroutine(comparText));
                        comparText = "";
                        GameManager.moveEnd = true;
                    }
                break;
               
                case 2:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "ありえた選択";
                    }
                    if(GameManager.state==GameManager.BattleState.move)
                    {
                        rateCorrection=(rate*100*0.3f)/100;
                        comparText="ありえた選択を繰り出した\nコマンドの成功率が上昇した";
                        publicPBuffStorage.Add(1);
                        StartCoroutine(moveTextCoroutine(comparText));
                        GameManager.moveEnd = true;
                    }
                }
                break;
                case 3:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "結末への調整";
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        float ehp= EnemyManager.EnemyInfo.Enemy_HP[0];
                        ehp -=ehp * (rate * 100 * 0.01f)*0.1f;
                        Debug.Log("ehpは"+ehp);
                        EnemyManager.EnemyInfo.Enemy_HP[0]=ehp;
                        EnemyManager.debugHPBer.fillAmount = ehp / (float)EnemyManager.maxEnemyHP[0];
                        DamageText=((int)(ehp * (rate * 100 * 0.01f) * 0.1f)).ToString();
                        targetText=EnemyNameGet.enemyNameText+"に";
                        comparText ="結末への調整を繰り出した\n"+targetText+DamageText+"のダメージを与えた";
                        StartCoroutine(moveTextCoroutine(comparText));
                        comparText = "";
                        GameManager.moveEnd = true;
                    }
                }
                break;
                case 4:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        BreakerEditor.skillName = "定められし運命";
                        BreakerEditor.allTime = 50*(2f-addSpeed);
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        annBreakerMaxTime =breakerRate*100+25;
                        annBreakerTime=annBreakerMaxTime;
                        comparText= "定められし運命を繰り出した\n今こそ運命を、Aの一文字に！";
                        publicPBuffStorage.Add(2);
                        StartCoroutine(moveTextCoroutine(comparText));
                        comparText = "";
                        BreakerEditor.BreakerGageCount=0;

                        BreakerEditor.breakerGageMax = false;
                        GameManager.moveEnd = true;
                    }
                }
                break;
        }
    }



    public static float DameCutPar;
    public static float DameCutTime;
    public  const float DameCutMaxTime=30f;
    public static int gordonHateCorrection;
    public static float hateUpTime;
    public static float hateUpMaxTime;
    public static float atkDownDeBuff;
    public static float atkDownTime;
    public static float atkDownMaxTime;
    public static float gordonBreakerMaxTime;
    public static float gordonBreakerTime;
    public static int gordonHateUp=0;
    public static int gordonCharaNumber;
    void GorDonSkill()
    {
        //moveMember.text = "ゴードンは\nどうする？";
        switch (SkillSelection.SkillNumber)
        {
            case 0:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "守護の構え";

                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        DameCutPar=(rate*100)-40f;
                        DameCutTime=DameCutMaxTime;
                        publicPBuffStorage.Add(3);
                        comparText ="守護の構えを繰り出した\nゴードンがダメージを一部防げるようになった";
                        StartCoroutine(moveTextCoroutine(comparText));
                        comparText = "";
                        GameManager.moveEnd = true;
                    }
                    break;
                }
            case 1:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "挑発";
                    }
                    if(GameManager.state==GameManager.BattleState.move)
                    {
                        if(gordonHateCorrection>=50)
                        {
                            PlayerEditorManager.PlayerInfo.Player_Hate[gordonCharaNumber] -= 50;
                            gordonHateCorrection -=50;
                        }
                        gordonHateCorrection += 50;
                        PlayerEditorManager.PlayerInfo.Player_Hate[gordonCharaNumber] += gordonHateCorrection;
                        gordonCharaNumber =charaNumber;
                        hateUpMaxTime=rate*100;
                        hateUpTime=hateUpMaxTime;
                        //publicPBuffStorage.Add(4);
                        comparText ="挑発を繰り出した\n敵から狙われやすくなった";
                        StartCoroutine(moveTextCoroutine(comparText));
                        comparText = "";
                        GameManager.moveEnd = true;
                    }
                }
                break;
                case 2:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "威圧";
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        float pAtk=PlayerInfo.Player_ATK[charaNumber];
                        atkDownDeBuff=(int)(rate*pAtk*0.3f);
                        Debug.Log(atkDownDeBuff);
                        atkDownMaxTime= rate * 100 * 0.6f;
                        Debug.Log(atkDownMaxTime);
                        atkDownTime =atkDownMaxTime;
                        publicEDeBuffStorage.Add(1);
                        comparText="威圧を繰り出した\n敵の攻撃力が減少した";
                        StartCoroutine(moveTextCoroutine(comparText));
                        comparText = "";
                        GameManager.moveEnd = true;
                    }
                }
                break;
                case 3:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "刺突";
                    }
                    if(GameManager.state == GameManager.BattleState.move)
                    {
                        float pAtk = PlayerInfo.Player_ATK[charaNumber];
                        addDamage = (pAtk * rate);
                        float ehp = EnemyManager.EnemyInfo.Enemy_HP[0] -addDamage;
                        EnemyManager.EnemyInfo.Enemy_HP[0] = ehp;
                        EnemyManager.debugHPBer.fillAmount = ehp / EnemyManager.maxEnemyHP[0];
                        targetText=EnemyNameGet.enemyNameText;
                        DamageText=
                        comparText="刺突を繰り出した\n"+targetText+"に"+ ((int)addDamage).ToString()+"のダメージ" ;
                        StartCoroutine(moveTextCoroutine(comparText));
                        comparText = "";
                        GameManager.moveEnd = true;
                    }
                }
                break;
                case 4:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        BreakerEditor.skillName = "絶対防衛陣";
                        BreakerEditor.allTime = 85*(2f - addSpeed);
                    }
                     if(GameManager.state==GameManager.BattleState.move)
                    {
                        if (gordonHateCorrection >= 50)
                        {
                            PlayerEditorManager.PlayerInfo.Player_Hate[gordonCharaNumber] -= 50;
                            gordonHateCorrection -= 50;
                        }
                        gordonHateCorrection += 50;
                        PlayerEditorManager.PlayerInfo.Player_Hate[gordonCharaNumber] += gordonHateCorrection;
                        gordonBreakerMaxTime = breakerRate * 130+130;
                        gordonBreakerTime=gordonBreakerMaxTime;
                        comparText="絶対防衛陣を繰り出した\nどんな攻撃も防いで見せましょう！";
                        StartCoroutine(moveTextCoroutine(comparText));
                        publicPBuffStorage.Add(4);
                        comparText = "";
                        BreakerEditor.BreakerGageCount = 0;

                        BreakerEditor.breakerGageMax = false;
                        GameManager.moveEnd = true;
                    }
                }
                break;
                
        }
    }
    public static float DeBuffSpeed=1;
    public static float DeSpeedTime;
    private const float DeSpeedMaxTime=20;
    public static float DeInvalidTime;
    public static float DeInvalidMaxTime;
    public static int RefrectCount;
    public static float RefrectDamage;
    public static bool reCoveryTargetFlg=false;
    public static float rinBreakerTime;
    public static float rinBreaker;
    private int recoveryTarget;
    void RinSkill()
    {
        //moveMember.text = "平櫛凛は\nどうする？";
        switch (SkillSelection.SkillNumber)
        {
            case 0:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "呪符：鈍足香";
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        DeBuffSpeed=((2-rate)/2);
                        DeSpeedTime=DeSpeedMaxTime;
                        comparText="呪符:鈍足香を繰り出した\n敵の行動速度が減少した";
                        publicEDeBuffStorage.Add(2);
                        StartCoroutine(moveTextCoroutine(comparText));
                        comparText = "";
                        GameManager.moveEnd=true;
                    }
                }
                break;
            case 1:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "御神水です！";
                    }
                    if(GameManager.state==GameManager.BattleState.move)
                    {
                        recoveryTarget=0;
                        if(!reCoveryTargetFlg)
                        {
                            recoveryTarget = RecoverySubject(recoveryTarget);
                            reCoveryTargetFlg=true;
                        }
                        Debug.Log(recoveryTarget);
                        float php=PlayerInfo.Player_HP[recoveryTarget];
                        Debug.Log(PlayerInfo.Player_HP);
                        php+=(int)((php*0.3f)+PlayerInfo.Player_ATK[charaNumber]);
                        Debug.Log(php);
                        PlayerInfo.Player_HP[recoveryTarget] =(int)php;
                        PlayerManager.playerHPBer[recoveryTarget].fillAmount=php/PlayerEditorManager.MaxHP[recoveryTarget];
                        comparText="御神水です!を繰り出した\n"+partyChara.transform.GetChild(recoveryTarget).gameObject.name+"のHPが"+
                        ((int)(php / PlayerEditorManager.MaxHP[recoveryTarget])).ToString()+"回復した";
                        StartCoroutine(moveTextCoroutine(comparText));
                        comparText = "";
                        GameManager.moveEnd=true;
                    }
                }
                break;
            case 2:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "護符：厄払";
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        DeInvalidMaxTime=((rate*100)*0.3f)+10;
                        DeInvalidTime=DeInvalidMaxTime;
                        comparText="護符:厄払を繰り出した\n一定時間弱体化攻撃を無効化する";
                        StartCoroutine(moveTextCoroutine(comparText));
                        publicPBuffStorage.Add(5);
                        comparText = "";
                        GameManager.moveEnd=true;
                    }
                }
                break;
             case 3:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "霊符：風鎌";
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        RefrectCount=0;
                        RefrectDamage=(PlayerInfo.Player_ATK[charaNumber]*pATKCorrect)/2;
                        for(int i=(int)(rate*100);i<=-1;i-=25)
                        {
                            RefrectCount++;
                        }
                        comparText="霊符:風鎌を繰り出した\nダメージの一部を反射するようになった";
                        StartCoroutine(moveTextCoroutine(comparText));
                        publicPBuffStorage.Add(6);
                        comparText = "";
                        GameManager.moveEnd = true;
                    }
                }
                break;
             case 4:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        BreakerEditor.skillName = "禁符：御法の障壁";
                        BreakerEditor.allTime = 50* (2f - addSpeed);
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                       
                        rinBreakerTime =130;
                        rinBreaker=90 * breakerRate;
                        comparText="禁符:御法の障壁を繰り出した\n味方全体がダメージの一部を防げるようになった";
                        StartCoroutine(moveTextCoroutine(comparText));
                        publicPBuffStorage.Add(7);
                        comparText = "";
                        BreakerEditor.BreakerGageCount = 0;

                        BreakerEditor.breakerGageMax = false;
                        GameManager.moveEnd = true;
                    }
                }
                break;
        }
    }

    public static int MagicBarrel;
    public static float MagicBarrelTime=0;
    private float maxMagicBarrelTime;
    public static float MagicBarrelBuff=0.5f;
    public static bool NextBarret;
    public static bool nowTurnExclusion;

    void LetitiaSkill()
    {
        switch (SkillSelection.SkillNumber)
        {
            case 0:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "サンバレット";
                        moveTextFlag = true;
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        int rand=Random.Range(1,6);
                        addDamage =(PlayerInfo.Player_ATK[charaNumber]*rate*atkBuff + atkStatusBuff )* rand+(rand*atkStatusBuff);
                        Debug.Log("ダメージは"+addDamage);
                        comparText =　"サンバレットを繰り出した\n"+CharaMoveGage.enemyName+"は"+((int)addDamage).ToString()+"のダメージを受けた";
                        StartCoroutine(moveTextCoroutine(comparText));
                        int ehp=(int)(EnemyManager.EnemyInfo.Enemy_HP[0]);
                        ehp-=(int)addDamage;
                        EnemyManager.EnemyInfo.Enemy_HP[0] = ehp;
                        EnemyManager.debugHPBer.fillAmount = ehp / EnemyManager.maxEnemyHP[0];
                        GameManager.moveEnd = true;
                    }
                }
                break;
            case 1:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "アイスランス";
                        moveTextFlag = true;
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        int rand = Random.Range(0, 4);
                        if(rand<=2)
                        {
                            addDamage = (PlayerInfo.Player_ATK[charaNumber] * rate * atkBuff + atkStatusBuff) * 1+atkStatusBuff;
                        }
                        if(rand==3)
                        {
                            addDamage = (PlayerInfo.Player_ATK[charaNumber] * rate * atkBuff + atkStatusBuff) *10+atkStatusBuff;
                        }
                        comparText = "アイスランスを繰り出した\n" + CharaMoveGage.enemyName + "は" + ((int)addDamage).ToString() + "のダメージを受けた";
                        StartCoroutine(moveTextCoroutine(comparText));
                        int ehp = (int)(EnemyManager.EnemyInfo.Enemy_HP[0]);
                        ehp -= (int)addDamage;
                        EnemyManager.EnemyInfo.Enemy_HP[0] = ehp;
                        EnemyManager.debugHPBer.fillAmount = ehp / EnemyManager.maxEnemyHP[0];
                        GameManager.moveEnd = true;
                    }
                }
                break;
             case 2:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "マジックバレル";
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        if (NextBarret)
                        {
                            NextBarret = false;
                        }
                        else
                        {
                            MagicBarrelBuff = 0.5f;
                        }
                        nowTurnExclusion =true;
                        comparText="マジックバレルを繰り出した\n味方が行動するたびに魔力が解き放たれる!";
                        StartCoroutine(moveTextCoroutine(comparText));
                        MagicBarrel =(int)((PlayerInfo.Player_ATK[charaNumber]*baseATKBuff*MagicBarrelBuff) + atkStatusBuff );
                        maxMagicBarrelTime=20+(rate*10);
                        MagicBarrelTime=maxMagicBarrelTime;
                        GameManager.moveEnd=true;
                    }
                }
                break;

            case 3:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "魔力次弾装填";
                        moveTextFlag = true;
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        NextBarret=true;
                        comparText="肉体に魔力を集中する\n次のマジックバレルの威力が上昇した";
                        StartCoroutine(moveTextCoroutine(comparText));
                        MagicBarrelBuff = 1 + (rate) / 2;
                        GameManager.moveEnd=true;
                    }
                }
                break;
            case 4:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        BreakerEditor.skillName = "デクテットブロウ";
                        BreakerEditor.allTime = 50 * (2f - addSpeed);
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        comparText="デクテットブロウを繰り出した\n敵に"+((int)(addDamage)).ToString()+"ダメージ\n与えた";
                        int atkCount=(int)BreakerEditor.NotesOKCount+1;                       
                        addDamage = (PlayerInfo.Player_ATK[charaNumber] * rate * atkBuff + atkStatusBuff) * atkCount;
                        int ehp = (int)(EnemyManager.EnemyInfo.Enemy_HP[0]);
                        ehp -= (int)addDamage;
                        EnemyManager.EnemyInfo.Enemy_HP[0] = ehp;
                        EnemyManager.debugHPBer.fillAmount = ehp / EnemyManager.maxEnemyHP[0];
                        StartCoroutine(moveTextCoroutine(comparText));
                        BreakerEditor.BreakerGageCount = 0;
                        BreakerEditor.breakerGageMax = false;
                        GameManager.moveEnd = true;
                    }
                }
                break;
        }
    }

    public static bool poisonFlag;
    public static int DoctorNumber;
    public static float DoctorAtkBuff;
    public static float DoctorAtkBuffTime;
    public static bool sleep;
    public static bool debuffDelate;
    [SerializeField]
    private bool tmpSleep;
    [SerializeField]
    private List<int> debuffStrage;
    void DoctorSkill() 
    {
        
        switch(SkillSelection.SkillNumber)
        {
            case 0: 
            {
                    if(GameManager.state == GameManager.BattleState.skillSelect) {
                        NotesEditor.skillName = "薬品投擲";
                        moveTextFlag = true;
                    }
                    if(GameManager.state == GameManager.BattleState.move) 
                    {
                        float pAtk = PlayerInfo.Player_ATK[charaNumber];
                        DoctorNumber=charaNumber;
                        addDamage = (pAtk * rate) * atkBuff + atkStatusBuff;
                        float ehp = EnemyManager.EnemyInfo.Enemy_HP[0] - addDamage;
                        EnemyManager.EnemyInfo.Enemy_HP[0] = ehp;
                        EnemyManager.debugHPBer.fillAmount = ehp / EnemyManager.maxEnemyHP[0];
                        targetText = EnemyNameGet.enemyNameText;
                        comparText = "薬品投擲を繰り出した\n" + targetText + "に" + ((int)addDamage).ToString() + "のダメージ";
                        if(rate>=1) {
                            int rand=Random.Range(0,2);//成功率が100%の時のみ50%の確率で毒状態を付与
                            if(rand==1) {
                                poisonFlag=true;
                            }
                        }
                        StartCoroutine(moveTextCoroutine(comparText));
                        comparText = "";
                        GameManager.moveEnd = true;
                    }
            }
            break;
            case 1: 
            {
                if(GameManager.state == GameManager.BattleState.skillSelect) 
                {
                    NotesEditor.skillName = "アドレ注射";
                    moveTextFlag = true;
                }
                if(GameManager.state == GameManager.BattleState.move) 
                {
                    float pAtk = PlayerInfo.Player_ATK[charaNumber];
                    DoctorAtkBuff=(int)(pAtk*rate);
                    DoctorAtkBuffTime=60;
                    comparText="味方全体の攻撃力が上昇した";
                        publicPBuffStorage.Add(7);
                        StartCoroutine(moveTextCoroutine(comparText));
                    GameManager.moveEnd = true;
                }
            }
            break;
            case 2:
            { 
                if (GameManager.state == GameManager.BattleState.skillSelect)
                {
                    NotesEditor.skillName = "睡眠薬射出";
                    moveTextFlag = true;
                }
                if (GameManager.state == GameManager.BattleState.move)
                {
                     int rand=Random.Range(0,101);
                        sleep=true;
                     if(rand<=100*rate*0.2f)
                     {
                         sleep=true;
                         comparText = "睡眠薬射出を繰り出した\n敵は深い眠りについた";
                     }
                     else
                     {
                         comparText="睡眠薬射出を繰り出した\nしかし外してしまった";
                     }
                     StartCoroutine(moveTextCoroutine(comparText));
                     GameManager.moveEnd = true;
                }
            }
            break;
            case 3:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "簡易オペ";
                        moveTextFlag = true;
                    }
                    if(GameManager.state == GameManager.BattleState.move)
                    {
                        Debug.Log("rateは"+rate);
                        if(rate<1&&!debuffDelate)
                        {
                            debuffStrage.RemoveAt(0);
                            debuffDelate=true;
                            GameManager.moveEnd = true;
                        }
                        if(rate==1 &&!debuffDelate)
                        {
                            Debug.Log("簡易オぺ");
                            debuffStrage.RemoveAt(0);
                            debuffStrage.RemoveAt(0);
                            debuffDelate=true;
                            GameManager.moveEnd = true;
                        }
                        comparText = "簡易オペを繰り出した\nデバフを解除した";
                        StartCoroutine(moveTextCoroutine(comparText));
                    }
                }
                break;
            case 4:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        BreakerEditor.skillName = "マッドワールド";
                        BreakerEditor.allTime = 50 * (2f - addSpeed);
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        DeInvalidMaxTime = (rate * 100) + 80;
                        DeInvalidTime = DeInvalidMaxTime;
                        comparText = "マッドワールド\n一定時間弱体化攻撃を無効化する";
                        publicPBuffStorage.Add(5);
                        StartCoroutine(moveTextCoroutine(comparText));
                        BreakerEditor.BreakerGageCount = 0;
                        GameManager.moveEnd = true;
                    }
                }
                break;
        }
    }

    public float melodyBuff;
   public  float melodyBuffTime;
    float melodyBuffMaxTime;
    public static float imnTime;
    float imnMaxTime;
    float gabMaxTime;
    float gabTime;
    public static float gabBuff;
    void StiataSkill()
    {
        switch(SkillSelection.SkillNumber)
        {
            case 0:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "音撃波";
                        moveTextFlag = true;
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        float pAtk = PlayerInfo.Player_ATK[charaNumber];
                        DoctorNumber = charaNumber;
                        addDamage = ((pAtk * rate) * atkBuff + atkStatusBuff)*rate;
                        float ehp = EnemyManager.EnemyInfo.Enemy_HP[0] - (int)addDamage;
                        EnemyManager.EnemyInfo.Enemy_HP[0] = ehp;
                        EnemyManager.debugHPBer.fillAmount = ehp / EnemyManager.maxEnemyHP[0];
                        targetText = EnemyNameGet.enemyNameText;
                        comparText = "音撃波を繰り出した\n"+targetText+"に"+((int)addDamage).ToString()+"ダメージ与えた";
                        StartCoroutine(moveTextCoroutine(comparText));
                        GameManager.moveEnd = true;
                    }
                }
                break;
            case 1:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "闘いの旋律";
                        moveTextFlag = true;
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                            float pAtk = PlayerInfo.Player_ATK[charaNumber];
                            DoctorNumber = charaNumber;
                            melodyBuffTime=30;
                            melodyBuffMaxTime=melodyBuffTime;
                            melodyBuff=(pAtk * rate) * atkBuff + (atkStatusBuff - melodyBuff);
                            Debug.Log((pAtk * rate) * atkBuff + (atkStatusBuff - melodyBuff));
                            Debug.Log("melodyBuff"+melodyBuff);
                            targetText = EnemyNameGet.enemyNameText;
                            publicPBuffStorage.Add(8);
                        comparText = "闘いの旋律を繰り出した\n味方全体の攻撃力が上昇した";
                            StartCoroutine(moveTextCoroutine(comparText));
                            GameManager.moveEnd=true;
                            GameManager.state=GameManager.BattleState.effect;
                    }
                }
                break;
            case 2:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "祈りのイムン";
                        moveTextFlag = true;
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        imnTime=30+(rate*100);
                        imnMaxTime=imnTime;
                        publicPBuffStorage.Add(9);
                        comparText = "祈りのイムンを繰り出した\n行動するたびに癒しの力が降りかかる";
                        StartCoroutine(moveTextCoroutine(comparText));
                        GameManager.moveEnd = true;
                    }
                }
                break;
            case 3:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "ガブ・サンク";
                        moveTextFlag = true;
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        for(int i=0;i<PlayerEditor.PlayerName.Length;i++)
                        {
                            float php = PlayerInfo.Player_HP[i];
                            php += 0.2f * PlayerEditorManager.MaxHP[i];
                            PlayerInfo.Player_HP[i] = (int)php;
                            PlayerManager.playerHPBer[i].fillAmount = PlayerInfo.Player_HP[i] / PlayerEditorManager.MaxHP[i];
                        }
                        gabTime=1;
                        gabMaxTime=gabTime;
                        gabBuff=1+rate;
                        publicPBuffStorage.Add(10);
                        comparText = "ガブサンクを繰り出した\n味方のHPが回復した\n味方の行動速度が上昇する";
                        StartCoroutine(moveTextCoroutine(comparText));
                        GameManager.moveEnd = true;
                    }
                }
                break;
            case 4:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        BreakerEditor.skillName = "世界樹の唄";
                        BreakerEditor.allTime = 50 * (2f - addSpeed);
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        for (int i = 0; i < PlayerEditor.PlayerName.Length; i++)
                        {
                            if(!GameManager.aliveFlag[i+1])
                            {
                                Debug.Log("蘇生しました");
                                PlayerInfo.Player_HP[i]=1;
                                GameManager.aliveFlag[i+1]=true;
                                Debug.Log("蘇生されたのは"+i+"キャラ目です");
                                Debug.Log(PlayerInfo.Player_HP[i]);
                            }
                            float php = PlayerInfo.Player_HP[i];
                            php += breakerRate * PlayerEditorManager.MaxHP[i];
                            PlayerInfo.Player_HP[i]=(int)php;
                            PlayerManager.playerHPBer[i].fillAmount = php/ PlayerEditorManager.MaxHP[i];
                        }
                        BreakerEditor.BreakerGageCount = 0;
                        comparText = "世界樹の唄を繰り出した\n世界樹の唄が、優しく空間を包み込む";
                        StartCoroutine(moveTextCoroutine(comparText));
                        GameManager.moveEnd = true;
                    }
                }
                break;
        }
    }

    public static float richardSkill2Time;
    public static float richardSkill2MaxTime;
    public static float richardSkill2Buff;
    public static bool richardSkill3Avoidance;
    public static float richardSkill3MaxTime;
    public static float richardSkill3Time;
    public static float richardSkill3HateUp;
    public static int richardNumber;

    void RichardSkill() 
    {
        switch(SkillSelection.SkillNumber) 
        {
            case 0: 
                {
                    if(GameManager.state == GameManager.BattleState.skillSelect) 
                    {
                        NotesEditor.skillName = "散桜";
                    }
                    if(GameManager.state == GameManager.BattleState.move) 
                    {
                        float pAtk = PlayerInfo.Player_ATK[charaNumber];
                        addDamage = pAtk;
                        int atkLoop = 1;
                        for (int i = 1; i < 5; i++)
                        {
                            if (rate >= i * 0.2f)
                            {
                                atkLoop = i;
                            }
                            else
                            {
                                atkLoop = i;
                                Debug.Log(i);
                                Debug.Log(addDamage);
                                break;
                            }
                        }
                        for (int i = 0; i < atkLoop; i++)
                        {
                            float ehp = EnemyManager.EnemyInfo.Enemy_HP[0] - addDamage;
                            EnemyManager.EnemyInfo.Enemy_HP[0] = ehp;
                            EnemyManager.debugHPBer.fillAmount = ehp / EnemyManager.maxEnemyHP[0];
                            if (richardSkill2Time >= 0)
                            {
                                float php = PlayerEditorManager.PlayerInfo.Player_HP[charaNumber];
                                php += php * richardSkill2Buff;
                                richardSkill2Buff += 0.01f;
                                richardSkill2Buff = 0.1f;

                            }
                        }
                        DamageText = ((int)(addDamage * atkLoop)).ToString() + "のダメージ";
                        targetText = EnemyNameGet.enemyNameText.ToString() + "に";
                        comparText = "散桜を繰り出した" + "\n" + targetText + DamageText;
                        StartCoroutine(moveTextCoroutine(comparText));
                        GameManager.state=GameManager.BattleState.effect;
                        GameManager.moveEnd = true;
                    }
                }
                break;
            case 1: 
                {
                    if(GameManager.state == GameManager.BattleState.skillSelect) 
                    {
                        NotesEditor.skillName = "血刃";
                    }
                    if(GameManager.state == GameManager.BattleState.move) 
                    {
                        richardSkill2Time=60*rate;
                        richardSkill2MaxTime=richardSkill2Time;
                        richardSkill2Buff=0.1f;
                        publicPBuffStorage.Add(11);
                        comparText ="血刃を繰り出した\nしばらくの間攻撃をすると\nHPをわずかに回復する";
                        StartCoroutine(moveTextCoroutine(comparText));
                        GameManager.moveEnd=true;
                    }
                }
                break;
            case 2:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "威風堂々";
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        richardNumber=charaNumber;
                        richardSkill3HateUp=30;
                        if(richardSkill3Time>0)
                        {
                            PlayerInfo.Player_Hate[charaNumber]-=30;
                        }
                        richardSkill3Time=30;
                        richardSkill3MaxTime = richardSkill3Time;
                        PlayerInfo.Player_Hate[charaNumber]+=30;
                       
                        if (Random.Range(0, 101)<100-rate*100)
                        {
                            richardSkill3Avoidance=true;
                        }
                        else
                        {
                            richardSkill3Avoidance=false;
                        }
                        publicPBuffStorage.Add(12);
                        comparText ="威風堂々を繰り出した\n少しの間狙われやすくなる\nさらに次の攻撃を確率で回避する";
                        StartCoroutine(moveTextCoroutine(comparText));
                        GameManager.moveEnd = true;
                    }
                }
                break;
                case 3:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "兜割";
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        int pAtk=(int)(PlayerInfo.Player_ATK[charaNumber]*rate);
                        int pAtk5=0;
                        if(playerSkill3Buff==1||atkDownTime>0||DeSpeedTime>0||sleep||poisonFlag)
                        {
                            pAtk5=pAtk*5;
                        }
                        addDamage=pAtk+pAtk5;
                        float ehp = EnemyManager.EnemyInfo.Enemy_HP[0] - addDamage;
                        EnemyManager.EnemyInfo.Enemy_HP[0] = ehp;
                        EnemyManager.debugHPBer.fillAmount = ehp / EnemyManager.maxEnemyHP[0];
                        targetText = EnemyNameGet.enemyNameText;
                        comparText ="兜割を繰り出した\n"+targetText+"に"+addDamage.ToString()+"ダメージ\n与えた";
                        StartCoroutine(moveTextCoroutine(comparText));
                        GameManager.moveEnd = true;
                    }
                }
                break;
                case 4:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        BreakerEditor.skillName = "紫電一閃";
                        BreakerEditor.allTime = 50 * (2f - addSpeed);
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        int pAtk=PlayerInfo.Player_ATK[charaNumber];
                        float ehp = EnemyManager.EnemyInfo.Enemy_HP[0] - pAtk;
                        EnemyManager.EnemyInfo.Enemy_HP[0] = ehp;
                        EnemyManager.debugHPBer.fillAmount = ehp / EnemyManager.maxEnemyHP[0];
                        richardSkill2Time=60;
                        richardSkill2MaxTime=richardSkill2Time;
                        BreakerEditor.BreakerGageCount = 0;
                        targetText = EnemyNameGet.enemyNameText;
                        publicPBuffStorage.Add(11);
                        comparText ="紫電一閃を繰り出した\n"+targetText+"に"+pAtk.ToString()+"のダメージを与えた\nさらに血刃の効果が付与された";
                        StartCoroutine(moveTextCoroutine(comparText));
                        GameManager.moveEnd = true;
                    }
                }
                break;
        }
    }
    void CharaSet()
    {
        if(CharaMoveGage.MoveChar.Count==0)return;
        string mChar=CharaMoveGage.MoveChar[0].name;
        EnemyMove.stoneSpeedTurn -= 1;
        switch (mChar)
        {
            
            case "主人公":
                {
                    PlayerSkill();
                }
                break;
            case "アンナリーナ":
                {
                    AnnaSkill();
                }
                break;
            case "ゴードン":
                {
                    GorDonSkill();
                }
                break;
            case "平櫛凛":
                {
                    RinSkill();
                }
                break;
            case "レティシア":
                {
                    LetitiaSkill();
                }
                break;
            case "フェレスト": 
                {
                    DoctorSkill();
                }
                break;
            case "スティアータ":
                {
                    StiataSkill();
                }
                break;
            case "リチャード":
                {
                    RichardSkill();
                }
                break;
        }
    }
    public static int nextCharaNumber=-1;
    [SerializeField]
    private int tmpnextCharaNumber;
    void CharNumberGet()//行動するキャラが何番目のキャラかを取得
    {
         for(int i=0;i<4;i++)
        {
            if(PlayerEditor.PlayerName[i] != "" && PlayerEditor.PlayerName[i] != null) {
                if(partyChara.transform.GetChild(i).gameObject==CharaMoveGage.MoveChar[0])
                {
                    charaNumber=i;
                }
            }
            if (CharaMoveGage.MoveChar.Count>=2)
            {
                if (!CharaMoveGage.MoveChar[1].CompareTag("Enemy"))
                {
                    if (partyChara.transform.GetChild(i).gameObject == CharaMoveGage.MoveChar[1])
                    {
                        nextCharaNumber = i;
                    }
                }

            }
            else if (CharaMoveGage.MoveChar.Count >=3)
            {
                nextCharaNumber = i;
            }
        }
        // if(CharaMoveGage.MoveChar[1]==null)
        //{
        //    nextCharaNumber=-1;
        //}
         tmpnextCharaNumber=nextCharaNumber;
    }

    float BuffTime(float time,float maxTime)//バフの時間を減らす関数
    {
        if(time>0)
        { 
            time-=Time.deltaTime;
            return time;
            if (false)//協力ブレイカー用　現在は到達できない
            {
                time=maxTime;
            }
        }
        
        return 0;
    }

    float BuffTime(float time, float maxTime,int IconNumber)//バフの時間を減らす関数
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            return time;
            if (false)//協力ブレイカー用　現在は到達できない
            {
                time = maxTime;
            }
        }
        else
        {
            for(int i=0;i<publicPBuffStorage.Count;i++)
            {
                if(publicPBuffStorage[i]==IconNumber)
                {
                    publicPBuffStorage.RemoveAt(IconNumber);
                }
            }
        }
        return 0;
    }
    public static float Buff(float time,float buff,float normal)//バフの時間が切れたら初期値に戻す関数
    {
        if(time<=0)
        {
            buff=normal;
        }
        return buff;
    }
    void BuffTimeStorage()//バフの時間を減らす関数を一括で管理するための関数
    {
        p2AtkUpTime=BuffTime(p2AtkUpTime,p2AtkUpMaxTime,0);
        pATKCorrect=Buff(p2AtkUpTime,pATKCorrect,0);
        addSpeed=Buff(addSpeedTurn,addSpeed,1);
        DameCutTime=BuffTime(DameCutTime,DameCutMaxTime,3);
        DameCutPar= Buff(DameCutTime,DameCutPar,0);
        hateUpTime=BuffTime(hateUpTime,hateUpMaxTime);
        melodyBuffTime=BuffTime(melodyBuffTime,melodyBuffMaxTime,8);
        //melodyBuff=Buff(melodyBuffTime,melodyBuff,0f);
        if(gordonHateCorrection>0)
        { 
            gordonHateCorrection= (int)Buff(hateUpTime,gordonHateCorrection,gordonHateCorrection-50);
            gordonHateCorrection=(int)Buff(gordonBreakerTime,gordonBreakerMaxTime,gordonHateCorrection-50);
        }
        atkDownTime=BuffTime(atkDownTime,atkDownMaxTime);
        atkDownDeBuff=Buff(atkDownTime,atkDownDeBuff,0);
        DeSpeedTime=BuffTime(DeSpeedTime,DeSpeedMaxTime);
        DeBuffSpeed=Buff(DeSpeedTime,DeBuffSpeed,1);
        DeInvalidTime=BuffTime(DeInvalidTime,DeInvalidMaxTime,5);
        MagicBarrelTime=BuffTime(MagicBarrelTime,maxMagicBarrelTime);
        annBreakerTime=BuffTime(annBreakerTime,annBreakerMaxTime,2);
        DoctorAtkBuffTime=BuffTime(DoctorAtkBuffTime,60f,7);
        DoctorAtkBuff=Buff(DoctorAtkBuffTime,DoctorAtkBuff,0);
        imnTime=BuffTime(imnTime,imnMaxTime,9);
        gabTime=BuffTime(gabTime,gabMaxTime,10);
        gabBuff=Buff(gabTime,gabBuff,1);
        richardSkill2Time=BuffTime(richardSkill2Time,richardSkill2MaxTime,11);
        richardSkill2Buff=Buff(richardSkill2Time,richardSkill2Buff,0);
    }
    public static int BuffTurn(int turn)
    {
        if(turn>0)
        {
            turn-=1;
            return turn;
        }
        return 0;
    }
    public static void DBuffTurnStorage()
    {
        BuffTurn(playerSkill3);
    }
    public static void BuffTurnStorage()
    {
        BuffTurn(addSpeedTurn);
    }

    int RecoverySubject(int target)
    {
        float minHP=1;
        
        for(int i=0;i<PlayerInfo.Player_HP.Length;i++)
        { 
            if(minHP>PlayerInfo.Player_HP[i]/MaxHP[i])
            {
                if(PlayerInfo.Player_HP[i]>0&&PlayerInfo.Player_HP[i]<PlayerEditorManager.MaxHP[i])
                {
                    minHP=(float)(PlayerInfo.Player_HP[i]/MaxHP[0]);
                    target=i;
                    Debug.Log("ターゲットは"+target);
                }
            }
        }
        return target;
    }
    int baseATKBuff;
    public static float baseDeBuff;
    void ATKBuff()//割合バフ関係はこっち
    {   
        baseATKBuff= (int)pATKCorrect+1;
        baseDeBuff= playerSkill3Buff+1;
        atkBuff =baseATKBuff*baseDeBuff;//これをキャラクターの基礎攻撃力に×
    }
    public static int atkStatusBuff;
    void AddStatus() {
        atkStatusBuff=(int)(DoctorAtkBuff+melodyBuff);
    }
    public static void MagicBarrelDamage()
    {
        Debug.Log("MAGICBARREL");
        
        if (MagicBarrelTime>=0)
        {
            EnemyManager.EnemyInfo.Enemy_HP[0] -= MagicBarrel*baseDeBuff*MagicBarrelBuff;
            EnemyManager.debugHPBer.fillAmount=EnemyManager.EnemyInfo.Enemy_HP[0]/EnemyManager.maxEnemyHP[0];
        }
    }

    public static void ImnRecovery()
    {
        if(imnTime>0)
        { 
            Debug.Log("祈りのイムンの効果が発動しました");
            float php = PlayerInfo.Player_HP[charaNumber];
            php += 0.1f * PlayerEditorManager.MaxHP[charaNumber];
            if(0.1f*PlayerEditorManager.MaxHP[charaNumber]<=0)
            {
                php+=1;
            }
            Debug.Log("回復量は"+0.1f*PlayerEditorManager.MaxHP[charaNumber]);
            PlayerInfo.Player_HP[charaNumber] = (int)php;
            PlayerManager.playerHPBer[charaNumber].fillAmount = PlayerInfo.Player_HP[charaNumber] / PlayerEditorManager.MaxHP[charaNumber];
        }
    }

    
}
