/*
*	Copyright (C) 2015 Dylan McCormack
*	
*	This program is free software; you can redistribute it and/or modify
*	it under the terms of the GNU General Public License as published by
*	the Free Software Foundation; either version 2 of the License, or
*	any later version.
*		
*	This program is distributed in the hope that it will be useful,
*	but WITHOUT ANY WARRANTY; without even the implied warranty of
*	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*	GNU General Public License for more details.
*			
*	You should have received a copy of the GNU General Public License along
*	with this program; if not, write to the Free Software Foundation, Inc.,
*	51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
*/
using UnityEngine;
using System.Collections;

public class PopcornGun : Tower {

	public static int[] upgradeCost {get; protected set;}
	public static int currentLevel {get; set;}
	
	static PopcornGun() {
		upgradeCost = new int[]{0, 1000, 10000}; //All values subject to change, and probably will
		currentLevel = 1;
	}
	
	public override void Fire(){
		Bullet bullet = (Bullet) Instantiate(this.bullet, transform.position, Quaternion.identity);
		bullet.shooter = this;
		Vector3 dir = (target.transform.position - transform.position).normalized;
		bullet.direction = dir;
		bullet.damage = baseDamage;
	}
}
