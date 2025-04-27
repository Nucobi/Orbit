using UnityEngine;
using System.Collections;
public class Objects : MonoBehaviour
{
    public ObjectType type;
    public GameObject destroyparticle;

    private Vector3 startPos;   // 시작 위치
    private Vector3 endPos;     // 목표 위치
    private float speed;        //  
    private float delay;        // 이동 시작 전 지연 시간
    private float startTime;    // 이동 시작 시간
    public bool isMoving = false;

    public void Initialize(Vector3 start, Vector3 end, float rotation, float globalSpeed, float globalDelay, float speedAmplifier, float objectDelay)
    {
        startPos = start;
        endPos = end;

        speed = globalSpeed * speedAmplifier;

        transform.rotation = Quaternion.Euler(0, 0, rotation);

        // 지연 시간은 globalDelay + objectDelay
        delay = globalDelay + objectDelay;

        transform.position = startPos; // 시작 위치로 설정
        isMoving = false;
    }

    public void StartMoving()
    {
        startTime = Time.time + delay; // 시작 시간에 지연 시간 반영
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
        if (elapsedTime < 0f) return; // 아직 이동을 시작하지 않음 (delay)

        float distance = Vector3.Distance(startPos, endPos); // 이동 거리
        float travelTime = distance / speed;                // 거리와 속도로 이동 시간 계산

        float progress = elapsedTime / travelTime; // 이동 진행률 계산

        if (progress >= 1f)
        {
            transform.position = endPos;
            isMoving = false;
            gameObject.SetActive(false); // 이동 완료 시 비활성화
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
        // 폭발 효과 생성
        if (destroyparticle != null)
        {
            GameObject explosion = Instantiate(destroyparticle, transform.position, Quaternion.identity);

            // 폭발 효과 재생 후 제거
            ParticleSystem particleSystem = explosion.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                particleSystem.Play();
            }
        }

        // 약간의 대기
        yield return new WaitForSeconds(0.5f);

        // 오브젝트 비활성화
        gameObject.SetActive(false);
    }


}
