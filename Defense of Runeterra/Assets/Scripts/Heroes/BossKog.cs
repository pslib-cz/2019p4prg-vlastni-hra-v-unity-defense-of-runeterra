using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Heroes.Abstract;
using UnityEngine;

namespace Assets.Scripts.Heroes
{
    public class BossKog : AbstractHero
    {
       
         
        // Start is called before the first frame update
        void Start()
        {
            
            
        }

        // Update is called once per frame
        void Update()
        {

            if (_rigidbody.velocity == Vector2.left * MS)
            {
                _rigidbody.velocity = new Vector2(-0.5f, 1) * MS;
            }
            if (_transform.position.y < -1.3f && _transform.position.y < -1.1f)
            {
                _rigidbody.velocity = new Vector2(-0.5f, 1) * MS;
            }
            else if (_transform.position.y >= 1.5f)
            {
                _rigidbody.velocity = new Vector2(-0.5f, -1) * MS;
            }

            if (_transform.position.x > -2.0f && _transform.position.x < -1.0f)
            {
                _rigidbody.velocity = Vector2.zero;
            }

            if (_rigidbody.velocity == Vector2.zero)
            {
                Attacking = true;
                if (!IsInvoking(nameof(Attacc)))
                    InvokeRepeating(nameof(Attacc), 1.5f, AS);
            }
            else
            {
                Attacking = false;
                CancelInvoke(nameof(Attacc));
            }

            
           

            void Attacc()
            {
                var script = _turret.GetComponent<PlayerControlModel>();
                if (AD > 0)
                {
                    if (script.TurretActualHP > 0)
                    {
                        script.TurretActualHP -= AD;
                        AS -= 0.1f;
                        if (AS <= 0)
                        {
                            AS = 0.1f;
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
                            script.TurretActualHP -= AD;
                        }
                    }
                }

            }
        }
    }
}

