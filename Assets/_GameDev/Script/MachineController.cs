using UnityEngine;

public class MachineController : MonoBehaviour
{
    [SerializeField] private MachinePaletScript machinePalet;
    [SerializeField] private MachineSpawnScript machinespawn;
    [SerializeField] private ConveyorScriptV2 conveyor;

    void Start()
    {
        machinePalet = GetComponent<MachinePaletScript>();
        machinespawn = GetComponent<MachineSpawnScript>();
        conveyor = GetComponentInChildren<ConveyorScriptV2>();
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




    // Update is called once per frame
    void Update()
    {
        
    }
}
