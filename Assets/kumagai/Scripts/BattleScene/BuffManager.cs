using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    [SerializeField]private GameObject sponePos;
    [SerializeField]private GameObject storageObject;
    [SerializeField] List<int> pBuffStorage;
    [SerializeField] List<int> pDeBuffStorage;
    [SerializeField] List<int> eBuffStorage;
    [SerializeField] List<int> eDeBuffStorage;
    [SerializeField]GameObject[] buffIcon;
    [SerializeField]GameObject[] deBuffIcon;
    public static List<int> publicPBuffStorage;
    public static List<int> publicPDeBuffStorage;
    public static List<int> publicEBuffStorage;
    public static List<int> publicEDeBuffStorage;
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
        pBuffStorage=publicPBuffStorage;
        pDeBuffStorage=publicPDeBuffStorage;
    }

    public void IconSponeManager()
    {
        for(int i=0;i<storageObject.transform.childCount;i++)
        {
            Destroy(storageObject.transform.GetChild(i).gameObject);
        }
        for(int i=0;i<pBuffStorage.Count;i++)
        {
            Instantiate(buffIcon[pBuffStorage[i]],pos+new Vector3 (i,0,0),Quaternion.identity,storageObject.transform);
        }
        for(int i = 0;i<pDeBuffStorage.Count;i++)
        {
            Instantiate(deBuffIcon[pDeBuffStorage[i]], pos + new Vector3(i+pBuffStorage.Count, 0, 0), Quaternion.identity, storageObject.transform);
        }
        
    }
}
