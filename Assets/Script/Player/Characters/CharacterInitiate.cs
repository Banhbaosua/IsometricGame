using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInitiate : MonoBehaviour
{
    [SerializeField] CurrentClassData currentClassData;
    [SerializeField] Transform spawnLocation;
    [SerializeField] CameraFollow cameraFollow;
    [SerializeField] GameEvent onCharacterSpawn;
    // Start is called before the first frame update
    private void Awake()
    {
        SpawnCharacter();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCharacter()
    {
        var character = Instantiate(currentClassData.GetCharacterClassData().Prefab);
        character.transform.position = spawnLocation.position;
        cameraFollow.SetFollowPlayer(character.transform);
        onCharacterSpawn.Notify(this,character);
    }
}
