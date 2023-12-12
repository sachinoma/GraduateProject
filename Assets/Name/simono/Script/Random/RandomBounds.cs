using UnityEngine;

public class RandomBounds : MonoBehaviour
{
    //Box範囲内ランダム
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

    //Box範囲内ランダム（許容値分内側）
    public static Vector3 GetRandomPointInBounds(BoxCollider _spawnTarget, Vector3 _tolerance)
    {
        //宣言
        Vector3 min = _spawnTarget.bounds.min;
        Vector3 max = _spawnTarget.bounds.max;

        //ランダム位置を取得
        float x = Random.Range(min.x + _tolerance.x, max.x - _tolerance.x);
        float y = Random.Range(min.y + _tolerance.y, max.y - _tolerance.y);
        float z = Random.Range(min.z + _tolerance.z, max.z - _tolerance.z);

        return new(x, y, z);
    }

    //Box範囲内ランダム（許容値分内側）
    public static Vector3 GetRandomPointInBounds(BoxCollider _spawnTarget, Collider _tolerance)
    {
        //宣言
        Vector3 min = _spawnTarget.bounds.min;
        Vector3 max = _spawnTarget.bounds.max;

        //許容するコリジョンのスケールを取得
        Vector3 scale = _tolerance.transform.lossyScale;
        if(_tolerance is BoxCollider bCol)
        {
            scale.x *= bCol.size.x;
            scale.y *= bCol.size.y;
            scale.z *= bCol.size.z;
        }
        else if(_tolerance is CapsuleCollider cCol)
        {
            scale.x *= cCol.radius;
            scale.y *= cCol.height;
            scale.z *= cCol.radius;
        }
        else if(_tolerance is SphereCollider sCol)
        {
            scale.x *= sCol.radius;
            scale.y *= sCol.radius;
            scale.z *= sCol.radius;
        }
        else if(_tolerance is MeshCollider mCol)
        {
            Vector3 size = mCol.bounds.size;

            scale.x *= size.x;
            scale.y *= size.y;
            scale.z *= size.x;
        }


        //ランダム位置を取得
        float x = Random.Range(min.x + scale.x, max.x - scale.x);
        float y = Random.Range(min.y + scale.y, max.y - scale.y);
        float z = Random.Range(min.z + scale.z, max.z - scale.z);

        return new(x, y, z);
    }

    //Sphere範囲内ランダム
    public static Vector3 GetRandomPointInBounds(SphereCollider _collider)
    {
        //宣言
        Vector3 center = _collider.bounds.center;
        float radius = _collider.radius;
        Vector3 scale = _collider.transform.lossyScale;

        //半径を大きさ準拠に（楕円だと困るので、平均で取得）
        radius *= (scale.x + scale.y + scale.z) / 3f;

        //極座標内のランダム位置を取得
        Vector3 sphere = Random.insideUnitSphere;

        //極座標から出るランダム位置を長さとかけ、ワールド座標を加算
        Vector3 randomPosition = sphere * radius + center;
        return randomPosition;
    }
}