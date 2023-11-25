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
    public static int enemyTmpHP;
    public static int[] CharaHP=new int[4];
    [SerializeField]private GameObject enemyImage;
    [SerializeField]private GameObject playerDamageImage;
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
                        for (int i = 0; i < 4; i++)
                        {
                            CharaHP[i] = PlayerEditorManager.PlayerInfo.Player_HP[i];
                        }
                        state =BattleState.enemyStay;
                    }
                    enemyTmpHP=(int)EnemyManager.EnemyInfo.Enemy_HP[0];
                }
                break;
            case BattleState.enemyStay:
                {
                    if(EnemyMove.skillOK)
                    {
                        
                        SkillStorage.DBuffTurnStorage();
                        state =BattleState.move;
                     
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
                    
                    
                    if (moveEnd)
                    {
                        if (CharaMoveGage.MoveChar[0].CompareTag("Player")&&!SkillStorage.nowTurnExclusion)
                        {
                            SkillStorage.MagicBarrelDamage();
                        }
                        if (CharaMoveGage.MoveChar[0].CompareTag("Enemy"))
                        {
                            Debug.Log("’Ê‚Á‚½‚æ");
                            for (int i = 0; i < 4; i++)
                            {
                                if (CharaHP[i] != PlayerEditorManager.PlayerInfo.Player_HP[i])
                                {
                                    StartCoroutine(PlayerDamage());
                                }
                            }
                        }
                        state =BattleState.effect; 
                    }
                    
                }
                break;
            case BattleState.effect:
                {
                    if((int)EnemyManager.EnemyInfo.Enemy_HP[0]!=enemyTmpHP)
                    {
                        StartCoroutine(enemyDamage());
                        state = BattleState.flagReSet;
                    }
                    else {
                        state = BattleState.flagReSet;
                    }
                  
                }
                break;
             case BattleState.flagReSet:
                {
                    SkillStorage.nowTurnExclusion=false;
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
    IEnumerator enemyDamage()
    {
        bool flg=false;
        int n=0;
        if(EnemyManager.EnemyInfo.Enemy_HP[0]>0)
        {
            n=6;
        }
        else
        {
            n=5;
        }
        for(int i=0;i<n;i++)
        {
            enemyImage.SetActive(flg);
            yield return new WaitForSeconds(0.1f);
            flg=!flg;
        }
    }
    IEnumerator PlayerDamage()
    {
        playerDamageImage.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        playerDamageImage.SetActive(false);
    }
}
