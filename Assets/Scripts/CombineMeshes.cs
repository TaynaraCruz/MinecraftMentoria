using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Copy meshes from children into the parent's Mesh.
// CombineInstance stores the list of meshes.  These are combined
// and assigned to the attached Mesh.

public class CombineMeshes : MonoBehaviour
{
    Vector3 currentPosition;
    public void Combine()
    { 
        currentPosition = gameObject.transform.position;
        gameObject.transform.position = Vector3.zero;

        ArrayList materials = new ArrayList();
        ArrayList combineInstanceArrays = new ArrayList();
        MeshFilter[] meshFilters = gameObject.GetComponentsInChildren<MeshFilter>();

        foreach (MeshFilter meshFilter in meshFilters)
        {
            MeshRenderer meshRenderer = meshFilter.GetComponent<MeshRenderer>();

            if (!meshRenderer ||
                !meshFilter.sharedMesh ||
                meshRenderer.sharedMaterials.Length != meshFilter.sharedMesh.subMeshCount)
            {
                continue;
            }

            for (int s = 0; s < meshFilter.sharedMesh.subMeshCount; s++)
            {
                int materialArrayIndex = Contains(materials, meshRenderer.sharedMaterials[s].name);
                if (materialArrayIndex == -1)
                {
                    materials.Add(meshRenderer.sharedMaterials[s]);
                    materialArrayIndex = materials.Count - 1;
                }
                combineInstanceArrays.Add(new ArrayList());

                CombineInstance combineInstance = new CombineInstance();
                combineInstance.transform = meshRenderer.transform.localToWorldMatrix;
                combineInstance.subMeshIndex = s;
                combineInstance.mesh = meshFilter.sharedMesh;
                meshRenderer.transform.gameObject.SetActive(false);
                (combineInstanceArrays[materialArrayIndex] as ArrayList).Add(combineInstance);
            }
        }

        // Get mesh filter & renderer
        MeshFilter meshFilterCombine = gameObject.AddComponent<MeshFilter>();
        
        MeshRenderer meshRendererCombine = gameObject.AddComponent<MeshRenderer>();
       
        // Combine by material index into per-material meshes
        // also, Create CombineInstance array for next step
        Mesh[] meshes = new Mesh[materials.Count];
        CombineInstance[] combineInstances = new CombineInstance[materials.Count];

        for (int m = 0; m < materials.Count; m++)
        {
            CombineInstance[] combineInstanceArray = (combineInstanceArrays[m] as ArrayList).ToArray(typeof(CombineInstance)) as CombineInstance[];
            meshes[m] = new Mesh();
            meshes[m].CombineMeshes(combineInstanceArray, true, true);

            combineInstances[m] = new CombineInstance();
            combineInstances[m].mesh = meshes[m];
            combineInstances[m].subMeshIndex = 0;
        }

        // Combine into one
        meshFilterCombine.sharedMesh = new Mesh();
        meshFilterCombine.sharedMesh.CombineMeshes(combineInstances, false, false);

       

        // Assign materials
        Material[] materialsArray = materials.ToArray(typeof(Material)) as Material[];
        meshRendererCombine.materials = materialsArray;

        transform.gameObject.SetActive(true);
        gameObject.transform.position = currentPosition;

    }

    public void Detach()
    {
        Debug.Log("destroy!");
        MeshFilter meshFilter = transform.GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = transform.GetComponent<MeshRenderer>();
        Destroy(meshRenderer);
        Destroy(meshFilter);
       
        for(int i = 0; i < transform.childCount; ++i) {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        
        // foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        // {
        //     child.gameObject.SetActive(true);
        // }
    }

    private int Contains(ArrayList searchList, string searchName)
    {
        for (int i = 0; i < searchList.Count; i++)
        {
            if (((Material)searchList[i]).name == searchName)
            {
                return i;
            }
        }
        return -1;
    }

}
