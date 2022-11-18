extends Node2D


# Declare member variables here. Examples:
# var a = 2
# var b = "text"

var Sound = preload("res://asset/Sounds/8-bit-fantasy-adventure-music boss.wav")


var playing = false
# Called when the node enters the scene tree for the first time.
func _ready():
	Hud.setvisibilitytrue()
	Global.playing = true
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

func _on_Timer_timeout():
	get_tree().change_scene("res://menus/GameOver.tscn")
