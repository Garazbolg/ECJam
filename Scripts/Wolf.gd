extends KinematicBody2D

const UP = Vector2(0,-1)

var motion = Vector2(0,0)

func _ready():
	set_physics_process(true)
	pass

func _physics_process(delta):
	var pos = get_global_position()
	
	var gravity = (-pos).normalized()
	
	if(is_on_floor()) :
		motion.x = 10000
	
	motion.y += 9800 * delta
	
	#velocity += motion.x*gravity.rotated(-PI/2)*delta + gravity * GRAVITY_FORCE * delta + jump * JUMP_FORCE*(-gravity)
	
	move_and_slide(motion.rotated(get_angle(pos))*delta,pos)
	set_rotation(get_angle(get_global_position()))
	pass

func get_angle(position):
	return UP.angle_to(position)