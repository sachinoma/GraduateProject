using UnityEngine;

public class RandomBounds : MonoBehaviour
{
    //Box�͈͓������_��
    public static Vector3 GetRandomPointInBounds(BoxCollider _collider)
    {
        //�錾
        Vector3 min = _collider.bounds.min;
        Vector3 max = _collider.bounds.max;

        //�����_���ʒu���擾
        float x = Random.Range(min.x, max.x);
        float y = Random.Range(min.y, max.y);
        float z = Random.Range(min.z, max.z);

        return new(x, y, z);
    }

    //Box�͈͓������_���i���e�l�������j
    public static Vector3 GetRandomPointInBounds(BoxCollider _spawnTarget, Vector3 _tolerance)
    {
        //�錾
        Vector3 min = _spawnTarget.bounds.min;
        Vector3 max = _spawnTarget.bounds.max;

        //�����_���ʒu���擾
        float x = Random.Range(min.x + _tolerance.x, max.x - _tolerance.x);
        float y = Random.Range(min.y + _tolerance.y, max.y - _tolerance.y);
        float z = Random.Range(min.z + _tolerance.z, max.z - _tolerance.z);

        return new(x, y, z);
    }

    //Box�͈͓������_���i���e�l�������j
    public static Vector3 GetRandomPointInBounds(BoxCollider _spawnTarget, Collider _tolerance)
    {
        //�錾
        Vector3 min = _spawnTarget.bounds.min;
        Vector3 max = _spawnTarget.bounds.max;

        //���e����R���W�����̃X�P�[�����擾
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


        //�����_���ʒu���擾
        float x = Random.Range(min.x + scale.x, max.x - scale.x);
        float y = Random.Range(min.y + scale.y, max.y - scale.y);
        float z = Random.Range(min.z + scale.z, max.z - scale.z);

        return new(x, y, z);
    }

    //Sphere�͈͓������_��
    public static Vector3 GetRandomPointInBounds(SphereCollider _collider)
    {
        //�錾
        Vector3 center = _collider.bounds.center;
        float radius = _collider.radius;
        Vector3 scale = _collider.transform.lossyScale;

        //���a��傫�������Ɂi�ȉ~���ƍ���̂ŁA���ςŎ擾�j
        radius *= (scale.x + scale.y + scale.z) / 3f;

        //�ɍ��W���̃����_���ʒu���擾
        Vector3 sphere = Random.insideUnitSphere;

        //�ɍ��W����o�郉���_���ʒu�𒷂��Ƃ����A���[���h���W�����Z
        Vector3 randomPosition = sphere * radius + center;
        return randomPosition;
    }
}