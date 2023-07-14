using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Column : MonoBehaviour, IHittable
{
    [SerializeField] float health;
    [SerializeField] TMP_Text healthText;
    [SerializeField] ParticleSystem breakParticle;
    private GameManager gameManager;

    Animator hitAnim;

    private void Start()
    {
        gameManager = GameObject.Find("/GameManager").GetComponent<GameManager>();
        hitAnim = transform.GetChild(0).GetComponent<Animator>();

        healthText.SetText(((int)health).ToString());
    }

    public void Damage()
    {
        if (health > 0)
        {
            health -= 4 + gameManager.currentDamage;
            healthText.SetText(((int)health).ToString());
            hitAnim.SetTrigger("hit");
        }
        if (health <= 0)
        {
            GetComponent<BoxCollider>().enabled = false;
            transform.GetChild(0).GetComponent<CapsuleCollider>().isTrigger = true;
            transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            breakParticle.Play();
            healthText.gameObject.SetActive(false);
        }
    }
}
