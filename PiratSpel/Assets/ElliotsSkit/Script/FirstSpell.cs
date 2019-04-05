using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSpell : SpellBaseCard
{
    public int Dmg;
    public int Heal;
    // Start is called before the first frame update
    void Start()
    {
        _Attack = Dmg;
        _heal = Heal;

    }

}
