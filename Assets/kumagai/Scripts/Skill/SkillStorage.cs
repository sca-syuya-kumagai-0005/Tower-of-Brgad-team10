using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerEditorManager;

public class SkillStorage : MonoBehaviour
{
    private float rate;
    private void Update()
    {
        rate=NotesEditor.NotesOKCount/NotesEditor.skillCommandCount;
        CharStatusGet();
        if(NotesEditor.commandEnd)
        {
            PlayerSkill();
        }
    }
    [SerializeField]
    private GameObject partyChara;
    [SerializeField]
    int charaNumber;
    public static void PlayerSkillExecution(string skillName,string charName)
    {

    }
    private void Skills(string charName)
    {

    }
    private void PlayerSkill()
    {
        
        switch(SkillSelection.SkillNumber)
        {
            case 1:
                {//�X���b�V��
                    Debug.Log(CharaMoveGage.MoveChar[0].name+"�̍U���͂�"+PlayerInfo.Player_ATK[charaNumber]);
                    NotesEditor.skillName="�X���b�V��";
                    if(GameManager.state==GameManager.BattleState.move)
                    {
                       // EnemyManager.EnemyInfo.Enemy_HP[0] -= (PlayerInfo.Player_ATK[charaNumber] * 2)*(
                    }
                }
                break;
                case 2:
                {

                }
                break;
        }
    }

    void CharStatusGet()
    {
         for(int i=0;i<4;i++)
        {
            if(partyChara.transform.GetChild(i).gameObject==CharaMoveGage.MoveChar[0])
            {
                charaNumber=i;
            }
        }
    }
}
