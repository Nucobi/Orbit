using UnityEngine;
using System.Collections;
public class Objects : MonoBehaviour
{
    public ObjectType type;
    public GameObject destroyparticle;

    private Vector3 startPos;   // ���� ��ġ
    private Vector3 endPos;     // ��ǥ ��ġ
    private float speed;        //  
    private float delay;        // �̵� ���� �� ���� �ð�
    private float startTime;    // �̵� ���� �ð�
    public bool isMoving = false;

    public void Initialize(Vector3 start, Vector3 end, float rotation, float globalSpeed, float globalDelay, float speedAmplifier, float objectDelay)
    {
        startPos = start;
        endPos = end;

        speed = globalSpeed * speedAmplifier;

        transform.rotation = Quaternion.Euler(0, 0, rotation);

        // ���� �ð��� globalDelay + objectDelay
        delay = globalDelay + objectDelay;

        transform.position = startPos; // ���� ��ġ�� ����
        isMoving = false;
    }

    public void StartMoving()
    {
        startTime = Time.time + delay; // ���� �ð��� ���� �ð� �ݿ�
        isMoving = true;
    }

    private void Update()
    {
        if (!isMoving || Time.time < startTime) return;
        
        if (type.Equals(ObjectType.EndTrigger))
        {
            LevelManager.Instance.CompleteLevel();
            return;
        }

        float elapsedTime = Time.time - startTime;
        if (elapsedTime < 0f) return; // ���� �̵��� �������� ���� (delay)

        float distance = Vector3.Distance(startPos, endPos); // �̵� �Ÿ�
        float travelTime = distance / speed;                // �Ÿ��� �ӵ��� �̵� �ð� ���

        float progress = elapsedTime / travelTime; // �̵� ����� ���

        if (progress >= 1f)
        {
            transform.position = endPos;
            isMoving = false;
            gameObject.SetActive(false); // �̵� �Ϸ� �� ��Ȱ��ȭ
        }
        else
        {
            transform.position = Vector3.Lerp(startPos, endPos, progress);
        }
    }

    public void ResetObject()
    {
        isMoving = false;
        transform.position = startPos;
    }


    public void Stop() {
        if (!gameObject.active) return;
        StartCoroutine(RollbackAnimation());
        ResetObject();
    }

    private IEnumerator RollbackAnimation()
    {
        // ���� ȿ�� ����
        if (destroyparticle != null)
        {
            GameObject explosion = Instantiate(destroyparticle, transform.position, Quaternion.identity);

            // ���� ȿ�� ��� �� ����
            ParticleSystem particleSystem = explosion.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                particleSystem.Play();
            }
        }

        // �ణ�� ���
        yield return new WaitForSeconds(0.5f);

        // ������Ʈ ��Ȱ��ȭ
        gameObject.SetActive(false);
    }


}
