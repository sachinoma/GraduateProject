using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Unity.Burst.Intrinsics.X86.Avx;

public class RingManager : MonoBehaviour
{
    [SerializeField] List<GameObject> RingPrefab;
    [SerializeField] List<GameObject> RingPos;
    [SerializeField] int Ring_nop;//Number of piece:個数
    GameObject[] Ring_tagObjects, GoldRing_tagObjects;
    //ランダム
    List<int> random_num = new List<int>() { 1, 0, 0, 0, 0};

    // Update is called once per frame
    void Update()
    {
        //tag取得
        Ring_tagObjects = GameObject.FindGameObjectsWithTag("Ring");
        GoldRing_tagObjects = GameObject.FindGameObjectsWithTag("GoldRing");
        //RingPrefabを指定数生成
        if ((Ring_tagObjects.Length + GoldRing_tagObjects.Length) < Ring_nop)
        {
            CreateRing(RingPos, Ring_nop);
        }
    }

    void CreateRing(List<GameObject> _rings, int _capacity)
    {
        //リストをコピー
        List<GameObject> ringPos = new List<GameObject>(_rings);
        int random_ring = random_num[Random.Range(0, random_num.Count)];
        while (true)
        {
            //Pos座標取得
            GameObject ringParent = ringPos[Random.Range(0, ringPos.Count)];
            Vector3 createPos = ringParent.transform.position;
            Quaternion createRot = ringParent.transform.rotation;

            
            //生成
            var ring = Instantiate(RingPrefab[random_ring], createPos, createRot, ringParent.transform);
            //子の数が多い場合、リングが複数存在することになるため、消す
            if (ringParent.transform.childCount > 1)
            {
                Destroy(ring);
                ringPos.Remove(ringParent);
            }
            else
            {             
                break;
            }
        }
    }
}

