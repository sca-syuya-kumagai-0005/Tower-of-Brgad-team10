using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayerEditorManager.PlayerInfo;
using static PlayerEditor;

public class PlayerEditorManager : MonoBehaviour
{
    
    //[SerializeField]
    //private GameObject Players;
    public static int Lv=10;

    public float[] Player_ActTime;
    public class PlayerInfo : MonoBehaviour//プレイヤー情報
    {
        public static string[] Player_Name;//プレイヤーの名前

        //ここまではCSVファイルからの取得
        public static int[] Player_HP;
        public static int[] Player_ATK;
        public static int[] Player_MP;
        public static int[] Player_EXP;

    }
    // Start is called before the first frame update
    void Start()
    {
        Player_HP=new int[partyTheNumberOf];
        Player_ATK = new int[partyTheNumberOf];
        Player_MP = new int[partyTheNumberOf];
        Player_EXP = new int[partyTheNumberOf];

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))//エンカウントしたらに変更する
        {
            PlayerStatas(PlayerEditor.playerDatas);
        }
    }

    public void PlayerStatas(List<string[]>[] EData)
    {
        for(int i=0;i<partyTheNumberOf;i++)
        { 
            Debug.Log(i+1+"キャラ目のステータスは");
            Player_HP[i] = int.Parse(EData[i][Lv+2][1]);//キャラHP
            Debug.Log("HP"+Player_HP[i]);
            Player_MP[i]=int.Parse(EData[i][Lv+2][2]);//キャラのMP
            Debug.Log("MP" + Player_MP[i]);
            Player_ATK[i] = int.Parse(EData[i][Lv+2][3]);//キャラ攻撃力
            Debug.Log("ATK" + Player_ATK[i]);
            Player_EXP[i] = int.Parse(EData[i][Lv+2][4]);//キャラの次のレベルまでに必要な経験値
            Debug.Log("次のレベルまで" + Player_EXP[i]);
            //Player_ActTime[i] = float.Parse(EData[i][Lv+2][5]);//キャラの再行動までの時間
            //[i番目のキャラクター]　[Lv]　[対応するステータス]
        }
    }
}
