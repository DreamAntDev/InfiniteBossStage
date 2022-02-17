using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Detector : MonoBehaviour
{
    public List<Collider> GetCurrentList()
    {
        var hits = Physics.OverlapBox(this.transform.position + this.transform.forward, new Vector3(1.0f, 1.0f, 1.0f));
        var selectList = hits.Where((o) => o.tag.Equals("Boss")).ToList();
        return selectList;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + transform.forward, new Vector3(1.0f, 1.0f, 1.0f));
    }
}
