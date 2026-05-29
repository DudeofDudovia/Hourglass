	Added Warnings
		They work on both Android and Windows:
			Due to programming limitations, the warnings may arrive out of time by a couple seconds, but should be reasonably acurate.
				Because of this, I would not recomend using warning values in the seconds, as they are often off by several seconds.
		They work with the timer to remind how much longer it has and when it runs out
			There are warning options for low time, warning time, and half time:
				Low time will do that amount of minutes from the end.
				Warning time will do that amount of minutes from the beginning.
				Half time will warn when the timer reaches half of the remaining time.
		They are profile specific:
			The toggle "Warnings", however, is not profile specific, and disabling it will disable all warnings.
	Added margins to the changelog font size so it displays propperly at small fonts.

	Fixed Bugs:
		Deleting profiles doesn't work.
		0 minutes left at half time?
		Windows siezes up when the timer starts.
		Timer would (in theory send a notification even if they were disabled).
	Added A Debug Console:
		This is used for debugging and the toggle for it is at the bottom of the options menu, though in normal use it should be empty (unless something goes wrong or I forgot to remove a logger).
		This doesn't exist on Windows due to excessive lag.
	Skipped 0.7.1 lol.
	-1.0.0-rc1
		Decided to make this the initial Release
		Fixed Changelog for 0.8.0-rc6
	-0.8.0-rc6
		Fixed a typo where the time and then minutes remaining would be together without a space on certain notifications.
		Made timer notifications be cancled when the timer isn't runing.
		Removed typos from notifications.
		Changed several times in the notifications to round down instead of up.
			Generally time used is rounded up and time remaining is rounded down.
		Logging 0 seconds(for the purposes of dividing I guess) is now supported.
		Changed in app renderings of "notifications" to "warnings"
	-0.8.0-rc5
		Made notification timing settings profile specific.
		Fixed bugs:
			Updated the changelog.
	-0.8.0-rc4
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
