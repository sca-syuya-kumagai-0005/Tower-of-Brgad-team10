using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class TeamCharacter : MonoBehaviour
{
    public static string[] charaName=new string[4];
    [SerializeField]
    private int[] charaNumbars;
    [SerializeField]
    private GameObject[] sponePos;
    [SerializeField]
    private GameObject Character;
    [SerializeField]
    private GameObject notSelectIcon;
    public static int selectCharaNumber;
    private string[] chara;
    [SerializeField]
    private GameObject characters;
    [SerializeField] private GameObject[] changeChara;
    [SerializeField] private GameObject[] oldChara;
    private GameObject oldCharaParent;
    private GameObject changeCharaParent;
    [SerializeField]
    private GameObject nowSlot;
    [SerializeField]
    private GameObject Out;
    [SerializeField]
    private GameObject Back;
    [SerializeField]
    private bool OutBack;
    [SerializeField]
    private bool OutorBack;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
            
        SelectCharaJudge();

    }
    [SerializeField]
    private int selectCharaNumberCorrection;
    [SerializeField]
    private int num;

    private void SelectCharaJudge()
    {
        if(CharaSelectManager.charaSelectScreen)
        {
            if(!OutBack) {
                Out.SetActive(false);
                Back.SetActive(false);
            }
            if(Input.GetKeyDown(KeyCode.Return)) {
                if(OutorBack && OutBack) {
                    for(int i = 0; i < Character.transform.childCount; i++) {
                        if(charaName[CharaSelectManager.selectSlot] == Character.transform.GetChild(i).gameObject.name) {
                            Debug.Log(Character.transform.GetChild(i).gameObject.name);
                            Destroy(Character.transform.GetChild(i).gameObject);
                        }
                    }
                }
            }
            
            selectCharaNumber =selectCharaNumberCorrection+num;
            ChangeCharaSpone();
            if (Input.GetKeyDown(KeyCode.D) && !OutBack || Input.GetKeyDown(KeyCode.RightArrow)&&!OutBack)
            {
                if (num < 3)
                {
                    num++;
                }
            }

            if (Input.GetKeyDown(KeyCode.A)&&!OutBack || Input.GetKeyDown(KeyCode.LeftArrow)&&!OutBack)
            {
                if (num > 0)
                {
                    num--;
                }
            }

            if(Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.UpArrow))
            {
                if(selectCharaNumberCorrection==4)
                {
                    selectCharaNumberCorrection=0;
                }
                else if(selectCharaNumberCorrection==-5) {
                    OutBack = false;
                    changeChara[8].SetActive(false);
                    selectCharaNumberCorrection =4;
                    
                }
            }
            if(Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.DownArrow))
            {
                if(selectCharaNumberCorrection==0)
                {
                    selectCharaNumberCorrection=4;
                }
                else if(selectCharaNumberCorrection==4) 
                {
                    OutBack=true;
                    OutorBack=false;
                    selectCharaNumberCorrection=-5;
                }
            }
            if(Input.GetKeyDown(KeyCode.D)&&OutBack||Input.GetKeyDown(KeyCode.RightArrow)&&OutBack) {
                OutorBack=true;
            }
            if(Input.GetKeyDown(KeyCode.A)&&OutBack||Input.GetKeyDown(KeyCode.LeftArrow)&&OutBack) {
                OutorBack=false;
            }
            if(OutBack) {
                for(int i = 0; i < 8; i++) {
                    changeChara[i].SetActive(false);
                }
                changeChara[8].SetActive(true);
                if(OutorBack) {
                    Out.SetActive(true);
                    Back.SetActive(false);
                }
                else {
                    for(int i=0;i<8;i++) {
                        if(changeChara[i].name==charaName[CharaSelectManager.selectSlot]) {
                            changeChara[i].SetActive(true);
                            changeChara[8].SetActive(false);
                        }
                        else {
                            changeChara[i].SetActive(false);
                        }
                    }
                    if(charaName[CharaSelectManager.selectSlot]=="") {
                        oldChara[8].SetActive(true);
                    }
                    Back.SetActive(true);
                    Out.SetActive(false);
                }
                for(int i=0;i<8;i++) {
                    notSelectIcon.transform.GetChild(i).gameObject.SetActive(true);
                }

            }
            else {
                for(int i = 0; i < notSelectIcon.transform.childCount; i++) {
                    GameObject obj = notSelectIcon.transform.GetChild(i).gameObject;
                    if(i == selectCharaNumber) {
                        obj.SetActive(false);
                    } else {
                        obj.SetActive(true);
                    }
                    
                }
            }
           CharaInstantiate();
        }

        
    }
    [SerializeField]
    private int count;
    GameObject objects;
    private void CharaInstantiate()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            count++;
            
            if(count%2==0)
            {
                if(selectCharaNumber>-1) {
                    objects = characters.transform.GetChild(selectCharaNumber).gameObject;
                    //Debug.Log(selectChara);
                    for(int i = 0; i < 4; i++) {
                        if(objects.name == charaName[i]) {
                            charaName[i] = "";
                        }
                    }
                    charaName[CharaSelectManager.selectSlot] = objects.name;
                    //Debug.Log(selectChara);
                    CharaSelectManager.charaSelectScreen = false;
                    CharaNameManager();
                }
                else {
                    if(OutorBack) {
                       if(Input.GetKeyDown(KeyCode.Return)) {
                            OutBack = false;
                            CharaSelectManager.charaSelectScreen=false;
                        }
                    }
                    else {
                        for(int i=0;i<8;i++) {
                            if(changeChara[i].name==charaName[CharaSelectManager.selectSlot]) {
                                changeChara[i].SetActive(true);
                            }
                            else {
                                changeChara[i].SetActive(false);
                            }
                        }
                        CharaSelectManager.charaSelectScreen = false;
                    }
                }
                
            }
            else
            {
                OutBack=false;
                changeChara[8].SetActive(false);
                selectCharaNumber =0;
                selectCharaNumberCorrection=0;
                num=0;
                for(int i=0;i<8;i++) {
                    oldChara[i].SetActive(false);
                }
                if(charaName[CharaSelectManager.selectSlot]!=null&&charaName[CharaSelectManager.selectSlot]!="") {
                    oldChara[8].SetActive(false);
                }
                else if(charaName[CharaSelectManager.selectSlot] == ""||charaName[CharaSelectManager.selectSlot]==null) {
                   oldChara[8].SetActive(true);
                }

                CharaSelectManager.charaSelectScreen=true;
                for(int i=0;i<4;i++) {
                    if(i == CharaSelectManager.selectSlot) {
                        nowSlot.transform.GetChild(i).gameObject.SetActive(true);
                    } else {
                        nowSlot.transform.GetChild(i).gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    private void CharaNameManager()
    {
        for(int i=0;i<Character.transform.childCount;i++)
        {
            Destroy(Character.transform.GetChild(i).gameObject);
        }
        for(int i=0;i<4;i++)
        {
            if(charaName[i]!=""&&charaName[i]!=null)
            {
                //Debug.Log("’Ê‚Á‚Ä‚¢‚é‚æ");
                
                charaNumbars[CharaSelectManager.selectSlot] = selectCharaNumber;
                GameObject obj=Instantiate(Resources.Load<GameObject>("PartyCharacter/" + charaName[i]), sponePos[i].transform.position, Quaternion.identity, Character.transform);
                obj.name=obj.name.Replace("(Clone)","");
            }

        }
    }

    void ChangeCharaSpone()
    {
            if (charaName[CharaSelectManager.selectSlot] != "")
            {
                for (int i = 0; i < 8; i++)
                {
                    if (charaName[CharaSelectManager.selectSlot] == oldChara[i].name)
                    {
                       oldChara[i].SetActive(true);
                    }
                    else
                    {
                       oldChara[i].SetActive(false);
                    }
                }
            }
            else
            {
                oldChara[8].SetActive(true);
            Debug.Log("  ");
            }
            for(int i=0;i<8;i++)
            {
                if(i==selectCharaNumber)
                {
                   changeChara[i].SetActive(true);
                }
                else
                {
                    changeChara[i].SetActive(false);
                }
            }
    }
}
