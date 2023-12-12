using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class RingManager : MonoBehaviour
{
    [SerializeField] GameObject RingPrefab;
    [SerializeField] List<GameObject> RingPos;

    GameObject[] tagObjects;

   
    // Update is called once per frame
    void Update()
    {
        //tag取得
        tagObjects = GameObject.FindGameObjectsWithTag("Ring");
        //RingPrefabを5個生成
        if (tagObjects.Length < 5)
        {
            //Pos座標取得
            Vector3 createPos = RingPos[Random.Range(0, 4)].transform.position;
            //生成
            Instantiate(RingPrefab, createPos, RingPrefab.transform.rotation);
        }
        
    }
}

