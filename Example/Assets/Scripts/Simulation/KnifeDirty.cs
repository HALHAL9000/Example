using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using PaintIn3D;
//using CW.Common;
//using Volpi.ObjectyPool;
public class KnifeDirty : MonoBehaviour
{
    [SerializeField] WeaponData weaponData;
    GameManager gameManager;
    //[SerializeField] private List<P3dChangeCounter> counters;

    private int decimalPlaces;

    Animator anim;
    bool isClean = false;
    bool isCleanAnimStarted = false;
    bool isKnifePlaced = false;



    private void Start()
    {
        gameManager = GameObject.Find("/GameManager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
    }

    private void OnParticleCollision(GameObject collision)
    {
        if (isClean == false)
        {
            if (collision.CompareTag("Water"))
            { /*
                if (CleaningRate() > 45)
                {
                    isClean = true;
                }*/
            }
        }
    }

    private void Update()
    {
        if (isClean == true)
        {
            if (isCleanAnimStarted == false)
            {
                //ObjectyManager.Instance.ObjectyPools["ObjectyPool"].Spawn("CleanKnife", transform.position,new Vector3(180,0,-90)).transform.SetParent(transform.parent); // When we clear the knife, it spawns a new knife onto the platform.
                anim.SetTrigger("clean");
                isCleanAnimStarted = true;
            }
            Invoke("GoToHand", 1.5f);

            if (transform.position == gameManager.targetPosition.position)
            {
                PlaceWeapon();
            }
        }
    }

    void PlaceWeapon()
    { /*
        if (isKnifePlaced == false)
        {
            GameObject spawnedWeapon;
            spawnedWeapon = ObjectyManager.Instance.ObjectyPools["ObjectyPool"].Spawn("CleanKnife", gameManager.weaponPositions[gameManager.currentWeaponIndex].position);
            spawnedWeapon.transform.SetParent(gameManager.targetPosition.parent);
            spawnedWeapon.transform.eulerAngles = new Vector3(150.346f, -19.711f, -266.125f);
            spawnedWeapon.GetComponent<KnifeRunner>().enabled = true;
            gameManager.currentWeaponIndex++;
            isKnifePlaced = true;
            Destroy(this.gameObject);
        }*/
            }
    void GoToHand()
    {
        if (isKnifePlaced == false)
        {
            anim.enabled = false;
            transform.position = Vector3.MoveTowards(transform.position, gameManager.targetPosition.transform.position, Time.deltaTime * 5);
            transform.rotation = Quaternion.Slerp(transform.rotation, gameManager.targetPosition.rotation, Time.deltaTime * 5);
        }
    } /*
    float CleaningRate()
    {
        var finalCounters = counters.Count > 0 ? counters : null;
        var total = P3dChangeCounter.GetTotal(finalCounters);
        var count = P3dChangeCounter.GetCount(finalCounters);
        count = total - count;
        var percent = P3dCommon.RatioToPercentage(CwHelper.Divide(count, total), decimalPlaces);   //Knife 54 Max.   
        return percent;
    }*/
   
 
}
