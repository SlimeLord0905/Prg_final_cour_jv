extends Area2D




onready var timer = $Timer
var invincible = false setget set_invincible 

signal invincibility_started
signal invincibility_ended

func set_invincible(value):
	invincible = value 
	if invincible == true:
		emit_signal("invincibility_started")
	else :
		emit_signal("invincibility_ended")
		
func start_invincibility(duration):
	self.invincible == true;
	timer.start(duration)

func create_hit_effect():
	var main = get_tree().current_scene


func _on_Timer_timeout():
	self.invincible = false


func _on_hurtbox_invincibility_started():
	set_deferred("monitorable", false)

func _on_hurtbox_invincibility_ended():
	monitorable = true 
