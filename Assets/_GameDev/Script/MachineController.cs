using UnityEngine;

public class MachineController : MonoBehaviour
{
    [SerializeField] private MachinePaletScript machinePalet;
    [SerializeField] private MachineSpawnScript machinespawn;
    [SerializeField] private ConveyorScriptV2 conveyor;
    [SerializeField] private SpawnListGameobject spawnListGameobject;


    void Start()
    {
        machinePalet = GetComponent<MachinePaletScript>();
        machinespawn = GetComponent<MachineSpawnScript>();
        conveyor = GetComponentInChildren<ConveyorScriptV2>();
        spawnListGameobject = GetComponent<SpawnListGameobject>();
    }


    public MachinePaletScript GetMachinePaletScript()
    {
        return machinePalet;
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




    // Update is called once per frame
    void Update()
    {
        
    }
}
