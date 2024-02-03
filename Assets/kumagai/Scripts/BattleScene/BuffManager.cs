using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    [SerializeField]private GameObject sponePos;
    [SerializeField]private GameObject storageObject;
    [SerializeField] List<int> storage;
    [SerializeField]GameObject[] buffIcon;
    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos=sponePos.GetComponent<RectTransform>().position;
        IconSponeManager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IconSponeManager()
    {
        for(int i=0;i<storageObject.transform.childCount;i++)
        {
            Destroy(storageObject.transform.GetChild(i).gameObject);
        }
        for(int i=0;i<storage.Count;i++)
        {
            Instantiate(buffIcon[storage[i]],pos+new Vector3 (i,0,0),Quaternion.identity,storageObject.transform);
        }
        
    }
}
