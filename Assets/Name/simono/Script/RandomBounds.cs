using UnityEngine;

public class RandomBounds : MonoBehaviour
{

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

    public static Vector3 GetRandomPointInBounds(SphereCollider _collider)
    {
        //�錾
        Vector3 center = _collider.bounds.center;
        float radius = _collider.radius;
        //���a��傫��������
        radius *= _collider.transform.lossyScale.x;
        //�����_���ʒu���擾
        Vector3 sphere = Random.insideUnitSphere;
        //�ɍ��W����o�郉���_���ʒu�𒷂��Ƃ����A���[���h���W�����Z
        Vector3 randomPosition = sphere * radius + center;
        return randomPosition;
    }
}