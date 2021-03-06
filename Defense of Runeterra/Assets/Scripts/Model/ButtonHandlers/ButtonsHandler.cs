﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsHandler : MonoBehaviour
{
    public float DMGCost = 3;
    public float ASCost = 3;
    public float MSCost = 3;
    public float HPCost = 3;

    public Text DMGCostText;
    public Text ASCostText;
    public Text MSCostText;
    public Text HPCostText;

    private Camera _turret;
    private PlayerControlModel _playerControl;
    private AppModel _appModel;

    void Start()
    {
        _turret = Camera.main;
        _playerControl = _turret.GetComponent<PlayerControlModel>();
        _appModel = _turret.GetComponent<AppModel>();

        DMGCostText.text = $"Cost: {DMGCost}";
        ASCostText.text = $"Cost: {ASCost}";
        MSCostText.text = $"Cost: {MSCost}";
        HPCostText.text = $"Cost: {HPCost}";
    }
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            IncDMG();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            IncAS();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            IncArmor();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            IncHP();
        }
    }
    public void IncDMG()
    {
        if (CheckMoney(DMGCost))
        {
            _playerControl.BulletDamage += 0.8f;
            DMGCost = DMGCost * 2;
            DMGCostText.text = $"Cost: {DMGCost}";
        }
    }
    public void IncAS()
    {
        if (CheckMoney(ASCost))
        {
            _playerControl.ShootCooldown -= 0.15f;
            ASCost = ASCost * 2;
            ASCostText.text = $"Cost: {ASCost}";
        }
    }
    public void IncArmor()
    {
        if (CheckMoney(MSCost))
        {
            _playerControl.TurretArmor += 1.5f;
            MSCost = MSCost * 2;
            MSCostText.text = $"Cost: {MSCost}";
        }
    }
    public void IncHP()
    {
        if (CheckMoney(HPCost))
        {
            _playerControl.TurretActualHP += 10f;
            _playerControl.TurretMaxHP += 10f;
            HPCost = HPCost * 2;
            HPCostText.text = $"Cost: {HPCost}";
        }
    }
    public void NextWave()
    {
        if (_appModel.NextWaveButtonText.text == "Begin game")
        {
            _appModel.NextWaveButtonText.text = "Next wave!";
        }
        _appModel.NextWaveButton.SetActive(false);

        _appModel.Actual_Level += 1;
    }

    private bool CheckMoney(float cost)
    {
        if (_appModel.Actual_Money >= cost)
        {
            _appModel.Actual_Money -= cost;
            return true;
        }
        else
        {
            return false;
        }
    }
}
