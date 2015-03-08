using UnityEngine;
using System.Collections;

public class KillZone : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D collider){
		if (collider.tag == "Enemy"){
			Enemy enemy = collider.GetComponent<Enemy>();
			Level.instance.happiness -= enemy.health;
			
			if (Level.instance.happiness <= 0){
				//TODO: Lose the game
				Debug.Log("You lost :(");
			}
			
			float refunds = enemy.health * 10;
			Game.money -= (int) refunds;
			Debug.Log("You lost " + refunds + " from refunds!");
			
			foreach (Tower tower in enemy.watchers){
				tower.tracked.Remove(enemy);
				tower.SelectTarget();
			}
			
			Destroy(enemy.gameObject);
		}
	}
}