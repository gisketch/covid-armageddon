using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextTypewriter : MonoBehaviour
{
    public TMP_Text _Text;
    string[] _textCharacter;

    [SerializeField]
    public bool isActive = false;
    public float timeInSeconds;
    float timer;
    public int charCount;

    void Start() {
        _Text.text = "";
    }

    public void StartShowing(string message)
    {
        _textCharacter = new string[message.Length];
        for (int i = 0; i < message.Length; i++)
        {
            //create an array of letters in the input string
            _textCharacter[i] = message.Substring(i,1);
        }
        _Text.text = "";
        charCount = 0;
        timer = 0;
        isActive = true;
    }

    void Update()
    {
        if(isActive){
           if(charCount < _textCharacter.Length) {
                timer += Time.deltaTime;
                if(timer >= timeInSeconds && charCount < _textCharacter.Length) {
                    _Text.text += _textCharacter[charCount];
                    charCount++;
                    timer = 0;
                }
            }

            if(charCount == _textCharacter.Length) {
                isActive = false;
            }
        }
    }
}
