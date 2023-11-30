using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goodManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ThisDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator ThisDestroy()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }
}
