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

/**
*	The main class that holds all persistant data
*/
public class Game {
	
	public static int money; //How much money the player currently has.
	
	//These are the current levels of each tower. 0 means the tower has not yet been unlocked
	public static int popcornLevel = 1;
	public static int hotdogLevel = 0;
	public static int coffeeLevel = 0;
	public static int sodaLevel = 0;
	public static int bubblegumLevel = 0;
	public static int iceCreamLevel = 0;
	
	//Statistics
	public static int shotsFired = 0; //Total time all towers have fired
	public static int enemiesKilled = 0; //Total enemies defeated
	public static int fails = 0; //Fail counter
	
	/**
	*	Resets money and towers. Used when starting a new game
	*/
	public static void reset(){
		money = 0;
		popcornLevel = 1;
		hotdogLevel = 0;
		coffeeLevel = 0;
		sodaLevel = 0;
		bubblegumLevel = 0;
		iceCreamLevel = 0;
	}
}