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
        breakerCommand,
        move,
        effect,
        flagReSet,
        reSult,
        DebugStay,
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
    public static bool[] aliveFlag=new bool[5];
    [SerializeField] private bool[] tmpAlliveFlag;
    [SerializeField]private GameObject enemyImage;
    [SerializeField]private GameObject playerDamageImage;
    [SerializeField]private GameObject BackGround;
    [SerializeField]private GameObject Enemy;
    [SerializeField]private GameObject Player;
    [SerializeField]private GameObject Floor;
    private bool backGroundWalk;
    
    //private void Awake()
    //{
    //    StartCoroutine(Walk());
    //}
    void Start()
    {
        Enemy.SetActive(true);
        Floor.SetActive(true);
        Player.SetActive(true);
        aliveCount=PlayerEditor.PlayerName.Length;
        state=BattleState.start;
        enemyImage=GameObject.Find(EnemySponer.enemy.name);
        for(int i=0;i<aliveFlag.Length;i++)
        {
            aliveFlag[i]=true;
            tmpAlliveFlag=aliveFlag;
        }
        backGroundWalk=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameClear)StartMoveCharacter();
        else EndMoveCharacter();
        if(backGroundWalk)Walk();
        tmpAlliveFlag=aliveFlag;
        BattleStateManager();
        CharaAliveJudge();
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
                    if (BreakerEditor.breakerGageMax)
                    {
                        SkillSelection.skillCount = 5;
                    }
                    else
                    {
                        SkillSelection.skillCount = 4;
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
                        for (int i = 0; i < PlayerEditor.PlayerName.Length; i++)
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
                    if(SkillSelection.skillSelect&&!SkillSelection.breakerFlag)
                    {
                        state=BattleState.command;
                    }
                    else if(SkillSelection.skillSelect&&SkillSelection.breakerFlag)
                    {
                        state=BattleState.breakerCommand;
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
            case BattleState.breakerCommand:
                {
                    if(BreakerEditor.commandEnd)
                    {
                        state = BattleState.move;
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
                            SkillStorage.ImnRecovery();
                            SkillStorage.nowTurnExclusion=true;
                        }
                        if (CharaMoveGage.MoveChar[0].CompareTag("Enemy"))
                        {
                            Debug.Log("’Ê‚Á‚½‚æ");
                            for (int i = 0; i < PlayerEditor.PlayerName.Length; i++)
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
                    
                    if ((int)EnemyManager.EnemyInfo.Enemy_HP[0]!=enemyTmpHP)
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
                    if (NowCharaSet.nowChara!=null)
                    { 
                        NowCharaSet.nowChara.SetActive(false);
                        NowCharaSet.nowChara=null;
                    }
                    if(NowCharaSet.nextChara!=null)
                    {
                        NowCharaSet.nextChara.SetActive(false);
                        NowCharaSet.nextChara=null;
                    }
                    if (BreakerEditor.breakerGageMax)
                    {
                        SkillSelection.skillCount = 5;
                    }
                    else
                    {
                        SkillSelection.skillCount = 4;
                    }
                    SkillStorage.comparText="";
                    MoveTextController.moveTextFlag=false;
                    NotesEditor.commandDestroy=0;
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
                    BreakerEditor.NotesOKCount=0;
                    BreakerEditor.commandDestroy=0;
                    BreakerEditor.commandEnd=false;
                    BreakerEditor.NotesCreate=false;
                    moveEnd=false;
                    SkillSelection.SkillNumber = 0;
                    SkillSelection.breakerFlag=false;
                    SkillStorage.rate=0;
                    SkillStorage.debuffDelate=false;
                    SkillStorage.Buff(EnemyMove.atkUpcorrection,EnemyMove.atkUpTurn,1);
                    SkillStorage.BuffTurn(EnemyMove.atkUpTurn);
                    BreakerEditor.circleSet=false;
                    StartCoroutine(GameSetController());
                    if (GameOver||GameClear)
                    {
                        state=BattleState.reSult;
                    }
                    else 
                    {
                        state = BattleState.moveWait;
                    }
                    
                }
                break;
            case BattleState.reSult:
                {
                    state=BattleState.DebugStay;
                   
                }
                break;
            //case BattleState.DebugStay:
            //    {

            //    }
            //    break;
        }
    }

    public static int aliveCount=4;
    IEnumerator GameSetController()
    {
        aliveCount=4;
        for (int i=0;i<PlayerEditor.PlayerName.Length;i++)
        {
            if(PlayerEditor.PlayerName[i] != "" && PlayerEditor.PlayerName[i] != null) {
                if(PlayerManager.playerHPBer[i].fillAmount==0)
                {
                    aliveCount-=1;
                }
            }
        }
        if(aliveCount==0)
        {
            GameOver=true;
            gameSetText.text="LOSE";
            StartCoroutine(MoveTextController.moveTextCoroutine(gameSetText.text));
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("TitleScene");
        }

        if(EnemyManager.EnemyInfo.Enemy_HP[0]<=0)
        {
            Debug.Log("“G‚ð“|‚µ‚Ü‚µ‚½");
            gameSetText.text="WIN";
            StartCoroutine(MoveTextController.moveTextCoroutine(gameSetText.text));
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("BattleScene");
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
        if(n==5) {
            yield return new WaitForSeconds(3);
            GameClear = true;
        }
    }
    IEnumerator PlayerDamage()
    {
        playerDamageImage.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        playerDamageImage.SetActive(false);
    }

    IEnumerator BattleEndWait()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("BattleScene");
    }

    void CharaAliveJudge()
    {
        for(int i=0;i<PlayerManager.playerHPBer.Length;i++)
        {
            if(PlayerEditor.PlayerName[i]!=""&&PlayerEditor.PlayerName[i]!=null) { 
            if(PlayerManager.playerHPBer[i].fillAmount==0)
            {
                PlayerManager.playerDeadBackGround[i].SetActive(true);
                if(CharaMoveGage.MoveChar[0]!=null)
                {
                    if (CharaMoveGage.MoveChar[0].name == PlayerEditor.PlayerName[i])
                    {
                        Debug.Log("’Ê‰ß‚µ‚Ä‚¢‚Ü‚·");
                        CharaMoveGage.MoveChar[0] = null;
                        if(CharaMoveGage.MoveChar[1]!=null)
                        {
                                //for (int j = 1; j < 5; j++)
                                //{
                                //    if (CharaMoveGage.MoveChar[j - 1] == null)
                                //    {
                                //        CharaMoveGage.order--;
                                //        CharaMoveGage. MoveChar[j - 1] = CharaMoveGage.MoveChar[j];
                                //        CharaMoveGage. MoveChar[j] = null;
                                    
                                //    }
                                //}
                            
                        }
                    }
                }
                aliveFlag[i+1]=false;
            }
            }
        }
    }
    float f=0;
    private void  Walk()
    {
        float y;
        if(f<=10)
        { 
            f+=Time.deltaTime*5;
            y=Mathf.Sin(f)/2;
            BackGround.transform.position=new Vector3(0,y,0);
        }
    }

    [SerializeField] private GameObject MoveChara;
    float x;
    private void  StartMoveCharacter() {
        x = MoveChara.transform.position.x;
        Debug.Log(MoveChara.transform.position);
            Debug.Log(x);
            if(x<-4.4f) 
            {
                x += Time.deltaTime*10;
                MoveChara.transform.position = new Vector3(x, MoveChara.transform.position.y, MoveChara.transform.position.z);
            }
            else {
                x=-4.4f;
                MoveChara.transform.position = new Vector3(x, MoveChara.transform.position.y, MoveChara.transform.position.z);
            }
    }

    private void EndMoveCharacter() {
        x = MoveChara.transform.position.x;
        if(x>=-10) {
            x -= Time.deltaTime * 10;
            MoveChara.transform.position = new Vector3(x, MoveChara.transform.position.y, MoveChara.transform.position.z);
        }
        else {
            backGroundWalk=true;
            Enemy.SetActive(false);
            Player.SetActive(false);
            Floor.SetActive(false);
        }
    }
}
