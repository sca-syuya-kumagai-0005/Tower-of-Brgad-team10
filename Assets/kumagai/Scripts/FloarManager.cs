using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloarManager : MonoBehaviour
{
    public static int nowFloar=1;
    [SerializeField]
    private Text FloorText;
    // Start is called before the first frame update
    void Start()
    {
        FloorText.text=nowFloar.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.state==GameManager.BattleState.reSult)
        {
            nowFloar++;
            FloorText.text=nowFloar.ToString();
        }
    }
}
