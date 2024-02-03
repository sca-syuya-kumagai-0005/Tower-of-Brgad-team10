using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Twinkle : MonoBehaviour
{
    private const float addSize=30;
    private float alpha;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Effect());
    }

    // Update is called once per frame
    void Update()
    {

        if(rand==0)
        {
            f += 10;
        }
        else
        {
            f-=10;
        }
        this.transform.Rotate(0, 0, f);
    }

    int f;
    int rand;
    private IEnumerator Effect()
    {
        alpha=0;
        bool alphaFlag=false;
       rand= Random.Range(0,2);
        Vector3 size = this.gameObject.GetComponent<RectTransform>().localScale;
        while (alpha<1&&!alphaFlag)
        {
            
            
            Debug.Log(size);
            alpha+=Time.deltaTime*10;
            this.gameObject.GetComponent<RectTransform>().localScale+=new Vector3(Time.deltaTime,Time.deltaTime,0)*addSize;
            this.gameObject.GetComponent<Image>().color=new Color(1,1,1,alpha);
            yield return null;
        }
       alphaFlag=true;
        while(alphaFlag&&alpha>0)
        {
            Debug.Log(size);
            alpha -= Time.deltaTime*10;
            this.gameObject.GetComponent<RectTransform>().localScale += new Vector3(Time.deltaTime, Time.deltaTime, 0)*addSize;
            this.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, alpha);
            yield return null;
        }
        //  this.gameObject.GetComponent<RectTransform>().localScale=size;
        //Destroy(this.gameObject);
    }
}
