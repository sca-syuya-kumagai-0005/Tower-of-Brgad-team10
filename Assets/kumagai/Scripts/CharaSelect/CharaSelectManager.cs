using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class CharaSelectManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] slotSelect;
    public int selectSlot;//デバッグ用にパブリックにしている
    [SerializeField]
    private GameObject[] sceneButtonSelect;
    private bool slot;
    private int sceneSelect;
    private bool charaSelectScreen;
    [SerializeField]
    private GameObject charaSelectBackGround;

    // Start is called before the first frame update
    void Start()
    {
        slot=true;
        sceneSelect=1;
    }

    // Update is called once per frame
    void Update()
    {
        CorsolManager();
        charaSelectBackGround.SetActive(charaSelectScreen);
        
    }

    void CorsolManager()
    {
        if(slot)
        { 
            sceneSelect=1;//キャラ編成が終わった後に下もしくは上を押すとすぐに次へボタンにカーソルが移動するようにするための処理
            if(Input.GetKeyDown(KeyCode.D)||Input.GetKeyDown(KeyCode.RightArrow))
            {
                if(selectSlot<3) 
                {
                    selectSlot++;
                }

                else if(selectSlot==3)
                {
                    selectSlot=0;
                }
            }

            if(Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.LeftArrow)) 
            {
                if(selectSlot>0) 
                {
                    selectSlot--;
                }

                else if(selectSlot==0) 
                {
                    selectSlot=3;
                }
            }
            for(int i = 0; i < 4; i++) {
                if(i == selectSlot) {
                    slotSelect[i].SetActive(true);
                } else {
                    slotSelect[i].SetActive(false);
                }
            }
            for(int i=0;i<2;i++) {
                sceneButtonSelect[i].SetActive(false);
            }
        } 
        else 
        {
            selectSlot=0;
            if(Input.GetKeyDown(KeyCode.D)||Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) 
            {
                if(sceneSelect==0) 
                {
                    sceneSelect++;
                }
                else 
                {
                    sceneSelect--;
                }
                
            }
            for(int i=0;i<4;i++) 
            {
                slotSelect[i].SetActive(false);
            }
            for(int i=0;i<2;i++)
            {
                if(i == sceneSelect) 
                { 
                    sceneButtonSelect[i].SetActive(true);
                }
                else 
                {
                    sceneButtonSelect[i].SetActive(false);
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.DownArrow)||Input.GetKeyDown(KeyCode.UpArrow))
        {
            slot=!slot;
        }

        if(Input.GetKeyDown(KeyCode.Return) && !charaSelectScreen&&slot) 
        {
            charaSelectScreen = true; 
        }
    }

    public void Slot1ButtonSystem() {
        slot=true;
        selectSlot=0;
        charaSelectScreen = true;
        Debug.Log("呼ばれてるよ");
    }

    public void Slot2ButtonSystem() {
        slot = true;
        selectSlot = 1;
        charaSelectScreen = true;
        Debug.Log("呼ばれてるよ");
    }

    public void Slot3ButtonSystem() {
        slot = true;
        selectSlot = 2;
        charaSelectScreen = true;
        Debug.Log("呼ばれてるよ");
    }

    public void Slot4ButtonSystem() {
        slot = true;
        selectSlot = 3;
        charaSelectScreen = true;
        Debug.Log("呼ばれてるよ");
    }

    public void Slot1ButtonEnter()
    {
        slot = true;
        selectSlot =0;
    }

    public void Slot2ButtonEnter()
    {
        slot = true;
        selectSlot =1;
    }

    public void Slot3ButtonEnter()
    {
        slot = true;
        selectSlot =2;
    }

    public void Slot4ButtonEnter()
    {
        slot = true;
        selectSlot =3;
    }
    public void NextButtonSystem() 
    {
        slot=false;
       // SetCursorPos(400, 400);
    }

    public void NextButtonEnter()
    {
        slot=false;
        sceneSelect=1;
    }

    public void BackButoonSystem()
    { 
        slot=false;
    }

    public void BackButtonEnter()
    {
        slot=false;
        sceneSelect=0;
    }
}
