using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoveGage : MonoBehaviour
{

    private GameObject Char;//�z��ւ̑���Ɏg�p�@���܂�C�ɂ��Ȃ��Ă悵
    private GameObject[] Char_MoveGage;//�L�����N�^�[�ɂ��Ă��郀�[�u�Q�[�W���擾����̂Ɏg�p�@�C���[�W����邽�߂Ɉ�x�Q�[���I�u�W�F�N�g���o�R
    private Image[] Player_MoveGageImage;//���[�u�Q�[�W��fillAmount��ύX����C���[�W
    private int order=0;//fillAmount���P�ɂȂ����Ƃ����ԖڂɊi�[���邩������
    public static GameObject[] MoveChar=new GameObject[10];//�s�����邽�߂̃Q�[�W�����܂��Ă���L�������i�[ ����4�����Ă��邪�A�p�[�e�B�̃L�������{�G�l�~�[�����K�v
    public static string[] MoveCharName;
    public static float[] ActTime=new float[4];//�L�����N�^�[�̍s�����x�@�ꎞ�I�ɃC���X�y�N�^�[���猈�肵�Ă��邪�A�{����CSV�t�@�C������Ƃ��Ă���
    float[] elapsedTime=new float[4];//Time.deltaTime�����Z�����Ƃ���1�𒴉߂����ꍇ�AfillAmount�ł͐؂�̂Ă��Ă��܂��A���̃L�����Ƃ̊Ԃɂ��ꂪ�����Ă��܂��̂ŁA������������邽�߂̕ϐ�
    // Start is called before the first frame update
    void Start()
    {
        order=0;//������
        MoveCharName=new string[PlayerEditor.partyTheNumberOf];
        for(int i=0;i<PlayerEditor.partyTheNumberOf;i++)
        { 
             MoveCharName[i]="";
        }
        Char_MoveGage=new GameObject[this.transform.childCount];//�L�����N�^�[�̐������Q�[���I�u�W�F�N�g�z����`
        Player_MoveGageImage=new Image[this.transform.childCount];//���l�ɃC���[�W���`
        for(int i=0;i<this.transform.childCount;i++)//�L�����N�^�[�̐������񂵂āA�L�����N�^�[�̍čs���܂ł̃Q�[�W�iImage�j���擾
        {
            Char=this.transform.GetChild(i).gameObject;
            Char_MoveGage[i]=Char.transform.Find("MoveGage").gameObject;
            Player_MoveGageImage[i]=Char_MoveGage[i].GetComponent<Image>();
        }
    }
    bool Flag;//����m�F�p
    // Update is called once per frame
    void Update()
    {
       
        SetMoveChar();
        if (PlayerEditorManager.SetCharStatus)
        {
            if (!Flag && MoveChar[0] == null)//�s�����Ă���L���������Ȃ����
            {
                for (int i = 0; i < this.transform.childCount; i++)
                {
                    elapsedTime[i] += Time.deltaTime;
                    Player_MoveGageImage[i].fillAmount = elapsedTime[i] / ActTime[i];//fillAmount�����Z�@ActTime�Ŋ��邱�Ƃ�ActTime�b��fillAmount��1�ɂȂ�
                    if (Player_MoveGageImage[i].fillAmount >= 1)//fillAmount���P�ɂȂ����L�������s������L�����̔z��Ɋi�[
                    {
                        MoveChar[order] = this.transform.GetChild(i).gameObject;//fillAmout��1�ɂȂ����L�������s������L�����ɑ��
                        MoveCharName[i]=MoveChar[order].name;
                        Debug.Log(MoveCharName[i]);
                        order += 1;//���̃L�����̎��ɍs������L����������̎��̔z��ɑ������ׂɉ��Z���� �����L�����������ɂ��܂����Ƃ��ׂ̈ɕK�v
                        elapsedTime[i] -= ActTime[i];//elapsedTime����ActTime���}�C�i�X�@1�𒴂������͎��Ɏ����z�����ƂŐ؂�̂Ăɂ��Y�����Ȃ����B

                        Flag = true;//�ȏ�̏������s�����߂̏����t�������œ���Ă���@��ŕύX
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Return) && MoveChar[0] != null)//���̏����t���@��ŕύX
        {//�s�������L������fillAount�����Z�b�g���čs������L�����̔z�񂩂�폜�A�z��̒��g���l�߂��Ƃ��s���Ă���
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
