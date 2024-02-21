using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
    public static bool GameClear;
    public static bool GameOver;
    [SerializeField]private bool tmpmoveEnd;
    [SerializeField]private Text gameSetText;
    public static int enemyTmpHP;
    public static int[] CharaHP=new int[4];
    public static bool[] aliveFlag=new bool[5];
    [SerializeField]private bool[] tmpAlliveFlag;
    [SerializeField]private GameObject enemyImage;
    [SerializeField]private GameObject playerDamageImage;
    [SerializeField]private GameObject BackGround;
    [SerializeField]private GameObject Enemy;
    [SerializeField]private GameObject Player;
    [SerializeField]private GameObject Floor;
    [SerializeField]private GameObject redJudge;
    [SerializeField]private GameObject backGround;
    private bool backGroundWalk;
    void Awake()
    {
        state = BattleState.start;
    }
    //private void Awake()
    //{
    //    StartCoroutine(Walk());
    //}
    void Start()
    {
        
        GameClear=false;
        GameOver=false;
        loopScene=false;
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

        if (GameOver || GameClear)
        {

            state = BattleState.reSult;
        }
        Debug.Log(state);
        if(!GameClear)StartMoveCharacter();
        else EndMoveCharacter();
        if(backGroundWalk&&loopScene)Walk();
        tmpAlliveFlag=aliveFlag;
        BattleStateManager();
        CharaAliveJudge();
        
        
        tmpmoveEnd=moveEnd;
        if (GameOver || GameClear)
        {
            state = BattleState.reSult;
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameOver = true;
            state =BattleState.reSult;
        }
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
                    if(BreakerEditor.breakerGageMax)
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
                    if(CharaMoveGage.characterAct&&!GameClear)
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
                            EnemyMove.KerberosPoison();
                        }
                        if (CharaMoveGage.MoveChar[0].CompareTag("Enemy"))
                        {
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
                        EnemyMove.goblinBuff--;
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
                    redJudge.SetActive(false);
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
                    ScoreManager.addScoreFlag=false;
                    SkillStorage.Buff(EnemyMove.atkUpcorrection,EnemyMove.atkUpTurn,1);
                    SkillStorage.Buff(EnemyMove.octopusPotSkill1Buff,EnemyMove.octopusPostSkill1Turn,1);
                    SkillStorage.Buff(EnemyMove.octopusPotSkill4Buff,EnemyMove.octopusPotSkill4Turn,1);
                    EnemyMove.octopusPotSkill4Turn--;
                    if(EnemyMove.octopusPotSkill4Turn<=0)
                    {
                        EnemyMove.octopusPotSkill4Buff=1;
                    }
                    EnemyMove.atkUpTurn=SkillStorage.BuffTurn(EnemyMove.atkUpTurn,BuffManager.publicEBuffStorage,0);
                    EnemyMove.moveUpTurn=SkillStorage.BuffTurn(EnemyMove.moveUpTurn,BuffManager.publicEBuffStorage,10);
                    EnemyMove.stoneSpeedTurn=SkillStorage.BuffTurn(EnemyMove.stoneSpeedTurn,BuffManager.publicPDeBuffStorage,2);
                    EnemyMove.succubusSkill2Turn=SkillStorage.BuffTurn(EnemyMove.succubusSkill2Turn,BuffManager.publicPDeBuffStorage,5);
                    EnemyMove.octopusPostSkill1Turn=SkillStorage.BuffTurn(EnemyMove.octopusPostSkill1Turn,BuffManager.publicEBuffStorage,3);
                    EnemyMove.octopusPotSkill4Turn=SkillStorage.BuffTurn(EnemyMove.octopusPotSkill4Turn,BuffManager.publicEBuffStorage,4);
                    EnemyMove.kerberosBuffTurn=SkillStorage.BuffTurn(EnemyMove.kerberosBuffTurn,BuffManager.publicEBuffStorage,0);
                    EnemyMove.kerberosPoisonTurn = SkillStorage.BuffTurn(EnemyMove.kerberosPoisonTurn, BuffManager.publicPDeBuffStorage, 4);
                    SkillStorage.Buff(EnemyMove.kerberosBuff,EnemyMove.kerberosBuffTurn,1);
                    BreakerEditor.circleSet=false;
                    AliveJudge();
                    for(int i=0;i<4;i++)
                    {
                        if(PlayerEditorManager.PlayerInfo.Player_HP[i]!=0)
                        {
                            break;
                        }
                        else
                        {
                            if (i == 4)
                            {
                                GameOver=true;
                            }
                        }
                    }
                      StartCoroutine(wait());
                    
                }
                break;
            case BattleState.reSult:
                {
                    StartCoroutine(GameSetController());
                    state =BattleState.DebugStay;
                   
                }
                break;
            //case BattleState.DebugStay:
            //    {

            //    }
            //    break;
        }
    }
    IEnumerator wait()
    {
        yield return null;
        state = BattleState.moveWait;
    }

    void AliveJudge()
    {
        bool flag=false;
        for(int i=0;i<4;i++)
        {
            if(PlayerEditorManager.PlayerInfo.Player_HP[i]>0)
            {
                flag=true;
                break;
            }
        }
        if(!flag)
        {
            GameOver=true;
        }
    }
    public static bool loopScene;
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
            yield return new WaitForSeconds(3);
        }

        if(loopScene)
        {
            yield return new WaitForSeconds(3);
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
            GameClear = true;
            yield return new WaitForSeconds(3);
            
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
        if(EnemyManager.EnemyInfo.Enemy_HP[0]>0)
        {
            for (int i = 0; i < PlayerManager.playerHPBer.Length; i++)
            {
                if (PlayerEditor.PlayerName[i] != "" && PlayerEditor.PlayerName[i] != null)
                {
                    if (PlayerManager.playerHPBer[i].fillAmount == 0)
                    {
                        PlayerManager.playerDeadBackGround[i].SetActive(true);
                        if (CharaMoveGage.MoveChar[0] != null)
                        {
                            if (CharaMoveGage.MoveChar[0].name == PlayerEditor.PlayerName[i])
                            {
                                CharaMoveGage.MoveChar[0] = null;
                                if (CharaMoveGage.MoveChar[1] != null)
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
                        aliveFlag[i + 1] = false;
                    }
                }
            }
        }
       
    }
    float f=0;
    private void  Walk()
    {
        float y;
        Vector3 size=backGround.GetComponent<RectTransform>().localScale;
        if(f<=10)
        { 
            f+=Time.deltaTime*5;
            y=-Mathf.Abs(Mathf.Sin(f)/2);
            size+=new Vector3(size.x,size.y,size.z)*Time.deltaTime/5;
            backGround.GetComponent<RectTransform>().localScale=size;
            BackGround.transform.position=new Vector3(0,y,0);
        }
    }

    [SerializeField] private GameObject MoveChara;
    float x;
    private void StartMoveCharacter() {
        x = MoveChara.transform.position.x;
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
