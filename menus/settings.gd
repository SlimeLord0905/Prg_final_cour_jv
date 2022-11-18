extends Control


# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	Global.playing = false
	$VBoxContainer/Button.grab_focus()

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass


func _on_CheckBox_toggled(button_pressed):
	if (Global.Sound == true):
		Global.Sound = false
	else:
		Global.Sound = true
	print(Global.Sound)

func _on_Button_pressed():
	get_tree().change_scene("res://menus/menu.tscn")
