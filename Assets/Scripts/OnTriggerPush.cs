using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerPush : MonoBehaviour
{
    public float forceAmount = 100f; // Amount of force to apply
    private Rigidbody rb;
    public GameObject respawnPoint = null;
    public int blueTeamGoals = 0;
    public int redTeamGoals = 0;

    public UnityEvent BlueWin;
    public UnityEvent RedWin;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider hit) {
        Debug.Log("Collisione");
        if (hit.CompareTag("Hit")) {
            Vector3 forceDirection = hit.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();
            rb.AddForceAtPosition(-forceDirection, transform.position, ForceMode.Impulse);
        } else if (hit.CompareTag("GoalBlue") || hit.CompareTag("GoalRed")) {
            this.transform.position = respawnPoint.transform.position;
            this.rb.velocity = Vector3.zero;
            // Detiene la rotación del objeto
            this.rb.angularVelocity = Vector3.zero;
            if (hit.CompareTag("GoalBlue")){
                blueTeamGoals++;
                if (blueTeamGoals == 3) {
                    BlueWin.Invoke();
                }
            } else {
                redTeamGoals++;
                if (redTeamGoals == 3) {
                    RedWin.Invoke();
                }
            }

        }
    }
}
