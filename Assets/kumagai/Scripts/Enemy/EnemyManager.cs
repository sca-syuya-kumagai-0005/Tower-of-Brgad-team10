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

    public class EnemyInfo : MonoBehaviour//�G�l�~�[���
    {
        public static string Enemy_Name;//�G�l�~�[�̖��O

        public static int Enemy_standardHP;//�G�l�~�[�̊�bHP�@
        public static int Enemy_risingHP;//�G�l�~�[��HP�㏸�l�i���x�����j
        public static float Enemy_minHP;//�G�l�~�[��HP�Œ�{���@
        public static float Enemy_maxHP;//�G�l�~�[��HP�ō��{��

        public static int Enemy_standardATK;// �G�l�~�[�̊�bATK
        public static int Enemy_risingATK;//�G�l�~�[��ATK�㏸�l�i���x�����j
        public static float Enemy_minATK;//�G�l�~�[��ATK�Œ�{���@
        public static float Enemy_maxATK;//�G�l�~�[��ATK�ō��{��

        public static int Enemy_EXP;//�G�l�~�[��|�����Ƃ��ɓ���ł���o���l��
        //�����܂ł�CSV�t�@�C������̎擾
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
        if (Input.GetKeyDown(KeyCode.A))//�G���J�E���g������ɕύX����
        {
            //enemyNumber = Random.Range(1, Enemys.transform.childCount+1);
            EnemyStataus(EnemyEditor.EnemyData);
            float HPScope=Random.Range(Enemy_minHP*10,(Enemy_maxHP*10))/10;
            float tmpEnemy_HP=(Enemy_standardHP+((Enemy_Lv-1)*Enemy_risingHP))*HPScope;//���̊֌W���xfloat�ō��
            Enemy_HP=(int)tmpEnemy_HP;//���float��int�ɕϊ�
           // HP.text=Enemy_HP.ToString();
            float ATKScope = Random.Range(Enemy_minHP * 10, (Enemy_maxHP * 10)) / 10;
            float tmpEnemy_ATK = (Enemy_standardATK + ((Enemy_Lv - 1) * Enemy_risingATK)) * ATKScope;//���̊֌W���xfloat�ō��
            Enemy_ATK = (int)tmpEnemy_ATK;//���float��int�ɕϊ�
            Debug.Log(Enemy_HP);
            Debug.Log(Enemy_ATK);
           // ATK.text=Enemy_ATK.ToString();
           // Name.text=Enemy_Name;
        }
    }

    void EnemyStataus(List<string[]> EData)
    {
        Enemy_Name = EData[enemyNumber][0];//�G�l�~�[�̖��O���擾
        Debug.Log(Enemy_Name);
        Enemy_standardHP = int.Parse(EData[enemyNumber][1]);//�G�l�~�[�̊�bHP���擾
        Enemy_risingHP = int.Parse(EData[enemyNumber][2]);//�G�l�~�[��HP�㏸�l���擾
        Enemy_minHP = float.Parse(EData[enemyNumber][3]);//�G�l�~�[��HP�Œ�{�����擾
        Enemy_maxHP = float.Parse(EData[enemyNumber][4]);//�G�l�~�[��HP�ō��{��
        Enemy_standardATK = int.Parse(EData[enemyNumber][5]);//�G�l�~�[�̊�bATK
        Enemy_risingATK = int.Parse(EData[enemyNumber][6]);//�G�l�~�[��ATK�㏸�l
        Enemy_minATK = float.Parse(EData[enemyNumber][7]);//�G�l�~�[��ATK�Œ�{��
        Enemy_maxATK = float.Parse(EData[enemyNumber][8]);//�G�l�~�[��ATK�ō��{��
        Enemy_EXP = int.Parse(EData[enemyNumber][9]);//�G�l�~�[�̌o���l
    }
}
