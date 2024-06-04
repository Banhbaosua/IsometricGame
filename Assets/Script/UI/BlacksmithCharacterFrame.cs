using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlacksmithCharacterFrame : MonoBehaviour
{
    [SerializeField] Image _characterImg;
    [SerializeField] Text _className;
    [SerializeField] CurrentClassData _currentClassData;

    private void OnEnable()
    {
        _className.text = _currentClassData.GetCharacterClassData().name;
        _characterImg.sprite = _currentClassData.GetCharacterClassData().Icon;
    }
}
