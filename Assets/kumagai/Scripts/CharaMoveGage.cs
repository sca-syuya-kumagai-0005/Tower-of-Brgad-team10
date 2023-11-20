using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaMoveGage : MonoBehaviour
{

    private GameObject Char;//�z��ւ̑���Ɏg�p�@���܂�C�ɂ��Ȃ��Ă悵
    private GameObject[] Char_MoveGage;//�L�����N�^�[�ɂ��Ă��郀�[�u�Q�[�W���擾����̂Ɏg�p�@�C���[�W����邽�߂Ɉ�x�Q�[���I�u�W�F�N�g���o�R
    private Image[] Player_MoveGageImage;//���[�u�Q�[�W��fillAmount��ύX����C���[�W
    public static int order = 0;//fillAmount���P�ɂȂ����Ƃ����ԖڂɊi�[���邩������
    [SerializeField]
    private GameObject[] tmpMoveChara = new GameObject[10];
    public static GameObject[] MoveChar=new GameObject[10];//�s�����邽�߂̃Q�[�W�����܂��Ă���L�������i�[ ����4�����Ă��邪�A�p�[�e�B�̃L�������{�G�l�~�[�����K�v
    public static string[] MoveCharName;
    public static float[] ActTime=new float[5];//�L�����N�^�[�̍s�����x�@�ꎞ�I�ɃC���X�y�N�^�[���猈�肵�Ă��邪�A�{����CSV�t�@�C������Ƃ��Ă���
    public static bool characterAct;
    float[] elapsedTime=new float[5];//Time.deltaTime�����Z�����Ƃ���1�𒴉߂����ꍇ�AfillAmount�ł͐؂�̂Ă��Ă��܂��A���̃L�����Ƃ̊Ԃɂ��ꂪ�����Ă��܂��̂ŁA������������邽�߂̕ϐ�
    // Start is called before the first frame update
    void Start()
    {
        order=0;//������
        MoveCharName=new string[PlayerEditor.partyTheNumberOf];
        for(int i=0;i<PlayerEditor.partyTheNumberOf;i++)
        { 
             MoveCharName[i]="";
        }
        Char_MoveGage=new GameObject[this.transform.childCount+1];//�L�����N�^�[�̐������Q�[���I�u�W�F�N�g�z����`+1��enemy
        Player_MoveGageImage=new Image[this.transform.childCount+1];//���l�ɃC���[�W���`
        Char_MoveGage[0]=GameObject.Find("Enemy").transform.GetChild(1).gameObject;//�G�l�~�[�ɂ��Ă���s���Q�[�W���擾
        Player_MoveGageImage[0]=Char_MoveGage[0].GetComponent<Image>();
        for(int i=1;i<this.transform.childCount+1;i++)//�L�����N�^�[�̐������񂵂āA�L�����N�^�[�̍čs���܂ł̃Q�[�W�iImage�j���擾
        {
            Char=this.transform.GetChild(i-1).gameObject;
            Char_MoveGage[i]=Char.transform.Find("MoveGage").gameObject;
            Player_MoveGageImage[i] = Char_MoveGage[i].GetComponent<Image>();

        }
        
    }
    bool Flag;//����m�F�p
    public static bool SetFlag=false;//if(SkillSelection.skillSelect)�ɂĂȂ��������s����邽�߂�����������邽�߂̃t���O
    // Update is called once per frame
    void Update()
    {
        tmpMoveChara = MoveChar;
        if (MoveChar[0]!=null&&GameManager.state==GameManager.BattleState.moveWait&&MoveChar[0].name!="Enemy")
        {
            characterAct=true;
        }
       AddGage();
       MoveCharaSort();

    }

    public static void MoveCharaSort()
    {
        if (SkillSelection.skillSelect || GameManager.state == GameManager.BattleState.flagReSet)//���̏����t���@��ŕύX
        {//�s�������L������fillAount�����Z�b�g���čs������L�����̔z�񂩂�폜�A�z��̒��g���l�߂��Ƃ��s���Ă���
            if (!SetFlag&&GameManager.state==GameManager.BattleState.flagReSet)
            {
                order -= 1;
                GameObject MG = MoveChar[0].transform.Find("MoveGage").gameObject;
                Image IM = MG.GetComponent<Image>();
                IM.fillAmount = 0;
                MoveChar[0]=null;
                MoveCharName[0] = "";
                for (int i = 1; i < 5; i++)
                {
                    if (MoveChar[i - 1] == null)
                    {
                        MoveChar[i - 1] = MoveChar[i];
                        MoveChar[i] = null;
                    }
                }
                SetFlag = true;
            }
        }
    }
    public static bool orderFlag;
    void AddGage()
    {
        if (GameManager.state == GameManager.BattleState.moveWait)
        {
            if (MoveChar[0] == null&&order<=MoveChar.Length-1)//�s�����Ă���L���������Ȃ����
            {
                Debug.Log("MoveChar�̒��g�͂���܂���");
                for (int i = 0; i < Player_MoveGageImage.Length; i++) //
                {
                    elapsedTime[i] += Time.deltaTime;
                    Player_MoveGageImage[i].fillAmount = elapsedTime[i] / ActTime[i];//fillAmount�����Z�@ActTime�Ŋ��邱�Ƃ�ActTime�b��fillAmount��1�ɂȂ�
                    if (Player_MoveGageImage[i].fillAmount >= 1)//fillAmount���P�ɂȂ����L�������s������L�����̔z��Ɋi�[
                    {
                        MoveChar[order] = Player_MoveGageImage[i].transform.parent.gameObject;//fillAmout��1�ɂȂ����L�������s������L�����ɑ��
                        //MoveCharName[i] = MoveChar[order].name;
                        //Debug.Log(MoveCharName[i]);
                        order += 1;//���̃L�����̎��ɍs������L����������̎��̔z��ɑ������ׂɉ��Z���� �����L�����������ɂ��܂����Ƃ��ׂ̈ɕK�v
                        elapsedTime[i] -= ActTime[i];//elapsedTime����ActTime���}�C�i�X�@1�𒴂������͎��Ɏ����z�����ƂŐ؂�̂Ăɂ��Y�����Ȃ����B


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
