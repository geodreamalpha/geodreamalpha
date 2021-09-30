Alright, so I have already given an example of how this will work with my components.
We need to implement a method in each of our component classes called "Hello()" which prints "Hello from" + <component name> + "Component".
Again, look at my example for a reference.  I have already set up your components.  If you look at the Hierarchy window in the Unity editor
(the one that is likely on the left side of your screen), you will see a gameobject named after your component with a your component script
attached to it.  These objects have been dropped into their appropriate slots on the HelloPrinter gameobject.  This object has a script
attached to it which imports your namespaces and accesses your classes so that I can call your Hello() methods.  We will likely need to talk
about how this works in a meeting so there is no confusion.  For now, don't worry about it.

DIRECTIONS:
What I need you to do is implement a Hello() method that returns a string that introduces your component: 
like this example: "Hello from Terrain Generator Component".
When everybody is done, I will access your Hello() methods.  You don't need to worry about that.  Just make sure you have them.

Push play to see an example of what these prints statements will do.