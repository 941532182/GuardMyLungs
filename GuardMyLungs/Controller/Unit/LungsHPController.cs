using System;
using UnityEngine;

namespace Controller
{
    public class LungsHPController : HPController
    {

        private HeartBeat hb;
        private float t;
        private bool isFailed;

        public event Action HPLow;
        public event Action HPHigh;

        private void Awake()
        {
            MaxHP = 100;
            hp = 100;
        }

        private void Start()
        {
            hb = Camera.main.GetComponent<HeartBeat>();
            HPChanged += OnHPChanged;
            Die += OnDie;
        }

        private void Update()
        {
            if (t < 3f)
            {
                t += Time.deltaTime;
            } else
            {
                TakeDamage(-1);
                t = 0;
            }
        }

        private void OnHPChanged(int hp)
        {
            if (hp <= 35 && !hb.enabled)
            {
                hb.enabled = true;
            }
            if (hp > 35 && hb.enabled)
            {
                hb.enabled = false;
            }
        }

        private void OnDie()
        {
            if (!isFailed)
            {
                GameObject.Find("Director").GetComponent<Director>().Fail();
                isFailed = true;
            }
        }

    }
}
