using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using PDollarGestureRecognizer;

public class Wand : MonoBehaviour
{
    private List<Point> points = new List<Point>();
	private List<Gesture> trainingSet = new List<Gesture>();
    private int strokeNum = 0;

    public GameObject particles;
    private ParticleSystem.EmissionModule emission;

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
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 pos = r.GetPoint(5);
        particles.transform.position = pos;

        if (Input.GetMouseButtonDown(0)) {
            strokeNum++;
            emission.enabled = true;
        }
        else if (Input.GetMouseButton(0)) {

            points.Add(new Point(Input.mousePosition.x, -Input.mousePosition.y, strokeNum));
        }
        else if (Input.GetMouseButtonUp(0)) {
            emission.enabled = false;
        }

        if (Input.GetMouseButtonDown(1)) {
            Point[] _points = points.ToArray();
            Gesture candidate = new Gesture(_points);
            
            Result gestureClass = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());
            
            Debug.Log(System.String.Join(", ", candidate.Points.Select(x => "(" + x.X + ", " + x.Y + ")")));
            Debug.Log(System.String.Join(", ", trainingSet.Select(x => x.Name)));
            Debug.Log(gestureClass.GestureClass + " " + gestureClass.Score);

            strokeNum = 0;
            points.Clear();

            if (gestureClass.GestureClass == "fire" && gestureClass.Score > .8) {
                gameObject.GetComponent<Player>().SpawnFireball();
            }
            else if (gestureClass.GestureClass == "light" && gestureClass.Score > .8) {
                gameObject.GetComponent<Player>().SpawnLight();
            }
        }
    }
}
