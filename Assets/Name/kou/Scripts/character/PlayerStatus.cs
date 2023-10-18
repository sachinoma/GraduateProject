using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField]
    InputReceiver inputReceiver;

    [SerializeField]
    private int playerNum;

    [SerializeField]
    private int outfitNum;

    [SerializeField]
    private GameObject outfitNow;
    [SerializeField]
    private GameObject[] outfit;

    [SerializeField]
    private Vector3 savePoint;

    [SerializeField]
    private Mover mover;

    [SerializeField]
    private PlayerConfiguration[] playerConfigs;

    private void Start()
    {
        if (inputReceiver.GetInput() != null)
            playerNum = inputReceiver.GetInput().GetPlayerNo();
        DiableAllOutfit();
        ChangeOutfit(GetOutfitNum());
        SetSavePoint(this.transform);
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -10.0f)
        {
            ReSpawn();
        }
    }

    public int GetPlayerNum()
    {
        return playerNum;
    }

    public int GetOutfitNum()
    {
        playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        outfitNum = playerConfigs[GetPlayerNum()].PlayerPrefabNum;
        return outfitNum;
    }

    public int GetOutfitMax()
    {
        return outfit.Length;
    }

    public void DiableAllOutfit()
    {
        for(int i = 0; i < outfit.Length; i++)
        {
            outfit[i].SetActive(false);
        }
    }

    public void ChangeOutfit(int num)
    {
        if(outfitNow != null)
            outfitNow.SetActive(false);
        outfit[num].SetActive(true);
        outfitNow = outfit[num].gameObject;
    }

    public void ReSpawn()
    {
        transform.position = savePoint;
        mover.MoveClear();
    }

    public void SetSavePoint(Transform gameObject)
    {
        savePoint = gameObject.position;
    }
}
