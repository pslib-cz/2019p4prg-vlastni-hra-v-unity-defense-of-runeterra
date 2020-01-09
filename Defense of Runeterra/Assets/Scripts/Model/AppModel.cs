using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppModel : MonoBehaviour
{

    public float Actual_Level = 0;
    public float Actual_Money = 0;
    public GameObject Defeat;
    public GameObject TowerSprite;
    public GameObject Explosion;
    public Text HPText;
    public Text MoneyText;
    public Text LevelText;
    public Text ArmorText;
    public GameObject NextWaveButton;
    public Text NextWaveButtonText;

    
    private Camera _turret;
    private SpriteRenderer _renderer;
    private SpriteRenderer _turretRend;
    private Color _alphaColor;
   
    private float _turretHP;
    private float _turretMaxHP;
    private float _actualMoney;
    private float _actualLevel;
    private float _actualArmor;
    private PlayerControlModel _playerControlModel;
    private HeroesGenerator _heroesGenerator;

    // Start is called before the first frame update
    void Start()
    {
        _turret = Camera.main;
        _playerControlModel = _turret.GetComponent<PlayerControlModel>();
        _heroesGenerator = _turret.GetComponent<HeroesGenerator>();
        _turretHP = _playerControlModel.TurretActualHP;
        _turretMaxHP = _playerControlModel.TurretMaxHP;
        _actualLevel = 0;
    

        HPText.text = $"{_turretHP}/{_turretMaxHP}";
        NextWaveButtonText.text = "Begin game";


        _renderer = Defeat.GetComponent<SpriteRenderer>();
        _turretRend = TowerSprite.GetComponent<SpriteRenderer>();

       
        _alphaColor = _renderer.material.color;
        _alphaColor.a = 0;

        Defeat.SetActive(false);
        Explosion.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckTurret();
        CheckMoney();
        CheckLevel();
      
    }

    private void RedColor()
    {
        _turretRend.color = new Color(1, 0, 0);
        InvokeRepeating(nameof(BackColor), 0.2f, 1f);
        
        
    }
    private void BackColor()
    {
        _turretRend.color = new Color(1, 1, 1, 1);
        CancelInvoke(nameof(BackColor));
    }
    private void CheckTurret()
    {
        if (_playerControlModel.TurretActualHP <= 0 && Explosion.activeSelf == false)
        { //Check defeat
            _heroesGenerator.SpawningPeriod = -1; //Disable spawning

            Explosion.SetActive(true);
            Defeat.SetActive(true);
        }
        if (_turretHP != _playerControlModel.TurretActualHP)
        { //Check current HP
            _turretHP = _playerControlModel.TurretActualHP;
            var hp = _turretHP;
            HPText.text = $"{hp}/{_turretMaxHP}";
            RedColor();
        }
        if (_turretMaxHP != _playerControlModel.TurretMaxHP)
        { //Check max HP
            _turretMaxHP = _playerControlModel.TurretMaxHP;
            var hp = _playerControlModel.TurretMaxHP;
            HPText.text = $"{_turretHP}/{hp}";
        }
        if(_actualArmor != _playerControlModel.TurretArmor)
        {
            _actualArmor = _playerControlModel.TurretArmor;
            ArmorText.text = $"Armor : {_actualArmor}";
        }
    }

    private void CheckMoney()
    {
        if (_actualMoney != Actual_Money)
        {
            _actualMoney = Actual_Money;
            MoneyText.text = $"Money : {_actualMoney}";
        }
    }

    private void CheckLevel()
    {
        if (_actualLevel != Actual_Level)
        {
            _actualLevel = Actual_Level;
            LevelText.text = $"Level: {_actualLevel.ToString()}";

            switch (_actualLevel)
            {
                case 1f:
                    _heroesGenerator.KillValue = 1;
                    _heroesGenerator.WaveCount = 10f;
                    _heroesGenerator.SpawningPeriod = 5.0f;
                    break;
                case 2f:
                    _heroesGenerator.KillValue = 2;
                    _heroesGenerator.WaveCount = 14f;
                    _heroesGenerator.SpawningPeriod = 4.0f;
                    break;
                case 3f:
                    _heroesGenerator.KillValue = 3;
                    _heroesGenerator.WaveCount = 18f;
                    _heroesGenerator.SpawningPeriod = 3.0f;
                    break;
                case 4f:
                    _heroesGenerator.KillValue = 4;
                    _heroesGenerator.WaveCount = 22f;
                    _heroesGenerator.SpawningPeriod = 2.0f;
                    break;
                case 5f:
                    _heroesGenerator.KillValue = 5;
                    _heroesGenerator.WaveCount = 26f;
                    _heroesGenerator.SpawningPeriod = 1.0f;
                    break;
            }
        }
    }
}