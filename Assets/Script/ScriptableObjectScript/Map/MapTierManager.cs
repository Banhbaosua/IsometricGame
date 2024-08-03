using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTierManager : MonoBehaviour
{
    [SerializeField] List<MapTier> tierList;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < tierList.Count; i++)
        {
            if (tierList[i].Data.IsCompleted)
            {
                if (!tierList[i + 1].Data.IsUnlocked)
                {
                    tierList[i + 1].Unlock();
                    break;
                }
            }
        }
    }

}
