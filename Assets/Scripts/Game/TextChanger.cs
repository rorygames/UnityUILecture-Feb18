using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChanger : MonoBehaviour {

    Text m_text;

    ShapeSpawner m_spawner;

    void Awake()
    {
        m_text = GetComponent<Text>();
    }

	// Use this for initialization
	void Start () {
        m_spawner = GameObject.Find("LogicObjects").GetComponent<ShapeSpawner>();
	}
	
	// Update is called once per frame
	void Update () {
        m_text.text = m_spawner.shapeCount.ToString();
	}
}
