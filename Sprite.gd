extends Sprite

const rot_speed = -0.1
var rot = 0

func _process(delta):
	set_rotation(rot)
	rot += rot_speed*delta
	pass
