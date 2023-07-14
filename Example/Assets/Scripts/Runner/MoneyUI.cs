using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Volpi.ObjectyPool;
public class MoneyUI : MonoBehaviour
{
    [SerializeField] Animator anim;

    private void OnEnable()
    {
        Invoke("DespawnMoneyUI", 0.833f);
    }
    private void OnDisable()
    {
        anim.Rebind();
    }
    void DespawnMoneyUI()
    {
        //ObjectyManager.Instance.ObjectyPools["ObjectyPool"].Despawn("MoneyUI", gameObject);
    }
}
