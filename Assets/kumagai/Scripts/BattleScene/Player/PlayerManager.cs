using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject partyChara;
    public Image[] phtm=new Image[4];
    public static Image[] playerHPBer=new Image[4];
    public static GameObject[] playerDeadBackGround=new GameObject[4];
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<PlayerEditor.PlayerName.Length;i++)
        {
            if(PlayerEditor.PlayerName[i]!=""&&PlayerEditor.PlayerName[i]!=null) { 
            GameObject p=partyChara.transform.GetChild(i).gameObject;
            GameObject mg=p.transform.Find("HP").gameObject;
            Debug.Log(mg.name);
            GameObject imobj=mg.transform.Find("HPGreen").gameObject;
            Debug.Log(imobj.name);
            Image im=imobj.GetComponent<Image>();
            playerHPBer[i]=im;
            GameObject obj=p.transform.Find("BackGround").gameObject;
            playerDeadBackGround[i]=obj.transform.Find("DeadBackGround").gameObject;
            playerDeadBackGround[i].SetActive(false);
            }
        }
        phtm=playerHPBer;
    }

    // Update is called once per frame
    void Update()
    {
      for(int i=0;i<PlayerEditor.PlayerName.Length;i++)
        {
            if(PlayerEditor.PlayerName[i] != "" && PlayerEditor.PlayerName[i] != null) {
                playerDeadBackGround[i].SetActive(!GameManager.aliveFlag[i+1]);
            }
        }
    }
}
