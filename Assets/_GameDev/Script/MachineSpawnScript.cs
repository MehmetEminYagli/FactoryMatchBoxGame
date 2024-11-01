using UnityEngine;
using System.Collections;
public class MachineSpawnScript : MonoBehaviour
{
    private MachineController machineController;


    void Start()
    {
        machineController = GetComponent<MachineController>();
        StartCoroutine(stopt());
    }


    public void StopMachinePalet()
    {
        machineController.machinePalet.StopOfsetSpeed();
    }
    public void StartMachinePalet()
    {
        machineController.machinePalet.StartOfsetSpeed();
    }

    IEnumerator stopt()
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
