extends ScrollContainer


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	var viewportSize = get_viewport().size
	var height = viewportSize.y
	var containerHeight = height - position.y
	size.y = containerHeight


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
