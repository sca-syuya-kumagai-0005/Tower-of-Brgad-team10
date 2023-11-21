using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject partyChara;
    public Image[] phtm=new Image[4];
    public static Image[] playerHPBer=new Image[4];
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<4;i++)
        {
            GameObject p=partyChara.transform.GetChild(i).gameObject;
            GameObject mg=p.transform.Find("HP").gameObject;
            playerHPBer[i]=mg.GetComponent<Image>();
        }
        phtm=playerHPBer;
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
