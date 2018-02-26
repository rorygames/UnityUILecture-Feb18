using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeSpawner : MonoBehaviour {

    GameObject m_sphere, m_cube, m_pyramid;

    Transform m_spawnPosition, m_spawnParent;

    List<ShapeController> m_currentShapes = new List<ShapeController>();

    Slider m_slider;

    [SerializeField]
    float m_currentForce = 100f;

    // Shows the private field in the inspector
    [SerializeField]
    float m_forceMultiplier = 5f;

    // Use setter and getter so we don't have other classes changing the value
    // These do not appear in the editor but still allow other classes to see the values
    public int shapeCount { get; private set; }

    // Use awake for finding/setting objects that are either required for this script, or known to be children of the object
    private void Awake()
    {

        // If you put all your prefabs within a folder caled Resources you can then load them without having to assign the value in the editor
        // It uses the path based on the resource path root you create
        // e.g. if you create a folder structure of Resources/Prefabs/Resources/Cube you could load it from both the full path and by simply calling Cube due to the second Resources folder
        m_sphere = Resources.Load<GameObject>("Prefabs/Game/Sphere");
        if (m_sphere == null)
            Debug.Log("Unable to load sphere prefab");

        m_cube = Resources.Load<GameObject>("Prefabs/Game/Cube");
        if (m_cube == null)
            Debug.Log("Unable to load cube prefab");

        m_pyramid = Resources.Load<GameObject>("Prefabs/Game/Pyramid");
        if (m_pyramid == null)
            Debug.Log("Unable to load pyramid prefab");

        m_spawnPosition = transform.Find("SpawnPosition");
        m_spawnParent = transform.Find("SpawnedObjects");

        shapeCount = 0;

        m_currentForce *= m_forceMultiplier;
    }

    // Use start for objects that are not related to or part of the object
	void Start ()
    {
        m_slider = GameObject.Find("Canvas/Slider").GetComponent<Slider>();
	}

    private void Update()
    {
        // By default Jump is the space bar in the Input Manager
        // You can force it to be the space button by using
        // Input.GetKeyUp(KeyCode.Space)
        if (Input.GetButtonUp("Jump"))
        {
            SpawnNewObject();
        }

        // Here we'll use the E key, by default nothing is bound to it
        if (Input.GetKeyUp(KeyCode.E))
        {
            ExplodeShapes();
        }

    }
	
    public void SpawnNewObject()
    {
        GameObject chosen;
        int objectChoice = Random.Range(0, 3);
        switch (objectChoice)
        {
            default:
            case 0:
                chosen = m_sphere;
                break;
            case 1:
                chosen = m_cube;
                break;
            case 2:
                chosen = m_pyramid;
                break;
        }

        GameObject go = Instantiate(chosen, m_spawnParent);
        go.transform.position = m_spawnPosition.position;

        ShapeController sc = go.GetComponent<ShapeController>();
        sc.SetSpawner(this);
        sc.name = chosen.name + shapeCount.ToString();

        sc.ChangeForces(m_currentForce);

        m_currentShapes.Add(sc);

        shapeCount++;
    }

    public void ChangeExplodeForce(float _force)
    {
        m_currentForce = m_slider.value * m_forceMultiplier;

        foreach(ShapeController sc in m_currentShapes)
        {
            sc.ChangeForces(m_currentForce);
        }
    }

    public void ExplodeShapes()
    {
        // We're simply calling a function so we'll just use a foreach
        // If we were editing the item in this script we would use a for loop
        foreach (ShapeController sc in m_currentShapes)
        {
            sc.Explode();
        }
    }

    // Remove the destroyed shape from the list so we don't try to call it during explode shapes
    // If we didn't remove it, we would end up with a MissingReferenceException error
    public void RemoveShape(ShapeController _sc)
    {
        m_currentShapes.Remove(_sc);
    }


}
