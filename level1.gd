extends Node2D


# Declare member variables here. Examples:
# var a = 2
# var b = "text"

var Sound = preload("res://asset/Sounds/16-Bit Fantasy RPG OST - Ancient Ruins of Light.mp3")


var playing = false

func _ready():
	Global.playing = true
	Hud.setvisibilitytrue()
	Hud.set_timer(100)
	if Global.Sound:
		if !playing:
			if !$backgroundmusic.is_playing():
				$backgroundmusic.stream = Sound
				$backgroundmusic.play()
		
	
# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
func _process(delta):
		if !Global.Sound:
			$backgroundmusic.stop()
			playing = false
		if Global.Sound:
			if !playing:
				if !$backgroundmusic.is_playing():
					$backgroundmusic.stream = Sound
					$backgroundmusic.play()


	
