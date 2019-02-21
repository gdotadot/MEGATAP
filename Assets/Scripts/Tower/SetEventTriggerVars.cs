using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//<alex> - set the pointer click variables so we don't have to serialze and drag them in for every single floor
[RequireComponent(typeof(SetPointerClickEvents))]
public class SetEventTriggerVars : MonoBehaviour {
    private SetPointerClickEvents customEventTrigger;


    private void Start()
    {
        customEventTrigger = GetComponent<SetPointerClickEvents>();

        GameObject playerTwo = GameObject.Find("Player 2");
        customEventTrigger.CastSpell = playerTwo.GetComponent<CastSpell>();
        customEventTrigger.PlaceTrap = playerTwo.GetComponent<PlaceTrap>();

    }
}
