extends Node2D

export (NodePath) var planet = 'Planet'

const wolf = preload('res://Wolf.tscn')

func _ready():
	planet = get_node(planet)
	pass


func _on_Planet_fullmoon_begun():
	var wolf1 = wolf.instance()
	add_child(wolf1)
	wolf1.set_global_position(planet.forest.get_global_position())
	wolf1.set_global_rotation(planet.forest.get_global_rotation())
