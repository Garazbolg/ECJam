extends KinematicBody2D
# Constants
const UP = Vector2(0,-1)

# Variables
var motion = Vector2(0,0)
var direction = 0
var velocity = Vector2(0,0)

# Inputs
export (float) var MAX_SPEED = 10000
export (float) var GRAVITY_FORCE = 9800
export (float) var JUMP_FORCE = 6000
export (NodePath) var planet;

func _ready():
	planet = get_node(planet)

func _physics_process(delta):
	var pos = get_player_relative_position()
	
	var gravity = (-pos).normalized()
	
	if(is_on_floor()) :
		#motion.y = 0
		if Input.is_action_pressed("ui_right"):
			direction = 1
			motion.x = direction * MAX_SPEED
		elif Input.is_action_pressed("ui_left"):
			direction = -1
			motion.x = direction * MAX_SPEED
		else:#elif(abs(motion.x) > 0 && abs(motion.x) < 20):
			motion.x = 0
		#elif(abs(motion.x)>0) :
		#	motion.x += MAX_SPEED*delta*direction*-1.5

	if Input.is_action_just_pressed("ui_up") && is_on_floor() :
		motion.y = -JUMP_FORCE
	
	motion.y += GRAVITY_FORCE * delta
	
	#velocity += motion.x*gravity.rotated(-PI/2)*delta + gravity * GRAVITY_FORCE * delta + jump * JUMP_FORCE*(-gravity)
	
	move_and_slide(motion.rotated(get_angle(pos))*delta,pos)
	set_rotation(get_angle(get_player_relative_position()))
	pass
	
func get_player_relative_position() :
	return get_global_position() - planet.get_global_position()
	
func get_angle(position):
	return UP.angle_to(position)
