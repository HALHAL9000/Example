using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Volpi.ObjectyPool;
public class MoneyCollectable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collect"))            
        {
            SpawnMoneyUI();
            SaveManager.Instance.Money += 10 + SaveManager.Instance.Income;
            Invoke("SpawnMoneyUI", 0.1f);
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
    void SpawnMoneyUI()
    {
        //ObjectyManager.Instance.ObjectyPools["ObjectyPool"].Spawn("MoneyUI", transform.position).transform.SetParent(transform.parent);
    }
}
