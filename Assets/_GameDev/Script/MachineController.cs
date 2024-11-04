using UnityEngine;

public class MachineController : MonoBehaviour
{
    [SerializeField] private MachineSpawnScript machinespawn;
    [SerializeField] private ConveyorScriptV2 conveyor;
    [SerializeField] private SpawnListGameobject spawnListGameobject;

    [SerializeField] private int machineID;
    void Start()
    {
        machinespawn = GetComponent<MachineSpawnScript>();
        conveyor = GetComponentInChildren<ConveyorScriptV2>();
        spawnListGameobject = GetComponent<SpawnListGameobject>();
        GetMachineID();
    }

    public int GetMachineID()
    {
        return machineID;
    }

    public MachineSpawnScript GetMachineSpawnScript()
    {
        return machinespawn;
    }

    public ConveyorScriptV2 GetConveyorScript()
    {
        return conveyor;
    }

    public SpawnListGameobject GetSpawnListScript()
    {
        return spawnListGameobject;
    }


}
