using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CommandController : MonoBehaviour
{
    private float speed;//ノーツのスピード
    private string myName;//生成されたプレハブの名前

    // Start is called before the first frame update

    private void OnEnable()
    {
       gameObject.tag=(NotesEditor.direction.ToString());//NotesEditorから方向を取得して対応タグに変更
       myName=this.gameObject.name;//自身の名前を取得
       myName=this.gameObject.name.Replace("(Clone)","");//生成時に自動で付く（Clone）を切り取り
       StartCoroutine(NotesDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        speed = NotesEditor.s;
        if(Input.GetKeyDown(KeyCode.Return))//後で変更　仕様待ち
        {
            Debug.Log(this.gameObject.name);
        }
        switch (gameObject.tag)//タグを判定して対応する方向へノーツを流す
        {
            case "Left":
                transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
                break;

            case "Up":
                transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
                break;

            case "Right":
                transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
                break;

            case "Down":
                transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
                break;
        }
        if (Input.GetKeyDown(myName))
        {
        }
    }

    IEnumerator NotesDestroy()
    {
        yield return new WaitForSeconds(10);
        Destroy(this.gameObject);
    }
}
