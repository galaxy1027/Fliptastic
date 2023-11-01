extends Label

var lives = 3 # Initialize with 3 lives
var hazard = false

func _ready():
	# Any initialization code can go here when the node is first added to the scene
	pass

func _process(delta):
	# Assuming the camera is a child of the root node (or another fixed node)
	var camera_position = get_window().get_camera_2d().get_screen_center_position()
	var camera_offset = get_viewport_rect().size / 2

	# Assuming the size of the lives node is already set and it's in the top-left corner by default
	var lives_position = camera_position - camera_offset

	# Set the position of the Lives node
	set_position(lives_position)
	
	# Code that needs to run every frame can go here
	
	# check for collision and change live count
	if hazard :
		hazard = !hazard
		set_text("lives: " + lives)
	
	# For instance, you might want to check if lives are zero and take action
	if lives <= 0:
		# Handle the case where lives reach zero
		game_over()

func game_over():
	# Game over logic, like reloading the scene or showing a game over screen
	pass
	
