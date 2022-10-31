extends Control


# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	$Button.grab_focus()


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
func _process(delta):
	$HBoxContainer/Label2.text = str(Global.score)

func _on_Button_pressed():
	get_tree().change_scene("res://menu.tscn")
