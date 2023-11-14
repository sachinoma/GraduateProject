using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultCharacter : MonoBehaviour
{
    [SerializeField]
    private Vector3 savePoint;

    Rigidbody rb;

    [SerializeField]
    private int playerNum;

    [SerializeField]
    private PlayerConfiguration[] playerConfigs;

    [SerializeField]
    private int outfitNum;

    [SerializeField]
    private GameObject[] outfitPrefabs;

    void Start()
    {
        SetSavePoint(this.transform);
        rb = this.GetComponent<Rigidbody>();  // rigidbody‚ðŽæ“¾
        //for(int i = 0; i < outfitPrefabs.Length; ++i)
        //{
        //    outfitPrefabs[i].SetActive(false);
        //}
    }


    private void FixedUpdate()
    {
        if (transform.position.y < -10.0f)
        {
            ReSpawn();
        }
    }


    public void ReSpawn()
    {
        transform.position = savePoint;
    }

    public void SetSavePoint(Transform gameObject)
    {
        savePoint = gameObject.position;
    }

    public void BounceAction(Vector3 forceVec, float force)
    {
        if(rb != null)
        {
            rb.AddForce(forceVec * force, ForceMode.Impulse);
        }  
    }
    public void ChangeOutfit()
    {
        playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        outfitNum = playerConfigs[GetPlayerNum()].PlayerPrefabNum;
        outfitPrefabs[outfitNum].SetActive(true);
    }
    public int GetPlayerNum()
    {
        return playerNum;
    }
    public void SetPlayerNum(int num)
    {
        playerNum = num;
    }
}
