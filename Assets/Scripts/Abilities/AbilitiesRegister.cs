using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesRegister : MonoBehaviour {
    public static AbilitiesRegister instance;

    public Hashtable abilities = new Hashtable();

    private void Start() {
        instance = this;

        abilities.Add("shield_bash", new AbilityShieldBash());
    }
}