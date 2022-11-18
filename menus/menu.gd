extends Control


# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	Global.playing = false
	$VBoxContainer/startButton.grab_focus()
	Hud.setvisibilityfalse()


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass


func _on_startButton_button_down():
	Hud.setvisibilitytrue()
	get_tree().change_scene("res://level1.tscn")

func _on_quitbutton_button_down():
		get_tree().quit()


func _on_optionbutton_button_down():
	get_tree().change_scene("res://menus/settings.tscn")
