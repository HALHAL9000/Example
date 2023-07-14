using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WeaponPlate : MonoBehaviour
{
    [SerializeField] WeaponData weaponData;
    [SerializeField] Material[] weaponPlateMaterials;
    [SerializeField] TMP_Text weaponLevelText;
    private void Start()
    {
        weaponLevelText.SetText("Lv. " + weaponData.WeaponLevel);
        PlateMaterialCheck();
    }
    public void PlateMaterialCheck()
    {
        if (SaveManager.Instance.ClearLevel >= weaponData.WeaponLevel)
        {
            GetComponent<MeshRenderer>().material = weaponPlateMaterials[0];
        }
        else
        {
            GetComponent<MeshRenderer>().material = weaponPlateMaterials[1];
        }
    }
    public int WeaponLevel()
    {
        return weaponData.WeaponLevel;
    }
}
