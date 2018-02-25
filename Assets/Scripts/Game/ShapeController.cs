using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeController : MonoBehaviour {

    Rigidbody m_rigidbody;

    ShapeSpawner m_spawner;

    float m_maxForce = 0, m_maxTorque = 0;
    float m_minForce = 0, m_minTorque = 0;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));   
    }

    public void SetSpawner(ShapeSpawner _spawner)
    {
        m_spawner = _spawner;
    }

    public void Explode()
    {
        // Set our positive and negatives
        int x = Random.Range(0, 2) * 2 - 1, y = Random.Range(0, 2) * 2 - 1, z = Random.Range(0, 2) * 2 - 1;

        // Force changes the direction
        Vector3 force = new Vector3(Random.Range(m_minForce, m_maxForce)*x, Random.Range(m_minForce, m_maxForce)*y, Random.Range(m_minForce, m_maxForce)*z);
        m_rigidbody.AddForce(force);

        // Reset the positive and negatives
        x = Random.Range(0, 2) * 2 - 1;
        y = Random.Range(0, 2) * 2 - 1;
        z = Random.Range(0, 2) * 2 - 1;

        // While torque changes the spin/rotation
        Vector3 torque = new Vector3(Random.Range(m_minTorque, m_maxTorque)*x, Random.Range(m_minTorque, m_maxTorque)*y, Random.Range(m_minTorque, m_minTorque)*z);
        m_rigidbody.AddTorque(torque);
    }

    private void FixedUpdate()
    {
        // Destroy this if below the plane.
        // Could use out of bounds of camera view, however this would also destroy those that haven't fallen off and simply in the left and right edges of the screen
        if(transform.position.y < -10)
        {
            KillShape();
        }
    }

    public void ChangeForces(float _force)
    {
        m_maxForce = _force;
        m_maxTorque = _force;

        m_minForce /= 3f;
        m_minTorque /= 3f;

    }

    void KillShape()
    {
        m_spawner.RemoveShape(this);
        Destroy(gameObject);
    }
}
