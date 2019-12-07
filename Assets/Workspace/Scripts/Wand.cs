using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using PDollarGestureRecognizer;

public class Wand : MonoBehaviour
{
    public GameObject particles;
    public Transform offHand;
    public Transform mainHand;
    public bool VRMode;

    public GameObject fireballPrefab;
    public GameObject lightPrefab;
    public GameObject teleportPrefab;
    public GameObject earthPrefab;
    public GameObject growthPrefab;

    private List<Point> points = new List<Point>();
	private List<Gesture> trainingSet = new List<Gesture>();
    private int strokeNum = 0;

    private ParticleSystem.EmissionModule emission;

    private bool drawing = false;
    private bool cast = false;

    private Vector3 castNormal;

    // Start is called before the first frame update
    void Start()
    {
		TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/10-stylus-MEDIUM/");
		foreach (TextAsset gestureXml in gesturesXml)
			trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));

        emission = particles.GetComponent<ParticleSystem>().emission;
        emission.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (VRMode) {
            particles.transform.position = mainHand.position;
        }
        else {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 pos = r.GetPoint(5);
            particles.transform.position = pos;
        }

        if (Input.GetAxis("Draw") > 0) {
            if (!drawing) {
                if (VRMode && strokeNum == 0) {
                    Vector3 direction = mainHand.position - transform.position;
                    direction.y = 0;
                    float angle = Mathf.Atan2(direction.z, direction.x);
                    castNormal = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
                }
                strokeNum++;
                emission.enabled = true;

                drawing = true;
            }

            if (VRMode) {
                // Imagine you're in one of those mirror houses, surrounded by mirrors.
                // Draw a line from your body, out through your arm, and whatever mirror it hits is the projection plane
                // Wherever that line hits the mirror is the Vector3 "projected" variable
                Vector3 projected = Vector3.ProjectOnPlane(mainHand.position, castNormal);

                // This is the X-distance from that point to the origin.
                // The reason the origin is used is because ProjectOnPlane's plane runs through the origin.
                float distanceXZ = Vector3.Distance(new Vector3(projected.x, transform.position.y, projected.z), Vector3.zero);
                points.Add(new Point(distanceXZ, -projected.y, strokeNum));
            }
            else {
                points.Add(new Point(Input.mousePosition.x, -Input.mousePosition.y, strokeNum));
            }
        }
        else if (Input.GetAxis("Draw") <= 0) {
            emission.enabled = false;
            drawing = false;
        }

        if (Input.GetAxis("Cast") > 0 && !cast) {
            Point[] _points = points.ToArray();
            Gesture candidate = new Gesture(_points);
            Debug.Log(_points.Count());
            
            Result gestureClass = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());
            
            Debug.Log(System.String.Join(", ", candidate.Points.Select(x => "(" + x.X + ", " + x.Y + ")")));
            Debug.Log(System.String.Join(", ", trainingSet.Select(x => x.Name)));
            Debug.Log(gestureClass.GestureClass + " " + gestureClass.Score);

            strokeNum = 0;
            points.Clear();

            if (gestureClass.Score > 0.8) {
                Spawn(gestureClass.GestureClass);
            }

            cast = true;
        }
        else if (Input.GetAxis("Cast") <= 0) {
            cast = false;
        }
    }

    public void Spawn(string item)
    {
        if (item == "fire") {
            Instantiate(fireballPrefab, offHand);
        }
        else if (item == "light") {
            Instantiate(lightPrefab, offHand);
        }
        else if (item == "teleport") {
            Instantiate(teleportPrefab, offHand);
        }
        else if (item == "earth") {
            Instantiate(earthPrefab, offHand);
        }
        else if (item == "growth") {
            Instantiate(growthPrefab, offHand);
        }
    }
}
