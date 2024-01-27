using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaMoveGage : MonoBehaviour
{

    public static GameObject Char;//�z��ւ̑���Ɏg�p�@���܂�C�ɂ��Ȃ��Ă悵
    public static GameObject[] Char_MoveGage;//�L�����N�^�[�ɂ��Ă��郀�[�u�Q�[�W���擾����̂Ɏg�p�@�C���[�W����邽�߂Ɉ�x�Q�[���I�u�W�F�N�g���o�R
    [SerializeField]
    private Image[] Player_MoveGageImage;//���[�u�Q�[�W��fillAmount��ύX����C���[�W
    public static int order = 0;//fillAmount���P�ɂȂ����Ƃ����ԖڂɊi�[���邩������
    public static List<GameObject> MoveChar = new List<GameObject>();//�s�����邽�߂̃Q�[�W�����܂��Ă���L�������i�[ ����4�����Ă��邪�A�p�[�e�B�̃L�������{�G�l�~�[�����K�v
    public static string[] MoveCharName;
    public List<GameObject> tes;
    public static float[] ActTime=new float[5];//�L�����N�^�[�̍s�����x�@�ꎞ�I�ɃC���X�y�N�^�[���猈�肵�Ă��邪�A�{����CSV�t�@�C������Ƃ��Ă���
    public static bool characterAct;
    public static string enemyName;
    [SerializeField]
    private GameObject needle;
    float[] elapsedTime=new float[5];//Time.deltaTime�����Z�����Ƃ���1�𒴉߂����ꍇ�AfillAmount�ł͐؂�̂Ă��Ă��܂��A���̃L�����Ƃ̊Ԃɂ��ꂪ�����Ă��܂��̂ŁA������������邽�߂̕ϐ�
    // Start is called before the first frame update

    private void OnEnable()
    {
        MoveChar = new List<GameObject>();
        enemyName = EnemySponer.sponeEnemy[0].name;
        alpha=0;
        Debug.Log(enemyName);
        needle.transform.rotation=new Quaternion(0,0,0,1);
    }
    void Start()
    {
        order=0;//������
        MoveCharName=new string[PlayerEditor.partyTheNumberOf];
        for(int i=0;i<PlayerEditor.partyTheNumberOf;i++)
        { 
             MoveCharName[i]="";
        }
        Char_MoveGage=new GameObject[this.transform.childCount+1];//�L�����N�^�[�̐������Q�[���I�u�W�F�N�g�z����`+1��enemy
        Player_MoveGageImage=new Image[4+1];//���l�ɃC���[�W���`
        
        Char_MoveGage[0]=GameObject.Find("Enemy").transform.GetChild(0).gameObject;//�G�l�~�[�ɂ��Ă���s���Q�[�W���擾
        Player_MoveGageImage[0]=Char_MoveGage[0].transform.Find("MoveGageBackGround").GetComponent<Image>();
        int count=1;
        for(int i=1;i<4+1;i++)//�L�����N�^�[�̐������񂵂āA�L�����N�^�[�̍čs���܂ł̃Q�[�W�iImage�j���擾
        {
            
            Char=this.transform.GetChild(i-1).gameObject;
            if(Char.name!="NullPrefab(Clone)")
            {
                Char_MoveGage[count] = Char.transform.Find("MoveGage").gameObject.transform.Find("MoveGage").gameObject;
                Player_MoveGageImage[count] = Char_MoveGage[i].GetComponent<Image>();
                count++;
            }
            else { 
                }
        }
        
    }
    bool Flag;//����m�F�p
    public static bool SetFlag=false;//if(SkillSelection.skillSelect)�ɂĂȂ��������s����邽�߂�����������邽�߂̃t���O
    // Update is called once per frame
    void Update()
    {
        tes=MoveChar;
        //if(MoveChar[0]==null&&MoveChar[1]!=null)
        //{
        //    MoveChar[0]=MoveChar[1];
        //    MoveChar[1]=null;
        //}
        if (MoveChar.Count != 0)
        {
            if (GameManager.state == GameManager.BattleState.moveWait && !MoveChar[0].CompareTag("Enemy"))
            {
                characterAct = true;
            }
        }
        
        Player_MoveGageImage[0].color = new Color(0 + alpha, 1 - alpha, 1 - alpha, 1);
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
                GameObject MG;
                if(MoveChar[0].name!="EnemyMoveGage")
                {
                MG = MoveChar[0].transform.Find("MoveGage").gameObject.transform.Find("MoveGage").gameObject;
                }
                else
                {
                  MG = MoveChar[0].transform.Find("MoveGageBackGround").gameObject;
                }
                Image IM = MG.GetComponent<Image>();
                IM.fillAmount = 0;
                MoveChar.RemoveAt(0);
                MoveCharName[0] = "";
                SetFlag = true;
            }
        }
    }
    public static bool orderFlag;
    public static float alpha;
    void AddGage()
    {
        if (GameManager.state == GameManager.BattleState.moveWait||GameManager.state==GameManager.BattleState.effect)
        {
            if (MoveChar.Count==0)//�s�����Ă���L���������Ȃ����
            {
                for (int i = 0; i < 5; i++) //
                {
                    if(Player_MoveGageImage[i].transform.parent.CompareTag("Enemy"))
                    {
                        elapsedTime[i]+=Time.deltaTime * SkillStorage.DeBuffSpeed;
                        needle.transform.Rotate(0,0,-360*Time.deltaTime*SkillStorage.DeBuffSpeed/ActTime[0]);
                        Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                        alpha +=Time.deltaTime/ActTime[0]*SkillStorage.DeBuffSpeed;
                    }
                    else { 
                    elapsedTime[i] += Time.deltaTime*SkillStorage.gabBuff*EnemyMove.stoneSpeedDebuff;
                    }
                    if(Player_MoveGageImage[i].transform.parent.CompareTag("Enemy"))
                    {
                        Player_MoveGageImage[i].color = new Color(0 + alpha, 1 - alpha, 1 - alpha, 1);
                    }
                    else if(GameManager.aliveFlag[i])
                    {
                        Player_MoveGageImage[i].fillAmount = elapsedTime[i] / ActTime[i];
                    }
                    
                   //fillAmount�����Z�@ActTime�Ŋ��邱�Ƃ�ActTime�b��fillAmount��1�ɂȂ�
                    
                   
                    if(alpha>=1 && Player_MoveGageImage[i].name == "MoveGageBackGround"&&!GameManager.moveEnd)
                    {
                        
                        MoveChar.Add(Player_MoveGageImage[i].transform.parent.gameObject);//fillAmout��1�ɂȂ����L�������s������L�����ɑ��
                        //MoveCharName[i] = MoveChar[order].name;
                        Debug.Log(Player_MoveGageImage[i].transform.parent.gameObject);
                        //Debug.Log(MoveCharName[i]);
                        order += 1;//���̃L�����̎��ɍs������L����������̎��̔z��ɑ������ׂɉ��Z���� �����L�����������ɂ��܂����Ƃ��ׂ̈ɕK�v
                        elapsedTime[i] -= ActTime[i];
                        alpha-=1;
                    }
                    if (Player_MoveGageImage[i].fillAmount >= 1 && Player_MoveGageImage[i].name == "MoveGage")//fillAmount���P�ɂȂ����L�������s������L�����̔z��Ɋi�[
                    {
                        GameObject obj=Player_MoveGageImage[i].transform.parent.gameObject.transform.parent.gameObject;
                        Debug.Log(obj.name);//�����܂ł͂���
                        MoveChar.Add(obj);//fillAmout��1�ɂȂ����L�������s������L�����ɑ��
                        //MoveCharName[i] = MoveChar[order].name;
                        //Debug.Log(MoveCharName[i]);
                       //���̃L�����̎��ɍs������L����������̎��̔z��ɑ������ׂɉ��Z���� �����L�����������ɂ��܂����Ƃ��ׂ̈ɕK�v
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
