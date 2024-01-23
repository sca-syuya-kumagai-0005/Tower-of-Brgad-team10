using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] statusObjects;
    private GameObject   status;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StatusActive();
    }

    void StatusActive()
    {
        for(int i=0;i<8;i++)
        {
            if(i==TeamCharacter.selectCharaNumber)
            {
                statusObjects[i].SetActive(true);
            }
            else
            {
                statusObjects[i].SetActive(false);
            }
        }
    }
}
