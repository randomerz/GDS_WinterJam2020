using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team {
    Player,
    Enemy,
}

public abstract class Projectile : MonoBehaviour {
    public abstract void initialize(float speed, Vector3 dir, float damage, GameObject target);
    public abstract Team getTeam();
    public abstract void setTeam(Team t);
}
