using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Control : MonoBehaviour
{
    public List<GameObject> GravityObjects;

    public float gravityAmplifier = 3f;
    public float multiplier = 1f;
    public float sensitivity = 1f;

    public ParticleSystem particle;
    public float startparticlespeed;

    public Animator animator;

    private void Awake() {
        startparticlespeed = particle.startSpeed;
    }
    void Update() {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;

        gameObject.transform.position = pos * sensitivity;

    }

    private void FixedUpdate() {
        // Add force to PlayerObject
        foreach (GameObject gravityObject in GravityObjects) {
            Vector3 force = (gameObject.transform.position - gravityObject.transform.position).normalized * gravityAmplifier * multiplier;

            if (Input.GetMouseButton(0))
            {
                multiplier = 2.5f;
                AdjustParticleSpeed(3f);
                animator.SetBool("Leftclick", true);
                animator.SetBool("Rightclick", false);
            }
            else if (Input.GetMouseButton(1))
            {
                multiplier = -1f;
                AdjustParticleSpeed(-3f);
                animator.SetBool("Leftclick", false);
                animator.SetBool("Rightclick", true);
            }
            else
            {
                multiplier = 1f;
                AdjustParticleSpeed(1f);
                animator.SetBool("Leftclick", false);
                animator.SetBool("Rightclick", false);
            }



            gravityObject.GetComponent<Rigidbody2D>().AddForce(force);
        }
    }

    private void AdjustParticleSpeed(float multiplier)
    {
        particle.startSpeed = startparticlespeed * multiplier;
    }
}
