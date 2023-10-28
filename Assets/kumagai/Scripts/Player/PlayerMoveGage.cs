using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoveGage : MonoBehaviour
{

    private GameObject Char;//配列への代入に使用　あまり気にしなくてよし
    private GameObject[] Char_MoveGage;//キャラクターについているムーブゲージを取得するのに使用　イメージを取るために一度ゲームオブジェクトを経由
    private Image[] Player_MoveGageImage;//ムーブゲージのfillAmountを変更するイメージ
    private int order=0;//fillAmountが１になったとき何番目に格納するかを決定
    public static GameObject[] MoveChar=new GameObject[10];//行動するためのゲージがたまっているキャラを格納 仮で4を入れているが、パーティのキャラ数＋エネミー数が必要
    public static string[] MoveCharName;
    public static float[] ActTime=new float[4];//キャラクターの行動速度　一時的にインスペクターから決定しているが、本来はCSVファイルからとってくる
    float[] elapsedTime=new float[4];//Time.deltaTimeを加算したときに1を超過した場合、fillAmountでは切り捨てられてしまい、他のキャラとの間にずれが生じてしまうので、それを解決するための変数
    // Start is called before the first frame update
    void Start()
    {
        order=0;//初期化
        MoveCharName=new string[PlayerEditor.partyTheNumberOf];
        for(int i=0;i<PlayerEditor.partyTheNumberOf;i++)
        { 
             MoveCharName[i]="";
        }
        Char_MoveGage=new GameObject[this.transform.childCount];//キャラクターの数だけゲームオブジェクト配列を定義
        Player_MoveGageImage=new Image[this.transform.childCount];//同様にイメージを定義
        for(int i=0;i<this.transform.childCount;i++)//キャラクターの数だけ回して、キャラクターの再行動までのゲージ（Image）を取得
        {
            Char=this.transform.GetChild(i).gameObject;
            Char_MoveGage[i]=Char.transform.Find("MoveGage").gameObject;
            Player_MoveGageImage[i]=Char_MoveGage[i].GetComponent<Image>();
        }
    }
    bool Flag;//動作確認用
    // Update is called once per frame
    void Update()
    {
       
        SetMoveChar();
        if (PlayerEditorManager.SetCharStatus)
        {
            if (!Flag && MoveChar[0] == null)//行動しているキャラがいなければ
            {
                for (int i = 0; i < this.transform.childCount; i++)
                {
                    elapsedTime[i] += Time.deltaTime;
                    Player_MoveGageImage[i].fillAmount = elapsedTime[i] / ActTime[i];//fillAmountを加算　ActTimeで割ることでActTime秒でfillAmountが1になる
                    if (Player_MoveGageImage[i].fillAmount >= 1)//fillAmountが１になったキャラを行動するキャラの配列に格納
                    {
                        MoveChar[order] = this.transform.GetChild(i).gameObject;//fillAmoutが1になったキャラを行動するキャラに代入
                        MoveCharName[i]=MoveChar[order].name;
                        Debug.Log(MoveCharName[i]);
                        order += 1;//このキャラの次に行動するキャラをこれの次の配列に代入する為に加算する 複数キャラが同時にたまったときの為に必要
                        elapsedTime[i] -= ActTime[i];//elapsedTimeからActTimeをマイナス　1を超えた分は次に持ち越すことで切り捨てによるズレをなくす。

                        Flag = true;//以上の処理を行うための条件付けを仮で入れている　後で変更
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Return) && MoveChar[0] != null)//仮の条件付け　後で変更
        {//行動したキャラのfillAountをリセットして行動するキャラの配列から削除、配列の中身を詰める作業を行っている
            order -= 1;
            GameObject MG = MoveChar[0].transform.Find("MoveGage").gameObject;
            Image IM = MG.GetComponent<Image>();
            IM.fillAmount = 0;
            MoveChar[0] = null;
            MoveCharName[0]="";
            for (int i = 1; i < 4; i++)
            {
                if (MoveChar[i - 1] == null)
                {
                    MoveChar[i - 1] = MoveChar[i];
                    MoveChar[i] = null;
                }
            }
            if (MoveChar[0] == null)
            {
                Flag = false;
            }
            else if(MoveChar[0]!=null)
            {
                
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
