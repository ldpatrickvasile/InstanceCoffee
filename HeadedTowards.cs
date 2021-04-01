using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadedTowards : MonoBehaviour
{
    // Start is called before the first frame update

    protected float debugGizmo = 1.0f;

    public virtual void OnDrawGizmos() {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, debugGizmo);
    }

}
