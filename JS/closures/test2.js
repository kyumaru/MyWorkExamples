/*
	JavaScript can "display" data in different ways:

Writing into an alert box, using window.alert().
Writing into the HTML output using document.write().
Writing into an HTML element, using innerHTML.
Writing into the browser console, using console.log().

*/

/*
	CLOSURE EXAMPLE	
*/

/*
	can be used to store private values like counters,
	js is obect oriented, but, the unit of work is the function+ its scope
	below is a self invokation function, adder allocates a reference to the
	internal function returned, so the code can be reached through it
	since it can be reached, the function scope wont be disposed upon garbage
	collecting, the scope of a function object is everything it can see(has access to).
	in js, the scope of a function is, its own objects, and its parent´s objects
	Notice how the internal annonymous function declaration is returned but
	this does not mean it is executed, so counter is kept =0 after parent´s
	self invokation.
	adder then is a reference to the internal function, wich has access to parent´s counter
	In js, a function can be called through its reference
*/

var adder=(function () {
    var counter = 0;
    return function () {return counter += 1;}/*internal function, making a closure with adder*/
})();


var add =adder; 

window.alert(add());
window.alert(add());

console.dir(adder);
console.log(adder());