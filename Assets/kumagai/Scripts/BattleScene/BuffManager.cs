using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuffManager : MonoBehaviour
{
    [SerializeField]private GameObject sponePos;
    [SerializeField]private GameObject storageObject;
    public static GameObject DerivaryRot;
    [SerializeField]private GameObject backGround;
    [SerializeField] GameObject[] buffIcon;
    [SerializeField] GameObject[] deBuffIcon;
    [SerializeField] List<int> pBuffStorage;
    [SerializeField] List<int> pDeBuffStorage;
    [SerializeField] List<int> eBuffStorage;
    [SerializeField] List<int> eDeBuffStorage;
    [SerializeField] private GameObject playerOrEnemy;
    public static List<int> publicPBuffStorage;
    public static List<int> publicPDeBuffStorage;
    public static List<int> publicEBuffStorage;
    public static List<int> publicEDeBuffStorage;
    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        publicPBuffStorage=new List<int>();
        publicEDeBuffStorage =new List<int>();
        publicPDeBuffStorage=new List<int>();
        publicEBuffStorage=new List<int>();
        pos=sponePos.GetComponent<RectTransform>().position;
        DerivaryRot=storageObject;
        StartCoroutine(ChangePlayerEnemyIcon());
        playerOrEnemy.GetComponent<Image>().sprite = Resources.Load<Sprite>("�A�C�R��/Player");
    }

    // Update is called once per frame
    void Update()
    {
        publicPBuffStorage=IconDestory(publicPBuffStorage);
        pBuffStorage=publicPBuffStorage;
        publicEDeBuffStorage=IconDestory(publicEDeBuffStorage);
        eDeBuffStorage=publicEDeBuffStorage;
        publicPDeBuffStorage=IconDestory(publicPDeBuffStorage);
        pDeBuffStorage=publicPDeBuffStorage;
        publicEBuffStorage=IconDestory(publicEBuffStorage);
        eBuffStorage=publicEBuffStorage;
        //pDeBuffStorage=publicPDeBuffStorage;
    }
    //�o�t�A�������̓f�o�t���|����ꂽ�Ƃ��ɍs������
    public void IconSponeManager(List<int> BuffStorage,List<int>DeBuffStorage)
    {
        //������o�t�����ׂă��Z�b�g
        for(int i=0;i<storageObject.transform.childCount;i++)
        {
            Destroy(storageObject.transform.GetChild(i).gameObject);
        }
        //���݂������Ă���o�t�ɉ����ăA�C�R���𐶐�����
        for(int i=0;i<BuffStorage.Count;i++)
        {
            Debug.Log(BuffStorage.Count);
            //�o�t�f�o�t�̃A�C�R���͍ő�12�܂ł����\�����Ȃ�����12�����������_�ŏ������~����
            if (i>=12)
            {
                break;
            }
            bool flag=false;
            if(!flag)
            {
                Instantiate(buffIcon[BuffStorage[i]], pos + new Vector3(i, 0, 0), Quaternion.identity, storageObject.transform);
                flag=true;
            }
        }
        //�o�t�𐶐����I������f�o�t�A�C�R���𐶐�(�o�t�̌�Ƀf�o�t��\�����邽��)
        for(int i = BuffStorage.Count;i<DeBuffStorage.Count+BuffStorage.Count;i++)
        {
           
            //���������L�Ɠ��l�ɒ�~�̏������s��
            if (i >= 12)
            {
                break;
            }
                GameObject obj=Instantiate(deBuffIcon[DeBuffStorage[i-BuffStorage.Count]], pos + new Vector3(i, 0, 0), Quaternion.Euler(backGround.transform.rotation.x,0,0), storageObject.transform);
                //GameObject obj=Instantiate(deBuffIcon[DeBuffStorage[i-BuffStorage.Count]], pos + new Vector3(i, 0, 0), Quaternion.identity, storageObject.transform);    
            }
        
    }

    private IEnumerator ChangePlayerEnemyIcon()
    {
        Coroutine coroutine;
        yield return new WaitForSeconds(5f);
        while (!GameManager.GameClear)
        {
            coroutine=StartCoroutine(BackGroundRotate1());
            yield return new WaitForSeconds(0.5f);
            StopCoroutine(coroutine);
            IconSponeManager(eBuffStorage,eDeBuffStorage);
            playerOrEnemy.GetComponent<Image>().sprite = Resources.Load<Sprite>("�A�C�R��/Enemy");
            coroutine =StartCoroutine(BackGroundRotate2());            yield return new WaitForSeconds(0.5f);
            StopCoroutine(coroutine);
            yield return new WaitForSeconds(5f);
            coroutine=StartCoroutine(BackGroundRotate1());
            yield return new WaitForSeconds(0.5f);
            StopCoroutine(coroutine);
            IconSponeManager(pBuffStorage,pDeBuffStorage);
            playerOrEnemy.GetComponent<Image>().sprite = Resources.Load<Sprite>("�A�C�R��/Player");
            coroutine =StartCoroutine(BackGroundRotate2());
            yield return new WaitForSeconds(0.5f);
            StopCoroutine(coroutine);
            yield return new WaitForSeconds(5f);
        }
    }
    float angle = 90;
    private IEnumerator BackGroundRotate1()
    {
       
        while(backGround.transform.rotation.x<angle)
        {
            backGround.transform.Rotate(angle * Time.deltaTime*2, 0, 0);
            storageObject.transform.Rotate(angle* Time.deltaTime*2, 0, 0);
            yield return null;
        }
        yield break;
    }

    private IEnumerator BackGroundRotate2()
    {
        while (backGround.transform.rotation.x > 0)
        {
            backGround.transform.Rotate(-angle * Time.deltaTime * 2, 0, 0);
            storageObject.transform.Rotate(-angle * Time.deltaTime * 2, 0, 0);
            yield return null;
        }

    }

 
    
    public static Quaternion storageRot()
    {
        return DerivaryRot.transform.rotation;
    }

    private List<int> IconDestory(List<int> BuffStorage)
    {
        for(int i=0;i<BuffStorage.Count;i++)
        {
            for (int j = 0; j < BuffStorage.Count; j++)
            {
                if (BuffStorage[i] == BuffStorage[j])
                {
                    if (i != j)
                    {
                        BuffStorage.RemoveAt(j);
                    }
                }
            }
        }
       
        return BuffStorage;
    }
}
