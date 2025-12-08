using UnityEngine;
using UnityEngine.Events;

public class ChangeEmissive : MonoBehaviour
{
    //public GameObject textMesh;
    public MeshRenderer screenRenderer;
    public Material emissiveMaterial;
    public Material normalMaterial;

    //public Material normalMaterial;
   // private bool isOn = false;
    

    private void Start()
    {
        //       TurnOffComputer();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Material[] materials = screenRenderer.materials; //our copy 
            materials[0] =  emissiveMaterial;
            screenRenderer.materials = materials; //change materials in gameobject using our altered copy 
            
           // TurnOnComputer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Material[] materials = screenRenderer.materials;
            materials[0] = normalMaterial; 
            screenRenderer.materials = materials; 
        }
    }
    
    
    //To change an emissive material, we copy the materials array, change it, and set it again 

    /*private void TurnOnComputer()
    {
        if (screenRenderer != null && emissiveMaterial != null)
        {
        
            Material[] mats = screenRenderer.materials; //Get copy of the materials list array 
            mats[1] = emissiveMaterial; //Desired emissive material (glowing screen) 
            screenRenderer.materials = mats;     // Override the original reference with our changed copy 
        }

        if (textMesh != null)
            textMesh.SetActive(true);

        isOn = true;
    }

    private void TurnOffComputer()
    {
        if (screenRenderer != null && normalMaterial != null)
        {
            Material[] mats = screenRenderer.materials; //Get copy of the materials list array 
            mats[1] = normalMaterial; //Desired material (screen off) 
            screenRenderer.materials = mats;  // Override the original reference with our changed copy 
        }

        if (textMesh != null)
            textMesh.SetActive(false);

        isOn = false;
    }
*/
    private void ChangeEmissiveTo(Color color)
    {
        emissiveMaterial.SetColor("_EmissionColor", color);
    }
    
}