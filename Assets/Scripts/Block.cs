using UnityEngine;
using System.Collections.Generic;

public class Block : MonoBehaviour
{
    public List<Transform> neighbouringBlocks = new List<Transform>();

    public LayerMask blockLm;

    public Material highlightedMaterial;
    public Material regularMaterial;
    public MeshRenderer meshRenderer;

    public GameObject dropItem;

    public void GetNeighbouringBlocks()
    {
        Collider[] nearByBlocks = Physics.OverlapSphere(transform.position, 1.5f, blockLm);

        for (int i = 0; i < nearByBlocks.Length; i++)
        {
            neighbouringBlocks.Add(nearByBlocks[i].transform);
        }
    }

    public void HighlightBlock(bool status)
    {
        meshRenderer.material = (status) ? highlightedMaterial : regularMaterial;
    }

    public void DestroyBlock()
    {
        // Reveal the neighbouring blocks:
        for (int i = 0; i < neighbouringBlocks.Count; i++)
        {
            if (neighbouringBlocks[i] != null)
            {
                neighbouringBlocks[i].gameObject.SetActive(true);
            }
        }

        Instantiate(dropItem, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
