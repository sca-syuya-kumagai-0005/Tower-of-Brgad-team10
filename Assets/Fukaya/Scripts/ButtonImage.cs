using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; Å@// <- í«â¡

public class ButtonImage : MonoBehaviour
{

    public Sprite _on;
    public Sprite _off;
    private bool flg = true;

    public void changeImage()
    {
        var img = GetComponent<Image>();
        img.sprite = (flg) ? _on : _off;
        flg = !flg;
    }
}

