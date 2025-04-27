using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // 따라갈 대상 (Star 오브젝트)
    public Vector2 threshold = new Vector2(1f, 1f); // 카메라 중앙에서의 허용 범위
    public float smoothSpeed = 0.125f; // 카메라 이동 속도 (값이 낮을수록 부드러움)

    private Vector3 velocity = Vector3.zero;
    public float zDepth = 0f; // 마우스 위치의 Z 축 깊이

    private void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
    }


    private void LateUpdate()
    {
        if (target == null) return;

        // 대상과 카메라 간의 거리 계산
        Vector3 targetPosition = transform.position;
        Vector3 offset = target.position - transform.position;

        // X 방향 이동
        if (Mathf.Abs(offset.x) > threshold.x)
        {
            targetPosition.x += offset.x - Mathf.Sign(offset.x) * threshold.x;
        }

        // Y 방향 이동
        if (Mathf.Abs(offset.y) > threshold.y)
        {
            targetPosition.y += offset.y - Mathf.Sign(offset.y) * threshold.y;
        }

        // 부드러운 이동
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);
    }

    private void OnDrawGizmos()
    {
        // Threshold 영역 시각화 (디버그 용도)
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(threshold.x * 2, threshold.y * 2, 0));
    }
}
