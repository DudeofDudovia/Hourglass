An App to Help Keep Track of Time

The way this application functions is that it enables you to log how much time has been used, and displays how much is left.
  The app also has a timer, which will keep track of time for you (granted you remember to stop it).


Changelog 0.7.0
	Added a Github button which directs to the Github page.
	Made background change hue better:
		Before, the way it worked meant that blues would be black, now, it is a proper hue shift.
	Logged times now log when they were logged.
		This fun, but confusing sentence simply means that when you click a time log it will show when that entry was made.
	Improved compatibility with landscape screens.
	Did lots of internal cleanup:
		This includes using files rather than PlayerPrefs.
		Removed useless scripts and commented lines.
	Fixed bugs:
		  Pressing enter will add time (or at least attempt to), even when the add box isn't focused.
		  Made "Keep Last Added Times" toggle work.
		  Made "Advanced Formatting" work better.
	Uncapitalized words that didn't need to be.
