extends Camera2D

#var position_target = Vector2()
var player

func _ready():
	player = get_node("../Player")
	pass

func _process(delta):
	set_position(player.get_global_position())
	set_rotation(player.get_rotation())
	pass
