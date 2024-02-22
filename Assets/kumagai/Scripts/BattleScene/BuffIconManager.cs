using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffIconManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.rotation = BuffManager.storageRot();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation=BuffManager.storageRot();
    }
}
