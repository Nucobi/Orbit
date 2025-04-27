using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Star : MonoBehaviour
{
    public ParticleSystem initialParticlePrefab;
    public ParticleSystem deathParticlePrefab; // ��� �� ��ƼŬ ȿ��
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
        // ��ƼŬ ȿ�� ���
        ParticleSystem deathEffect = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        deathEffect.Play();
        
        
        spriteRenderer.enabled = false;
        rigid.velocity = Vector3.zero;
        rigid.isKinematic = true;
        light.enabled = false;

        LevelManager.Instance.StopAllObjects();


        yield return new WaitForSeconds(1f);

        LevelManager.Instance.LoadLevel();

        
        // ������Ʈ�� �߾����� �̵�
        transform.position = Vector3.zero;
        
        ParticleSystem initialEffect = Instantiate(initialParticlePrefab, transform.position, Quaternion.identity);
        initialEffect.Play();
        
        spriteRenderer.enabled = true;
        rigid.isKinematic = false;
        light.enabled = true;
    }

}
