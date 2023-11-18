using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerEditorManager;

public class SkillStorage : MonoBehaviour
{
    [SerializeField]
    private float rate;
    [SerializeField]
    private float pATKCorrect=1;
    [SerializeField]
    private float playerSkill2;
    public static int playerSkill3;
    [SerializeField]
    private float playerSkill3Buff;
    [SerializeField]
    GameObject movechara;
    public static int CommandCount;
    private float addDamage;
    private void Update()
    {
        PlayerSkill();
        if(GameManager.state==GameManager.BattleState.skillSelect)
        { 
            CharStatusGet();
        }
        if(NotesEditor.commandEnd)
        {
            Debug.Log(100);
            Debug.Log(CharaMoveGage.MoveChar[0].name);
            CharaSet(movechara.name);
        }
        BuffTimeStorage();
    }
    [SerializeField]
    private GameObject partyChara;
    [SerializeField]
    int charaNumber;
    public static void PlayerSkillExecution()
    {
      
    }
    private void PlayerSkill()
    {
        Debug.Log(SkillSelection.SkillNumber);
        rate = NotesEditor.NotesOKCount / CommandCount;
        switch (SkillSelection.SkillNumber)
        {
            case 0:
                {//ÉXÉâÉbÉVÉÖ
                   
                    if(GameManager.state==GameManager.BattleState.skillSelect)
                    { 
                    NotesEditor.skillName="ÉXÉâÉbÉVÉÖ";
                    }
                    if (GameManager.state==GameManager.BattleState.move)
                    {
                        float pAtk= PlayerInfo.Player_ATK[charaNumber]*pATKCorrect*2;
                        addDamage=(pAtk*rate)*playerSkill3Buff;
                        Debug.Log("pAtkÇÕ"+pAtk*rate);
                        float ehp= EnemyManager.EnemyInfo.Enemy_HP[0]- pAtk * rate;
                        EnemyManager.debugHPBer.fillAmount=ehp/EnemyManager.EnemyInfo.Enemy_HP[0];
                        Debug.Log("ehpÇÕ"+ehp);
                        Debug.Log("enemyÇÃç≈ëÂHPÇÕ"+ EnemyManager.EnemyInfo.Enemy_HP[0]);

                        GameManager.moveEnd=true;
                    }
                }
                break;
                case 1://ì¨éuì¸ç∞
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "ì¨éuì¸ç∞";
                    }
                    
                    if (GameManager.state == GameManager.BattleState.move)
                    { 
                    pATKCorrect = (NotesEditor.NotesOKCount / CommandCount)+1;

                    playerSkill2 =10;
                    }
                    
                }
                break;
                case 2:
                {

                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "ñWäQçHçÏ";
                    }
                   if(GameManager.state==GameManager.BattleState.move)
                    { 
                    playerSkill3Buff =(rate*100*0.2f)/100+1;
                    playerSkill3=1;
                    }
                }
                break;
            case 3:
                {
                    if (GameManager.state == GameManager.BattleState.skillSelect)
                    {
                        NotesEditor.skillName = "âûã}éËìñ";
                    }
                    if (GameManager.state == GameManager.BattleState.move)
                    {
                       float php=PlayerInfo.Player_HP[0];
                        php+=(rate*100*0.3f)*PlayerEditorManager.MaxHP[0];
                        PlayerInfo.Player_HP[0]=(int)php;
                    }
                }
                break;
        }
    }
    void CharaSet(string str)
    {
        if(CharaMoveGage.MoveChar[0].name=="éÂêlåˆ")
        {
            PlayerSkill();
        }
    }
    void CharStatusGet()
    {
         for(int i=0;i<4;i++)
        {
            if(partyChara.transform.GetChild(i).gameObject==CharaMoveGage.MoveChar[0])
            {
                charaNumber=i;
                movechara=CharaMoveGage.MoveChar[0];
            }
        }
    }

    float BuffTime(float time)//ÉoÉtÇÃéûä‘Çå∏ÇÁÇ∑ä÷êî
    {
        if(time>0)
        { 
            time-=Time.deltaTime;
            return time;
        }
        return 0;
    }

    float Buff(float time,float buff,float normal)
    {
        if(time<=0)
        {
            buff=normal;
        }
        return buff;
    }
    void BuffTimeStorage()//ÉoÉtÇÃéûä‘Çå∏ÇÁÇ∑ä÷êîÇàÍäáÇ≈ä«óùÇ∑ÇÈÇΩÇﬂÇÃä÷êî
    {
        playerSkill2=BuffTime(playerSkill2);
        pATKCorrect=Buff(playerSkill2,pATKCorrect,1);
    }
    int DBuffTurn(int turn)
    {
        if(turn>0)
        {
            turn-=1;
            return turn;
        }
        return 0;
    }
    void DBuffTurnStorage()
    {
        DBuffTurn(playerSkill3);
    }
}
