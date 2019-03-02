extends Node2D

signal fullmoon_begun

export (int) var fullmoon_frame
export(NodePath) var moon = 'Sky/moon'

export(NodePath) var house = 'house'
export(NodePath) var forest = 'forest'

func _ready():
	house = get_node(house)
	forest = get_node(forest)
	moon = get_node(moon)
	moon.connect('frame_changed', self, '_on_moon_frame_changed')



func _on_moon_frame_changed():
	if moon.frame == fullmoon_frame:
		emit_signal('fullmoon_begun')
