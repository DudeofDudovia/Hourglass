An App to Help Keep Track of Time

The way this application functions is that it enables you to log how much time has been used, and displays how much is left.
  The app also has a timer, which will keep track of time for you (granted you remember to stop it).

	Changelog 1.0.0 - The Notification Update
	Added Warnings
		They work on both Android and Windows:
			Due to programming limitations, the warnings may arrive out of time by a couple seconds, but should be reasonably accurate.
				Because of this, I would not recommend using warning values in the seconds, as they are often off by several seconds.
		They work with the timer to remind how much longer it has and when it runs out
			There are warning options for low time, warning time, and half time:
				Low time will do that amount of minutes from the end.
				Warning time will do that amount of minutes from the beginning.
				Half time will warn when the timer reaches half of the remaining time.
		They are profile specific:
			The toggle "Warnings", however, is not profile specific, and disabling it will disable all warnings.
	Added margins to the changelog font size so it displays properly at small fonts.

	Fixed Bugs:
		Deleting profiles doesn't work.
		0 minutes left at half time?
		Windows seizes up when the timer starts.
		Timer would (in theory send a notification even if they were disabled).
	Added A Debug Console:
		This is used for debugging and the toggle for it is at the bottom of the options menu, though in normal use it should be empty (unless something goes wrong or I forgot to remove a logger).
		This doesn't exist on Windows due to excessive lag.
		The output will be saved in the Download directory on Android, and, despite being disabled, to the following path on Windows:
			C:\Users\[USER]\AppData\LocalLow\DudeofDudovia\Hourglass\HourglassLogs\DerbossLog.txt.
	Skipped 0.7.1 lol.
	-1.0.0-rc8
		Removed debug log messages used for markers.
	-1.0.0-rc7
		Moved from -x to -rc because I fixed all bugs that I remember.
	-1.0.0-x8
		I was using an older vsion of the code which is why nothing was wokring.
	-1.0.0-x7
		Further testing.
	-1.0.0-x6
		Tried to find more info into the issue.
	-1.0.0-x5
        Warnings don't get canceled.
	-1.0.0-x4
		2nd test of fixing the errors.
	-1.0.0-x3
		First test of fixing the errors relating to permission to save the logs on Android to the Download directory.
	-1.0.0-x2
		Did some more slight changes to get the app to respond to notiifcation settings changes better on Android.
		Forgot to update the changelog for -x1.
	-1.0.0-x1
		Made changes to the way notifications are handled when permission is denied on Android.
	-1.0.0-rc6
		Removed code to make "-rc" releases act as debug builds, and therefore all previous "-rc" builds can be seen as "-x" builds going forward.
		Hopefully this is the last build before release.
	-1.0.0-rc5
		Fixed Bugs:
			Warnings don't get canceled.
	-1.0.0-rc4
		Made it so that all Windows notifications don't have the id "sad".
		Added Some safety for edge cases in the warning scripts.
                Fixed numerous typos
                Fixed bugs:
		Reset App doesn't work on Windows.
			NOOOoOOOoooOoOo!
			it actually seems that this was due to some Android code being run.
	-1.0.0-rc3
		Apparently I forget to update the version number now *sigh*.
	-1.0.0-rc2
		Made the last update do something(I forgot to update the changelog again).
		Added some internal code to automatically update the changelog for me so that I can stop writing these stupid messages.
	-1.0.0-rc1
		Decided to make this the initial Release
		Fixed Changelog for 0.8.0-rc6
	-0.8.0-rc6
		Fixed a typo where the time and then minutes remaining would be together without a space on certain notifications.
		Made timer notifications be canceled when the timer isn't running.
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
			It caused the app not to function and wasn't necessary
				It is ironic that PC doesn't get the console however.
		Fixed Bugs:
			Windows seizes up when the timer starts.
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
	- Changelog 0.7.1
	Made improvement to the file system:
		Made the new file system create directories more properly.
		It should now be more effecient and less prone to bugs (though it caused a TONNE in the process).
			This should work better on Unix based operating systems(Including Android), the improvements on Windows are negligible.
		This, however, means that old versions are not compatible.
	Changed some logic so that it is independent of framerate.
	Fixed bugs:
		Timer = 9h 11m for some reason (, well, the log says 9h but it doesn't add that much).
		4m is "INVALID" and = 9h; (, well, the log says 9h but it adds the right amount):
			It seems anything having to do with advance formating is broken like this.
		1 = 59s (UGH).
		The first log added always goes to the time when it was created rather than the time it was logged.
		Reset app doesn't properly reset.
		Rainbow logs don't work.
		On Android, it will sometimes display the logged time as 0(seems fixed) or a number that isn't quite right(6:20 ish as opposed to 8 20ish, 8 19 instead of 8 22).
			This was because it wouldn't read a new number, but the old number.
		The low res background looks really bad on a 1080p screen.
		Fixed a bug where the update indicator was not centered on the hamburger menu.
		The app doesn't export properly:
			The app doesn't work when no prior save data is given.
			The background isn't even there:
				further research says this is due to lack of saturation and value.
			Nothing involving saving is working.
			Fixed numerous bugs on Android involving it looking for files with slashes in the name instead of directories.
	-rc 9
		Fixed changelog for -rc 8.
	-rc 8
		Can't figure out why some logs get set to timestamp 0.
			Because remaining time uses the same address as the logged time.
		Did the same thing and forgot to update the changelog for -x1
		Previous time logs won't be able to propperly display when they were logged(They all display the current time), but no one uses this app anyway so...
	-x1
		I forgot to make the millisecond toggle do anything for the displays at the top.
		Forgot to update the changelog for -rc 7.
		Not a -rc because there are known bugs
	-rc 7
		The timer wouldn't change the time but it would create a log.
