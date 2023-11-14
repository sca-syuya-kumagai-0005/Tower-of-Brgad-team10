using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]private GameObject mainCanvas;
    public enum BattleState
    {
        start=0,
        moveWait,
        skillSelect,
        command,
        effect,
        nextChar,
    }

    public static BattleState state;
    // Start is called before the first frame update
    void Start()
    {
        
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
                        state=BattleState.effect;
                    }
                }
                break;
            case BattleState.effect:
                {
                    SkillSelection.skillSelect=false;
                    NotesEditor.commandEnd=false;
                    NotesEditor.lastNotes=false;
                    CharaMoveGage.characterAct=false;
                    state =BattleState.moveWait;
                }
                break;

        }
    }
}
