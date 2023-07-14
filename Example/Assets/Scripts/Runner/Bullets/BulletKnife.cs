using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Volpi.ObjectyPool;
public class BulletKnife : MonoBehaviour
{
    bool isHit;
    [SerializeField] ParticleSystem hitParticle;
    [SerializeField] Animator childKnifeAnim;
    private void OnEnable()
    {
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
        childKnifeAnim.enabled = true;
        isHit = false;
       // ObjectyManager.Instance.ObjectyPools["ObjectyPool"].Despawn("BulletKnife", gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IHittable hittableObject))
        {
            transform.GetChild(0).transform.eulerAngles = new Vector3(175, 8, -159);
            childKnifeAnim.enabled = false;
            hitParticle.Play();
            isHit = true;
            hittableObject.Damage();
            Invoke("BackToPool", 0.6f);
        }
    }
}
