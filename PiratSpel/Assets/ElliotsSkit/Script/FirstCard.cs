﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class FirstCard : MinionBaseCard
{
    public int ad;
    public int hp;
    public int co;
    public AudioClip Spawnsound;
    public AudioClip Attacksound;
    // Start is called before the first frame update
    public override void Start()
    {
        StartingStats(ad, hp, co);
        _SpawnSound = Spawnsound;
        _AttackSound = Attacksound;

        base.Start();
    }
    // Update is called once per frame
}
