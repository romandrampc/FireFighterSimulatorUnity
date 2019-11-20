using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFireWithCapsule : MonoBehaviour
{
    [SerializeField] float damageFire = -20.0f;
    [SerializeField] GameObject nozzle;
    [SerializeField] float radius;
    [SerializeField] float range;

    //public Mesh mesh;

    Vector3 startPoint = Vector3.zero;
    Vector3 endPoint = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        nozzle = this.gameObject.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        startPoint = nozzle.transform.position;
        endPoint = nozzle.transform.position + (nozzle.transform.forward * range);
       
        if (Input.GetMouseButton(0))
        {
            
            Collider[] hitColliders = Physics.OverlapCapsule(startPoint,endPoint,radius);
            foreach (var collider in hitColliders)
            {
               if (collider.CompareTag("Fire"))
                {
                    collider.gameObject.SendMessage("DouseFire", damageFire);
                    Debug.Log("Hit");
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 firstSphere = startPoint + (radius * nozzle.transform.forward);
        Vector3 secondSphere = endPoint - (radius *  nozzle.transform.forward);
        //Debug.Log("start point " + firstSphere);
        //Debug.Log("end Point " + secondSphere);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(firstSphere,radius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(secondSphere, radius);
        //Gizmos.dra (fireExtinguisher.transform.position, 1);
    }

   /* public static void DrawWireCapsule(Vector3 _pos, Quaternion _rot, float _radius, float _height, Color _color = default(Color))
    {
        if (_color != default(Color))
            Handles.color = _color;
        Matrix4x4 angleMatrix = Matrix4x4.TRS(_pos, _rot, Handles.matrix.lossyScale);
        using (new Handles.DrawingScope(angleMatrix))
        {
            var pointOffset = (_height - (_radius * 2)) / 2;

            //draw sideways
            Handles.DrawWireArc(Vector3.up * pointOffset, Vector3.left, Vector3.back, -180, _radius);
            Handles.DrawLine(new Vector3(0, pointOffset, -_radius), new Vector3(0, -pointOffset, -_radius));
            Handles.DrawLine(new Vector3(0, pointOffset, _radius), new Vector3(0, -pointOffset, _radius));
            Handles.DrawWireArc(Vector3.down * pointOffset, Vector3.left, Vector3.back, 180, _radius);
            //draw frontways
            Handles.DrawWireArc(Vector3.up * pointOffset, Vector3.back, Vector3.left, 180, _radius);
            Handles.DrawLine(new Vector3(-_radius, pointOffset, 0), new Vector3(-_radius, -pointOffset, 0));
            Handles.DrawLine(new Vector3(_radius, pointOffset, 0), new Vector3(_radius, -pointOffset, 0));
            Handles.DrawWireArc(Vector3.down * pointOffset, Vector3.back, Vector3.left, -180, _radius);
            //draw center
            Handles.DrawWireDisc(Vector3.up * pointOffset, Vector3.up, _radius);
            Handles.DrawWireDisc(Vector3.down * pointOffset, Vector3.up, _radius);

        }
    }*/
}
