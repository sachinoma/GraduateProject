using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Unity.Burst.Intrinsics.X86.Avx;

public class RingManager : MonoBehaviour
{
    [SerializeField] List<GameObject> RingPrefab;
    [SerializeField] List<GameObject> RingPos;
    [SerializeField] int Ring_nop;//Number of piece:��
    GameObject[] tagObjects;
    //�����_��
    List<int> random_num = new List<int>() { 1, 0, 0, 0, 0, 0};

    // Update is called once per frame
    void Update()
    {
        //tag�擾
        tagObjects = GameObject.FindGameObjectsWithTag("Ring");
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
        int random_ring = random_num[Random.Range(0, random_num.Count)];
        while (true)
        {
            //Pos���W�擾
            GameObject ringParent = ringPos[Random.Range(0, ringPos.Count)];
            Vector3 createPos = ringParent.transform.position;
            Quaternion createRot = ringParent.transform.rotation;

            
            //����
            var ring = Instantiate(RingPrefab[random_ring], createPos, createRot, ringParent.transform);

            //�q�̐��������ꍇ�A�����O���������݂��邱�ƂɂȂ邽�߁A����
            if (ringParent.transform.childCount > 2)
            {
                Destroy(ring);
                ringPos.Remove(ringParent);
            }
            else
            {
                Debug.Log(random_ring);
                break;
            }
        }
    }
}

