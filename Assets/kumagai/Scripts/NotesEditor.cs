using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NotesEditor : MonoBehaviour
{
    [SerializeField]
    private float speed;   //���������R�}���h�̈ړ����x
    public static float s;     //�C���X�y�N�^�[����A�^�b�`���ꂽCSV�t�@�C�����i�[���铮�I�z��//���X�g�ɕϊ��������̃f�[�^���i�[���铮�I�z��
    [SerializeField]
    private GameObject[] notes;        //�R�}���h�̉摜���i�[����z��
    public static string skillName;
    [SerializeField]
    private float minWait;
    [SerializeField]
    private float maxWait;

    public enum NotesType
    {
        w=0,
        a=1,
        s=2,
        d=3,
        L=4,
        U=5,
        R=6,
        D=7
    }

    public enum NotesDirection
    {
        Left=0,
        Up,
        Right,
        Down
    }

    public static NotesDirection direction;

    void Start()
    {
    }

    void Update()
    {
        s = speed;
        var notesDatas = Resources.Load<TextAsset>(skillName);
        if (GameManager.state==GameManager.BattleState.command&&Input.GetKeyDown(KeyCode.Return))
        { 
            StartCoroutine(NotesCreater());
        }
    }
    int maxi=5;
    public static bool lastNotes;
    public static bool commandEnd;
    IEnumerator NotesCreater() //�����ɓ��͂��ꂽ���X�g���m�[�c�Ƃ��Đ�������֐�
    {
        for(int i=0;i<maxi;i++)
        { 
            if(i==maxi-1)
            {
                lastNotes=true;
            }
        NotesType c = NotesType.w;
        int data = Random.Range(0, 8); 
            switch (data) //�z��̈�ڂɓ����Ă��镶������Đ�������R�}���h������
            {
                case 0:
                    c = NotesType.w;
                    break;

                case 1:
                    c = NotesType.a;
                    break;

                case 2:
                    c = NotesType.s;
                    break;

                case 3:
                    c = NotesType.d;
                    break;

                case 4:
                    c = NotesType.L;
                    break;

                case 5:
                    c = NotesType.U;
                    break;

                case 6:
                    c = NotesType.R;
                    break;

                case 7:
                    c = NotesType.D;
                    break;
            }�@�@�@�@//���ڂ̒l�ɂ���ăm�[�c�̎�ނ�����
        
            float t = Random.Range(minWait,maxWait);�@//���ڂ̒l�ɂ���ăm�[�c������ė���܂ł̎��Ԃ�����
            int dir=Random.Range(0,4);
            Vector3 pos=new Vector3(10,-4,0);
            switch (dir) 
            {
                case 0:
                    {
                       
                        direction = NotesDirection.Left;
                    }
                    break;

                case 1:
                    {
                      
                        direction = NotesDirection.Up;
                    }
                    break;

                case 2:
                    {
                      
                        direction = NotesDirection.Right;
                    }
                    break;

                case 3:
                    {
                      
                        direction = NotesDirection.Down;
                    }
                    break;
            }�@     //�O��ڂ̒l�ɂ���ăm�[�c�̌���������
            yield return new WaitForSeconds(t); //���ڂ̒l�������ҋ@
            Instantiate(notes[(int)c], pos, Quaternion.identity, transform); //����
        }
    }
}


