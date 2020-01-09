using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Heroes.Abstract
{
    public abstract class AbstractHero : ExposableMonobehaviour
    {
        [ExposeProperty]
        public float AD { get; set; }
        [ExposeProperty]
        public float HP { get; set; }
        [ExposeProperty]
        public float AS { get; set; }
        [ExposeProperty]
        public float MS { get; set; }
        [ExposeProperty]
        public bool Ranged { get; set; }
        [ExposeProperty]
        public bool Attacking { get; set; }

        protected Transform _transform;
        protected Rigidbody2D _rigidbody;
        protected Camera _turret;
        protected AppModel _appModel;
        protected PlayerControlModel _playerControlModel;
        protected HeroesGenerator _heroesGenerator;
        protected bool _isLast;
        protected GameObject _heroes;

        public void StartDefault(float _ad,
                                    float _hp,
                                    float _as,
                                    float _ms,
                                    bool _ranged,
                                    bool _attacking = false)
        {
            AD = _ad;
            HP = _hp;
            AS = _as;
            MS = _ms;
            Ranged = _ranged;
            Attacking = _attacking;
            _transform = GetComponent<Transform>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.velocity = Vector2.left * MS; //Make it moving left
            _turret = Camera.main;
            _playerControlModel = _turret.GetComponent<PlayerControlModel>();
            _appModel = _turret.GetComponent<AppModel>();
            _heroesGenerator = _turret.GetComponent<HeroesGenerator>();
            _isLast = _heroesGenerator.MobsReleased == _heroesGenerator.WaveCount - 1 ? true : false;
            _heroes = GameObject.Find("Heroes");
        }

        protected void UpdateDefault()
        {

            if (Ranged)
            {
                if (_transform.position.x > -2.0f && _transform.position.x < -1.0f)
                {
                    _rigidbody.velocity = Vector2.zero;
                }
            }
            else
            {
                if (_transform.position.x > -4.0f && _transform.position.x < -3.5f)
                {
                    _rigidbody.velocity = Vector2.zero;
                }
            }

            if(_rigidbody.velocity == Vector2.zero)
            {
                Attacking = true;
                if(!IsInvoking(nameof(Attacc)))
                    InvokeRepeating(nameof(Attacc), 1.5f, AS);
            }
            else
            {
                Attacking = false;
                CancelInvoke(nameof(Attacc));
            }


        }
        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Bullet"))
            {
                HP -= _playerControlModel.BulletDamage;
                //scoreText.text = _score.ToString();
                Destroy(collider.gameObject);
                if (HP <= 0)
                {
                    Destroy(gameObject);

                    if (_heroes.transform.childCount == 1 && (_heroesGenerator.MobsReleased == _heroesGenerator.WaveCount))
                    {
                        _playerControlModel.TurretActualHP = _playerControlModel.TurretMaxHP;
                        _appModel.Actual_Money += (_appModel.Actual_Level / 3) * 100;
                        _appModel.NextWaveButton.SetActive(true);
                    }
                    _appModel.Actual_Money += _heroesGenerator.KillValue;
                }
            } 
        }

        void Attacc()
        {
            var script = _turret.GetComponent<PlayerControlModel>();
            if (AD > 0)
            {
                if (script.TurretActualHP > 0)
                {
                    if  (AD - script.TurretArmor > 0)
                    {
                        script.TurretActualHP -= AD - script.TurretArmor;
                    }
                    
                    if (script.TurretActualHP < 0)
                    {
                        script.TurretActualHP = 0;
                    }
                }
            }
            else
            {
                if (script.TurretActualHP > 0)
                {
                    if ((script.TurretActualHP - AD) > script.TurretMaxHP)
                    {
                        script.TurretActualHP = script.TurretMaxHP;
                    }
                    else
                    {
                        script.TurretActualHP -= AD - script.TurretArmor;
                    }
                }
            }
        }

    }
}
