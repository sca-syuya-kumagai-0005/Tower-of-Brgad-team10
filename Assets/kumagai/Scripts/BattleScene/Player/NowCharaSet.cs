using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NowCharaSet : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> nowCharacter;
    [SerializeField]
    private List<GameObject> nextCharacter;
    [SerializeField]
    GameObject nowCharaSponePos;
    [SerializeField]
    private GameObject MoveChara;
    [SerializeField]
    private GameObject nextCharaSponePos;
    public static GameObject nextChara;
    public static GameObject nowChara;
    [SerializeField]
    private GameObject NullPrefab;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<PlayerEditor.PlayerName.Length;i++) {
            if(PlayerEditor.PlayerName[i]!=null&&PlayerEditor.PlayerName[i]!="") { 
            GameObject now=Resources.Load<GameObject>("NowChara/"+PlayerEditor.PlayerName[i]);
            GameObject next=Resources.Load<GameObject>("NextChara/"+PlayerEditor.PlayerName[i]);
            nowCharacter.Add(Instantiate(now,nowCharaSponePos.transform.position,Quaternion.identity,MoveChara.transform));
            nextCharacter.Add(Instantiate(next,nextCharaSponePos.transform.position,Quaternion.identity,MoveChara.transform));
            }
            else {
                nowCharacter.Add(Instantiate(NullPrefab, nowCharaSponePos.transform.position, Quaternion.identity, MoveChara.transform));
                nextCharacter.Add(Instantiate(NullPrefab, nextCharaSponePos.transform.position, Quaternion.identity, MoveChara.transform));
            }
        }
        for(int i=0;i<PlayerEditor.PlayerName.Length;i++)
        {
            if(PlayerEditor.PlayerName[i] != "" && PlayerEditor.PlayerName[i] != null) {
                nowCharacter[i].SetActive(false);
                nextCharacter[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.BattleState.start!=GameManager.BattleState.flagReSet)
        { 
            for(int i=0;i<PlayerEditor.PlayerName.Length;i++) 
            {
                if(PlayerEditor.PlayerName[i] != "" && PlayerEditor.PlayerName[i] != null) {
                    if(SkillStorage.charaNumber==i) 
                    {
                        nowCharacter[i].SetActive(true);
                        nowChara =nowCharacter[i];
                    } 
                    if(CharaMoveGage.MoveChar[0]==null)
                    {
                        nowCharacter[i].SetActive(false);
                    }

                    if(SkillStorage.nextCharaNumber==i)
                    {
                        nextCharacter[i].SetActive(true);
                        nextChara=nextCharacter[i];
                        Debug.Log("’Ê‰ß‚µ‚Ä‚¢‚é‚æ");
                    }
                }
            }
        }
      
    }
}
