using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SpawnableManager : MonoBehaviour
{
    public ARRaycastManager m_RaycastManager;
    List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();
    public GameObject spawnablePrefab;

    Camera arCam;
    GameObject spawnedObject;
    // Start is called before the first frame update
    void Start()
    {
        spawnedObject = null;
        arCam = GameObject.Find("AR Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
            return;
        
        RaycastHit hit;
        Ray ray = arCam.ScreenPointToRay(Input.GetTouch(0).position);

        if (m_RaycastManager.Raycast(Input.GetTouch(0).position, m_Hits)) {
            if (Input.GetTouch(0).phase == TouchPhase.Began && spawnedObject == null) {
                if (Physics.Raycast(ray, out hit)) {
                    if (hit.collider.gameObject.tag == "Spawnable") {
                        spawnedObject = hit.collider.gameObject;
                    } else {
                        (Vector3 pos, Quaternion rotation) plane = findPlaneWithinThreshold();
                        if (plane.pos != Vector3.zero) {
                            SpawnPrefab(plane.pos, plane.rotation);
                        }
                    }
                }
            } else if (Input.GetTouch(0).phase == TouchPhase.Moved && spawnedObject != null) {
                (Vector3 pos, Quaternion rotation) plane = findPlaneWithinThreshold();

                if (plane.pos != Vector3.zero) {
                    spawnedObject.transform.position = plane.pos;
                    spawnedObject.transform.rotation = plane.rotation;
                }
                
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended) {
                spawnedObject = null;
            }
        }
    }

    private (Vector3, Quaternion) findPlaneWithinThreshold() {
        Vector3 pos = Vector3.zero;
        Quaternion rotation = Quaternion.identity;
        foreach (ARRaycastHit hit in m_Hits) {
            if (hit.distance > 1.0f) {
                Vector3 normal = - hit.pose.up;
                Quaternion r = Quaternion.LookRotation(normal, Vector3.up);
                if (r.y != 0) {
                    pos = hit.pose.position;
                    rotation = r;
                    break;
                }
            }
        }
        return (pos, rotation);
    }

    private void SpawnPrefab(Vector3 spawnPosition, Quaternion spawnRotation) {
        spawnedObject = Instantiate(spawnablePrefab, spawnPosition, spawnRotation);
    }
}
