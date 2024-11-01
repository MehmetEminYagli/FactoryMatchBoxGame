using UnityEngine;
using System.Collections;

public class MachinePaletScript : MonoBehaviour
{
    [SerializeField] private GameObject paletObject;
    private Material paletMaterial;
    [SerializeField] private float offsetSpeed;
    private float currentSpeed;
    void Start()
    {
        currentSpeed = offsetSpeed;
        GetPaletMaterial();
        StartCoroutine(PaletAnimation());
    }


    public Material GetPaletMaterial()
    {
        return paletMaterial = paletObject.GetComponentInChildren<MeshRenderer>().material;
    }

    IEnumerator PaletAnimation()
    {
        while (true)
        {
            Vector2 currentOffset = paletMaterial.mainTextureOffset;
            currentOffset.y += offsetSpeed;

            paletMaterial.mainTextureOffset = currentOffset;
            yield return new WaitForEndOfFrame();
        }
    }

    public void StopOfsetSpeed()
    {
        offsetSpeed = 0;
    }
    public void StartOfsetSpeed()
    {
        offsetSpeed = currentSpeed;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
