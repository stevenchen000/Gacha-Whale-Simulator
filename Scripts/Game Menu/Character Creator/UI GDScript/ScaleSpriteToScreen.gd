extends Sprite2D

func _on_texture_changed():
	if texture != null:
		var size = texture.get_size()
		var spriteWidth = size.x
		var screenSize = get_viewport_rect()
		var screenWidth = screenSize.size.x
		var newSize = Vector2(1,1) * screenWidth / spriteWidth * 0.8
	
