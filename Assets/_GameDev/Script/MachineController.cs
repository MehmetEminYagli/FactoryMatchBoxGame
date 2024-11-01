using UnityEngine;

public class MachineController : MonoBehaviour
{
    [SerializeField] public MachinePaletScript machinePalet;
    [SerializeField] public MachineSpawnScript machinespawn;

    void Start()
    {
        machinePalet = GetComponent<MachinePaletScript>();
        machinespawn = GetComponent<MachineSpawnScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
