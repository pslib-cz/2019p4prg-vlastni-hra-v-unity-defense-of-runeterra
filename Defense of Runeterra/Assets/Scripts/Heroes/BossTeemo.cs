using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Heroes.Abstract;
using UnityEngine;

namespace Assets.Scripts.Heroes
{
    public class BossTeemo : AbstractHero
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


        }
    }
}

