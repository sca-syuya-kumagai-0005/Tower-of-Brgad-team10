using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goodManager : MonoBehaviour
{
    Vector2 targetSize;
    // Start is called before the first frame update
    void Start()
    {
        flag=false;
        a=1;
        sizeX=1;
        sizeY=1;
        targetSize = new Vector2(sizeX, sizeY);
        StartCoroutine(ThisDestroy());
    }

    // Update is called once per frame
    float a;
    bool flag;
    float sizeX;
    float sizeY;
    void Update()
    {
        if(flag)
        {
            a+=0.8f;
            if(sizeX-0.03f>=0)
            { 
                sizeX-=0.01f;
                sizeY-=0.01f;
            }
            else
            {
                sizeX=0;
                sizeY=0;
            }
            targetSize=new Vector2(sizeX,sizeY);
            this.gameObject.transform.localScale = targetSize;
            this.transform.position = new Vector3(this.transform.position.x - 2 * (Mathf.Cos(a / 100)) / 100, this.transform.position.y + (Mathf.Sin(a / 10)) / 100, this.transform.position.z);
        }
    }
    IEnumerator ThisDestroy()
    {
        flag=true;
        yield return new WaitForSeconds(0.3f);
        flag=false;
        Destroy(this.gameObject);
    }
}
