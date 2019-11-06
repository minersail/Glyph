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

    public GameObject particleSystem;

    // Start is called before the first frame update
    void Start()
    {
		TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/10-stylus-MEDIUM/");
		foreach (TextAsset gestureXml in gesturesXml)
			trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));

        particleSystem.GetComponent<ParticleSystem>().enableEmission = false;
    }

    // Update is called once per frame
    void Update()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 pos = r.GetPoint(10);
        particleSystem.transform.position = pos;

        if (Input.GetMouseButtonDown(0)) {
            strokeNum++;
            particleSystem.GetComponent<ParticleSystem>().enableEmission = true;
        }
        else if (Input.GetMouseButton(0)) {

            points.Add(new Point(Input.mousePosition.x, -Input.mousePosition.y, strokeNum));
        }
        else if (Input.GetMouseButtonUp(0)) {
            particleSystem.GetComponent<ParticleSystem>().enableEmission = false;
        }

        if (Input.GetMouseButtonDown(1)) {
            Point[] _points = points.ToArray();
            Gesture candidate = new Gesture(_points);
            
            Result gestureCLass = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());
            
            Debug.Log(System.String.Join(", ", candidate.Points.Select(x => "(" + x.X + ", " + x.Y + ")")));
            Debug.Log(System.String.Join(", ", trainingSet.Select(x => x.Name)));
            Debug.Log(gestureCLass.GestureClass + " " + gestureCLass.Score);

            strokeNum = 0;
            points.Clear();
        }

        // if (Input.GetMouseButtonDown(0))
        // {
        //     clone = Instantiate(trailHolderPrefab, transform.position, Quaternion.identity) as GameObject;
        //     clone.transform.SetParent(transform);
        //     i++;
        // }
        // if (Input.GetMouseButton(0))
        // {
        //     points.Add(new Point(Input.mousePosition.x, Input.mousePosition.y, i));

        //     Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     Vector3 pos = r.GetPoint(_distance);
        //     transform.position = pos;
        // }
        // if (Input.GetMouseButtonUp(0))
        // {
        //     if(i >= 3)
        //     {
        //         i = 0;
        //         Point[] _points = points.ToArray();
        //         Gesture candidate = new Gesture(_points);
        //         string gestureCLass = PointCloudRecognizer.Classify(candidate, trainingSet);
        //         spellname.text = gestureCLass;
        //     }
        //     clone.transform.parent = null;
        // }
    }
}
