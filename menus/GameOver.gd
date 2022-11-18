extends Control


# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	$VBoxContainer/ButtonResume.grab_focus()
	Global.playing = false
	$VBoxContainer/HBoxContainer/Label2.text = str(Global.score + Global.timeleft).substr(0, 5) 
	Hud.setvisibilityfalse()

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
func _process(delta):
	pass

func _on_Button_pressed(): 
	Global.score = 0
	Hud.reset_timer(1)
	get_tree().change_scene("res://level1.tscn")


func _on_ButtonQuit_pressed():
	get_tree().quit()


func _on_ButtonBackToMenue_pressed():
	Global.score = 0
	get_tree().change_scene("res://menus/menu.tscn")
