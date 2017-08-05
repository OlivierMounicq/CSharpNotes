## Static vs Instance 

Why you should not choose ```static``` 

The reason to not choose static are:
- the static violate the POO 
  - a static class __cannot inherite__
  - a static class __cannot implement interface__
- The static class/method violates the Open/Close principle (SOLID):
  - Open/Close : a class is open for a bug fix and close for feature enhancement. It means that you have to use the inheritance to
  add a new behavior.
  - So if you want to add a feature or update a behavior: either you modifiy the former method or you create a new method 
- a static class will always stuck in memory during the application lifetime : the static is reference root for the GC
- you cannot test a static method : forget the IoC to test your code.


#### Links

[Are helper class evil ?](https://blogs.msdn.microsoft.com/nickmalik/2005/09/06/are-helper-classes-evil/)  
[Static methods bad practices](https://r.je/static-methods-bad-practice.html)  
[static methods are death to testability](http://misko.hevery.com/2008/12/15/static-methods-are-death-to-testability/)  
[When to use static classes in C&#35;](http://stackoverflow.com/questions/241339/when-to-use-static-classes-in-c-sharp)  
[Class with single method best approach](http://stackoverflow.com/questions/205689/class-with-single-method-best-approach#206481)  

