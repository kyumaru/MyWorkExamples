/*
	JavaScript can "display" data in different ways:

Writing into an alert box, using window.alert().
Writing into the HTML output using document.write().
Writing into an HTML element, using innerHTML.
Writing into the browser console, using console.log().

*/
function makeClickable(){
	
	var computerParts= document.querySelectorAll('.menuitem');
	var l=computerParts.length;
	var offset=0;
	var myDiv= document.getElementById('#menu');
	
	function generateClickHandler(menuitem){
			return function(){
				window.alert(menuitem);					
			} ;
	}
	
	function generateHoverHandler(i){
			return function(){
				computerParts[i].style.textDecoration="underline";	
			} ;
	}
	
	while(l--){
		//window.alert(l);	
		//document.write(computerParts[offset].innerHTML + '</br>');
		
		/*the calling or parent function to generateClickHandler is addEventListener,
		closure only sees the variables kept in direct parent, which is not this, so
		offset must be given to instantiate the function objects accordinly
		*/
		computerParts[offset].addEventListener('click',generateClickHandler(offset));
		computerParts[offset].addEventListener('mouseenter',generateHoverHandler(offset));
		offset++;		
	}
		
	
}
//window.alert(5 + 6);
makeClickable();