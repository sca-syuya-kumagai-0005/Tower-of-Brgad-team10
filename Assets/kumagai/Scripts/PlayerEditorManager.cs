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
    public class PlayerInfo : MonoBehaviour//�v���C���[���
    {
        public static string[] Player_Name;//�v���C���[�̖��O

        //�����܂ł�CSV�t�@�C������̎擾
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
        if (Input.GetKeyDown(KeyCode.A))//�G���J�E���g������ɕύX����
        {
            PlayerStatas(PlayerEditor.playerDatas);
        }
    }

    public void PlayerStatas(List<string[]>[] EData)
    {
        for(int i=0;i<partyTheNumberOf;i++)
        { 
            Debug.Log(i+1+"�L�����ڂ̃X�e�[�^�X��");
            Player_HP[i] = int.Parse(EData[i][Lv+2][1]);//�L����HP
            Debug.Log("HP"+Player_HP[i]);
            Player_MP[i]=int.Parse(EData[i][Lv+2][2]);//�L������MP
            Debug.Log("MP" + Player_MP[i]);
            Player_ATK[i] = int.Parse(EData[i][Lv+2][3]);//�L�����U����
            Debug.Log("ATK" + Player_ATK[i]);
            Player_EXP[i] = int.Parse(EData[i][Lv+2][4]);//�L�����̎��̃��x���܂łɕK�v�Ȍo���l
            Debug.Log("���̃��x���܂�" + Player_EXP[i]);
            //Player_ActTime[i] = float.Parse(EData[i][Lv+2][5]);//�L�����̍čs���܂ł̎���
            //[i�Ԗڂ̃L�����N�^�[]�@[Lv]�@[�Ή�����X�e�[�^�X]
        }
    }
}
