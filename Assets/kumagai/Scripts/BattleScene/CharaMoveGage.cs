using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaMoveGage : MonoBehaviour
{

    public static GameObject Char;//配列への代入に使用　あまり気にしなくてよし
    public static GameObject[] Char_MoveGage;//キャラクターについているムーブゲージを取得するのに使用　イメージを取るために一度ゲームオブジェクトを経由
    [SerializeField]
    private Image[] Player_MoveGageImage;//ムーブゲージのfillAmountを変更するイメージ
    public static int order = 0;//fillAmountが１になったとき何番目に格納するかを決定
    public static List<GameObject> MoveChar = new List<GameObject>();//行動するためのゲージがたまっているキャラを格納 仮で4を入れているが、パーティのキャラ数＋エネミー数が必要
    public static string[] MoveCharName;
    public List<GameObject> tes;
    public static float[] ActTime=new float[5];//キャラクターの行動速度　一時的にインスペクターから決定しているが、本来はCSVファイルからとってくる
    public static bool characterAct;
    public static string enemyName;
    [SerializeField]
    private GameObject needle;
    float[] elapsedTime=new float[5];//Time.deltaTimeを加算したときに1を超過した場合、fillAmountでは切り捨てられてしまい、他のキャラとの間にずれが生じてしまうので、それを解決するための変数
    // Start is called before the first frame update

    private void OnEnable()
    {
        MoveChar = new List<GameObject>();
        enemyName = EnemySponer.sponeEnemy[0].name;
        alpha=0;
        Debug.Log(enemyName);
        needle.transform.rotation=new Quaternion(0,0,0,1);
    }
    void Start()
    {
        order=0;//初期化
        MoveCharName=new string[PlayerEditor.partyTheNumberOf];
        for(int i=0;i<PlayerEditor.partyTheNumberOf;i++)
        { 
             MoveCharName[i]="";
        }
        Char_MoveGage=new GameObject[this.transform.childCount+1];//キャラクターの数だけゲームオブジェクト配列を定義+1はenemy
        Player_MoveGageImage=new Image[4+1];//同様にイメージを定義
        
        Char_MoveGage[0]=GameObject.Find("Enemy").transform.GetChild(0).gameObject;//エネミーについている行動ゲージを取得
        Player_MoveGageImage[0]=Char_MoveGage[0].transform.Find("MoveGageBackGround").GetComponent<Image>();
        int count=1;
        for(int i=1;i<4+1;i++)//キャラクターの数だけ回して、キャラクターの再行動までのゲージ（Image）を取得
        {
            
            Char=this.transform.GetChild(i-1).gameObject;
            if(Char.name!="NullPrefab(Clone)")
            {
                Char_MoveGage[count] = Char.transform.Find("MoveGage").gameObject.transform.Find("MoveGage").gameObject;
                Player_MoveGageImage[count] = Char_MoveGage[i].GetComponent<Image>();
                count++;
            }
            else { 
                }
        }
        
    }
    bool Flag;//動作確認用
    public static bool SetFlag=false;//if(SkillSelection.skillSelect)にてなぜか二回実行されるためそれを解決するためのフラグ
    // Update is called once per frame
    void Update()
    {
        tes=MoveChar;
        //if(MoveChar[0]==null&&MoveChar[1]!=null)
        //{
        //    MoveChar[0]=MoveChar[1];
        //    MoveChar[1]=null;
        //}
        if (MoveChar.Count != 0)
        {
            if (GameManager.state == GameManager.BattleState.moveWait && !MoveChar[0].CompareTag("Enemy"))
            {
                characterAct = true;
            }
        }
        
        Player_MoveGageImage[0].color = new Color(0 + alpha, 1 - alpha, 1 - alpha, 1);
        AddGage();
       MoveCharaSort();

    }

    public static void MoveCharaSort()
    {
        if (SkillSelection.skillSelect || GameManager.state == GameManager.BattleState.flagReSet)//仮の条件付け　後で変更
        {//行動したキャラのfillAountをリセットして行動するキャラの配列から削除、配列の中身を詰める作業を行っている
            if (!SetFlag&&GameManager.state==GameManager.BattleState.flagReSet)
            {
                order -= 1;
                GameObject MG;
                if(MoveChar[0].name!="EnemyMoveGage")
                {
                MG = MoveChar[0].transform.Find("MoveGage").gameObject.transform.Find("MoveGage").gameObject;
                }
                else
                {
                  MG = MoveChar[0].transform.Find("MoveGageBackGround").gameObject;
                }
                Image IM = MG.GetComponent<Image>();
                IM.fillAmount = 0;
                MoveChar.RemoveAt(0);
                MoveCharName[0] = "";
                SetFlag = true;
            }
        }
    }
    public static bool orderFlag;
    public static float alpha;
    void AddGage()
    {
        if (GameManager.state == GameManager.BattleState.moveWait||GameManager.state==GameManager.BattleState.effect)
        {
            if (MoveChar.Count==0)//行動しているキャラがいなければ
            {
                for (int i = 0; i < 5; i++) //
                {
                    if(Player_MoveGageImage[i].transform.parent.CompareTag("Enemy"))
                    {
                        elapsedTime[i]+=Time.deltaTime * SkillStorage.DeBuffSpeed;
                        needle.transform.Rotate(0,0,-360*Time.deltaTime*SkillStorage.DeBuffSpeed/ActTime[0]);
                        Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                        alpha +=Time.deltaTime/ActTime[0]*SkillStorage.DeBuffSpeed;
                    }
                    else { 
                    elapsedTime[i] += Time.deltaTime*SkillStorage.gabBuff*EnemyMove.stoneSpeedDebuff;
                    }
                    if(Player_MoveGageImage[i].transform.parent.CompareTag("Enemy"))
                    {
                        Player_MoveGageImage[i].color = new Color(0 + alpha, 1 - alpha, 1 - alpha, 1);
                    }
                    else if(GameManager.aliveFlag[i])
                    {
                        Player_MoveGageImage[i].fillAmount = elapsedTime[i] / ActTime[i];
                    }
                    
                   //fillAmountを加算　ActTimeで割ることでActTime秒でfillAmountが1になる
                    
                   
                    if(alpha>=1 && Player_MoveGageImage[i].name == "MoveGageBackGround"&&!GameManager.moveEnd)
                    {
                        
                        MoveChar.Add(Player_MoveGageImage[i].transform.parent.gameObject);//fillAmoutが1になったキャラを行動するキャラに代入
                        //MoveCharName[i] = MoveChar[order].name;
                        Debug.Log(Player_MoveGageImage[i].transform.parent.gameObject);
                        //Debug.Log(MoveCharName[i]);
                        order += 1;//このキャラの次に行動するキャラをこれの次の配列に代入する為に加算する 複数キャラが同時にたまったときの為に必要
                        elapsedTime[i] -= ActTime[i];
                        alpha-=1;
                    }
                    if (Player_MoveGageImage[i].fillAmount >= 1 && Player_MoveGageImage[i].name == "MoveGage")//fillAmountが１になったキャラを行動するキャラの配列に格納
                    {
                        GameObject obj=Player_MoveGageImage[i].transform.parent.gameObject.transform.parent.gameObject;
                        Debug.Log(obj.name);//ここまではいい
                        MoveChar.Add(obj);//fillAmoutが1になったキャラを行動するキャラに代入
                        //MoveCharName[i] = MoveChar[order].name;
                        //Debug.Log(MoveCharName[i]);
                       //このキャラの次に行動するキャラをこれの次の配列に代入する為に加算する 複数キャラが同時にたまったときの為に必要
                        elapsedTime[i] -= ActTime[i];//elapsedTimeからActTimeをマイナス　1を超えた分は次に持ち越すことで切り捨てによるズレをなくす。
                    }
                }
            }
        }
    }
    void CheckMoveChar()
    {
            
        
    }
    void SetMoveChar()
    {
       
    }
}
