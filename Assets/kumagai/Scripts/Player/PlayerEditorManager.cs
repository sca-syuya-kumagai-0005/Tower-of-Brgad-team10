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
    public static bool SetCharStatus=false;
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
        Player_EXP = new int[partyTheNumberOf];
        Player_ActTime=new float[partyTheNumberOf];
        
    }

    // Update is called once per frame
    void Update()
    {
        
       if(GameManager.state==GameManager.BattleState.start)
        { 
            for (int i = 0; i < partyTheNumberOf; i++)
            {
                PlayerStatas(PlayerEditor.playerDatas[i], i);
            }
            SetCharStatus=true;
        }
        
    }

    public void PlayerStatas(List<string[]> EData,int Integer)
    {
           // Debug.Log(Integer+1+"キャラ目のステータスは");
            Player_HP[Integer] = int.Parse(EData[Lv+2][1]);//キャラHP
           // Debug.Log("HP"+Player_HP[Integer]);
            Player_ATK[Integer] = int.Parse(EData[Lv+2][2]);//キャラ攻撃力
           // Debug.Log("ATK" + Player_ATK[Integer]);
            Player_EXP[Integer] = int.Parse(EData[Lv+2][3]);//キャラの次のレベルまでに必要な経験値
           // Debug.Log("次のレベルまで" + Player_EXP[Integer]);
            Player_ActTime[Integer] = float.Parse(EData[Lv+2][4]);//キャラの再行動までの時間
           // Debug.Log("再行動までの時間は"+Player_ActTime[Integer]+"秒です");
            CharaMoveGage.ActTime[Integer] = Player_ActTime[Integer];
        //[i番目のキャラクター]　[Lv]　[対応するステータス]

    }
}
