using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Outline : MonoBehaviour
{
    // Start is called before the first frame update
    Material outline;
    Renderer[] renderers;
    List<Material> materialList = new List<Material>();

    void Start()
    {
        renderers = this.GetComponents<Renderer>();
        var childRenderer = this.GetComponentsInChildren<Renderer>();
        if(childRenderer != null)
        {
            if(renderers.Count() > 0)
            {
                renderers.Concat(childRenderer);
            }
            else
            {
                renderers = childRenderer;
            }
        }

        foreach(var renderer in renderers)
        {
            var list = renderer.sharedMaterials.ToList();
            outline = new Material(Shader.Find("Common/Outline"));
            outline.mainTexture = renderer.sharedMaterials[0].mainTexture;
            list[0] = outline;
            renderer.materials = list.ToArray();
        }
        //materialList.Clear();
        //materialList.AddRange(renderers.sharedMaterials);
        //materialList.Add(outline);

        //renderers.materials = materialList.ToArray();
    }
}
