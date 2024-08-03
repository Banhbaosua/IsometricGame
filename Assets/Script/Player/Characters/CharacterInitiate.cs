using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInitiate : MonoBehaviour
{
    [SerializeField] CurrentClassData currentClassData;
    [SerializeField] Transform spawnLocation;
    [SerializeField] CameraFollow cameraFollow;
    [SerializeField] GameEvent onCharacterSpawn;
    [SerializeField] CharacterData characterData;
    private GameObject player;
    public GameObject Player => player;
    // Start is called before the first frame update
    private void Awake()
    {
        SpawnCharacter();
    }

    public void SpawnCharacter()
    {
        player = Instantiate(currentClassData.GetCharacterClassData().Prefab);
        
        player.GetComponent<PlayerController>().SetWeapon(currentClassData.GetWeapon().Tier);
        player.transform.position = spawnLocation.position;
        cameraFollow.SetFollowPlayer(player.transform);
        onCharacterSpawn.Notify(this,player);
    }
}
