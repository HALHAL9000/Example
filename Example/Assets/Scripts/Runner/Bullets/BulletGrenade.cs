using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Volpi.ObjectyPool;

public class BulletGrenade : MonoBehaviour
{
    bool isHit;
    [SerializeField] ParticleSystem hitParticle;
    [SerializeField] Animator childKnifeAnim;
    private void OnEnable()
    {
        childKnifeAnim.GetComponent<MeshRenderer>().enabled = true;
        Invoke("BackToPool", 1.0f);
    }

    void Update()
    {
        if (isHit == false)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 8);
        }
    }
    void BackToPool()
    {
        childKnifeAnim.GetComponent<MeshRenderer>().enabled = true;
        childKnifeAnim.enabled = true;
        isHit = false;
        //ObjectyManager.Instance.ObjectyPools["ObjectyPool"].Despawn("BulletGrenade", gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IHittable hittableObject))
        {
            childKnifeAnim.GetComponent<MeshRenderer>().enabled = false;
            childKnifeAnim.enabled = false;
            hitParticle.Play();
            isHit = true;
            hittableObject.Damage();
            Invoke("BackToPool", 0.6f);
        }
    }
}
