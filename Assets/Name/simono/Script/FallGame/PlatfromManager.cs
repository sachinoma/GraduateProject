using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Transform createOffset;
    [SerializeField] int length = 50;

    //���̃I�t�Z�b�g
    Vector3 HorOffset = new(1.74f, 0.0f, 0.0f);
    //�΂߂̃I�t�Z�b�g
    Vector3 angOffset = new(-0.87f, 0f, 1.5f);

    void Start()
    {
        //�x�[�X�ƂȂ���W
        Vector3 baseVec = createOffset.position;
        
        //�㔼��
        for (int i = length / 2; i > 0; i--)
        {
            //���ɐ���
            for (int j = 0; j < length - i; j++)
            {
                Vector3 offset = baseVec + j * HorOffset;
                Instantiate(prefab, offset, Quaternion.identity, transform);
            }
            //�΂ߕ����ɉ��Z
            baseVec += angOffset;

        }

        //������
        for (int i = 0; i <= length / 2; i++)
        {
            //���ɐ���
            for (int j = 0; j < length - i; j++)
            {
                Vector3 offset = baseVec + j * HorOffset;
                Instantiate(prefab, offset, Quaternion.identity, transform);
            }
            //�����͌��̈ʒu�ɖ߂�悤�ɉ��Z
            baseVec = baseVec + new Vector3(-angOffset.x, angOffset.y, angOffset.z);
        }
    }
}
