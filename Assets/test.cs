using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] CharacterData characterData;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(characterData.DamageModifier);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
