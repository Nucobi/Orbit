using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Level/LevelData")]
public class LevelData : ScriptableObject
{
    [System.Serializable]
    public class ObjectSettings
    {
        public GameObject prefab; // ������Ʈ ������
        public Vector3 startPosition; // ���� ��ġ
        public Vector3 endPosition;   // ��ǥ ��ġ
        public float rotation;        // ȸ��
        public float delay;           // ���� ���� �ð�
        public float speedAmplifier = 1f; // �̵� �ӵ� ���� (�⺻�� 1)
    }

    public ObjectSettings[] objects;  // ��ֹ� ���� �迭
    public float globalspeed = 2f;    // ��ü �ӵ� (�⺻��)
    public float globalDelay = 0.5f;  // ��ü ���� �ð�
    public LevelData nextLevel;
}
