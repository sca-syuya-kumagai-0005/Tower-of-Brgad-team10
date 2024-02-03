using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Twinkle : MonoBehaviour
{
    private float alpha;
    private const float addSize=0.03f;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Effect());
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.BattleState.command!=GameManager.state)
        {
            Destroy(this.gameObject);
        }
    }

    
    private IEnumerator Effect()
    {
        alpha=1;
        while(timer<0.2f)
        {
            Vector3 size=this.gameObject.GetComponent<RectTransform>().localScale;
            size+=new Vector3(addSize,addSize,0);
            Debug.Log(size);
            alpha-=Time.deltaTime;
            timer+=Time.deltaTime;
            this.gameObject.GetComponent<RectTransform>().localScale=size;
            this.gameObject.GetComponent<Image>().color=new Color(1,1,1,alpha);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Destroy(this.gameObject);
    }
}
