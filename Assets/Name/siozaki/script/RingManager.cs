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
        //tag�擾
        tagObjects = GameObject.FindGameObjectsWithTag("Ring");
        //RingPrefab��5����
        if (tagObjects.Length < 5)
        {
            //Pos���W�擾
            Vector3 createPos = RingPos[Random.Range(0, 4)].transform.position;
            //����
            Instantiate(RingPrefab, createPos, RingPrefab.transform.rotation);
        }
        
    }
}

