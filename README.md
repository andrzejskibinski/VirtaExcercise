# VirtaExcercise

The implementation here, in priniciple, does the same thing as your initial implementation.
Few things that I've done differently tho:
- added a base class for all Virta elements, that is used for sorting (Virta vs other scripts) and gives category property to the scripts
- added ability to filter out / hide components based on input field text or categories
- used Linq for sorting and iterating through the component collections
- few small useablity improvements for the artists (only warning when not in prefab mode)

You asked to a different approach, so I thought about it on few levels
- instead of using the unity's MoveComponentUp / Down, I looked at possible alternative. 
My idea was to duplicate virta components (in memory or to temp object), sort them then copy over back to original obeject in new order. 
But quickly learned that this might fail on many levels (possibly loosing references, not copying all properties, etc) I abandoned this idea.
- different approach might be to write a EditorWindow that would handle offer the same functionality, but from a seperate window
