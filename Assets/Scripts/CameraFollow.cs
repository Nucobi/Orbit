using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // ���� ��� (Star ������Ʈ)
    public Vector2 threshold = new Vector2(1f, 1f); // ī�޶� �߾ӿ����� ��� ����
    public float smoothSpeed = 0.125f; // ī�޶� �̵� �ӵ� (���� �������� �ε巯��)

    private Vector3 velocity = Vector3.zero;
    public float zDepth = 0f; // ���콺 ��ġ�� Z �� ����

    private void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
    }


    private void LateUpdate()
    {
        if (target == null) return;

        // ���� ī�޶� ���� �Ÿ� ���
        Vector3 targetPosition = transform.position;
        Vector3 offset = target.position - transform.position;

        // X ���� �̵�
        if (Mathf.Abs(offset.x) > threshold.x)
        {
            targetPosition.x += offset.x - Mathf.Sign(offset.x) * threshold.x;
        }

        // Y ���� �̵�
        if (Mathf.Abs(offset.y) > threshold.y)
        {
            targetPosition.y += offset.y - Mathf.Sign(offset.y) * threshold.y;
        }

        // �ε巯�� �̵�
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);
    }

    private void OnDrawGizmos()
    {
        // Threshold ���� �ð�ȭ (����� �뵵)
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(threshold.x * 2, threshold.y * 2, 0));
    }
}
