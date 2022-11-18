extends CanvasLayer


# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	pass
func setvisibilitytrue():
	$HBoxContainer.visible = true
	$HBoxContainer2.visible = true
func setvisibilityfalse():
	$HBoxContainer.visible = false
	$HBoxContainer2.visible = false
func _process(delta):
	Global.timeleft = $Timer.get_time_left()
	
func set_timer(value):
	Global.timeleft = $Timer.get_time_left() + value
	$Timer.start(Global.timeleft)
func reset_timer(value):
	$Timer.stop()

func _on_Timer_timeout():
	get_tree().change_scene("res://menus/GameOver.tscn")
