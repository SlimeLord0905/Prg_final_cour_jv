extends KinematicBody2D

export (PackedScene) var hearth: PackedScene= preload("res://player/hearth/TimeBonus.tscn")

export var ACCELERATION = 300
export var MAX_SPEED = 200
export var FRICTION = 200 

enum{
	IDLE,
	WANDER,
	CHASE	
}
var velocity = Vector2.ZERO
var knockback = Vector2.ZERO

onready var stats =$stats
onready var detection_zone = $player_detection
onready var sprite = $corrpse
onready var hurtbox = $hurtbox

var state = IDLE

func _physics_process(delta):
	knockback = knockback.move_toward(Vector2.ZERO, FRICTION * delta )
	knockback = move_and_slide(knockback)
	
	match state:
		IDLE:
			velocity = velocity.move_toward(Vector2.ZERO, FRICTION * delta )
			seek_player()
		WANDER:
			pass
		CHASE:
			var player = detection_zone.player
			if player != null:
				var direction = (player.global_position - global_position).normalized()
				velocity = velocity.move_toward(direction * MAX_SPEED, ACCELERATION * delta )
			if player == null:
				state = IDLE
			sprite.flip_h = velocity.x < 0
	velocity = move_and_slide(velocity)	
		
func seek_player():
	if detection_zone.can_see_player():
		state = CHASE
	
func _on_hurtbox_area_entered(area):
		Global.score += 100
		queue_free()

func _on_stats_no_health():
	var rng = RandomNumberGenerator.new()
	rng.randomize()
	var random_number = rng.randf_range(-10.0, 10.0)
	if random_number > 0:
		var hearth_health = hearth.instance()
		get_tree().current_scene.add_child(hearth_health)
		hearth_health.global_position = self.global_position
		hearth_health.global_position.x -= 10
		hearth_health.global_position.y -= 10
	queue_free()

