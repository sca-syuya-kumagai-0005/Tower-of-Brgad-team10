using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PleaseEnter : MonoBehaviour
{
    [SerializeField]
    Text text;
    private string pleaseEnter="Please Enter Key";
    private float alpha;
    private bool Plus;
    private bool FeedFlag;
    // Start is called before the first frame update
    void Start()
    {
        alpha=1;
        FeedFlag=false;
        StartCoroutine(TextCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if(FeedFlag)
        {
            Feed();
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("BattleScene");
        }
    }

    private void Feed()
    {
        if (alpha >= 1)
        {
            Plus = false;
        }
        if (alpha <= 0)
        {
            Plus = true;
        }
        if (Plus)
        {
            alpha += Time.deltaTime;
        }
        else
        {
            alpha -= Time.deltaTime;
        }
        text.color=new Color(1,1,1,alpha);
    }

    private IEnumerator TextCoroutine()
    {
        for(int i=0;i<pleaseEnter.Length;i++)
        {
            text.text+=pleaseEnter[i];
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.2f);
        FeedFlag=true;
    }
}
