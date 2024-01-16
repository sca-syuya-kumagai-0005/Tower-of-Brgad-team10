using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerEditorManager;
using static MoveTextController;

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
    private float pATKCorrect=1;
    public static int playerSkill3;
    [SerializeField]
    private float playerSkill3Buff;
    public static int CommandCount;
    public static float addDamage;
    [SerializeField]
    private int hate;
    public static float enemyActTime=4;
    public static float atkBuff;
    public static IEnumerator croutine;
    [SerializeField]
    private Text moveMember;

    private void Start()
    {
        
    }
    private void Update()
    {
        tmpRate=rate;
        tmpSleep=sleep;
        rate = NotesEditor.NotesOKCount / CommandCount;
        if (CharaMoveGage.MoveChar[0]==null||CharaMoveGage.MoveChar[0].name=="Enemy") {
            charaNumber=-1;
        }
       hate=PlayerEditorManager.PlayerInfo.Player_Hate[2]+gordonHateCorrection;
       ATKBuff();
        if (CharaMoveGage.MoveChar[0]!=null)
        { 
            CharaSet();
        } 
            CharNumberGet();
        
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
                        float pAtk= PlayerInfo.Player_ATK[charaNumber];
                        addDamage=(pAtk*rate)*2+2*atkStatusBuff;
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
                        switch(rate)
                        {
                            case 0.2f:
                                {
                                    addSpeedTurn=1;
                                }
                                break;
                            case 0.4f:
                                {
                                    addSpeedTurn=2;
                                }
                                break;
                            case 0.6f:
                                {
                                    addSpeedTurn=3;
                                }
                                break;
                            case 0.8f:
                                {
                                    addSpeedTurn=4;
                                }
                                break;
                            case 1f:
                                {
                                    addSpeedTurn=5;
                                }
                                break;
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
                        switch (rate)
                        {
                            case 0.2f:
                                {
                                    addSpeedTurn = 1;
                                }
                                break;
                            case 0.4f:
                                {
                                    addSpeedTurn = 2;
                                }
                                break;
                            case 0.6f:
                                {
                                    addSpeedTurn = 3;
                                }
                                break;
                            case 0.8f:
                                {
                                    addSpeedTurn = 4;
                                }
                                break;
                            case 1f:
                                {
                                    addSpeedTurn = 5;
                                }
                                break;
                        }
                        comparText = "減速する過去を繰り出した\nコマンドの流れる速度が変化した";
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
                        NotesEditor.skillName = "ありえた選択";
                    }
                    if(GameManager.state==GameManager.BattleState.move)
                    {
                        rateCorrection=(rate*100*0.3f)/100;
                        comparText="ありえた選択を繰り出した\nコマンドの成功率が上昇した";
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
                        comparText="守護の構えを繰り出した\nゴードンがダメージを一部防げるようになった";
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
                        gordonHateCorrection += 50;
                        hateUpMaxTime=rate*100;
                        hateUpTime=hateUpMaxTime;
                        comparText="挑発を繰り出した\n敵から狙われやすくなった";
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
                        gordonHateCorrection += 50;
                        gordonBreakerMaxTime = breakerRate * 130+130;
                        gordonBreakerTime=gordonBreakerMaxTime;
                        comparText="絶対防衛陣を繰り出した\nどんな攻撃も防いで見せましょう！";
                        StartCoroutine(moveTextCoroutine(comparText));
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
                        int rand = Random.Range(0, 2);
                        if(rand==1)
                        {
                            addDamage = (PlayerInfo.Player_ATK[charaNumber] * rate * atkBuff + atkStatusBuff) * 1+atkStatusBuff;
                        }
                        if(rand==0)
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
                        
                        nowTurnExclusion=true;
                        comparText="マジックバレルを繰り出した\n味方が行動するたびに魔力が解き放たれる!";
                        StartCoroutine(moveTextCoroutine(comparText));
                        MagicBarrel =(int)((PlayerInfo.Player_ATK[charaNumber]*baseATKBuff + atkStatusBuff ));
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
                        NotesEditor.skillName = "マッドワールド";
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        DeInvalidMaxTime = (rate * 100) + 80;
                        DeInvalidTime = DeInvalidMaxTime;
                        comparText = "マッドワールド\n一定時間弱体化攻撃を無効化する";
                        StartCoroutine(moveTextCoroutine(comparText));
                        GameManager.moveEnd = true;
                    }
                }
                break;
        }
    }
    float melodyBuff;
    float melodyBuffTime;
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
                        melodyBuff= (int)((pAtk * rate) * atkBuff + (atkStatusBuff-melodyBuff) * rate);
                        Debug.Log("melodyBuff"+melodyBuff);
                        targetText = EnemyNameGet.enemyNameText;
                        comparText = "闘いの旋律を繰り出した\n味方全体の攻撃力が上昇した";
                        StartCoroutine(moveTextCoroutine(comparText));
                        GameManager.moveEnd = true;
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
                            float php = PlayerInfo.Player_HP[charaNumber];
                            php += 0.2f * PlayerEditorManager.MaxHP[charaNumber];
                            PlayerInfo.Player_HP[charaNumber] = (int)php;
                            PlayerManager.playerHPBer[charaNumber].fillAmount = PlayerInfo.Player_HP[charaNumber] / PlayerEditorManager.MaxHP[charaNumber];
                        }
                        gabTime=1;
                        gabMaxTime=gabTime;
                        gabBuff=1+rate;
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
                        NotesEditor.skillName = "世界樹の唄";
                        moveTextFlag = true;
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        for (int i = 0; i < PlayerEditor.PlayerName.Length; i++)
                        {
                            if(!GameManager.aliveFlag[i])
                            {
                                PlayerInfo.Player_HP[i]=1;
                            }
                            float php = PlayerInfo.Player_HP[i];
                            php += breakerRate * PlayerEditorManager.MaxHP[i];
                            PlayerManager.playerHPBer[charaNumber].fillAmount = PlayerInfo.Player_HP[i] / PlayerEditorManager.MaxHP[i];
                        }
                        comparText = "世界樹の唄を繰り出した\n世界樹の唄が、優しく空間を包み込む";
                        StartCoroutine(moveTextCoroutine(comparText));
                        GameManager.moveEnd = true;
                    }
                }
                break;
        }
    }
    void CharaSet()
    {
        string mChar=CharaMoveGage.MoveChar[0].name;
        switch(mChar)
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
        }
    }
    public static int nextCharaNumber=-1;
    [SerializeField]
    private int tmpnextCharaNumber;
    void CharNumberGet()//行動するキャラが何番目のキャラかを取得
    {
         for(int i=0;i<4;i++)
        {
            if(partyChara.transform.GetChild(i).gameObject==CharaMoveGage.MoveChar[0])
            {
                charaNumber=i;
            }
            if(CharaMoveGage.MoveChar[1]!=null)
            {
                if(!CharaMoveGage.MoveChar[1].CompareTag("Enemy"))
                {
                    if(partyChara.transform.GetChild(i).gameObject==CharaMoveGage.MoveChar[1])
                    {
                        nextCharaNumber=i;
                    }
                }
               
            }
            else if (CharaMoveGage.MoveChar[2] != null)
            {
                nextCharaNumber = i;
            }
        }
         if(CharaMoveGage.MoveChar[1]==null)
        {
            nextCharaNumber=-1;
        }
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
        p2AtkUpTime=BuffTime(p2AtkUpTime,p2AtkUpMaxTime);
        pATKCorrect=Buff(p2AtkUpTime,pATKCorrect,1);
        addSpeed=Buff(addSpeedTurn,addSpeed,1);
        DameCutTime=BuffTime(DameCutTime,DameCutMaxTime);
        DameCutPar= Buff(DameCutTime,DameCutPar,0);
        hateUpTime=BuffTime(hateUpTime,hateUpMaxTime);
        melodyBuffTime=BuffTime(melodyBuffTime,melodyBuffMaxTime);
        melodyBuff=Buff(melodyBuffTime,melodyBuff,0f);
        if(gordonHateCorrection>0)
        { 
            gordonHateCorrection= (int)Buff(hateUpTime,gordonHateCorrection,gordonHateCorrection-50);
            gordonHateCorrection=(int)Buff(gordonBreakerTime,gordonBreakerMaxTime,gordonHateCorrection-50);
        }
        atkDownTime=BuffTime(atkDownTime,atkDownMaxTime);
        atkDownDeBuff=Buff(atkDownTime,atkDownDeBuff,0);
        DeSpeedTime=BuffTime(DeSpeedTime,DeSpeedMaxTime);
        DeBuffSpeed=Buff(DeSpeedTime,DeBuffSpeed,1);
        DeInvalidTime=BuffTime(DeInvalidTime,DeInvalidMaxTime);
        MagicBarrelTime=BuffTime(MagicBarrelTime,maxMagicBarrelTime);
        annBreakerTime=BuffTime(annBreakerTime,annBreakerMaxTime);
        DoctorAtkBuffTime=BuffTime(DoctorAtkBuffTime,60f);
        DoctorAtkBuff=Buff(DoctorAtkBuffTime,DoctorAtkBuff,0);
        imnTime=BuffTime(imnTime,imnMaxTime);
        gabTime=BuffTime(gabTime,gabMaxTime);
        gabBuff=Buff(gabTime,gabBuff,1);
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
        if (NextBarret)
        {
            NextBarret = false;
        }
        else
        {
            MagicBarrelBuff = 0.5f;
        }
        if (MagicBarrelTime>=0)
        {
            EnemyManager.EnemyInfo.Enemy_HP[0] -= MagicBarrel*baseDeBuff*MagicBarrelBuff;
            EnemyManager.debugHPBer.fillAmount=EnemyManager.EnemyInfo.Enemy_HP[0]/EnemyManager.maxEnemyHP[0];
        }
    }

    public static void ImnRecovery()
    {
        if(imnTime>=0)
        { 
            Debug.Log("祈りのイムンの効果が発動しました");
            float php = PlayerInfo.Player_HP[charaNumber];
            php += 0.1f * PlayerEditorManager.MaxHP[charaNumber];
            Debug.Log("回復量は"+0.1f*PlayerEditorManager.MaxHP[charaNumber]);
            PlayerInfo.Player_HP[charaNumber] = (int)php;
            PlayerManager.playerHPBer[charaNumber].fillAmount = PlayerInfo.Player_HP[charaNumber] / PlayerEditorManager.MaxHP[charaNumber];
        }
    }
}
