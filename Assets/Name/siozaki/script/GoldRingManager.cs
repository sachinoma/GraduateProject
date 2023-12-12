using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Unity.Burst.Intrinsics.X86.Avx;

public class GoldRingManager : MonoBehaviour
{
    [SerializeField] GameObject RingPrefab;
    [SerializeField] List<GameObject> RingPos;
    [SerializeField] int Ring_nop;//Number of piece:個数
    GameObject[] tagObjects;


    // Update is called once per frame
    void Update()
    {
        //tag取得
        tagObjects = GameObject.FindGameObjectsWithTag("GoldRing");
        //RingPrefabを指定数生成
        if (tagObjects.Length < Ring_nop)
        {
            CreateRing(RingPos, Ring_nop);
        }
    }

    void CreateRing(List<GameObject> _rings, int _capacity)
    {
        //リストをコピー
        List<GameObject> ringPos = new List<GameObject>(_rings);

        while (true)
        {
            //Pos座標取得
            GameObject ringParent = ringPos[Random.Range(0, ringPos.Count)];
            Vector3 createPos = ringParent.transform.position;
            //生成
            var ring = Instantiate(RingPrefab, createPos, Quaternion.identity, ringParent.transform);

            //子の数が多い場合、リングが複数存在することになるため、消す
            if (ringParent.transform.childCount > 2)
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

