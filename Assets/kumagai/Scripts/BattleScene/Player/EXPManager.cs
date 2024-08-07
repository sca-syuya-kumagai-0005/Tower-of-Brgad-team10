using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EXPManager : MonoBehaviour
{
    [SerializeField]
    private int[] tmpLv = new int[4];
    private int enemyCount = 1;
    private int GetEXP = 0;
    [SerializeField]
    private int[] LvUpCount = new int[4];
    private bool GetEXPFlag = false;
    public static int[] OverEXP=new int[4];
    [SerializeField]
    private int[] tmpOver=new int[4];
    private bool flag;
    public static int a=5;
    [SerializeField]
    private  Text[] playerLv;
    private GameObject obj;
    private bool EXPGetFlag;
    [SerializeField]
    private GameObject LvUpSheet;
    [SerializeField]
    private Text hpText;
    [SerializeField]
    private Text atkText;
    [SerializeField]
    private Text actText;
    // Start is called before the first frame update
    void Awake()
    {
        //for (int i = 0; i < 4; i++)
        //{
        //    PlayerEditorManager.Lv[i] = 99;
        //}
    }
    void Start()
    {
        playerLv=new Text[PlayerEditor.PlayerName.Length];
        LvUpCharaName=new List<string>();
        LvUpCharaNumber=new List<int>();
        flag=false;
        GetEXPFlag = false;
        EXPGetFlag=false;
        coroutineFlag=false;
        for(int i=0;i<4;i++)
        {
            tmpHp[i] = (int)PlayerEditorManager.MaxHP[i];
            tmpAtk[i] = PlayerEditorManager.PlayerInfo.Player_ATK[i];
            tmpAct[i] = PlayerEditorManager.PlayerInfo.Player_ActTime[i];
        }
    }

    Coroutine coroutine;

    // Update is called once per frame
    void Update()
    {
        if (!flag)
        {
            for (int i = 0; i < 4; i++)
            {
                if(PlayerEditor.PlayerName[i] != "" && PlayerEditor.PlayerName[i] != null) {
                    a =(int)OverEXP[i];
                    float b=a;
                    OverEXP[i]=Mathf.Abs(OverEXP[i]);
                    obj= GameObject.Find("MainCanvas").gameObject.transform.Find("PlayerManager").gameObject;
                    playerLv[i] =GameObject.Find("MainCanvas").gameObject.transform.Find("PlayerManager").gameObject.transform.Find("partyChar").GetChild(i).gameObject.
                        transform.Find("HP").gameObject.transform.Find("Lv").GetComponent<Text>();
                    playerLv[i].text = "Lv"+PlayerEditorManager.Lv[i].ToString();
                    if (OverEXP[i]!=0)
                    {
                        PlayerEditorManager.PlayerInfo.Player_EXP[i] = OverEXP[i];
                    }
                }
            }
            flag=true;
        }
        if(!EXPGetFlag)
        { 
            StartCoroutine(LvJudge()); 
        }
     
      

    }
    [SerializeField]
    private Text CharaName;
    private List<string> LvUpCharaName;
    public int[] tmpHp=new int[4];
    public int[] tmpAtk = new int[4];
    public float[] tmpAct = new float[4];
    public int[] newHp = new int[4];
    public int[] newAtk = new int[4];
    public float[] newAct = new float[4];
    private int LvUpChara;
    public List<int> LvUpCharaNumber;
    IEnumerator  LvJudge()
    {

        if (GameManager.state == GameManager.BattleState.start)
        {
            for (int i = 0; i < 4; i++)
            {
                LvUpCount[i] = 0;
                tmpLv[i] = PlayerEditorManager.Lv[i];
            }

        }
        if (GameManager.state == GameManager.BattleState.reSult)
        {
            if (!GetEXPFlag)
            {
                for (int j = 0; j < enemyCount; j++)
                {
                    GetEXP += EnemyManager.EnemyInfo.Enemy_EXP[j];//�l�����o���l���Z�o
                }
                GetEXPFlag=true;
            }
            
            for (int i = 0; i < 4; i++)
            {
                int EXP = GetEXP;
                bool flag = false;
                while (EXP != 0)
                {
                    EXP -= PlayerEditorManager.PlayerInfo.Player_EXP[i];
                    int exp = EXP;
                   
                    if (exp >= 0)
                    {
                        if(!flag)
                        {
                            LvUpCharaName.Add(PlayerEditor.PlayerName[i]);
                            LvUpCharaNumber.Add(i);
                            LvUpChara++;
                            flag=true;
                        }
                        if(PlayerEditorManager.Lv[i]<=99)
                        {
                            PlayerEditorManager.Lv[i] += 1;
                            LvUpCount[i]++;
                            PlayerEditorManager.PlayerStatas(PlayerEditor.playerDatas[i], i);
                            newHp[i] = (int)PlayerEditorManager.MaxHP[i];
                            newAtk[i] = PlayerEditorManager.PlayerInfo.Player_ATK[i];
                            newAct[i] = PlayerEditorManager.PlayerInfo.Player_ActTime[i];
                        }
                        
                    }
                    else
                    {
                        OverEXP[i] =EXP;
                        EXP = 0;
                        break;
                    }
                }
            }
            
           
            EXPGetFlag = true;
            if (LvUpChara >= 1)
            {
                coroutine = StartCoroutine(LvUpSheetManager());
            }
            else if(!GameManager.GameOver)
            {
                GameManager.loopScene = true;
            }
            yield break;
           
        }
        
    }
    bool coroutineFlag;
     private IEnumerator LvUpSheetManager()
    {
        if (GameManager.state==GameManager.BattleState.reSult&&!coroutineFlag&&GameManager.GameClear)
        {
            coroutineFlag = true;
            yield return new WaitForSeconds(1.5f);
            while (LvUpSheet.transform.position.y > 800)
            {
                Vector3 pos = LvUpSheet.transform.position;
                LvUpSheet.transform.position = new Vector3(pos.x, LvUpSheet.transform.position.y - 1200 * Time.deltaTime, pos.z);
                yield return null;
            }
            for (int i = 0; i < LvUpChara; i++)
            {
                CharaName.text = LvUpCharaName[i];
                hpText.text  = "HP    " + tmpHp[LvUpCharaNumber[i]].ToString() + " ���@" + newHp[LvUpCharaNumber[i]].ToString();
                atkText.text = "ATK   " + tmpAtk[LvUpCharaNumber[i]].ToString() + " ���@" + newAtk[LvUpCharaNumber[i]].ToString();
                actText.text = "SPEED " + tmpAct[LvUpCharaNumber[i]].ToString() + " ���@" + newAct[LvUpCharaNumber[i]].ToString();
                yield return new WaitForSeconds(1.5f);
            }
            GameManager.loopScene = true;
        }
        if(GameManager.loopScene)
        {
            while (LvUpSheet.transform.position.y < 800+1700)
            {
                Vector3 pos = LvUpSheet.transform.position;
                LvUpSheet.transform.position = new Vector3(pos.x, LvUpSheet.transform.position.y + 1200 * Time.deltaTime, pos.z);
                yield return null;
            }
        }
       
        yield break;
    }
}
