// ===================================================================
// Author: Matt Kruse <matt@mattkruse.com>
// WWW: http://www.mattkruse.com/
//
// NOTICE: You may use this code for any purpose, commercial or
// private, without any further permission from the author. You may
// remove this notice from your final code if you wish, however it is
// appreciated by the author if at least my web site address is kept.
//
// You may *NOT* re-distribute this code in any way except through its
// use. That means, you can include it in your product, or your web
// site, or any other form where the code is actually being used. You
// may not put the plain javascript up on your site for download or
// include it in your javascript libraries for download. Instead,
// please just point to my URL to ensure the most up-to-date versions
// of the files. Thanks.
// ===================================================================


/* 
AnchorPosition.js
Author: Matt Kruse
Last modified: 2/18/02

DESCRIPTION: These functions find the position of an <A> tag in a document,
so other elements can be positioned relative to it.

COMPATABILITY: Works with Netscape 4.x, 6.x, IE 5.x on Windows. Some small
positioning errors - usually with Window positioning - occur on the 
Macintosh platform.

FUNCTIONS:
getAnchorPosition(anchorname)
  Returns an Object() having .x and .y properties of the pixel coordinates
  of the upper-left corner of the anchor. Position is relative to the PAGE.

getAnchorWindowPosition(anchorname)
  Returns an Object() having .x and .y properties of the pixel coordinates
  of the upper-left corner of the anchor, relative to the WHOLE SCREEN.

NOTES:

1) For popping up separate browser windows, use getAnchorWindowPosition. 
   Otherwise, use getAnchorPosition

2) Your anchor tag MUST contain both NAME and ID attributes which are the 
   same. For example:
   <A NAME="test" ID="test"> </A>

3) There must be at least a space between <A> </A> for IE5.5 to see the 
   anchor tag correctly. Do not do <A></A> with no space.
*/ 

// AnchorPosition_resolve(anchorname)
//   Accepts an element, an id, or an anchor/element name and returns the element.
function AnchorPosition_resolve(anchorname) {
	if (!anchorname) { return null; }
	if (anchorname.nodeType==1) { return anchorname; }
	var o=document.getElementById(anchorname);
	if (o) { return o; }
	var byName=document.getElementsByName(anchorname);
	if (byName && byName.length) { return byName[0]; }
	return null;
	}

// getAnchorPosition(anchorname)
//   This function returns an object having .x and .y properties which are the coordinates
//   of the named anchor, relative to the page.
//
//   The original implementation returned offsetLeft/offsetTop, which are relative to the
//   element's offset parent rather than to the page. Inside nested/positioned containers
//   (every Bootstrap card and table layout in this application) that produced coordinates
//   that were far too small, so popups were drawn in the wrong place. Coordinates are now
//   taken from getBoundingClientRect() plus the current scroll offset, which is the real
//   page position, with the accumulated-offset walk kept as a fallback.
function getAnchorPosition(anchorname) {
	// This function will return an Object with x and y properties
	var coordinates=new Object();
	coordinates.x=0; coordinates.y=0;
	var o=AnchorPosition_resolve(anchorname);
	if (!o) { return coordinates; }
	if (o.getBoundingClientRect) {
		var rect=o.getBoundingClientRect();
		var scrollX=(window.pageXOffset!=null)?window.pageXOffset:(document.documentElement.scrollLeft||document.body.scrollLeft||0);
		var scrollY=(window.pageYOffset!=null)?window.pageYOffset:(document.documentElement.scrollTop||document.body.scrollTop||0);
		coordinates.x=rect.left+scrollX;
		coordinates.y=rect.top+scrollY;
		return coordinates;
		}
	coordinates.x=AnchorPosition_getPageOffsetLeft(o);
	coordinates.y=AnchorPosition_getPageOffsetTop(o);
	return coordinates;
	}

// getAnchorWindowPosition(anchorname)
//   This function returns an object having .x and .y properties which are the coordinates
//   of the named anchor, relative to the window
function getAnchorWindowPosition(anchorname) {
	var coordinates=getAnchorPosition(anchorname);
	var x=0;
	var y=0;
	if (document.getElementById) {
		if (isNaN(window.screenX)) {
			x=coordinates.x-document.body.scrollLeft+window.screenLeft;
			y=coordinates.y-document.body.scrollTop+window.screenTop;
			}
		else {
			x=coordinates.x+window.screenX+(window.outerWidth-window.innerWidth)-window.pageXOffset;
			y=coordinates.y+window.screenY+(window.outerHeight-24-window.innerHeight)-window.pageYOffset;
			}
		}
	else if (document.all) {
		x=coordinates.x-document.body.scrollLeft+window.screenLeft;
		y=coordinates.y-document.body.scrollTop+window.screenTop;
		}
	else if (document.layers) {
		x=coordinates.x+window.screenX+(window.outerWidth-window.innerWidth)-window.pageXOffset;
		y=coordinates.y+window.screenY+(window.outerHeight-24-window.innerHeight)-window.pageYOffset;
		}
	coordinates.x=x;
	coordinates.y=y;
	return coordinates;
	}

// Functions for IE to get position of an object
function AnchorPosition_getPageOffsetLeft (el) {
	var ol=el.offsetLeft;
	while ((el=el.offsetParent) != null) { ol += el.offsetLeft; }
	return ol;
	}
function AnchorPosition_getWindowOffsetLeft (el) {
	return AnchorPosition_getPageOffsetLeft(el)-document.body.scrollLeft;
	}	
function AnchorPosition_getPageOffsetTop (el) {
	var ot=el.offsetTop;
	while((el=el.offsetParent) != null) { ot += el.offsetTop; }
	return ot;
	}
function AnchorPosition_getWindowOffsetTop (el) {
	return AnchorPosition_getPageOffsetTop(el)-document.body.scrollTop;
	}
