extends Control

var lives = 3  # Example value for lives

func _ready():
	update_lives_ui()

func update_lives_ui():
	# Assuming you have a Label child node named "LivesLabel"
	if $LivesLabel:
		$LivesLabel.text = "Lives: " + str(lives)
