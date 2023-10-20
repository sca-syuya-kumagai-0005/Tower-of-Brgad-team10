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
    private int Lv=0;

    public class PlayerInfo : MonoBehaviour//プレイヤー情報
    {
        public static string[] Player_Name;//プレイヤーの名前

        //ここまではCSVファイルからの取得
        public static int[] Player_HP;
        public static int[] Player_ATK;
        public static int[] Player_MP;
        public static int[] Player_EXP;
        public static float[] Player_ActTime;
    }
    // Start is called before the first frame update
    void Start()
    {
        Player_HP=new int[partyTheNumberOf];
        Player_ATK = new int[partyTheNumberOf];
        Player_MP = new int[partyTheNumberOf];
        Player_EXP = new int[partyTheNumberOf];
        Player_ActTime=new float[partyTheNumberOf];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))//エンカウントしたらに変更する
        {
            Debug.Log(Player_HP.Length);
            Debug.Log(playerDatas.Length);
            for(int i=0;i<partyTheNumberOf;i++)
            { 
                PlayerStatas(PlayerEditor.playerDatas[i],i);
            }
        }
    }

    public void PlayerStatas(List<string[]> EData,int Integer)
    {
            Debug.Log(Integer+1+"キャラ目のステータスは");
            Player_HP[Integer] = int.Parse(EData[Lv+2][1]);//キャラHP
            Debug.Log("HP"+Player_HP[Integer]);
            Player_MP[Integer]=int.Parse(EData[Lv+2][2]);//キャラのMP
            Debug.Log("MP" + Player_MP[Integer]);
            Player_ATK[Integer] = int.Parse(EData[Lv+2][3]);//キャラ攻撃力
            Debug.Log("ATK" + Player_ATK[Integer]);
            Player_EXP[Integer] = int.Parse(EData[Lv+2][4]);//キャラの次のレベルまでに必要な経験値
            Debug.Log("次のレベルまで" + Player_EXP[Integer]);
            Player_ActTime[Integer] = float.Parse(EData[Lv+2][5]);//キャラの再行動までの時間
           // Debug.Log("再行動までの時間は"+Player_ActTime[i]+"秒です");
            //[i番目のキャラクター]　[Lv]　[対応するステータス]
        
    }
}
