using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JustClass : MonoBehaviour
{
    public Text[] text;
    public string[] Names;

    public void Start()
    {
        Swap();
        for (int name = 0,txt = 0; name < Names.Length; name++)
        {
            text[txt].text += "\n" + Names[name];
            txt++;
            if (txt == 4)
                txt = 0;
        }
    }

    private void Swap()
    {
        for (int i = 0; i < Names.Length; i++)
        {
            int tempNumber = Random.Range(0, Names.Length);
            string currentName = Names[i];
            Names[i] = Names[tempNumber];
            Names[tempNumber] = currentName;
        }
    }
}
