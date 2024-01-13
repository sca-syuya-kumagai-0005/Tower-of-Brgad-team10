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
        sleep=true;
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
       // moveMember.text =  "���I����\n�ǂ�����H";
        switch (SkillSelection.SkillNumber)
        {
            case 0:
                {//�X���b�V��
                   
                    if(GameManager.state==GameManager.BattleState.skillSelect)
                    { 
                        NotesEditor.skillName="�X���b�V��";
                    }
                    if (GameManager.state==GameManager.BattleState.move)
                    {
                        
                        float pAtk= PlayerInfo.Player_ATK[charaNumber]*atkBuff*2;
                        addDamage=(pAtk*rate)*playerSkill3Buff + atkStatusBuff;
                        float ehp= EnemyManager.EnemyInfo.Enemy_HP[0]- pAtk * rate;
                        EnemyManager.EnemyInfo.Enemy_HP[0] = ehp;
                        EnemyManager.debugHPBer.fillAmount=ehp/EnemyManager.maxEnemyHP[0];
                        DamageText=((int)(pAtk*rate)).ToString()+"�̃_���[�W";
                        targetText=EnemyNameGet.enemyNameText.ToString()+"��";
                        comparText="�X���b�V�����J��o����"+"\n"+targetText+DamageText;
                        StartCoroutine(moveTextCoroutine(comparText));
                        comparText = "";
                        GameManager.moveEnd=true;
                    }
                }
                break;
                case 1://���u����
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "���u����";
                    }
                    
                    if (GameManager.state == GameManager.BattleState.move)
                    { 
                    pATKCorrect = (NotesEditor.NotesOKCount / CommandCount);
                    p2AtkUpTime=p2AtkUpMaxTime;
                    comparText="���u�������J��o����\n�����S�̂̍U���͂��㏸����";
                        StartCoroutine(moveTextCoroutine(comparText));
                        GameManager.moveEnd=true;
                    }
                }
                break;
                case 2:
                {

                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "�W�Q�H��";
                    }
                   if(GameManager.state==GameManager.BattleState.move)
                    { 
                        playerSkill3Buff =(rate*100/20f)/100;
                        playerSkill3=1;
                        comparText="�W�Q�H����J��o����\n�G�̔�_���[�W���㏸����";
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
                        NotesEditor.skillName = "���}�蓖";
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                       float php=PlayerInfo.Player_HP[charaNumber];
                        php+=(((rate*100*0.3f)/100))*PlayerEditorManager.MaxHP[charaNumber];
                        PlayerInfo.Player_HP[charaNumber]=(int)php;
                        PlayerManager.playerHPBer[charaNumber].fillAmount=PlayerInfo.Player_HP[charaNumber]/PlayerEditorManager.MaxHP[charaNumber];
                        comparText="���}�蓖���J��o����\n���I����HP��"+ (((rate * 100 * 0.3f) / 100)) * PlayerEditorManager.MaxHP[charaNumber]+"�񕜂���";
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
                        BreakerEditor.skillName = "�E�C�̋O��";
                        BreakerEditor.allTime = 65*(2f-addSpeed);
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        float pAtk = PlayerInfo.Player_ATK[charaNumber]*atkBuff;
                        float ehp = EnemyManager.EnemyInfo.Enemy_HP[0] - pAtk * (breakerRate)*GameManager.aliveCount + atkStatusBuff;
                        Debug.Log("�_���[�W��"+pAtk * breakerRate * GameManager.aliveCount);
                        EnemyManager.EnemyInfo.Enemy_HP[0] = ehp;
                        EnemyManager.debugHPBer.fillAmount = ehp / EnemyManager.maxEnemyHP[0];
                        targetText=EnemyNameGet.enemyNameText+"��";
                        comparText="�E�C�̋O�Ղ��J��o����\n" + targetText+ (int)(pAtk * (breakerRate) * GameManager.aliveCount)+"�̃_���[�W";
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
        //moveMember.text="�A���i���[�i��\n�ǂ�����H";
        switch (SkillSelection.SkillNumber)
        {
            case 0:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "�������関��";
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
                        comparText="�������関�����J��o����\n"+"�R�}���h�̗���鑬�x���ω�����";
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
                        NotesEditor.skillName = "��������ߋ�";
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
                        comparText = "��������ߋ����J��o����\n�R�}���h�̗���鑬�x���ω�����";
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
                        NotesEditor.skillName = "���肦���I��";
                    }
                    if(GameManager.state==GameManager.BattleState.move)
                    {
                        rateCorrection=(rate*100*0.3f)/100;
                        comparText="���肦���I�����J��o����\n�R�}���h�̐��������㏸����";
                        StartCoroutine(moveTextCoroutine(comparText));
                        GameManager.moveEnd = true;
                    }
                }
                break;
                case 3:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "�����ւ̒���";
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        float ehp= EnemyManager.EnemyInfo.Enemy_HP[0];
                        ehp -=ehp * (rate * 100 * 0.01f)*0.1f;
                        Debug.Log("ehp��"+ehp);
                        EnemyManager.EnemyInfo.Enemy_HP[0]=ehp;
                        EnemyManager.debugHPBer.fillAmount = ehp / (float)EnemyManager.maxEnemyHP[0];
                        DamageText=((int)(ehp * (rate * 100 * 0.01f) * 0.1f)).ToString();
                        targetText=EnemyNameGet.enemyNameText+"��";
                        comparText ="�����ւ̒������J��o����\n"+targetText+DamageText+"�̃_���[�W��^����";
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
                        BreakerEditor.skillName = "��߂�ꂵ�^��";
                        BreakerEditor.allTime = 50*(2f-addSpeed);
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        annBreakerMaxTime =breakerRate*100+25;
                        annBreakerTime=annBreakerMaxTime;
                        comparText= "��߂�ꂵ�^�����J��o����\n�������^�����AA�̈ꕶ���ɁI";
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
        //moveMember.text = "�S�[�h����\n�ǂ�����H";
        switch (SkillSelection.SkillNumber)
        {
            case 0:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "���̍\��";

                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        DameCutPar=(rate*100)-40f;
                        DameCutTime=DameCutMaxTime;
                        comparText="���̍\�����J��o����\n�S�[�h�����_���[�W���ꕔ�h����悤�ɂȂ���";
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
                        NotesEditor.skillName = "����";
                    }
                    if(GameManager.state==GameManager.BattleState.move)
                    {
                        gordonHateCorrection += 50;
                        hateUpMaxTime=rate*100;
                        hateUpTime=hateUpMaxTime;
                        comparText="�������J��o����\n�G����_���₷���Ȃ���";
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
                        NotesEditor.skillName = "�Ј�";
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        float pAtk=PlayerInfo.Player_ATK[charaNumber];
                        atkDownDeBuff=(int)(rate*pAtk*0.3f);
                        Debug.Log(atkDownDeBuff);
                        atkDownMaxTime= rate * 100 * 0.6f;
                        Debug.Log(atkDownMaxTime);
                        atkDownTime =atkDownMaxTime;
                        comparText="�Ј����J��o����\n�G�̍U���͂���������";
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
                        NotesEditor.skillName = "�h��";
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
                        comparText="�h�˂��J��o����\n"+targetText+"��"+ ((int)addDamage).ToString()+"�̃_���[�W" ;
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
                        BreakerEditor.skillName = "��Ζh�q�w";
                        BreakerEditor.allTime = 85*(2f - addSpeed);
                    }
                     if(GameManager.state==GameManager.BattleState.move)
                    {
                        gordonHateCorrection += 50;
                        gordonBreakerMaxTime=rate*130+130;
                        gordonBreakerTime=gordonBreakerMaxTime;
                        comparText="��Ζh�q�w���J��o����\n�ǂ�ȍU�����h���Ō����܂��傤�I";
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
        //moveMember.text = "�����z��\n�ǂ�����H";
        switch (SkillSelection.SkillNumber)
        {
            case 0:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "�����F�ݑ���";
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        DeBuffSpeed=((2-rate)/2);
                        DeSpeedTime=DeSpeedMaxTime;
                        comparText="����:�ݑ������J��o����\n�G�̍s�����x����������";
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
                        NotesEditor.skillName = "��_���ł��I";
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
                        comparText="��_���ł�!���J��o����\n"+partyChara.transform.GetChild(recoveryTarget).gameObject.name+"��HP��"+
                        (php / PlayerEditorManager.MaxHP[recoveryTarget]).ToString()+"�񕜂���";
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
                        NotesEditor.skillName = "�아�F�";
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        DeInvalidMaxTime=((rate*100)*0.3f)+10;
                        DeInvalidTime=DeInvalidMaxTime;
                        comparText="�아:����J��o����\n��莞�Ԏ�̉��U���𖳌�������";
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
                        NotesEditor.skillName = "�아�F����";
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        RefrectCount=0;
                        RefrectDamage=(PlayerInfo.Player_ATK[charaNumber]*pATKCorrect)/2;
                        for(int i=(int)(rate*100);i<=-1;i-=25)
                        {
                            RefrectCount++;
                        }
                        comparText="�아:�������J��o����\n�_���[�W�̈ꕔ�𔽎˂���悤�ɂȂ���";
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
                        BreakerEditor.skillName = "�֕��F��@�̏��";
                        BreakerEditor.allTime = 50* (2f - addSpeed);
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                       
                        rinBreakerTime =130;
                        rinBreaker=90*rate;
                        comparText="�֕�:��@�̏�ǂ��J��o����\n�����S�̂��_���[�W�̈ꕔ��h����悤�ɂȂ���";
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
    float MagicBarrelBuff=0.5f;
    bool NextBarret;
    public static bool nowTurnExclusion;

    void LetitiaSkill()
    {
        moveText.text = CharaMoveGage.MoveChar[0].name + "�͂ǂ�����H";
        switch (SkillSelection.SkillNumber)
        {
            case 0:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "�T���o���b�g";
                        croutine =(moveTextCoroutine("�G�P�̂Ƀ����_���Ȕ{���Ń_���[�W"));
                        StartCoroutine(croutine);
                        moveTextFlag = true;
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        int rand=Random.Range(1,6);
                        addDamage=(PlayerInfo.Player_ATK[charaNumber]*rate*atkBuff + atkStatusBuff )* rand;
                        Debug.Log("�_���[�W��"+addDamage);
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
                        NotesEditor.skillName = "�A�C�X�����X";
                        croutine =(moveTextCoroutine("�G�S�̂Ƀ����_���Ȕ{���Ń_���[�W"));
                        StartCoroutine(croutine);
                        moveTextFlag = true;
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        int rand = Random.Range(1, 4);
                        addDamage = (PlayerInfo.Player_ATK[charaNumber]  * rate * atkBuff+ atkStatusBuff) * rand;
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
                        NotesEditor.skillName = "�}�W�b�N�o����";
                        croutine =(moveTextCoroutine("�������s�����邽�тɓG�P�̂ɒǉ��_���[�W��^����"));
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
                        MagicBarrel=(int)((PlayerInfo.Player_ATK[charaNumber]*atkBuff + atkStatusBuff )* MagicBarrelBuff);
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
                        NotesEditor.skillName = "���͎��e���U";
                        croutine =(moveTextCoroutine("���Ɏg���}�W�b�N�o�����̌��ʂ��㏸"));
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
        moveText.text = CharaMoveGage.MoveChar[0].name + "�͂ǂ�����H";
        switch(SkillSelection.SkillNumber)
        {
            case 0: 
            {
                    if(GameManager.state == GameManager.BattleState.skillSelect) {
                        NotesEditor.skillName = "��i����";
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
                        comparText = "��i�������J��o����\n" + targetText + "��" + ((int)addDamage).ToString() + "�̃_���[�W";
                        if(rate>=1) {
                            int rand=Random.Range(0,2);//��������100%�̎��̂�50%�̊m���œŏ�Ԃ�t�^
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
                    NotesEditor.skillName = "�A�h������";
                    moveTextFlag = true;
                }
                if(GameManager.state == GameManager.BattleState.move) 
                {
                    float pAtk = PlayerInfo.Player_ATK[charaNumber];
                    DoctorAtkBuff=(int)(pAtk*rate);
                    DoctorAtkBuffTime=60;
                    comparText="�����S�̂̍U���͂��㏸����";
                    StartCoroutine(moveTextCoroutine(comparText));
                    GameManager.moveEnd = true;
                }
            }
            break;
            case 2:
            { 
                if (GameManager.state == GameManager.BattleState.skillSelect)
                {
                    NotesEditor.skillName = "������ˏo";
                    moveTextFlag = true;
                }
                if (GameManager.state == GameManager.BattleState.move)
                {
                     int rand=Random.Range(0,101);
                        sleep=true;
                     if(rand<=100*rate*0.2f)
                     {
                         sleep=true;
                         comparText = "������ˏo���J��o����\n�G�͐[������ɂ���";
                     }
                     else
                     {
                         comparText="������ˏo���J��o����\n�������O���Ă��܂���";
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
                        NotesEditor.skillName = "�ȈՃI�y";
                        moveTextFlag = true;
                    }
                    if(GameManager.state == GameManager.BattleState.move)
                    {

                           Debug.Log("rate��"+rate);
                        if(rate<1&&!debuffDelate)
                        {
                            debuffStrage.RemoveAt(0);
                            debuffDelate=true;
                            GameManager.moveEnd = true;
                        }
                        if(rate==1 &&!debuffDelate)
                        {
                            Debug.Log("�ȈՃI��");
                            debuffStrage.RemoveAt(0);
                            debuffStrage.RemoveAt(0);
                            debuffDelate=true;
                            GameManager.moveEnd = true;
                        }
                        
                        comparText = "�ȈՃI�y���J��o����\n�f�o�t����������";
                        StartCoroutine(moveTextCoroutine(comparText));
                            
                        
                       
                       
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
            case "��l��":
                {
                    PlayerSkill();
                }
                break;
            case "�A���i���[�i":
                {
                    AnnaSkill();
                }
                break;
            case "�S�[�h��":
                {
                    GorDonSkill();
                }
                break;
            case "�����z":
                {
                    RinSkill();
                }
                break;
            case "���e�B�V�A":
                {
                    LetitiaSkill();
                }
                break;
            case "�t�F���X�g": 
                {
                    DoctorSkill();
                }
                break;
        }
    }
    public static int nextCharaNumber=-1;
    [SerializeField]
    private int tmpnextCharaNumber;
    void CharNumberGet()//�s������L���������Ԗڂ̃L���������擾
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

    float BuffTime(float time,float maxTime)//�o�t�̎��Ԃ����炷�֐�
    {
        if(time>0)
        { 
            time-=Time.deltaTime;
            return time;
            if (false)//���̓u���C�J�[�p�@���݂͓��B�ł��Ȃ�
            {
                time=maxTime;
            }
        }
        
        return 0;
    }

    public static float Buff(float time,float buff,float normal)//�o�t�̎��Ԃ��؂ꂽ�珉���l�ɖ߂��֐�
    {
        if(time<=0)
        {
            buff=normal;
        }
        return buff;
    }
    void BuffTimeStorage()//�o�t�̎��Ԃ����炷�֐����ꊇ�ŊǗ����邽�߂̊֐�
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
        DoctorAtkBuffTime=BuffTime(DoctorAtkBuffTime,60f);
        DoctorAtkBuff=Buff(DoctorAtkBuffTime,DoctorAtkBuff,0);
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
                    Debug.Log("�^�[�Q�b�g��"+target);
                }
            }
        }
        return target;
    }
    void ATKBuff()//�����o�t�֌W�͂�����
    {
        atkBuff=((pATKCorrect)+1)*((playerSkill3Buff)+1);//������L�����N�^�[�̊�b�U���͂Ɂ~
    }
    public static int atkStatusBuff;
    void AddStatus() {
        atkStatusBuff=(int)DoctorAtkBuff;
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
