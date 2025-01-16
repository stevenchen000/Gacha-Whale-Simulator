extends Control

@export var importerUI : Control

func _ready():
	_hide()
	

func _reveal():
	visible = true
	position = Vector2(0,0)
	
func _hide():
	visible = false
	position = Vector2(99999,99999)
	
func _close_importer_ui():
	importerUI._make_invisible()
