using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField] private Vector3[] movePosition = new Vector3[2];
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float startUpTime = 2.5f;
    [SerializeField] private float totalTime = 3.0f;
    [SerializeField] private float boundLength = 5.0f;
    private bool isUp;
    private float time;

    private Vector3 startPos, endPos;

    // Start is called before the first frame update
    void Start()
    {
        isUp = true;
        time = startUpTime;
        startPos = transform.position + movePosition[0];
        endPos = transform.position + movePosition[1];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (time >= totalTime - 1f || time <= 0f + 1f)
        {
            isUp = !isUp;
        }

        if (isUp) { time += Time.deltaTime * speed; }
        else { time -= Time.deltaTime * speed; }

        Vector3 pos;

        pos.x = BackInOut(time, totalTime, startPos.x, endPos.x, boundLength);
        pos.y = BackInOut(time, totalTime, startPos.y, endPos.y, boundLength);
        pos.z = BackInOut(time, totalTime, startPos.z, endPos.z, boundLength);

        transform.localPosition = pos;

    }

    public static float BackInOut(float t, float totaltime, float min, float max, float s)
    {
        max -= min;
        s *= 1.525f;
        t /= totaltime / 2;
        if (t < 1) return max / 2 * (t * t * ((s + 1) * t - s)) + min;

        t = t - 2;
        return max / 2 * (t * t * ((s + 1) * t + s) + 2) + min;
    }
}
