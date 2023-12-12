using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRePop : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] float delay = 60;
    float timer;
    BoxCollider bCollider;
    SphereCollider sCollider;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;

        sCollider = GetComponent<SphereCollider>();
        bCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        timer++;
        if (timer > delay)
        {
            timer = 0;

            if (bCollider != null)
            {
                var pos = RandomBounds.GetRandomPointInBounds(bCollider);
                Instantiate(prefab, pos, Quaternion.identity);
            }

            if (sCollider != null)
            {
                var pos = RandomBounds.GetRandomPointInBounds(sCollider);
                Instantiate(prefab, pos, Quaternion.identity);
            }
        }
    }
}
