using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerEditorManager;

public class SkillStorage : MonoBehaviour
{
    [SerializeField]
    private Text skilltext;
    public  static float rate;
    [SerializeField]
    private float pATKCorrect=1;
    public static int playerSkill3;
    [SerializeField]
    private float playerSkill3Buff;
    public static int CommandCount;
    private float addDamage;
    [SerializeField]
    private int hate;
    public static float enemyActTime;
    private void Update()
    {
       hate=PlayerEditorManager.PlayerInfo.Player_Hate[2]+gordonHateCorrection;
       
        if (CharaMoveGage.MoveChar[0]!=null)
        { 
            CharaSet();
        }
        if(GameManager.state==GameManager.BattleState.skillSelect)
        { 
            CharNumberGet();
        }
        if(NotesEditor.commandEnd)
        {
            rate = NotesEditor.NotesOKCount / CommandCount;
            Debug.Log(CharaMoveGage.MoveChar[0].name);
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
            CharaMoveGage.ActTime[0]=enemyActTime*DeBuffSpeed;
        
    }
    [SerializeField]
    private GameObject partyChara;
    [SerializeField]
    int charaNumber;
    public static void PlayerSkillExecution()
    {
      
    }
    private const float p2AtkUpMaxTime=45f;
    private float p2AtkUpTime;
    private void PlayerSkill()
    {

        switch (SkillSelection.SkillNumber)
        {
            case 0:
                {//�X���b�V��
                   
                    if(GameManager.state==GameManager.BattleState.skillSelect)
                    { 
                    NotesEditor.skillName="�X���b�V��";
                        skilltext.text="�G��̂ɍU��";
                    }
                    if (GameManager.state==GameManager.BattleState.move)
                    {
                        
                        float pAtk= PlayerInfo.Player_ATK[charaNumber]*pATKCorrect*2;
                        addDamage=(pAtk*rate)*playerSkill3Buff;
                        float ehp= EnemyManager.EnemyInfo.Enemy_HP[0]- pAtk * rate;
                        EnemyManager.EnemyInfo.Enemy_HP[0] = ehp;
                        EnemyManager.debugHPBer.fillAmount=ehp/EnemyManager.maxEnemyHP[0];
                        GameManager.moveEnd=true;
                    }
                }
                break;
                case 1://���u����
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "���u����";
                        skilltext.text="�����S�̂̍U���͂��㏸";
                    }
                    
                    if (GameManager.state == GameManager.BattleState.move)
                    { 
                    pATKCorrect = (NotesEditor.NotesOKCount / CommandCount)+1;
                    p2AtkUpTime=p2AtkUpMaxTime;
                    GameManager.moveEnd=true;
                    }
                    
                }
                break;
                case 2:
                {

                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "�W�Q�H��";
                        skilltext.text="�G�̔�_���[�W����";
                    }
                   if(GameManager.state==GameManager.BattleState.move)
                    { 
                    playerSkill3Buff =(rate*100*0.2f)/100+1;
                    playerSkill3=1;
                        GameManager.moveEnd = true;
                    }
                }
                break;
            case 3:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "���}�蓖";
                        skilltext.text="���g����";
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                       float php=PlayerInfo.Player_HP[charaNumber];
                        php+=(((rate*100*0.3f)/100))*PlayerEditorManager.MaxHP[charaNumber];
                        Debug.Log("����܂���");
                        PlayerInfo.Player_HP[charaNumber]=(int)php;
                        PlayerManager.playerHPBer[charaNumber].fillAmount=PlayerInfo.Player_HP[charaNumber]/PlayerEditorManager.MaxHP[charaNumber];
                        Debug.Log("�ʉ߂��܂���");
                        GameManager.moveEnd = true;
                    }
                }
                break;
        }
    }
    
    public static float addSpeed=1;
    public static int addSpeedTurn;
    [SerializeField]
    private float rateCorrection;
    public static float annaSKill3;
    public static float annaSkill3MaxTime;
    public static float annSkill3Time;
    void AnnaSkill()
    {
        switch(SkillSelection.SkillNumber)
        {
            case 0:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "�������関��";
                        skilltext.text = "�R�}���h���x�㏸";
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        Debug.Log("�������関��");
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
                        GameManager.moveEnd = true;
                    }
                    break;
                }
                case 1:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "��������ߋ�";
                        skilltext.text = "�R�}���h���x����";
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
                        GameManager.moveEnd = true;
                    } 
                }
                break;
                case 2:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "���肦���I��";
                        skilltext.text = "�R�}���h�����������Z";
                    }
                    if(GameManager.state==GameManager.BattleState.move)
                    {
                        rateCorrection=(rate*100*0.3f)/100;
                        GameManager.moveEnd = true;
                    }
                }
                break;
                case 3:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "�����ւ̒���";
                        skilltext.text = "�G�̌���HP�Ɋ����U��";
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        float ehp= EnemyManager.EnemyInfo.Enemy_HP[0];
                        ehp -=ehp * (rate * 100 * 0.01f)*0.1f;
                        Debug.Log("ehp��"+ehp);
                        EnemyManager.EnemyInfo.Enemy_HP[0]=ehp;
                        EnemyManager.debugHPBer.fillAmount = ehp / (float)EnemyManager.maxEnemyHP[0];
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
    void GorDonSkill()
    {
        switch (SkillSelection.SkillNumber)
        {
            case 0:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "���̍\��";
                        skilltext.text = "��_���[�W�����J�b�g";
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
                        NotesEditor.skillName = "����";
                        skilltext.text = "�_���₷���Ȃ�";
                    }
                    if(GameManager.state==GameManager.BattleState.move)
                    {
                        gordonHateCorrection = 50;
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
                        NotesEditor.skillName = "�Ј�";
                        skilltext.text = "�G�̍U���̓_�E��";
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
                        NotesEditor.skillName = "�h��";
                        skilltext.text = "�G��̂ɍU��";
                    }
                    if(GameManager.state == GameManager.BattleState.move)
                    {
                        float pAtk = PlayerInfo.Player_ATK[charaNumber];
                        addDamage = ((pAtk * rate) +PlayerInfo.Player_ATK[charaNumber])*playerSkill3Buff;
                        float ehp = EnemyManager.EnemyInfo.Enemy_HP[0] - pAtk * rate;
                        EnemyManager.EnemyInfo.Enemy_HP[0] = ehp;
                        EnemyManager.debugHPBer.fillAmount = ehp / EnemyManager.maxEnemyHP[0];
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
    private int recoveryTarget;
    void RinSkill()
    {
        switch(SkillSelection.SkillNumber)
        {
            case 0:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "�����F�ݑ���";
                        skilltext.text="�G�s�����x�_�E��";
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                        DeBuffSpeed=1+((rate)/2);
                        DeSpeedTime=DeSpeedMaxTime;
                        GameManager.moveEnd=true;
                    }
                }
                break;
            case 1:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "��_���ł��I";
                        skilltext.text="HP�̍ł��Ⴂ��������";
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
                        NotesEditor.skillName = "�아�F�";
                        skilltext.text = "��Ԉُ�𖳌�";
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
                        NotesEditor.skillName = "�아�F����";
                        skilltext.text = "�_���[�W���˕t�^";
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
        }


    }
    void CharNumberGet()//�s������L���������Ԗڂ̃L���������擾
    {
         for(int i=0;i<4;i++)
        {
            if(partyChara.transform.GetChild(i).gameObject==CharaMoveGage.MoveChar[0])
            {
                charaNumber=i;
            }
        }
    }

    float BuffTime(float time,float maxTime)//�o�t�̎��Ԃ����炷�֐�
    {
        if(time>0)
        { 
            time-=Time.deltaTime;
            return time;
            if (false)//�u���C�J�[�p�@���݂͓��B�ł��Ȃ�
            {
                time=maxTime;
            }
        }
        
        return 0;
    }

    float Buff(float time,float buff,float normal)//�o�t�̎��Ԃ��؂ꂽ�珉���l�ɖ߂��֐�
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
        gordonHateCorrection= (int)Buff(hateUpTime,gordonHateCorrection,0);
        atkDownTime=BuffTime(atkDownTime,atkDownMaxTime);
        atkDownDeBuff=Buff(atkDownTime,atkDownDeBuff,0);
        DeSpeedTime=BuffTime(DeSpeedTime,DeSpeedMaxTime);
        DeBuffSpeed=Buff(DeSpeedTime,DeBuffSpeed,1);
        DeInvalidTime=BuffTime(DeInvalidTime,DeInvalidMaxTime);
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
        int minHP=10000000;
        
        for(int i=0;i<PlayerInfo.Player_HP.Length;i++)
        { 
            if(minHP>PlayerInfo.Player_HP[i])
            {
                if(PlayerInfo.Player_HP[i]>0&&PlayerInfo.Player_HP[i]<PlayerEditorManager.MaxHP[i])
                {
                    minHP=PlayerInfo.Player_HP[i];
                    target=i;
                    Debug.Log("�^�[�Q�b�g��"+target);
                }
            }
        }
        return target;
    }
}
