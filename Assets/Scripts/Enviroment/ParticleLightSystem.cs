using System.Collections.Generic;
using UnityEngine;

public class ParticleLightSystem : MonoBehaviour
{
    public List<ParticleSystem> particleSystems;
    public GameObject lightPrefab;

    private Dictionary<ParticleSystem, ParticleSystem.Particle[]> particleBuffers = new Dictionary<ParticleSystem, ParticleSystem.Particle[]>();
    private List<GameObject> activeLights = new List<GameObject>();
    private Queue<GameObject> pooledLights = new Queue<GameObject>();

    void Start()
    {
        foreach (var ps in particleSystems)
        {
            if (ps != null)
                particleBuffers[ps] = new ParticleSystem.Particle[ps.main.maxParticles];
        }
    }

    void Update()
    {
        foreach (var lightObj in activeLights)
        {
            lightObj.SetActive(false);
            pooledLights.Enqueue(lightObj);
        }
        activeLights.Clear();

        foreach (var ps in particleSystems)
        {
            if (ps == null) continue;

            var particles = particleBuffers[ps];
            int aliveCount = ps.GetParticles(particles);

            Debug.Log($"Sistema {ps.name} tiene {aliveCount} partículas activas.");

            for (int i = 0; i < aliveCount; i++)
            {
                GameObject lightObj;

                if (pooledLights.Count > 0)
                    lightObj = pooledLights.Dequeue();
                else
                    lightObj = Instantiate(lightPrefab);

                lightObj.SetActive(true);

                Vector3 lightPos = particles[i].position;
                lightPos.z = 1f;  // Ajusta este valor para que quede detrás de la partícula

                lightObj.transform.position = lightPos;

                Debug.DrawLine(lightObj.transform.position, lightObj.transform.position + Vector3.up * 0.5f, Color.red, 0.1f);

                activeLights.Add(lightObj);
            }
        }
    }
}
