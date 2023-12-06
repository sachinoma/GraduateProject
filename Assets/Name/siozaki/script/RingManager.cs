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
        //tagæ“¾
        tagObjects = GameObject.FindGameObjectsWithTag("Ring");
        //RingPrefab‚ğ5ŒÂ¶¬
        if (tagObjects.Length < 5)
        {
            //PosÀ•Wæ“¾
            Vector3 createPos = RingPos[Random.Range(0, 4)].transform.position;
            //¶¬
            Instantiate(RingPrefab, createPos, RingPrefab.transform.rotation);
        }
        
    }
}

