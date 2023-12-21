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
    [SerializeField]
    public  bool OkFlag;
    private int tmpi;
    public static int Count;
    private GameObject commandManager;
    [SerializeField]private GameObject mainCanvas;
    [SerializeField]Vector3 pos;
    private GameObject good;
    public float judgeDistance;
    [SerializeField]
    private GameObject judgeObject;

    // Start is called before the first frame update

    private void OnEnable()
    {
        if(GameManager.state==GameManager.BattleState.breakerCommand)
        {
            this.transform.localScale = new Vector3(90, 90, 0);
        }
        judgeObject=GameObject.Find("judge").gameObject;
        mainCanvas=GameObject.Find("MainCanvas").gameObject;
        sponePos=GameObject.Find("goodSponePos");
        pos=sponePos.transform.position;
        good=Resources.Load<GameObject>("Prefabs/Good");
       judgeFlag=false;
       OkFlag=false;
       //gameObject.tag=(NotesEditor.direction.ToString());//NotesEditor����������擾���đΉ��^�O�ɕύX
       myName=this.gameObject.name;//���g�̖��O���擾
       myName=this.gameObject.name.Replace("(Clone)","");//�������Ɏ����ŕt���iClone�j��؂���
       this.gameObject.name= this.gameObject.name.Replace("(Clone)", "");
        commandManager=this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        speed = int.Parse(this.gameObject.transform.GetChild(0).gameObject.name)*SkillStorage.addSpeed;//*SkillStorage.addSpeed;
        if (Input.GetKeyDown(KeyCode.Return))//��ŕύX�@�d�l�҂�
        {
            Debug.Log(this.gameObject.name);
        }
        switch (gameObject.tag)//�^�O�𔻒肵�đΉ���������փm�[�c�𗬂�
        {
            case "U":
                transform.Translate(new Vector3(0,speed*Time.deltaTime, 0));
                judgeDistance=Math.Abs(judgeObject.transform.position.y)-Math.Abs(this.transform.position.y);
                break;

            case "D":
                transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
                judgeDistance = Math.Abs(judgeObject.transform.position.y) - Math.Abs(this.transform.position.y);
                break;

            case "L":
                transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
                judgeDistance = Math.Abs(judgeObject.transform.position.x) - Math.Abs(this.transform.position.x);
                break;

            case "R":
                transform.Translate(new Vector3( speed * Time.deltaTime,0, 0));
                judgeDistance = Math.Abs(judgeObject.transform.position.x) - Math.Abs(this.transform.position.x);
                break;
        }
        if (Input.GetKeyDown(myName)&&judgeFlag)
        {
            NotesEditor.NotesOKCount +=1;
            BreakerEditor.NotesOKCount+=1;
            BreakerEditor.BreakerGageCount++;
            OkFlag =true;
            Debug.Log("�u���C�J�[�Q�[�W�J�E���g��"+BreakerEditor.BreakerGageCount);
            CommandKeyManager.KeyFlag[tmpi] = false;
            BreakerKeyJudge.KeyFlag[tmpi]=false;
            if (NotesEditor.commandDestroy>=Count)
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
                NotesEditor.commandDestroy+=1;
                BreakerEditor.commandDestroy+=1;
                // this.tag = "EndCommand";
                Destroy(this.gameObject);
            }
            else 
            {
                //StartCoroutine(NotesEditor.good(this.gameObject));
                OkFlag=true;
              //  this.tag = "EndCommand";
                Instantiate(good, pos, Quaternion.identity, mainCanvas.transform);
                NotesEditor.commandDestroy+=1;
                BreakerEditor.commandDestroy += 1;
                Destroy(this.gameObject);
                GameManager.moveEnd = true;
            }
        }
        //if(this.tag=="Command")
        //{ 
        //this.transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        //}
    }

    //IEnumerator NotesDestroy()
    //{
    //    if (NotesEditor.lastNotes && commandManager.transform.childCount == 1+1)
    //    {
    //        NotesEditor.commandEnd = true;
    //        Debug.Log(NotesEditor.commandEnd);
    //    }
    //    yield return new WaitForSeconds(6);
    //    GameManager.moveEnd = true;
    //    NotesEditor.commandDestroy+=1;
    //    Destroy(this.gameObject);

    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
       // this.tag="Command";
        if (other.transform.CompareTag("judge"))
        {
            for (int i = 0; i < 8; i++)
            {
                if (CommandKeyManager.AllKey[i] == this.gameObject.name)
                {
                    BreakerKeyJudge.KeyFlag[i]=true;
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
            Debug.Log("aa");
            judgeFlag =false;
            BreakerKeyJudge.KeyFlag[tmpi]=false;
            CommandKeyManager.KeyFlag[tmpi] = false;
            if (NotesEditor.lastNotes && commandManager.transform.childCount == 1+1)
            {
                NotesEditor.commandEnd = true;
                Debug.Log(NotesEditor.commandEnd);
            }
            NotesEditor.commandDestroy+=1;
            BreakerEditor.commandDestroy += 1;
            Destroy(this.gameObject);
        }
    }
}
