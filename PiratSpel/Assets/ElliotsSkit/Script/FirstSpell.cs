using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSpell : SpellBaseCard
{
    public int Dmg;
    public int Heal;
    public int Cost;
    // Start is called before the first frame update
    void Start()
    {
        _dmg = Dmg;

        _heal = Heal;

        _cost = Cost;
    }

}
