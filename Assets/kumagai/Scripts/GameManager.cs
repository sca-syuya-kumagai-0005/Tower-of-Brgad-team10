using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public enum BattleState
    {
        start=0,
        enemyStatausSet,
        moveWait,
        enemyStay,
        skillSelect,
        command,
        move,
        effect,
        flagReSet,
        gameEnd,
    }

    public static BattleState state;
    // Start is called before the first frame update
    public static bool moveEnd;
    [SerializeField]private bool GameClear;
    [SerializeField]private bool GameOver;
    [SerializeField]private bool tmpmoveEnd;
    [SerializeField]private Text gameSetText;
    void Start()
    {
        state=BattleState.start;
    }

    // Update is called once per frame
    void Update()
    {
        BattleStateManager();
        Debug.Log(state);
        tmpmoveEnd=moveEnd;
    }

    void BattleStateManager()
    {
        switch (state)
        {
            case BattleState.start:
                { 
                    if(PlayerEditorManager.SetCharStatus&&EnemyEditor.enemyDataSet)
                    {
                        state=BattleState.enemyStatausSet;
                    }
                }
                break;
            case BattleState.enemyStatausSet:
                {
                    if(EnemyManager.enemyStatusSet)
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
                    if(EnemyMove.skillOK)
                    {
                        state=BattleState.move;
                        SkillStorage.DBuffTurnStorage();
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
                    
                    if(NotesEditor.commandEnd)
                    {
                        state=BattleState.move;
                        SkillStorage.BuffTurnStorage();
                    }
                }
                break;
                case BattleState.move:
                {
                    if(moveEnd)
                    { 
                        state=BattleState.effect; 
                    }
                    
                }
                break;
            case BattleState.effect:
                {
                    state=BattleState.flagReSet;
                }
                break;
             case BattleState.flagReSet:
                {
                    SkillStorage.reCoveryTargetFlg=false;
                    SkillSelection.skillSelect = false;
                    NotesEditor.commandStart=false;
                    NotesEditor.commandEnd = false;
                    NotesEditor.lastNotes = false;
                    CharaMoveGage.characterAct = false;
                    EnemyMove.enemyMove = false;
                    EnemyMove.skillOK=false;
                    EnemyMove.skillSet=false;
                    moveEnd=false;
                    SkillSelection.SkillNumber = 0;
                    GameSetController();
                    if(GameOver||GameClear)
                    {
                        state=BattleState.gameEnd;
                    }
                    else 
                    {
                        state = BattleState.moveWait;
                    }
                    
                }
                break;
        }
    }

    void GameSetController()
    {
        int count = 0;
        for (int i=0;i<4;i++)
        {
            if(PlayerManager.playerHPBer[i].fillAmount==0)
            {
                count+=1;
            }
        }
        if(count==4)
        {
            GameOver=true;
            gameSetText.text="GameOver";
        }

        if(EnemyManager.debugHPBer.fillAmount==0)
        {
            GameClear=true;
            gameSetText.text="GameClear";
        }
    }
}
