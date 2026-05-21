# Hourglass

Changelog 0.8.0 - The Notification Update
	Added margins to the changelog font size so it displays propperly at small fonts.
	Added A Debug Console:
		This is used for debugging and the toggle for it is at the bottom of the options menu, though in normal use it should be empty (unless something goes wrong or I forgot to remove a logger).
	Skipped 0.7.1 lol.
	-0.8.0-rc3
		Removed excessive log spam.
	-0.8.0-rc2
		Removed the console on windows
			It caused the app not to function and wasn't neccesary
				It is ironic that PC doesn't get the console however.
		Fixed Bugs:
			Windows siezes up when the timer starts.
			Timer would (in theory send a notification even if they were disabled).
	-0.8.0-rc1
		Added Warnings
			They work on both Android and Windows.
			They work with the timer to remind how much longer it has and when it runs out
				There are warning options for low time, warning time, and half time:
					Low time will do that amount of minutes from the end.
					Warning time will do that amount of minutes from the beginning.
					Half time will warn when the timer reaches half of the remaining time.
		Changed Margins(Moved) for Version Indicator
			Old coords were 72 Left, -14 Top, -25, 10
				0.8.0-rc1 doesn't fit on the screen with these coords.
	Bugs:
		Windows siezes up when the timer starts.
		Timer would (in theory send a notification even if they were disabled)
	Fixed Bugs:
		Deleting profiles doesn't work.
