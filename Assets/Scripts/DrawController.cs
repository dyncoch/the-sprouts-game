using UnityEngine;

public class DrawController : MonoBehaviour {

    [SerializeField]
    private GameObject drawPrefab;

    private GameObject theLine;

    private Plane plane;
    private Vector3 startPos;

    public Vector3 StartPos { get => startPos; set => startPos = value; }
    public Vector3 FinishPos { get; set; }
    public GameObject[] AllLines { get; set; }

    public DrawController(GameObject[] allLines)
    {
        AllLines = allLines;
    }

    // Start is called before the first frame update
    void Start() {
        plane = new Plane(Camera.main.transform.forward * -1, transform.position);

    }

    // Update is called once per frame
    void Update() {
        // Get mouse position.
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Check if screen was touched. 
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0)) {
            theLine = Instantiate(drawPrefab, transform.position, Quaternion.identity);

            if (plane.Raycast(mouseRay, out float _dis)) {
                startPos = mouseRay.GetPoint(_dis);
            }
        } else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetMouseButton(0)) {

            if (plane.Raycast(mouseRay, out float _dis)) {
                theLine.transform.position = mouseRay.GetPoint(_dis);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("coooldslad");
    }

    private bool CheckIfLinesIntersect(LineRenderer line1, LineRenderer line2) {
        AllLines = GameObject.FindGameObjectsWithTag("Line");

        Vector3[] line1AllPoints = new Vector3[line1.positionCount];
        Vector3[] line2AllPoints = new Vector3[line2.positionCount];

        line1.GetPositions(line1AllPoints);
        line2.GetPositions(line2AllPoints);

        foreach (var pointLine1 in line1AllPoints) {
            foreach(var pointLine2 in line2AllPoints) {
                if (pointLine1.Equals(pointLine2)) {
                    return true;
                }
            }
        }

        return false;
    }

}
