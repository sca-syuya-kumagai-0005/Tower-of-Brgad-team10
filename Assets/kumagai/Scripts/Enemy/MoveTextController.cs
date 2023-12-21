using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveTextController : MonoBehaviour
{
    [SerializeField]private Text text;
    public static Text moveText;
    public static bool moveTextFlag;
    // Start is called before the first frame update
    void Start()
    {
        moveText=text;
        moveTextFlag=false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static IEnumerator moveTextCoroutine(string str)
    {
            moveText.text="";
            for(int i=0;i<str.Length;i++)
            {
                moveText.text=str.Substring(0,i+1);
                yield return new WaitForSeconds(0.01f);
            }
            moveTextFlag = true;
        
      
    }
}
