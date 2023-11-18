using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerEditorManager;

public class SkillStorage : MonoBehaviour
{
    [SerializeField]
    private float rate;
    private float pATKCorrect=1;
    [SerializeField]
    GameObject movechara;
    public static int CommandCount;
    private void Update()
    {
        if(GameManager.state==GameManager.BattleState.move)
        { 
            CharStatusGet();
        }
        if(NotesEditor.commandEnd)
        {
            Debug.Log(100);
            Debug.Log(CharaMoveGage.MoveChar[0].name);
            CharaSet(movechara.name);
        }
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
        switch(SkillSelection.SkillNumber)
        {
            case 0:
                {//スラッシュ
                    Debug.Log(CharaMoveGage.MoveChar[0].name+"の攻撃力は"+PlayerInfo.Player_ATK[charaNumber]);
                    NotesEditor.skillName="スラッシュ";
                    rate = NotesEditor.NotesOKCount / CommandCount;
                    if (GameManager.state==GameManager.BattleState.move)
                    {
                        float pAtk= PlayerInfo.Player_ATK[charaNumber]*pATKCorrect*2;
                        Debug.Log("pAtkは"+pAtk*rate);
                        float ehp= EnemyManager.EnemyInfo.Enemy_HP[0]- pAtk * rate;
                        EnemyManager.debugHPBer.fillAmount=ehp/EnemyManager.EnemyInfo.Enemy_HP[0];
                        Debug.Log("ehpは"+ehp);
                        Debug.Log("enemyの最大HPは"+ EnemyManager.EnemyInfo.Enemy_HP[0]);

                        GameManager.moveEnd=true;
                    }
                }
                break;
                case 1:
                {

                }
                break;
        }
    }
    void CharaSet(string str)
    {
        if(CharaMoveGage.MoveChar[0].name=="主人公")
        {
            PlayerSkill();
            Debug.Log("trueです");
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
}
