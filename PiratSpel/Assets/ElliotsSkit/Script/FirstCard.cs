using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class FirstCard : TempBaseCard2
{
    public int ad;
    public int hp;
    public int co;
    public AudioClip spawnsound;
    // Start is called before the first frame update
    public override void Start()
    {
        StartingStats(ad, hp, co);
        _SpawnSound = spawnsound;

        base.Start();
    }
    // Update is called once per frame
}
