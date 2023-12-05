using UnityEngine;

public class RandomBounds : MonoBehaviour
{

    public static Vector3 GetRandomPointInBounds(BoxCollider _collider)
    {
        //宣言
        Vector3 min = _collider.bounds.min;
        Vector3 max = _collider.bounds.max;
        //ランダム位置を取得
        float x = Random.Range(min.x, max.x);
        float y = Random.Range(min.y, max.y);
        float z = Random.Range(min.z, max.z);

        return new(x, y, z);
    }

    public static Vector3 GetRandomPointInBounds(SphereCollider _collider)
    {
        //宣言
        Vector3 center = _collider.bounds.center;
        float radius = _collider.radius;
        //半径を大きさ準拠に
        radius *= _collider.transform.lossyScale.x;
        //ランダム位置を取得
        Vector3 sphere = Random.insideUnitSphere;
        //極座標から出るランダム位置を長さとかけ、ワールド座標を加算
        Vector3 randomPosition = sphere * radius + center;
        return randomPosition;
    }
}