﻿//using RPGCharacterAnims;
using RPGCharacterAnims;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

// 武器の種類
public enum WeaponType
{
    None,
    DARK_SWORD,
    AXE,
    HAMMER,
    MACE,
    SKULLAXE
}

public class WeaponManager : MonoBehaviour
{
    // ディゾルブする秒数(仮）
    [SerializeField] float dissolveSeconds = 1.0f;  

    //武器リスト
    [SerializeField] private List<GameObject> weaponList = null;

    //武器管理リスト
    private Dictionary<WeaponType, GameObject> weaponDict = null;

    // 現在持っている武器
    private GameObject currentWeapon = null;

    private void Start()
    {
        //武器リストが無ければ
        if (weaponList == null) return;

        //武器管理リストを生成
        weaponDict = new Dictionary<WeaponType, GameObject>();

        //------------------------------------
        // 使用する武器をインスタンス化し登録
        //------------------------------------
        foreach (var weaponPref in weaponList)
        {
            if (weaponPref == null)
            {
                Debug.LogError(weaponPref.name + "がロードされませんでした");
                continue;
            }

            WeaponType type = WeaponType.None;

            switch(weaponPref.name)
            {
                case "dark_sword":
                    type = WeaponType.DARK_SWORD;
                    break;

                case "axe":
                    type = WeaponType.AXE;
                    break;

                case "hammer":
                    type = WeaponType.HAMMER;
                    break;

                case "mace":
                    type = WeaponType.MACE;
                    break;

                case "skull_axe":
                    type = WeaponType.SKULLAXE;
                    break;

                default:
                    break;
            }
            if (type != WeaponType.None)
            {
                //既にあれば
                if (weaponDict.ContainsKey(type)) continue;

                var weaponObj = GameObject.Instantiate(weaponPref, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
                weaponObj.SetActive(false);
                weaponDict.Add(type, weaponObj);
            }
        }

        // 最初持っている武器の設定
        currentWeapon = weaponDict[WeaponType.DARK_SWORD];
        // 仮のポジション
        Vector3 initPos = new Vector3(-10.0f, 22.0f, -5.5f);
        currentWeapon.transform.position = initPos;
        // 武器を有効化
        currentWeapon.SetActive(true);
    }

    // 武器変更の関数
    // weapontype...WeaponType型
    public void ChangeWeapon(WeaponType weapontype)
    {
        // 元のオブジェクトの位置を記憶しておく
        Vector3 weaponPos = currentWeapon.transform.position;

        // ディゾルブをかける
        if (weapontype==WeaponType.DARK_SWORD) {
            var dissolvecontroller = weaponDict[weapontype].GetComponent<DissolveController>();
            dissolvecontroller.Dissolve();
        }
        /*
        // 武器を無効化する
        currentWeapon.SetActive(false);

        currentWeapon = weaponDict[weapontype];

        // 新しい武器の位置を変更する
        currentWeapon.transform.position = weaponPos;

        // 新しい武器を有効化する
        currentWeapon.SetActive(true);
        */
    }
}
