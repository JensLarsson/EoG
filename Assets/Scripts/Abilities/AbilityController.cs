using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Abilites should probably be remade to use monobehaviours, so that variables can be changed in the editor
public class AbilityController : MonoBehaviour
{
    public enum Ability { None = 0, Tälékanenys, Bless, Fireball };
    public Ability ability = Ability.None;
    IAbility[] abilities;
    public ParticleSystem particles; //Only used in the Bless ability, which only makes visuals right now
    public GameObject fireball; //Only used in the fireball ability
    // Start is called before the first frame update
    void Start()
    {
        abilities = new IAbility[Enum.GetNames(typeof(Ability)).Length];
        abilities[(int)Ability.None] = new NoAbility();
        abilities[(int)Ability.Tälékanenys] = new Telekinesis(this.transform);
        abilities[(int)Ability.Bless] = new BlessAbility(particles);
        abilities[(int)Ability.Fireball] = new FireBall(this.transform, fireball);

        EventManager.Subscribe("ChangeAbility", ChangeAbility);
    }

    private void OnDisable()
    {
        EventManager.UnSubscribe("ChangeAbility", ChangeAbility);
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

    public void ChangeAbility(Ability _ability)
    {
        abilities[(int)ability].IDisable();
        ability = _ability;
        abilities[(int)ability].IStart();
    }
    void ChangeAbility(EventParameter eParam)
    {
        if (eParam.intParam < abilities.Length)
        {
            abilities[(int)ability].IDisable();
            ability = (Ability)eParam.intParam;
            abilities[(int)ability].IStart();
        }
    }
}
