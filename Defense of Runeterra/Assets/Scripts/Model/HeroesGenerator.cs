using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Assets.Scripts.Heroes;
using UnityEngine;

public class HeroesGenerator : MonoBehaviour
{

    public List<GameObject> Heroes;
    public float SpawningPeriod;
    public float WaveCount;
    public float KillValue;
    public float MobsReleased;

    private bool _isBoss;
    private Camera _turret;
    private AppModel _appModel;
    private PlayerControlModel _playerControlModel;
    private GameObject _heroesEmpty;
    private System.Random index;
    private float _spawningPeriod;
    private float _waveCount;
    private float _mobsReleased;
    private float _killValue;

    // Start is called before the first frame update
    void Start()
    {
        foreach(var hero in Heroes) //Assign rigidbody at the begining so we dont have to do it manually
        {
            hero.AddComponent<Rigidbody2D>();
            hero.GetComponent<Rigidbody2D>().gravityScale = 0;
            hero.GetComponent<SpriteRenderer>().sortingOrder = 2;
            hero.tag = "Hero";
        }

        _turret = Camera.main;
        _appModel = _turret.GetComponent<AppModel>();
        _playerControlModel = _turret.GetComponent<PlayerControlModel>();

        _heroesEmpty = GameObject.Find("Heroes");

        index = new System.Random();

        _isBoss = false;
        _waveCount = -1f;
        _spawningPeriod = -1f;
        _killValue = -1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_spawningPeriod != SpawningPeriod)
        {
            CancelInvoke(nameof(generateNewRandomHero));
            _spawningPeriod = SpawningPeriod;
            if (_spawningPeriod != -1)
                InvokeRepeating(nameof(generateNewRandomHero), 1.5f, _spawningPeriod);
        }

        if (_waveCount != WaveCount) //New level is set up
        {
            _mobsReleased = 0;
            _waveCount = WaveCount;
        }

        if (_mobsReleased != MobsReleased)
        {
            MobsReleased = _mobsReleased;
            if ((_mobsReleased + 1) == _waveCount)
            {
                _isBoss = true;
            }
            else
            {
                _isBoss = false;
            }
        }

        if(_mobsReleased == _waveCount)
        {
            if (IsInvoking(nameof(generateNewRandomHero)))
            {
                CancelInvoke(nameof(generateNewRandomHero));
            }
        }
        else
        {
            if (!IsInvoking(nameof(generateNewRandomHero)) && _spawningPeriod != -1f)
            {
                _spawningPeriod = SpawningPeriod;
                InvokeRepeating(nameof(generateNewRandomHero), 1.5f, _spawningPeriod);
            }
        }

        if(_killValue != KillValue)
        {
            _killValue = KillValue;
        }
        
    }

    private void generateNewRandomHero()
    {
        try
        {
            GameObject hero = Instantiate(Heroes[index.Next(0, Heroes.Count)]) as GameObject;    //Instantiating a new random enemy
            hero.transform.parent = _heroesEmpty.transform;
            hero.transform.position = new Vector2(6.5f, UnityEngine.Random.Range(-1.3f, 1.5f));

            heroInit(hero);
        }
        catch(Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private void generateNewSpecificHero(GameObject _hero)
    {
        try
        {
            GameObject hero = Instantiate(_hero) as GameObject;    //Instantiating a new random enemy
            hero.transform.parent = _heroesEmpty.transform;
            hero.transform.position = new Vector2(6.5f, UnityEngine.Random.Range(-1.3f, 1.5f));

            heroInit(hero);
        }

        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private void heroInit(GameObject hero)
    {
        _mobsReleased += 1;

        //Split name by uppercase so we can just access hero name
        string[] split = Regex.Split(hero.name, @"(?<!^)(?=[A-Z])");
        name = split[0];
        var actual_level = _appModel.Actual_Level / 3;
        if(_isBoss ==true)
        {
            actual_level = _appModel.Actual_Level / 2;
        }
        
        switch (name)
        {
            case "Nocturne":
                hero.AddComponent<Hero>().StartDefault(9 * actual_level, 4 * actual_level, 0.9f / actual_level, 2, false);
                break;
            case "Ashe":
                hero.AddComponent<Hero>().StartDefault(8 * actual_level, 6 * actual_level, 1.2f / actual_level, 1, true);
                break;
            case "Karthus":
                hero.AddComponent<Hero>().StartDefault(2 * actual_level, 6 * actual_level, 0.3f / actual_level, 1, true);
                break;
            case "Taric":
                hero.AddComponent<Hero>().StartDefault(2 * actual_level, 12 * actual_level , 1.3f / actual_level, 0.5f, false);
                break;
            case "Teemo":
                hero.AddComponent<Hero>().StartDefault(6 * actual_level, 5 * actual_level , 0.6f / actual_level, 0.7f, true); //Easter-EGG
                break;
            case "Cho":
                hero.AddComponent<Hero>().StartDefault(7 * actual_level, 9 * actual_level, 1.2f / actual_level, 1, false);
                break;
            case "Alistar":
                hero.AddComponent<Hero>().StartDefault(3 * actual_level, 8 * actual_level, 0.9f / actual_level, 1.3f, false);
                break;
            case "Kog":
                hero.AddComponent<BossKog>().StartDefault(4 * actual_level, 4 * actual_level, 1f / actual_level, 2.5f, true);
                break;
        }
    }
}
