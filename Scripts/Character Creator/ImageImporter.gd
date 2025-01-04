extends Control


#@export var textureRect : TextureRect
@export var sprite : Sprite2D
@export var secondSprite : Sprite2D
var plugin

func _ready():
	print("ready")
	if Engine.has_singleton("GodotGetImage"):
		print("Image getter found")
		plugin = Engine.get_singleton("GodotGetImage")
		set_options()
		plugin.connect("image_request_completed", _set_image)
	
func _import_image():
	if plugin != null:
		print("importing single image")
		plugin.getGalleryImage()
	else:
		print("Missing singleton")

func _import_multiple_images():
	if plugin != null:
		print("importing multiple images")
		plugin.getGalleryImages()
	else:
		print("Missing singleton")
	

func _set_image(dict):
	print("Image found!")
	for image in dict.values():
		var currImage = Image.new()
		currImage.load_png_from_buffer(image)
		
		var texture = ImageTexture.create_from_image(currImage)
		#sprite.texture = texture
		#secondSprite.texture = texture
		create_sprite(texture)

func create_sprite(texture):
		var newSprite = Sprite2D.new()
		newSprite.texture = texture
		newSprite.position = Vector2(99999,99999)
		add_child(newSprite)

#sets the options for importing images
func set_options():
	var options = {
		"auto_rotate_image" : true,
		"image_format" : "png",
		"keep_aspect" : true
	}
	plugin.setOptions(options)
	
	
#Visibility changed functions
func _on_visibility_changed():
	if(visible):
		print("visible")
	else:
		print("not visible")

func _make_visible():
	visible = true
	position = Vector2(0,0)
	
func _make_invisible():
	visible = false
	position = Vector2(99999,99999)
