using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CommandController : MonoBehaviour
{
    [SerializeField]private GameObject sponePos;
    [SerializeField]
    private float speed;//ノーツのスピード
    private string myName;//生成されたプレハブの名前
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
       //gameObject.tag=(NotesEditor.direction.ToString());//NotesEditorから方向を取得して対応タグに変更
       myName=this.gameObject.name;//自身の名前を取得
       myName=this.gameObject.name.Replace("(Clone)","");//生成時に自動で付く（Clone）を切り取り
       this.gameObject.name= this.gameObject.name.Replace("(Clone)", "");
        commandManager=this.transform.parent.gameObject;
        StartCoroutine(NotesDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        speed = int.Parse(this.gameObject.transform.GetChild(0).gameObject.name);//*SkillStorage.addSpeed;
        if (Input.GetKeyDown(KeyCode.Return))//後で変更　仕様待ち
        {
            Debug.Log(this.gameObject.name);
        }
        //switch (gameObject.tag)//タグを判定して対応する方向へノーツを流す
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
                    Debug.Log("lastNotesはTrue");
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
