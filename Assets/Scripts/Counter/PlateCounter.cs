// Plate Counter: players can pick up plates from the plate counter
// Plate Counter will periodically spawn plates. It can spawn $plateMax plates at max
// The visual of the game counter shall update as plates are spawned
// but the actual game object is only created at player interaction
// For the sake of simplicity, the prefab of plate object and plate visual are loaded as serializeField
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO plateSO;
    private float spawnPlateTimer;
    private float plateSpawnTime = 1f;

    private int plateCount = 0;
    private readonly int plateMax = 4;
    
    // for plate stacking visual
    [SerializeField] private GameObject plateVisual;
    private const float plateHeight = 0.12f;
    List<GameObject> plateVisualList = new List<GameObject>();
    private void Awake()
    {
        spawnPlateTimer = 0f;
        plateCount = 0;
    }
    
    void Update()
    {
        this.spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer >= plateSpawnTime && plateCount < plateMax)
        {
            plateCount++;
            this.PlateVisualUpdate();
            spawnPlateTimer = 0f;
        }
    }
    
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject() && plateCount > 0)
        {
            KitchenObject.SpawnKitchenObject(plateSO, player);
            plateCount--;
            this.PlateVisualUpdate();
        }
    }

    private void PlateVisualUpdate()
    {
        // Note the script do nothing if plateCount = plateVisualList.Count
        // Add plate if there is not enough
        for (int i = plateVisualList.Count; i < plateCount; ++i)
        {
            GameObject plateGO =
                Instantiate(plateVisual, GetKitchenObjectFollowTransform());
            plateGO.transform.localPosition = new Vector3(0, (plateCount-1)*plateHeight,0);
            plateVisualList.Add(plateGO);
        }
        // remove if there are more.
        for (int i = plateCount; i < plateVisualList.Count; ++i)
        {
            Destroy(plateVisualList[i]);
            plateVisualList.RemoveAt(plateVisualList.Count - 1);
        }
    }
}
