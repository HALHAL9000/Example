using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gate : MonoBehaviour,IHittable
{
    [SerializeField] float value; 
    [SerializeField] TMP_Text valueText,gateTypeText;
    private GameManager gameManager;

    Animator hitAnim;
    [SerializeField] Material green, red, blue;

    [Header("1-Fire Rate | 2-Damage")]
    [SerializeField] int gateType;

    [SerializeField] bool isDoubleGate;
    [SerializeField] bool isLockGate;
    [SerializeField] GameObject padlock;
    public int hitCount;

    private void Start()
    {
        gameManager = GameObject.Find("/GameManager").GetComponent<GameManager>();
        hitAnim = transform.parent.GetComponent<Animator>();
        MaterialCheck();
        valueText.SetText(value.ToString("+#0.0;-#0.0"));

        if (gateType == 1)
        {
            gateTypeText.SetText("FIRERATE");
        }
        if (gateType == 2)
        {
            gateTypeText.SetText("DAMAGE");
        }
    }
    public void Damage()
    {
        if (isLockGate == true)
        {
            if (hitCount < 5)
            {
                hitCount++;
                if (hitCount >= 5)
                {
                    padlock.SetActive(false);
                    isLockGate = false;
                }
            }
        }
        else
        {
            value += gameManager.currentDamage / 10;
            valueText.SetText(value.ToString("+#0.0;-#0.0"));
            hitAnim.SetTrigger("hit");
            MaterialCheck();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collect"))
        {
            AddValue();
            if (isDoubleGate == false)
            {
                Destroy(transform.parent.gameObject);
            }
            if (isDoubleGate == true)
            {
                Destroy(transform.parent.parent.gameObject);
            }
        }
    }
    void AddValue()
    {
        if (gateType == 1)
        {
            gameManager.currentFireRate += value*6;
        }
        if (gateType == 2)
        {
            gameManager.currentDamage += value;
        }
    }
    void MaterialCheck()
    {
        if (value < 0)
        {
            GetComponent<MeshRenderer>().material = red;
        }
        if (value == 0)
        {
            GetComponent<MeshRenderer>().material = blue;
        }
        if (value > 0)
        {
            GetComponent<MeshRenderer>().material = green;
        }
    }
}
