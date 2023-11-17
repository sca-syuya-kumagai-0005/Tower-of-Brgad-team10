using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum BattleState
    {
        start=0,
        moveWait,
        enemyStay,
        skillSelect,
        command,
        move,
        effect,
        flagReSet,
    }

    public static BattleState state;
    // Start is called before the first frame update
    void Start()
    {
        state=BattleState.start;
    }

    // Update is called once per frame
    void Update()
    {
        BattleStateManager();
        Debug.Log(state);
    }

    void BattleStateManager()
    {
        switch (state)
        {
            case BattleState.start:
                { 
                    if(PlayerEditorManager.SetCharStatus)
                    {
                        state=BattleState.moveWait;
                    }
                }
                break;
            case BattleState.moveWait:
                {
                    CharaMoveGage.SetFlag=false;
                    if(CharaMoveGage.characterAct)
                    {
                        state=BattleState.skillSelect;
                    }
                    if(EnemyMove.enemyMove)
                    {
                        state=BattleState.enemyStay;
                    }
                }
                break;
            case BattleState.enemyStay:
                {
                    Debug.Log(CharaMoveGage.SetFlag);
                    if(EnemyMove.enemyMove)
                    {
                        state=BattleState.move;
                    }
                }
                break;
            case BattleState.skillSelect:
                {
                    if(SkillSelection.skillSelect)
                    {
                        state=BattleState.command;
                    }
                }
                break;
            case BattleState.command:
                {
                    SkillSelection.skillSelect=false;
                    if(NotesEditor.commandEnd)
                    {
                        state=BattleState.move;
                    }
                }
                break;
                case BattleState.move:
                {
                    state=BattleState.effect;
                }
                break;
            case BattleState.effect:
                {
                    state=BattleState.flagReSet;
                }
                break;
             case BattleState.flagReSet:
                {
                    state = BattleState.moveWait;
                    SkillSelection.skillSelect = false;
                    NotesEditor.commandEnd = false;
                    NotesEditor.lastNotes = false;
                    CharaMoveGage.characterAct = false;
                    EnemyMove.enemyMove = false;
                    SkillSelection.SkillNumber = 0;
                }
                break;

        }
    }
}
