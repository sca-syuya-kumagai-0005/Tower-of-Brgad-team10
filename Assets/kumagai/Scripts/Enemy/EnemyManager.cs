using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static EnemyManager.EnemyInfo;

public class EnemyManager : MonoBehaviour
{
    
    [SerializeField]
    private GameObject Enemys;
    public static int enemyNumber;
    public static float[] maxEnemyHP=new float[1];
    [SerializeField]private Image HPBer;
    public static Image debugHPBer;
   // [SerializeField]
   // private Text HP;
   // [SerializeField]
   // private Text ATK;
   // [SerializeField]
  //  private Text Name;

    public class EnemyInfo : MonoBehaviour//エネミー情報
    {
        public static string[] Enemy_Name;//エネミーの名前

        public static int[] Enemy_standardHP;//エネミーの基礎HP　
        public static int[] Enemy_risingHP;//エネミーのHP上昇値（レベル毎）
        public static float[] Enemy_minHP;//エネミーのHP最低倍率　
        public static float[] Enemy_maxHP;//エネミーのHP最高倍率

        public static int[] Enemy_standardATK;// エネミーの基礎ATK
        public static int[] Enemy_risingATK;//エネミーのATK上昇値（レベル毎）
        public static float[] Enemy_minATK;//エネミーのATK最低倍率　
        public static float[] Enemy_maxATK;//エネミーのATK最高倍率

        public static int[] Enemy_EXP;//エネミーを倒したときに入手できる経験値量
        //ここまではCSVファイルからの取得
        public static int[] Enemy_Lv;
        public static float[] Enemy_HP;
        public static float[] Enemy_ATK;
    }
    public static bool enemyStatusSet;
    // Start is called before the first frame update
    void Start()
    {
        enemyStatusSet=false;
        debugHPBer=HPBer;
        Enemy_Name=new string[1];
        Enemy_standardHP=new int[1];
        Enemy_risingHP=new int[1];
        Enemy_minHP=new float[1];
        Enemy_maxHP=new float[1];
        Enemy_standardATK=new int[1];
        Enemy_risingATK=new int[1];
        Enemy_minATK=new float[1];
        Enemy_maxATK=new float[1];
        Enemy_EXP=new int[1];
        Enemy_Lv=new int[1];
        Enemy_HP=new float[1];
        Enemy_ATK=new float[1];
        Enemy_Lv[0]=Enemy_Lv[0];
    }

    // Update is called once per frame
    void Update()
    {
       EnemyStatausSet();
    }

    public static void EnemyStataus(List<string[]> EData)
    {
        Enemy_Name[0] = EData[enemyNumber][0];//エネミーの名前を取得
        Enemy_standardHP[0] = int.Parse(EData[enemyNumber][1]);//エネミーの基礎HPを取得
        Enemy_risingHP[0] = int.Parse(EData[enemyNumber][2]);//エネミーのHP上昇値を取得
        Enemy_minHP[0] = float.Parse(EData[enemyNumber][3]);//エネミーのHP最低倍率を取得
        Enemy_maxHP[0] = float.Parse(EData[enemyNumber][4]);//エネミーのHP最高倍率
        Enemy_standardATK[0] = int.Parse(EData[enemyNumber][5]);//エネミーの基礎ATK
        Enemy_risingATK[0] = int.Parse(EData[enemyNumber][6]);//エネミーのATK上昇値
        Enemy_minATK[0] = float.Parse(EData[enemyNumber][7]);//エネミーのATK最低倍率
        Enemy_maxATK[0] = float.Parse(EData[enemyNumber][8]);//エネミーのATK最高倍率
        Enemy_EXP[0] = int.Parse(EData[enemyNumber][9]);//エネミーの経験値
    }

    public static void EnemyStatausSet()
    {
        if (GameManager.state == GameManager.BattleState.enemyStatausSet||GameManager.state==GameManager.BattleState.reSult)
        {
            //enemyNumber = Random.Range(1, Enemys.transform.childCount+1);
            EnemyStataus(EnemyEditor.EnemyData);
            float HPScope = Random.Range(Enemy_minHP[0] * 10, (Enemy_maxHP[0] * 10)) / 10;//*10はintとfloat間の変換
            float tmpEnemy_HP = (Enemy_standardHP[0] + ((Enemy_Lv[0] - 1) * Enemy_risingHP[0])) * HPScope;//式の関係上一度floatで作る
            Enemy_HP[0] = (int)tmpEnemy_HP;//上のfloatをintに変換
            maxEnemyHP[0] = Enemy_HP[0];
            // HP.text=Enemy_HP.ToString();
            float ATKScope = Random.Range(Enemy_minATK[0] * 10, (Enemy_maxATK[0] * 10)) / 10;
            float tmpEnemy_ATK = (Enemy_standardATK[0] + ((Enemy_Lv[0] - 1) * Enemy_risingATK[0])) * ATKScope;//式の関係上一度floatで作る
            Enemy_ATK[0] = (int)tmpEnemy_ATK;//上のfloatをintに変換
            enemyStatusSet = true;
            Enemy_EXP[0]*=FloarManager.nowFloar;
            // ATK.text=Enemy_ATK.ToString();
            // Name.text=Enemy_Name;
        }
    }

    void EnemyHPJudge()
    {
        for(int i=0;i<1;i++)//エネミーの数だけループ　プロト版では敵は一体のみの実装のため1
        {

        }
    }
}
