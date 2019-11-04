using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempAbilityField : MonoBehaviour
{
    InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponent<InputField>();
    }

    // Update is called once per frame
    void OnGUI()
    {
        if (inputField.isFocused && Input.GetKeyDown(KeyCode.Return))
        {
            int i;
            if (int.TryParse(inputField.text, out i))
            {
                EventParameter eParam = new EventParameter()
                {
                    intParam = i
                };
                EventManager.TriggerEvent("ChangeAbility", eParam);
            }
        }
    }
}
