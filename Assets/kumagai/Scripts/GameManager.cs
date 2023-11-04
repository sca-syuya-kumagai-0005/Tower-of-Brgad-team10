using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
                    if(CharaMoveGage.characterAct)
                    {
                        state=BattleState.skillSelect;
                    }
                }
                break;
            case BattleState.skillSelect:
                {

                }
                break;
            case BattleState.command:
                {

                }
                break;
            case BattleState.effect:
                {

                }
                break;

        }
    }
}
