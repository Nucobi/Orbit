using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Level/LevelData")]
public class LevelData : ScriptableObject
{
    [System.Serializable]
    public class ObjectSettings
    {
        public GameObject prefab; // 오브젝트 프리팹
        public Vector3 startPosition; // 시작 위치
        public Vector3 endPosition;   // 목표 위치
        public float rotation;        // 회전
        public float delay;           // 개별 지연 시간
        public float speedAmplifier = 1f; // 이동 속도 증폭 (기본값 1)
    }

    public ObjectSettings[] objects;  // 장애물 설정 배열
    public float globalspeed = 2f;    // 전체 속도 (기본값)
    public float globalDelay = 0.5f;  // 전체 지연 시간
    public LevelData nextLevel;
}
