extends Control


var is_paused  = false setget set_is_paused




func _unhandled_input(event):
	if event.is_action_pressed("ui_pause") && Global.playing:
		self.is_paused = !is_paused
		$CenterContainer/VBoxContainer/ButtonResume.grab_focus()


func set_is_paused(value):
	is_paused = value
	get_tree().paused = is_paused
	visible = is_paused



func _on_ButtonResume_pressed():
	self.set_is_paused(false)
	
func _on_ButtonQuit_pressed():
	get_tree().quit()
