using UnityEngine;
using System.Collections;
public class MachineSpawnScript : MonoBehaviour
{
    private MachineController machineController;


    void Start()
    {
        machineController = GetComponent<MachineController>();
        StartCoroutine(stop());
    }


    public void StopMachinePalet()
    {
        machineController.GetMachinePaletScript().StopOfsetSpeed();
        machineController.GetConveyorScript().StopMoveObject();
    }
    public void StartMachinePalet()
    {
        machineController.GetMachinePaletScript().StartOfsetSpeed();
        machineController.GetConveyorScript().StartMoveObject();
    }

    IEnumerator stop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("bekliyor");
            StopMachinePalet();
            yield return new WaitForSeconds(1f);
            Debug.Log("calismaya basladi");
            StartMachinePalet();

        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
