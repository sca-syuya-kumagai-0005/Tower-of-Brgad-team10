using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CommandController : MonoBehaviour
{
    [SerializeField]private GameObject sponePos;
    [SerializeField]
    private float speed;//�m�[�c�̃X�s�[�h
    private string myName;//�������ꂽ�v���n�u�̖��O
    private bool judgeFlag;
    private bool OkFlag;
    private int tmpi;
    private GameObject commandManager;
    [SerializeField]private GameObject mainCanvas;
    [SerializeField]Vector3 pos;
    private GameObject good;

    // Start is called before the first frame update

    private void OnEnable()
    {
        mainCanvas=GameObject.Find("MainCanvas").gameObject;
        sponePos=GameObject.Find("goodSponePos");
        pos=sponePos.transform.position;
        good=Resources.Load<GameObject>("Prefabs/Good");
        Debug.Log(good);
       judgeFlag=false;
       OkFlag=false;
       //gameObject.tag=(NotesEditor.direction.ToString());//NotesEditor����������擾���đΉ��^�O�ɕύX
       myName=this.gameObject.name;//���g�̖��O���擾
       myName=this.gameObject.name.Replace("(Clone)","");//�������Ɏ����ŕt���iClone�j��؂���
       this.gameObject.name= this.gameObject.name.Replace("(Clone)", "");
        commandManager=this.transform.parent.gameObject;
        StartCoroutine(NotesDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        speed = int.Parse(this.gameObject.transform.GetChild(0).gameObject.name);//*SkillStorage.addSpeed;
        if (Input.GetKeyDown(KeyCode.Return))//��ŕύX�@�d�l�҂�
        {
            Debug.Log(this.gameObject.name);
        }
        //switch (gameObject.tag)//�^�O�𔻒肵�đΉ���������փm�[�c�𗬂�
        //{
        //    case "Left":
                transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
               // break;

            //case "Up":
            //    transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
            //    break;

            //case "Right":
            //    transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            //    break;

            //case "Down":
            //    transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
            //    break;
       // }
        if (Input.GetKeyDown(myName)&&judgeFlag)
        {
            NotesEditor.NotesOKCount += 1;
            OkFlag =true;
            Debug.Log(NotesEditor.commandEnd);
            CommandKeyManager.KeyFlag[tmpi] = false;
            if (NotesEditor.lastNotes&&commandManager.transform.childCount==1+1)
            {
                NotesEditor.commandEnd=true;
                
                if (NotesEditor.commandEnd)
                { 
                    GameManager.moveEnd=true;
                    Debug.Log(NotesEditor.commandEnd);
                    Debug.Log("lastNotes��True");
                }
                //StartCoroutine(NotesEditor.good(this.gameObject));
                Instantiate(good,pos,Quaternion.identity,mainCanvas.transform);
                Destroy(this.gameObject);
            }
            else {
                //StartCoroutine(NotesEditor.good(this.gameObject));
                Instantiate(good, pos, Quaternion.identity, mainCanvas.transform);
                Destroy(this.gameObject);
                GameManager.moveEnd = true;
            }
        }
    }

    IEnumerator NotesDestroy()
    {
        if (NotesEditor.lastNotes && commandManager.transform.childCount == 1+1)
        {
            NotesEditor.commandEnd = true;
            Debug.Log(NotesEditor.commandEnd);
        }
        yield return new WaitForSeconds(6);
        GameManager.moveEnd = true;
        Destroy(this.gameObject);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.transform.CompareTag("judge"))
        {
            for (int i = 0; i < 8; i++)
            {
                if (CommandKeyManager.AllKey[i] == this.gameObject.name)
                {
                    CommandKeyManager.KeyFlag[i] = true;
                    tmpi = i;
                }
            }
            judgeFlag =true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.transform.CompareTag("judge")&&!OkFlag)
        {
            judgeFlag=false;
            CommandKeyManager.KeyFlag[tmpi] = false;
            if (NotesEditor.lastNotes && commandManager.transform.childCount == 1+1)
            {
                NotesEditor.commandEnd = true;
                Debug.Log(NotesEditor.commandEnd);
            }
            Destroy(this.gameObject);
        }
    }
}
