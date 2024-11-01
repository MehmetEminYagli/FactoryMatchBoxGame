using UnityEngine;

public class MachineController : MonoBehaviour
{
    [SerializeField] private MachinePaletScript machinePalet;
    [SerializeField] private MachineSpawnScript machinespawn;
    [SerializeField] private ConveyorScript conveyor;

    void Start()
    {
        machinePalet = GetComponent<MachinePaletScript>();
        machinespawn = GetComponent<MachineSpawnScript>();
        conveyor = GetComponentInChildren<ConveyorScript>();
    }


    public MachinePaletScript GetMachinePaletScript()
    {
        return machinePalet;
    }

    public MachineSpawnScript GetMachineSpawnScript()
    {
        return machinespawn;
    }

    public ConveyorScript GetConveyorScript()
    {
        return conveyor;
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
