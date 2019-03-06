using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCard : TempBaseCard
{
    public int ad;
    public int hp;
    public int co;
    // Start is called before the first frame update
    public override void Start()
    {
        StartingStats(ad, hp, co);
        base.Start();
    }

    // Update is called once per frame
}
