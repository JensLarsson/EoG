using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Abilites should probably be remade to use monobehaviours, so that variables can be changed in the editor
public class AbilityController : MonoBehaviour
{
    public enum Ability { None = 0, Tälékanenys, Bless };
    public Ability ability = Ability.None;
    IAbility[] abilities;
    public ParticleSystem particles; //Only used in the Bless ability, which only makes visuals right now
    // Start is called before the first frame update
    void Start()
    {
        abilities = new IAbility[Enum.GetNames(typeof(Ability)).Length];
        abilities[(int)Ability.None] = new NoAbility();
        abilities[(int)Ability.Tälékanenys] = new Telekinesis(this.transform);
        abilities[(int)Ability.Bless] = new BlessAbility(particles);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            abilities[(int)ability].IExecute();
        }
        abilities[(int)ability].IUpdate();
    }
}
