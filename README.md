# HW-1-Automower

## Aufgabe

Erstellen Sie einen Algorithmus für einen automatischen Rasenmäher. Dieser soll eine beliebige Rasenfläche so befahren, dass es zu einem möglichst gleichmäßigen Schnitt kommt. D.h. jeder Teil des Rasens wird gleichmäßig oft befahren.  
Testen Sie den Algorithmus in einer Simulation mit einstellbaren Parametern, die die Gleichmäßigkeit des Schnitts zeigt.


Parameter 

- Dimensionen Grundstück  
  - Ecke
  - Mitte
- Schnittbreite
- Geschwindigkeit

Winkel Drehung nicht 50/50


Wir beachten die Ladezeit und so weiter nicht


How to calculate:

Version 1:
	use a draw algorithm to figure out where the mower is moving through  
	in this case look at the image "Calculation.png"  
	The calculation works this way:  
	transform the trajectory to be represented with this formular: a * x + b = y  or x = (y - b) / a
	then there is an evaluation in what direction the mower is going (Q1 - Q4)  
	then evaluate if the mower is moving more flat, more steep or exactly diagonal  
		now you can check where the line is intersecting with the border lines (care for half values) 
		* if the line is steep we walk down every y value to the wall and look at what 2 tiles are connected
		* if the line is flat we walk down every x value to the wall and look at what 2 tiles are beeing connected
		
This framework is used to display the entire thing
https://github.com/MCR2601/ConsoleRenderingFramework
		
		
		
		

