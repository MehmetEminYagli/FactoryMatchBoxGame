using UnityEngine;

public class MachineController : MonoBehaviour
{
    [SerializeField] private MachineSpawnScript machinespawn;
    [SerializeField] private ConveyorScriptV2 conveyor;
    //[SerializeField] private GameObject spawnListGameobject;

    [SerializeField] private int machineID;


    void Start()
    {
        
        machinespawn = GetComponent<MachineSpawnScript>();
        conveyor = GetComponentInChildren<ConveyorScriptV2>();
        //spawnListGameobject.GetComponent<SpawnListGameobject>();
        GetMachineID();
        GameManager.Instance.levelManager.AddMachineID(GetMachineID());
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

   





}
