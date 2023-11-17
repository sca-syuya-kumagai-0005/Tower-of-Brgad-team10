using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static EnemyManager.EnemyInfo;

public class EnemyManager : MonoBehaviour
{
    
    [SerializeField]
    private GameObject Enemys;
    public static int enemyNumber=1;
   // [SerializeField]
   // private Text HP;
   // [SerializeField]
   // private Text ATK;
   // [SerializeField]
  //  private Text Name;

    public class EnemyInfo : MonoBehaviour//エネミー情報
    {
        public static string Enemy_Name;//エネミーの名前

        public static int Enemy_standardHP;//エネミーの基礎HP　
        public static int Enemy_risingHP;//エネミーのHP上昇値（レベル毎）
        public static float Enemy_minHP;//エネミーのHP最低倍率　
        public static float Enemy_maxHP;//エネミーのHP最高倍率

        public static int Enemy_standardATK;// エネミーの基礎ATK
        public static int Enemy_risingATK;//エネミーのATK上昇値（レベル毎）
        public static float Enemy_minATK;//エネミーのATK最低倍率　
        public static float Enemy_maxATK;//エネミーのATK最高倍率

        public static int Enemy_EXP;//エネミーを倒したときに入手できる経験値量
        //ここまではCSVファイルからの取得
        public static int Enemy_Lv;
        public static int Enemy_HP;
        public static int Enemy_ATK;
    }
    // Start is called before the first frame update
    void Start()
    {
        Enemy_Lv=10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))//エンカウントしたらに変更する
        {
            //enemyNumber = Random.Range(1, Enemys.transform.childCount+1);
            EnemyStataus(EnemyEditor.EnemyData);
            float HPScope=Random.Range(Enemy_minHP*10,(Enemy_maxHP*10))/10;
            float tmpEnemy_HP=(Enemy_standardHP+((Enemy_Lv-1)*Enemy_risingHP))*HPScope;//式の関係上一度floatで作る
            Enemy_HP=(int)tmpEnemy_HP;//上のfloatをintに変換
           // HP.text=Enemy_HP.ToString();
            float ATKScope = Random.Range(Enemy_minHP * 10, (Enemy_maxHP * 10)) / 10;
            float tmpEnemy_ATK = (Enemy_standardATK + ((Enemy_Lv - 1) * Enemy_risingATK)) * ATKScope;//式の関係上一度floatで作る
            Enemy_ATK = (int)tmpEnemy_ATK;//上のfloatをintに変換
            Debug.Log(Enemy_HP);
            Debug.Log(Enemy_ATK);
           // ATK.text=Enemy_ATK.ToString();
           // Name.text=Enemy_Name;
        }
    }

    void EnemyStataus(List<string[]> EData)
    {
        Enemy_Name = EData[enemyNumber][0];//エネミーの名前を取得
        Debug.Log(Enemy_Name);
        Enemy_standardHP = int.Parse(EData[enemyNumber][1]);//エネミーの基礎HPを取得
        Enemy_risingHP = int.Parse(EData[enemyNumber][2]);//エネミーのHP上昇値を取得
        Enemy_minHP = float.Parse(EData[enemyNumber][3]);//エネミーのHP最低倍率を取得
        Enemy_maxHP = float.Parse(EData[enemyNumber][4]);//エネミーのHP最高倍率
        Enemy_standardATK = int.Parse(EData[enemyNumber][5]);//エネミーの基礎ATK
        Enemy_risingATK = int.Parse(EData[enemyNumber][6]);//エネミーのATK上昇値
        Enemy_minATK = float.Parse(EData[enemyNumber][7]);//エネミーのATK最低倍率
        Enemy_maxATK = float.Parse(EData[enemyNumber][8]);//エネミーのATK最高倍率
        Enemy_EXP = int.Parse(EData[enemyNumber][9]);//エネミーの経験値
    }
}
