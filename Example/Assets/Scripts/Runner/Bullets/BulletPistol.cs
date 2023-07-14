using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Volpi.ObjectyPool;

public class BulletPistol : MonoBehaviour
{
    bool isHit;
    [SerializeField] ParticleSystem hitParticle;
    private void OnEnable()
    {
        Invoke("BackToPool", 0.6f);
    }

    void Update()
    {
        if (isHit == false)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 12);
        }
    }
    void BackToPool()
    {
        isHit = false;
        //ObjectyManager.Instance.ObjectyPools["ObjectyPool"].Despawn("BulletPistol", gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IHittable hittableObject))
        {
            hitParticle.Play();
            isHit = true;
            hittableObject.Damage();
            Invoke("BackToPool", 0.6f);
        }
    }
}
