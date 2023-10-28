using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelection : MonoBehaviour
{
    private GameObject nowChar;
    private string[] skillName;
    public bool skillSelect;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerMoveGage.MoveCharName[0]!=null)
        {
            skillSelect=true;

        }
    }
}
