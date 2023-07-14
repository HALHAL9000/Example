using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using Volpi.ObjectyPool;

public class Stickman : MonoBehaviour, IHittable
{
    [SerializeField] float health;
    [SerializeField] TMP_Text healthText;

    Animator hitAnim,anim;
    [SerializeField] Material stickmanGrayMaterial;
    SkinnedMeshRenderer character;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("/GameManager").GetComponent<GameManager>();
        hitAnim = transform.parent.GetComponent<Animator>();
        anim = GetComponent<Animator>();
        character = transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();
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
            Death();
            SpawnMoneyUI();
            Invoke("SpawnMoneyUI", 0.1f);
            SaveManager.Instance.Money += 10 + SaveManager.Instance.Income;
        }
    }
    void Death()
    {
        GetComponent<BoxCollider>().enabled = false;
        character.material = stickmanGrayMaterial;
        healthText.gameObject.SetActive(false);
        anim.SetBool("death", true);
    }
    void SpawnMoneyUI()
    {
        //ObjectyManager.Instance.ObjectyPools["ObjectyPool"].Spawn("MoneyUI", transform.parent.GetChild(1).transform.position).transform.SetParent(transform.parent.GetChild(1));
    }
}
