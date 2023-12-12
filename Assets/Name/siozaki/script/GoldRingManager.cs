using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Unity.Burst.Intrinsics.X86.Avx;

public class GoldRingManager : MonoBehaviour
{
    [SerializeField] GameObject RingPrefab;
    [SerializeField] List<GameObject> RingPos;
    [SerializeField] int Ring_nop;//Number of piece:��
    GameObject[] tagObjects;


    // Update is called once per frame
    void Update()
    {
        //tag�擾
        tagObjects = GameObject.FindGameObjectsWithTag("GoldRing");
        //RingPrefab���w�萔����
        if (tagObjects.Length < Ring_nop)
        {
            CreateRing(RingPos, Ring_nop);
        }
    }

    void CreateRing(List<GameObject> _rings, int _capacity)
    {
        //���X�g���R�s�[
        List<GameObject> ringPos = new List<GameObject>(_rings);

        while (true)
        {
            //Pos���W�擾
            GameObject ringParent = ringPos[Random.Range(0, ringPos.Count)];
            Vector3 createPos = ringParent.transform.position;
            //����
            var ring = Instantiate(RingPrefab, createPos, Quaternion.identity, ringParent.transform);

            //�q�̐��������ꍇ�A�����O���������݂��邱�ƂɂȂ邽�߁A����
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

