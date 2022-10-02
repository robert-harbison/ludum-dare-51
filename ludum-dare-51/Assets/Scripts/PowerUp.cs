using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType {
    HEALTH,
    FORCEFIELD,
    AMMO
}

public class PowerUp : MonoBehaviour {

    public PowerUpType type;

	private void Start() {
        if (type == PowerUpType.AMMO) {
            Destroy(gameObject, 45f);
        } else {
            Destroy(gameObject, 10f);
        }
	}
}
