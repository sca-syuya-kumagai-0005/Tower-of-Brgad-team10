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
    public static int[] Lv = { 1, 1, 1, 1 };
    private int nowATK;
    public static bool EXPGET = false;
    public static bool SetCharStatus = false;
    public static int[] tmpExp = new int[4];
    public class PlayerInfo : MonoBehaviour//プレイヤー情報
    {
        public static string[] Player_Name;//プレイヤーの名前

        //ここまではCSVファイルからの取得
        public static int[] Player_HP;
        public static int[] Player_ATK;
        public static int[] Player_MP;
        public static int[] Player_EXP = new int[4];
        public static float[] Player_ActTime;
        public static int[] Player_Hate = new int[4];
    }
    public static float[] MaxHP = new float[4];
    public static int[] standardATK = new int[4];
    // Start is called before the first frame update
    void Start()
    {
        Player_HP = new int[partyTheNumberOf];
        Player_ATK = new int[partyTheNumberOf];
        Player_ActTime = new float[partyTheNumberOf];
        for (int i = 0; i < partyTheNumberOf; i++)
        {
            if(PlayerEditor.PlayerName[i]!=""&&PlayerEditor.PlayerName[i]!=null) {
                PlayerStatas(PlayerEditor.playerDatas[i], i);
            }
           
        }
        EXPGET = true;
        SetCharStatus = true;


    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void PlayerStatas(List<string[]> EData, int Integer)
    {
        // Debug.Log(Integer+1+"キャラ目のステータスは");
        Player_HP[Integer] = int.Parse(EData[Lv[Integer] + 1][1]);//キャラHP
                                                                  // Debug.Log("HP"+Player_HP[Integer]);
        Player_ATK[Integer] = int.Parse(EData[Lv[Integer] + 1][2]);//キャラ攻撃力
                                                                   // Debug.Log("ATK" + Player_ATK[Integer]);
        Player_EXP[Integer] = int.Parse(EData[Lv[Integer] + 1][3]); ;//キャラの次のレベルまでに必要な経験値
        
        Debug.Log("次のレベルまで" + Player_EXP[Integer]);
        Player_ActTime[Integer] = float.Parse(EData[Lv[Integer] + 1][4]);//キャラの再行動までの時間
                                                                         // Debug.Log("再行動までの時間は"+Player_ActTime[Integer]+"秒です");
        CharaMoveGage.ActTime[Integer + 1] = Player_ActTime[Integer];
        MaxHP[Integer] = PlayerInfo.Player_HP[Integer];
        standardATK[Integer] = Player_ATK[Integer];
        Player_Hate[Integer] = int.Parse(EData[0][1]);
        Debug.Log("ヘイト値は" + Player_Hate[Integer]);

        //[i番目のキャラクター]　[Lv]　[対応するステータス]

    }
}
