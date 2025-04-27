using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Star : MonoBehaviour
{
    public ParticleSystem initialParticlePrefab;
    public ParticleSystem deathParticlePrefab; // 사망 시 파티클 효과
    public TrailRenderer trail;
    public Light2D light;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Rigidbody2D rigid;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        originalColor = spriteRenderer.color;
        spriteRenderer.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Objects obj;
        if (!collision.gameObject.TryGetComponent<Objects>(out obj)) return;

        if (obj.type.Equals(ObjectType.Obstacle) && obj.isMoving)
        {
            Debug.Log("Player hit an obstacle!");
            StartCoroutine(PlayerDeath());
        }
    }

    private IEnumerator PlayerDeath()
    {
        // 파티클 효과 재생
        ParticleSystem deathEffect = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        deathEffect.Play();
        
        
        spriteRenderer.enabled = false;
        rigid.velocity = Vector3.zero;
        rigid.isKinematic = true;
        light.enabled = false;

        LevelManager.Instance.StopAllObjects();


        yield return new WaitForSeconds(1f);

        LevelManager.Instance.LoadLevel();

        
        // 오브젝트를 중앙으로 이동
        transform.position = Vector3.zero;
        
        ParticleSystem initialEffect = Instantiate(initialParticlePrefab, transform.position, Quaternion.identity);
        initialEffect.Play();
        
        spriteRenderer.enabled = true;
        rigid.isKinematic = false;
        light.enabled = true;
    }

}
