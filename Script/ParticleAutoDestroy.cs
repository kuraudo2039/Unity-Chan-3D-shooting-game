using UnityEngine;
using System.Collections;

public class ParticleAutoDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //爆発エフェクトの自動消滅
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        Destroy(gameObject, particleSystem.duration);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
