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
    private float addDamage;
    [SerializeField]
    private int hate;
    public static float enemyActTime=8;
    private float atkBuff;
    public static IEnumerator croutine;
    [SerializeField]
    private Text moveMember;

    private void Start()
    {
    }
    private void Update()
    {
        tmpRate=breakerRate;
      
        if(CharaMoveGage.MoveChar[0]==null||CharaMoveGage.MoveChar[0].name=="Enemy") {
            charaNumber=-1;
        }
       hate=PlayerEditorManager.PlayerInfo.Player_Hate[2]+gordonHateCorrection;
       ATKBuff();
        if (CharaMoveGage.MoveChar[0]!=null)
        { 
            CharaSet();
        }
        if(GameManager.state==GameManager.BattleState.skillSelect)
        { 
            CharNumberGet();
        }
        if(BreakerEditor.commandEnd)
        {
            Debug.Log("BreakerEditor.NotesOKCountは"+ BreakerEditor.NotesOKCount);
            Debug.Log(" CommandCountは" + CommandCount);
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
        moveMember.text =  "レオンはどうする？";
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
                        
                        float pAtk= PlayerInfo.Player_ATK[charaNumber]*atkBuff*2;
                        addDamage=(pAtk*rate)*playerSkill3Buff;
                        float ehp= EnemyManager.EnemyInfo.Enemy_HP[0]- pAtk * rate;
                        EnemyManager.EnemyInfo.Enemy_HP[0] = ehp;
                        EnemyManager.debugHPBer.fillAmount=ehp/EnemyManager.maxEnemyHP[0];
                        DamageText=(pAtk*rate).ToString()+"のダメージ";
                        targetText=EnemyNameGet.enemyNameText.ToString()+"に";
                        comparText="スラッシュを繰り出した"+"\n"+targetText+DamageText;
                        Debug.Log(DamageText);
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
                        GameManager.moveEnd = true;
                    }
                }
                break;
            case 4:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        BreakerEditor.skillName = "勇気の斬跡";
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        float pAtk = PlayerInfo.Player_ATK[charaNumber]*atkBuff;
                        float ehp = EnemyManager.EnemyInfo.Enemy_HP[0] - pAtk * (breakerRate*100)*GameManager.aliveCount;
                        Debug.Log("ダメージは"+pAtk * breakerRate * GameManager.aliveCount);
                        EnemyManager.EnemyInfo.Enemy_HP[0] = ehp;
                        EnemyManager.debugHPBer.fillAmount = ehp / EnemyManager.maxEnemyHP[0];
                        targetText=EnemyNameGet.enemyNameText;
                        comparText="勇気の斬跡を繰り出した\n" + pAtk * (breakerRate * 100) * GameManager.aliveCount+targetText+"のダメージ";
                        StartCoroutine(moveTextCoroutine(comparText));
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
        moveMember.text = CharaMoveGage.MoveChar[0].name + "はどうする？";
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
                        DamageText=(ehp * (rate * 100 * 0.01f) * 0.1f).ToString();
                        targetText=EnemyNameGet.enemyNameText+"に";
                        comparText ="結末への調整を繰り出した\n"+targetText+DamageText+"のダメージを与えた";
                        StartCoroutine(moveTextCoroutine(comparText));
                        GameManager.moveEnd = true;
                    }
                }
                break;
                case 4:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        BreakerEditor.skillName = "定められし運命";
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        annBreakerMaxTime=breakerRate*100+25;
                        annBreakerTime=annBreakerMaxTime;
                        comparText= "定められし運命を繰り出した\n今こそ運命を、Aの一文字に！";
                        StartCoroutine(moveTextCoroutine(comparText));
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
        moveText.text = CharaMoveGage.MoveChar[0].name + "はどうする？";
        switch (SkillSelection.SkillNumber)
        {
            case 0:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "守護の構え";
                        croutine =(moveTextCoroutine("被ダメージを割合カット"));
                        StartCoroutine(croutine);
                        moveTextFlag = true;
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        DameCutPar=(rate*100)-40f;
                        DameCutTime=DameCutMaxTime;
                        GameManager.moveEnd = true;
                        Debug.Log(DameCutPar);
                    }
                    break;
                }
            case 1:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "挑発";
                        croutine =(moveTextCoroutine("敵の攻撃を受けやすくなる"));
                        StartCoroutine(croutine);
                        moveTextFlag = true;
                    }
                    if(GameManager.state==GameManager.BattleState.move)
                    {
                        gordonHateCorrection += 50;
                        hateUpMaxTime=rate*100;
                        hateUpTime=hateUpMaxTime;
                        GameManager.moveEnd = true;
                    }
                }
                break;
                case 2:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "威圧";
                        croutine =(moveTextCoroutine("敵の攻撃力低下"));
                        StartCoroutine(croutine);
                        moveTextFlag = true;
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        float pAtk=PlayerInfo.Player_ATK[charaNumber];
                        atkDownDeBuff=(int)(rate*pAtk*0.3f);
                        Debug.Log(atkDownDeBuff);
                        atkDownMaxTime= rate * 100 * 0.6f;
                        Debug.Log(atkDownMaxTime);
                        atkDownTime =atkDownMaxTime;
                        GameManager.moveEnd = true;
                    }
                }
                break;
                case 3:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "刺突";
                        croutine =(moveTextCoroutine("敵1体に攻撃"));
                        StartCoroutine(croutine);
                        moveTextFlag = true;
                    }
                    if(GameManager.state == GameManager.BattleState.move)
                    {
                        float pAtk = PlayerInfo.Player_ATK[charaNumber];
                        addDamage = (pAtk * rate) *atkBuff;
                        float ehp = EnemyManager.EnemyInfo.Enemy_HP[0] - pAtk * rate;
                        EnemyManager.EnemyInfo.Enemy_HP[0] = ehp;
                        EnemyManager.debugHPBer.fillAmount = ehp / EnemyManager.maxEnemyHP[0];
                        GameManager.moveEnd = true;
                    }
                }
                break;
                case 4:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        BreakerEditor.skillName = "絶対防衛陣";
                        croutine = (moveTextCoroutine("狙われやすくなる＋攻撃を無効化"));
                        StartCoroutine(croutine);
                        moveTextFlag = true;
                    }
                     if(GameManager.state==GameManager.BattleState.move)
                    {
                        gordonHateCorrection += 50;
                        gordonBreakerMaxTime=rate*130+130;
                        gordonBreakerTime=gordonBreakerMaxTime;
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
        moveText.text = CharaMoveGage.MoveChar[0].name + "はどうする？";
        switch (SkillSelection.SkillNumber)
        {
            case 0:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "呪符：鈍足香";
                        croutine =(moveTextCoroutine("敵の行動速度低下"));
                        StartCoroutine(croutine);
                        moveTextFlag = true;
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        DeBuffSpeed=((rate)/2);
                        DeSpeedTime=DeSpeedMaxTime;
                        GameManager.moveEnd=true;
                    }
                }
                break;
            case 1:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "御神水です！";
                        croutine =(moveTextCoroutine("HPの最も低い味方1体を回復"));
                        StartCoroutine(croutine);
                        moveTextFlag = true;
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
                        php+=(php*0.3f)+PlayerInfo.Player_ATK[charaNumber];
                        Debug.Log(php);
                        PlayerInfo.Player_HP[recoveryTarget] =(int)php;
                        PlayerManager.playerHPBer[recoveryTarget].fillAmount=php/PlayerEditorManager.MaxHP[recoveryTarget];
                        GameManager.moveEnd=true;
                    }
                }
                break;
            case 2:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "護符：厄払";
                        croutine =(moveTextCoroutine("状態異常を無効化"));
                        StartCoroutine(croutine);
                        moveTextFlag = true;
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        DeInvalidMaxTime=((rate*100)*0.3f)+10;
                        DeInvalidTime=DeInvalidMaxTime;
                        GameManager.moveEnd=true;
                    }
                }
                break;
             case 3:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "霊符：風鎌";
                        croutine =(moveTextCoroutine("味方全体にダメージ反射を付与"));
                        StartCoroutine(croutine);
                        moveTextFlag = true;
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        RefrectCount=0;
                        RefrectDamage=(PlayerInfo.Player_ATK[charaNumber]*pATKCorrect)/2;
                        for(int i=(int)(rate*100);i<=-1;i-=25)
                        {
                            RefrectCount++;
                        }
                        GameManager.moveEnd = true;
                    }
                }
                break;
             case 4:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        BreakerEditor.skillName = "禁符：御法の障壁";
                        croutine = (moveTextCoroutine("味方全体にダメージカットを付与"));
                        StartCoroutine(croutine);
                        moveTextFlag = true;
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        rinBreakerTime=130;
                        rinBreaker=90*rate;
                        GameManager.moveEnd = true;
                    }
                }
                break;
        }
    }

    public static int MagicBarrel;
    public static float MagicBarrelTime=0;
    private float maxMagicBarrelTime;
    float MagicBarrelBuff=0.5f;
    bool NextBarret;
    public static bool nowTurnExclusion;

    void LetitiaSkill()
    {
        moveText.text = CharaMoveGage.MoveChar[0].name + "はどうする？";
        switch (SkillSelection.SkillNumber)
        {
            case 0:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "サンバレット";
                        croutine =(moveTextCoroutine("敵単体にランダムな倍率でダメージ"));
                        StartCoroutine(croutine);
                        moveTextFlag = true;
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        int rand=Random.Range(1,6);
                        addDamage=PlayerInfo.Player_ATK[charaNumber]*rand*rate*atkBuff;
                        Debug.Log("ダメージは"+addDamage);
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
                        croutine =(moveTextCoroutine("敵全体にランダムな倍率でダメージ"));
                        StartCoroutine(croutine);
                        moveTextFlag = true;
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        int rand = Random.Range(1, 4);
                        addDamage = PlayerInfo.Player_ATK[charaNumber] * rand * rate * atkBuff;
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
                        croutine =(moveTextCoroutine("味方が行動するたびに敵単体に追加ダメージを与える"));
                        StartCoroutine(croutine);
                        moveTextFlag = true;
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        if(NextBarret)
                        {
                            NextBarret=false;
                        }
                        else
                        {
                            MagicBarrelBuff=0.5f;
                        }
                        nowTurnExclusion=true;
                        MagicBarrel=(int)(PlayerInfo.Player_ATK[charaNumber]*atkBuff*MagicBarrelBuff);
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
                        croutine =(moveTextCoroutine("次に使うマジックバレルの効果が上昇"));
                        StartCoroutine(croutine);
                        moveTextFlag = true;
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        NextBarret=true;
                        MagicBarrelBuff = 1 + (rate) / 2;
                        GameManager.moveEnd=true;
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
        }


    }
    void CharNumberGet()//行動するキャラが何番目のキャラかを取得
    {
         for(int i=0;i<4;i++)
        {
            if(partyChara.transform.GetChild(i).gameObject==CharaMoveGage.MoveChar[0])
            {
                charaNumber=i;
            }
        }
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

    float Buff(float time,float buff,float normal)//バフの時間が切れたら初期値に戻す関数
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
    }
    public static int DBuffTurn(int turn)
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
        DBuffTurn(playerSkill3);
    }
    public static void BuffTurnStorage()
    {
        DBuffTurn(addSpeedTurn);
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
    void ATKBuff()
    {
        atkBuff=((pATKCorrect)+1)*((playerSkill3Buff)+1);//これをキャラクターの基礎攻撃力に×
    }

    public static void MagicBarrelDamage()
    {
        if(MagicBarrelTime>=0)
        {
            EnemyManager.EnemyInfo.Enemy_HP[0] -= MagicBarrel;
            EnemyManager.debugHPBer.fillAmount=EnemyManager.EnemyInfo.Enemy_HP[0]/EnemyManager.maxEnemyHP[0];
        }
        
    }
}
