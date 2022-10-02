using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType {
    HEALTH,
    FORCEFIELD
}

public class PowerUp : MonoBehaviour {

    public PowerUpType type;

	private void Start() {
		Destroy(gameObject, 10f);
	}
}
