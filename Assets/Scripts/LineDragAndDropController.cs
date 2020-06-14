using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LineDragAndDropController : MonoBehaviour
{

    private GameObject lineObj;
    private GameObject[] allObjLines;
    private LineRenderer lineRnd;
   
    private Vector3 mousePos;
    private int currLines = 0;

    [SerializeField]
    private GameObject linePrefab;

    private Plane plane;



    // Start is called before the first frame update
    void Start()
    {
        plane = new Plane(Camera.main.transform.forward * -1, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        // Get mouse position.
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0)) {
            if (lineObj == null) {
                lineObj = Instantiate(linePrefab, transform.position, Quaternion.identity);
                lineRnd = lineObj.GetComponent<LineRenderer>();
            }

            if (plane.Raycast(mouseRay, out float _dis)) {
                mousePos = mouseRay.GetPoint(_dis);
            }

            lineRnd.SetPosition(0, mousePos);
            lineRnd.SetPosition(1, mousePos);

        } else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0)) && lineObj) {

            /*
            if (CheckIfLinesIntersect(lineObj)) {
                Debug.LogError("Lines Intersect");
                Destroy(lineObj);
            } else {
                
            }*/

            if (plane.Raycast(mouseRay, out float _dis)) {
                mousePos = mouseRay.GetPoint(_dis);
            }

            lineRnd.SetPosition(1, mousePos);

            lineObj = null;
            
        } else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetMouseButton(0)) && lineObj) {
           
                if (plane.Raycast(mouseRay, out float _dis)) {
                    mousePos = mouseRay.GetPoint(_dis);
                }

                lineRnd.SetPosition(1, mousePos); 

            
        }
    }

    private bool CheckIfLinesIntersect(GameObject newLineObj) {

        allObjLines = GameObject.FindGameObjectsWithTag("Line");

        return true;
    }

    /*

    private bool CheckIfLinesIntersect(GameObject newLineObj) {
       
        allObjLines = GameObject.FindGameObjectsWithTag("Line");

        if (allObjLines != null) {
            LineRenderer newLine = newLineObj.GetComponent<LineRenderer>();
            Vector3[] newLineAllPoints = new Vector3[newLine.positionCount];
            newLine.GetPositions(newLineAllPoints);

            foreach (var oldObjLine in allObjLines) {
                if (!newLineObj.Equals(oldObjLine)) {
                    LineRenderer allOldLines = oldObjLine.GetComponent<LineRenderer>();
                    Vector3[] oldLineAllPoints = new Vector3[allOldLines.positionCount];
                    Debug.Log("Position Count: " + allOldLines.positionCount);
                    allOldLines.GetPositions(oldLineAllPoints);

                    foreach (var pointLine1 in newLineAllPoints) {
                        foreach (var pointLine2 in oldLineAllPoints) {
                            if (pointLine1.Equals(pointLine2)) {
                                Debug.Log(pointLine1 + " " + pointLine2);
                                return true;
                            }
                        }
                    }
                }
            }
        }

        return false;
    }
    */

}
