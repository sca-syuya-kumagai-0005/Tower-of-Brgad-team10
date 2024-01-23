using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class TeamCharacter : MonoBehaviour
{
    [SerializeField]
    private string[] charaName=new string[4];
    [SerializeField]
    private int[] charaNumbars;
    [SerializeField]
    private GameObject[] sponePos;
    [SerializeField]
    private GameObject Character;
    [SerializeField]
    private GameObject notSelectIcon;
    [SerializeField]
    private int selectCharaNumber;
    private string[] chara;
    [SerializeField]
    private GameObject characters;
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
            selectCharaNumber=selectCharaNumberCorrection+num;
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (num < 3)
                {
                    num++;
                }
            }

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
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
            }
            if(Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.DownArrow))
            {
                if(selectCharaNumberCorrection==0)
                {
                    selectCharaNumberCorrection=4;
                }
            }
           CharaInstantiate();
        }

        for(int i=0;i<notSelectIcon.transform.childCount;i++)
        {
            GameObject obj = notSelectIcon.transform.GetChild(i).gameObject;
            if (i==selectCharaNumber)
            {
                obj.SetActive(false);
            }
            else
            {
               obj.SetActive(true);
            }
        }
    }
    [SerializeField]
    private int count;
    [SerializeField]
    GameObject objects;
    private void CharaInstantiate()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            count++;
            if(count%2==0)
            {
                objects= characters.transform.GetChild(selectCharaNumber).gameObject;
                //Debug.Log(selectChara);
                for (int i=0;i<4;i++)
                {
                    if(objects.name==charaName[i])
                    {
                        charaName[i]="";
                    }
                }
                charaName[CharaSelectManager.selectSlot]=objects.name;
                //Debug.Log(selectChara);
                CharaSelectManager.charaSelectScreen = false;
                 CharaNameManager();
            }
            else
            {
                CharaSelectManager.charaSelectScreen=true;
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
            if(charaName[i]!="")
            {
                //Debug.Log("’Ê‚Á‚Ä‚¢‚é‚æ");
                GameObject obj = Resources.Load<GameObject>("PartyCharacter/" + charaName[i]);
                charaNumbars[CharaSelectManager.selectSlot] = selectCharaNumber;
                GameObject insObj = Instantiate(obj, sponePos[i].transform.position, Quaternion.identity, Character.transform);
                insObj.name = insObj.name.Replace("(Clone)", "");
            }

        }
     


    }
}
