using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class deathMessage : MonoBehaviour
{
    public Text deathMessager;
    int letterIndex;
    float showNextLetter;
    string fullThing = "Oh fuck, you're dead";
    // Update is called once per frame
    void Update()
    {
        if (Time.time >= showNextLetter)
        {
            string currentMessage = fullThing.Substring(0, letterIndex);
            letterIndex++;
            deathMessager.text = currentMessage;
            showNextLetter = Time.time + 0.20f;
        }
    }
}
