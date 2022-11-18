extends Node

var score = 0
var playerposition = Vector2(0,0)
var Sound = true
var timeleft = 0
var playing = false
# Declare member variables here. Examples:
# var a = 2
# var b = "text"

func set_timer(value):
	Hud.set_timer(value)
# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
