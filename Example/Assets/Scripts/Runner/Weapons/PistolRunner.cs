using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Volpi.ObjectyPool;

public class PistolRunner : MonoBehaviour
{
    Animator anim;
    bool isRunnerStart;

    float randomStartTime;
    Transform bulletSpawnPoint;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("/GameManager").GetComponent<GameManager>();
        bulletSpawnPoint = transform.GetChild(0);
        randomStartTime = Random.Range(0.5f, 0.8f);
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (PlateLevelController.isRunnerStarted && !isRunnerStart)
        {
            RunnerStart();
            InvokeRepeating("Fire", randomStartTime, 2f - (gameManager.currentFireRate / 35));
            isRunnerStart = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Column"))
        {
            transform.SetParent(null);
            CancelInvoke("Fire");
            anim.SetTrigger("fall");
            gameManager.currentWeaponIndex--;
        }
    }
    void RunnerStart()
    {
        if (isRunnerStart == false)
        {
            anim.enabled = true;
            isRunnerStart = true;
        }
    }
    void Fire()
    {
        isRunnerStart = true;
        anim.SetTrigger("rebound");
        //ObjectyManager.Instance.ObjectyPools["ObjectyPool"].Spawn("BulletPistol", bulletSpawnPoint.position);
        CancelInvoke("Fire");
        InvokeRepeating("Fire", 2f - (gameManager.currentFireRate / 35), 2f - (gameManager.currentFireRate / 35));
    }
}
